using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LargeTextEditor
{
    class DisposableFlag : IDisposable
    {

        public bool Value { get; set; }

        public DisposableFlag(bool value = false)
        {
            this.Value = value;
        }

        public DisposableFlag Set(bool value)
        {
            this.Value = value;

            return this;
        }

        public void Dispose()
        {
            Value = false;
        }
    }
}
