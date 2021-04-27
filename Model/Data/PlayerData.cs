using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykovProject.Model.Data
{
    public class PlayerData
    {
        public List<AbstractInfrastucture> infraList;
        public string companyName;
        public int money;
        public List<Material> goods;

        public PlayerData(string companyName, int money)
        {
            this.companyName = companyName;

            this.infraList = new List<AbstractInfrastucture>();
            this.goods = new List<Material>();
            this.money = money;
        }
    }
}
