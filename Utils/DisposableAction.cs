using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LargeTextEditor
{
    class DisposableAction : IDisposable
    {

        private Action action = null;

        public DisposableAction(Action action)
        {
            this.action = action;
        }

        public void Dispose()
        {
            action?.Invoke();
        }
    }
}
