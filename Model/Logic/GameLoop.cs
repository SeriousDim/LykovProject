using LykovProject.Controller;
using LykovProject.Model.Data;
using LykovProject.Model.Data.Interfaces;
using System;
using System.Threading;

namespace LykovProject.Model.Logic
{
    public class GameLoop : IStepable
    {
        public GameWorld gameWorld;
        public GameState gameState;
        public AbstractInfrastucture infraToBuild;

        public Thread gameThread;
        public Form1 form;
        public InputController<Form1> cont;

        private object locker;

        public GameLoop(GameWorld gameWorld, Form1 form)
        {
            this.gameWorld = gameWorld;
            this.form = form;
            cont = new InputController<Form1>(form);
            locker = new object();
            gameState = GameState.PLAYING;
        }

        public void ProcessEsc()
        {
            if (gameState != GameState.PLAYING)
            {
                SetPlayingState();
            }
            else
                gameState = GameState.PAUSED;
        }

        public void SetDebuildingState()
        {
            gameState = GameState.DEBUILDING;
        }

        public void SetBuidlingState(AbstractInfrastucture infra)
        {
            gameState = GameState.BUILDING;
            this.infraToBuild = infra;
        }

        public void SetPlayingState()
        {
            gameState = GameState.PLAYING;
            infraToBuild = null;
        }

        public void ProcessKeyInput()
        {
            lock (cont.locker)
            {
                cont.ProccessKey();
            }
        }

        public void ProcessUniversalInput()
        {
            lock (cont.locker)
            {
                cont.ProcessAction();
            }
        }

        // вызывает onTick у всех объектов классов-наследников AbstractInfra
        public void ProccessTicks()
        {
            lock (cont.locker)
            {
                foreach (var data in gameWorld.playerData)
                {
                    foreach (var infra in data.Value.infraList)
                    {
                        infra.OnTick();
                    }
                }
            }
        }

        public void InvalidateForm()
        {
            form.OnLoopInvalidation();
        }

        public void OnStart()
        {
            gameThread = new Thread(() => OnStep());
            gameThread.Start();
        }

        public void OnStep()
        {
            while (true)
            {
                ProcessKeyInput();
                ProcessUniversalInput();
                ProccessTicks();

                // обработать что-то еще здесь

                InvalidateForm();
            }
        }

        public void OnFinish()
        {
            gameThread.Abort();
            
        }

        public void Start()
        {
            OnStart();
        }

        public void Finish()
        {
            OnFinish();
        }
    }
}