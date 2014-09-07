namespace MetaType.Tests.Models
{
    public class Delta
    {
        public virtual string Hello()
        {
            return "World";
        }

        public virtual string HelloPlus(int x, string y)
        {
            return x + " " + y;
        }

        public virtual string Extra { get; set; }
    }
}
