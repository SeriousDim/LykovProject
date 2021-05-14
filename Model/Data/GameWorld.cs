using System;
using System.Collections.Generic;
using System.Drawing;
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

        public int Width => gameMap.GetLength(1);
        public int Height => gameMap.GetLength(0);

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
                    gameMap[i, j] = new GameCell(i, i, null, true);

            for (var i = 300; i < 310; i++)
                for (var j = groundLevel - 3; j < groundLevel; j++)
                    gameMap[j, i].infra = Prefabs.StorageInfra;
        }

        public void AddPlayer(string companyName, int money)
        {
            playerData.Add(companyName, new PlayerData(companyName, money));
        }

        public bool IsInBounds(PointF p)
        {
            var y = (int)p.Y;
            var x = (int)p.X;
            return y >= 0 && y < Height && x >= 0 && x < Width;
        }

        public void Build(int i, int j, AbstractInfrastucture infra)
        {
            if (gameMap[i, j].infra == null)
                gameMap[i, j].infra = infra;
        }

        public void Build(PointF p, AbstractInfrastucture infra)
        {
            if (IsInBounds(p))
                Build((int)p.Y, (int)p.X, infra);
        }

        public void Debuild(int i, int j)
        {
            gameMap[i, j].infra = null;
        }

        public void Debuild(PointF p)
        {
            if (IsInBounds(p))
                Debuild((int)p.Y, (int)p.X);
        }
    }
}
