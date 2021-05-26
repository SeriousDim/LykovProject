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
        Material Produce(Material[] from);

        // всегда должен вызывать AbstractInfrastucture.AddRawMaterial
        void Send(AbstractInfrastucture to, Material mat);

        // Всегда должен вызывать AbstractInfrastucture.AddRawMaterial.
        // Передает материал случайному соседу
        void Send(Material mat);
    }
}
