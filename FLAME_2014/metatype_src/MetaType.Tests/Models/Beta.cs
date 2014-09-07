namespace MetaType.Tests.Models
{
    public class Beta
    {
        public Beta(int foo)
        {
            Foo = foo;
        }

        public int _foo;
        public virtual int Foo
        {
            get { return _foo; }
            set { _foo = value; }
        }
    }
}