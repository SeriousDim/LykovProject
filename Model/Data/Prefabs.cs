using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using LykovProject.Model.Data.InfraClasses;

namespace LykovProject.Model.Data
{
    public class Prefabs
    {
        public StorageInfra GenStorageInfra()
        {
            return new StorageInfra("Хранилище", new Sprite(Color.Green, 48));
        }
    }
}
