using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykovProject.Model.Data
{
    public class GameWorld
    {
        public PlayerData playerData;
        public GameCell[,] gameMap;

        public int Width => gameMap.GetLength(0);
        public int Height => gameMap.GetLength(1);

        public GameWorld(string companyName, int money, int width, int height)
        {
            this.playerData = new PlayerData(companyName, money);
            this.gameMap = new GameCell[height, width];
        }

        public void Build(AbstractInfrastucture infra)
        {

        }

        public void Debuild(AbstractInfrastucture infra)
        {

        }
    }
}
