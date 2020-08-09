using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LargeTextEditor
{
    /// <summary>
    /// <para>Ref&lt;T&gt; should be used for replacing a ref or out parameter of async method.</para>
    /// <para>For example: public async Task&lt;bool&gt; SomeAction(int param1, Ref&lt;int&gt; param2){}</para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Ref<T>
    {
        public T Value;

        public Func<T> Getter;

        public Action<T> Setter;

        public Ref()
        {
        }

        public Ref(T Value)
        {
            this.Value = Value;
        }

        public Ref(Func<T> Getter)
        {
            this.Getter = Getter;
        }

        public Ref(Action<T> Setter)
        {
            this.Setter = Setter;
        }

        public Ref(Func<T> Getter, Action<T> Setter)
        {
            this.Getter = Getter;
            this.Setter = Setter;
        }

        public T Get()
        {
            if (Getter != null)
            {
                return Getter();
            }
            else
            {
                return Value;
            }

        }

        public void Set(T Value)
        {
            if (Setter != null)
            {
                Setter(Value);
            }
            else
            {
                this.Value = Value;
            }

        }

        public override string ToString()
        {
            return Value.ToString();
        }

    }

}
