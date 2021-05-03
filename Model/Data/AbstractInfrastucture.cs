using LykovProject.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykovProject.Model.Data
{
    abstract public class AbstractInfrastucture : IChangable
    {
        public List<Conveyor> conveyors;
        public List<Material> rawMaterials;
        public string name;
        public InfraType type;
        public GameCell bottomLeftPosition;
        public List<GameCell> gameCells; // все клетки, которые занимает здание
        public string owner;
        public Sprite sprite;
        public int level;

        protected AbstractInfrastucture(string name, InfraType type, Sprite sprite)
        {
            this.name = name;
            this.type = type;
            this.sprite = sprite;

            this.conveyors = new List<Conveyor>();
            this.rawMaterials = new List<Material>();
            this.gameCells = new List<GameCell>();
            this.bottomLeftPosition = null;
            this.owner = null;
            this.level = 1;
        }

        public abstract void OnTick();
    }
}
