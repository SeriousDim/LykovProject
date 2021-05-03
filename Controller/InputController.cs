using LykovProject.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LykovProject.Controller
{
    public class InputController<F> where F : Form
    {
        private F form;
        private Dictionary<Keys, Action> actions;
        private Keys action;

        public object locker;

        public InputController(F form)
        {
            this.form = form;
            this.actions = new Dictionary<Keys, Action>();
            locker = new object();
        }

        public void EnqueueKey(Keys k)
        {
            lock (locker)
                action = k;
        }

        public void ProccessQueue()
        {
            lock (locker)
            {
                if (action != Keys.None)
                {
                    ProcessKey(action);
                    action = Keys.None;
                }
            }
        }

        public void AddAction(Keys k, Action act)
        {
            actions.Add(k, act);
        }

        public void RemoveAction(Keys key)
        {
            if (actions.ContainsKey(key))
                actions.Remove(key);
        }

        public void ProcessKey(Keys key)
        {
            actions[key].Invoke();
        }
    }
}
