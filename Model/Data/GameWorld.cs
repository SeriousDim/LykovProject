using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykovProject.Model.Data
{
    public class GameWorld
    {
        public Dictionary<string, PlayerData> playerData;
        public GameCell[,] gameMap;
        public int groundLevel;

        public int Width => gameMap.GetLength(0);
        public int Height => gameMap.GetLength(1);

        public GameWorld(string playerCompanyName, int playerMoney, int groundLevel, int width, int height)
        {
            this.playerData = new Dictionary<string, PlayerData>();
            this.gameMap = new GameCell[height, width];
            this.groundLevel = groundLevel;

            AddPlayer(playerCompanyName, playerMoney);
        }

        public void GenMap()
        {
            for (var i = 0; i < groundLevel; i++)
                for (var j = 0; j < Width; j++)
                    gameMap[i, j] = new GameCell(i, j, null, false);
            
            for (var i = groundLevel; i < Height; i++)
                for (var j = 0; j < Width; j++)
                    gameMap[i, j] = new GameCell(i, j, null, true);
        }

        public void AddPlayer(string companyName, int money)
        {
            playerData.Add(companyName, new PlayerData(companyName, money));
        }

        public void Build(AbstractInfrastucture infra)
        {

        }

        public void Debuild(AbstractInfrastucture infra)
        {

        }
    }
}
