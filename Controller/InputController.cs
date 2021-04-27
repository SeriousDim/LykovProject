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

        public InputController(F form)
        {
            this.form = form;
            this.actions = new Dictionary<Keys, Action>();
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
