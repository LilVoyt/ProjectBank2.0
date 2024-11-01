using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBank.BusinessLogic.ChainOfResponsibility
{
    public class ActionQueue
    {
        private readonly List<Func<Task>> _actions = new List<Func<Task>>();

        public void AddAction(Func<Task> action)
        {
            _actions.Add(action);
        }

        public async Task ExecuteAll()
        {
            foreach (var action in _actions)
            {
                await action();
            }
            _actions.Clear();
        }
    }

}
