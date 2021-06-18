using LykovProject.Controller;
using LykovProject.Model.Data;
using LykovProject.Model.Data.Interfaces;
using LykovProject.View;
using System;
using System.Threading;

namespace LykovProject.Model.Logic
{
    public class GameLoop : IStepable
    {
        private readonly int moneyGoal = 6000;

        public GameWorld gameWorld;
        public GameState gameState;

        public Func<AbstractInfrastucture> infraBuilder;
        //public AbstractInfrastucture infraToBuild;

        public Thread gameThread;
        public Form1 form;
        public InputController<Form1> cont;

        private object locker;

        public GameLoop(GameWorld gameWorld, Form1 form, long timeInMs)
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
                form.UpdateNotifier("");
            }
            /*else
            {
                gameState = GameState.PAUSED;
                form.UpdateNotifier("Пауза");
            }*/
        }

        public void SetDebuildingState()
        {
            gameState = GameState.DEBUILDING;
        }

        public void SetBuidlingState(Func<AbstractInfrastucture> infra)
        {
            lock (cont.locker)
            {
                gameState = GameState.BUILDING;
                this.infraBuilder = infra;
            }
        }

        public void SetPlayingState()
        {
            gameState = GameState.PLAYING;
            //infraBuilder = null;
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
            while (gameState != GameState.FINISHED)
            {
                if (gameState != GameState.PAUSED)
                {
                    if (gameWorld.playerData[gameWorld.playerCompanyName].money >= moneyGoal)
                        gameState = GameState.FINISHED;

                    ProcessKeyInput();
                    ProcessUniversalInput();
                    ProccessTicks();

                    // обработать что-то еще здесь

                    InvalidateForm();
                }
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