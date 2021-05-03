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
        }

        public void ProcessInput()
        {
            lock (cont.locker)
            {
                cont.ProccessQueue();
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
                ProcessInput();
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