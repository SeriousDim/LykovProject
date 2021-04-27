using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykovProject.Model.Data;

namespace LykovProject.Data.Interfaces
{
    public interface IProducing
    {
        Material Produce(Material from);
        void Send(Conveyor to, Material mat);
    }
}
