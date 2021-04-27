using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LykovProject.Model.Data.Interfaces
{
    public interface IStepable
    {
        void OnStart();
        void OnStep();
        void OnFinish();
    }
}
