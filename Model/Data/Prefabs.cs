using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using LykovProject.Model.Data.InfraClasses;
using LykovProject.Model.Data.Conveyors;

namespace LykovProject.Model.Data
{
    public class Prefabs
    {
        public static BitmapContainer btms = null;

        public static Material RawIron => new Material(3, "Железо", "", new Sprite(Color.Red, 5), MatType.RAW);
        public static Material RawCoal => new Material(5, "Уголь", "", new Sprite(Color.Black, 5), MatType.RAW);

        public static Ore IronOre => new Ore("Железная руда", btms["iron_ore"], RawIron);
        public static Ore CoalOre => new Ore("Угольная руда", btms["coal_ore"], RawCoal);

        public static ArticulatedConveyor ArticulatedConveyorLeft => new ArticulatedConveyor("Шарнирный конвеер", btms["conv"], 0.024f, Conveyor.LEFT);
        public static ArticulatedConveyor ArticulatedConveyorRight => new ArticulatedConveyor("Шарнирный конвеер", btms["conv_right"], 0.024f, Conveyor.RIGHT);
        public static ArticulatedConveyor ArticulatedConveyorUp => new ArticulatedConveyor("Шарнирный конвеер", btms["conv_up"], 0.024f, Conveyor.UP);
        public static ArticulatedConveyor ArticulatedConveyorDown => new ArticulatedConveyor("Шарнирный конвеер", btms["conv_down"], 0.024f, Conveyor.DOWN);

        public static StorageInfra StorageInfra => new StorageInfra("Хранилище", btms["storage"]);
        public static FurnaceInfra FurnaceInfra => new FurnaceInfra("Печь", btms["furnace"]);
        public static DrillInfra DrillInfra => new DrillInfra("Бур", btms["drill"], 2000);
    }
}
