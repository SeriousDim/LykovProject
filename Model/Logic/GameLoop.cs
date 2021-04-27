using LykovProject.Model.Data;
using LykovProject.Model.Data.Interfaces;
using System.Threading;

namespace LykovProject.Model.Logic
{
    public class GameLoop : IStepable
    {
        public GameWorld gameWorld;
        public Thread gameThread;

        public GameLoop(GameWorld gameWorld)
        {
            this.gameWorld = gameWorld;
        }

        public void OnStart()
        {
            gameThread = new Thread(() => OnStep());
            gameThread.Start();
        }

        public void OnStep()
        {
            // здесь обрабатывается каждый кадр
            // вызываются OnTick() всех объектов в GameWorld
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