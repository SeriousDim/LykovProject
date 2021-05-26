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
        public Dictionary<GameCell, AbstractInfrastucture> neighbors;
        public List<Material> rawMaterials;
        public int amount;

        public string name;
        public InfraType type;
        public GameCell bottomLeftPosition;
        public List<GameCell> gameCells; // все клетки, которые занимает здание
        public PlayerData owner;
        public Sprite sprite;
        public int level;
        public string buildCommand;

        protected AbstractInfrastucture(string name, InfraType type, Sprite sprite, int amount, string buildCommand)
        {
            this.name = name;
            this.type = type;
            this.sprite = sprite;
            this.buildCommand = buildCommand;
            this.amount = amount;

            this.neighbors = new Dictionary<GameCell, AbstractInfrastucture>();
            this.rawMaterials = new List<Material>();
            this.gameCells = new List<GameCell>();
            this.bottomLeftPosition = null;
            this.owner = null;
            this.level = 1;
        }

        protected AbstractInfrastucture(string name, InfraType type, Sprite sprite, string buildCommand)
        {
            this.name = name;
            this.type = type;
            this.sprite = sprite;
            this.buildCommand = buildCommand;
            this.amount = 100;

            this.neighbors = new Dictionary<GameCell, AbstractInfrastucture>();
            this.rawMaterials = new List<Material>();
            this.gameCells = new List<GameCell>();
            this.bottomLeftPosition = null;
            this.owner = null;
            this.level = 1;
        }

        public void OnBuild(GameCell pos, PlayerData owner)
        {
            this.bottomLeftPosition = pos;
            this.owner = owner;
        }

        public void SetNeighbor(GameCell c, AbstractInfrastucture infra)
        {
            neighbors[c] = infra;
        }

        // любой IProducing.Send должен вызывать этот метод
        // определяет, как добавлять материал в эту инфраструктуру
        public virtual void AddRawMaterial(Material mat)
        {
            if (mat != null)
            {
                if (rawMaterials.Count < amount)
                {
                    rawMaterials.Add(mat);
                }
            }
        }

        public void RemoveRawMaterial(int index)
        {
            if (index >= 0 && index < rawMaterials.Count)
                rawMaterials.RemoveAt(index);
        }

        public string RawMatToString()
        {
            var result = String.Format("{0}\n\nИнвентарь({1}/{2}):\n", name.ToUpper(), rawMaterials.Count, amount);

            foreach (var e in rawMaterials)
                result += String.Format("- {0}, x1\n", e.name);

            return result;
        }

        public abstract void OnTick();
    }
}
