using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LykovProject.Data.Interfaces;

namespace LykovProject.Model.Data
{
    public class Conveyor
    {
        public IProducing toWhich;
        public InfraType type;
        public string name;

        public Conveyor(IProducing toWhich, InfraType type, string name)
        {
            this.toWhich = toWhich;
            this.type = type;
            this.name = name;
        }
    }
}
