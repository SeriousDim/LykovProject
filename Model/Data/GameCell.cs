using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykovProject.Model.Data
{
    public class GameCell
    {
        public int x;
        public int y;
        public AbstractInfrastucture infra;
        public bool land;

        public GameCell(int x, int y, AbstractInfrastucture infra, bool land)
        {
            this.x = x;
            this.y = y;
            this.infra = infra;
            this.land = land;
        }
    }
}
