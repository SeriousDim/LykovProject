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
        private Dictionary<Keys, Action> keyActions;
        private Dictionary<string, Action> universalActions; // в частности, для действий, совершаемых мышкой

        private Keys curKey;
        private string curActionKey;

        public object locker;

        public InputController(F form)
        {
            this.form = form;
            this.keyActions = new Dictionary<Keys, Action>();
            this.universalActions = new Dictionary<string, Action>();
            locker = new object();
        }

        public void EnqueueKey(Keys k)
        {
            lock (locker)
                curKey = k;
        }

        public void EnqueueAction(string key)
        {
            lock (locker)
                curActionKey = key;
        }

        public void ProccessKey()
        {
            lock (locker)
            {
                if (curKey != Keys.None)
                {
                    ProcessKeyAction(curKey);
                    curKey = Keys.None;
                }
            }
        }

        public void ProcessAction()
        {
            lock (locker)
            {
                if (curActionKey != null)
                {
                    ProcessAction(curActionKey);
                    curActionKey = null;
                }
            }
        }

        public void AddAction(Keys k, Action act)
        {
            keyActions.Add(k, act);
        }

        public void AddAction(string s, Action act)
        {
            universalActions.Add(s, act);
        }

        public void RemoveAction(Keys key)
        {
            if (keyActions.ContainsKey(key))
                keyActions.Remove(key);
        }

        public void RemoveAction(string key)
        {
            if (universalActions.ContainsKey(key))
                universalActions.Remove(key);
        }

        public void ProcessKeyAction(Keys key)
        {
            if (keyActions.ContainsKey(key))
                keyActions[key].Invoke();
        }

        public void ProcessAction(string key)
        {
            if (universalActions.ContainsKey(key))
                universalActions[key].Invoke();
        }
    }
}
