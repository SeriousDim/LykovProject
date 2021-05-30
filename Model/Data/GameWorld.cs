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
        public string playerCompanyName;

        public GameCell[,] gameMap;
        public int groundLevel;

        public int Width => gameMap.GetLength(1);
        public int Height => gameMap.GetLength(0);

        public GameWorld(string playerCompanyName, int playerMoney, int groundLevel, int width, int height)
        {
            this.playerData = new Dictionary<string, PlayerData>();
            this.gameMap = new GameCell[height, width];
            this.groundLevel = groundLevel;

            this.playerCompanyName = playerCompanyName;
            AddPlayer(playerCompanyName, playerMoney);
        }

        public void GenMap()
        {
            for (var i = 0; i < groundLevel; i++)
                for (var j = 0; j < Width; j++)
                    gameMap[i, j] = new GameCell(j, i, null, false);
            
            for (var i = groundLevel; i < Height; i++)
                for (var j = 0; j < Width; j++)
                    gameMap[i, j] = new GameCell(j, i, null, true);

            for (var i = 300; i < 310; i++)
                for (var j = groundLevel - 3; j < groundLevel; j++)
                    Build(playerCompanyName, new PointF(i, j), Prefabs.StorageInfra);

            for (var i = 280; i < 290; i++)
                for (var j = groundLevel + 10; j < groundLevel + 13; j++)
                    gameMap[j, i].ore = Prefabs.IronOre;

            for (var i = 315; i < 319; i++)
                for (var j = groundLevel + 5; j < groundLevel + 7; j++)
                    gameMap[j, i].ore = Prefabs.CoalOre;
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
            {
                gameMap[i, j].infra = infra;
                playerData[playerCompanyName].infraList.Add(infra);
            }
        }

        // рекомендуется этот метод
        public void Build(string company, PointF p, AbstractInfrastucture infra)
        {
            if (IsInBounds(p))
            {
                var c = gameMap[(int)p.Y, (int)p.X];
                Build((int)p.Y, (int)p.X, infra);
                infra.OnBuild(c, playerData[company]);

                foreach (var np in GetNeighborPoints(p))
                {
                    var nCell = gameMap[(int)np.Y, (int)np.X];
                    infra.SetNeighbor(nCell, nCell.infra);

                    if (gameMap[(int)np.Y, (int)np.X].infra != null)
                        gameMap[(int)np.Y, (int)np.X].infra.SetNeighbor(infra.bottomLeftPosition, infra);
                }
            }
        }

        public IEnumerable<PointF> GetNeighborPoints(PointF p)
        {
            for (var dx = -1; dx <= 1; dx++)
            {
                for (var dy = -1; dy <= 1; dy++)
                {
                    var np = new PointF() { X = p.X + dx, Y = p.Y + dy };
                    if (IsInBounds(np) && Math.Abs(dx) != Math.Abs(dy))
                        yield return np;
                }
            }
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
