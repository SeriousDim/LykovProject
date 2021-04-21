using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LykovProject.Model.Data;

namespace LykovProject.Data.Interfaces
{
    interface IProducing
    {
        Material Produce(Material from);
    }
}
