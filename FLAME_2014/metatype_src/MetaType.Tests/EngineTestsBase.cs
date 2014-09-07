using NUnit.Framework;

namespace MetaType.Tests
{
    public abstract class EngineTestsBase {
        protected IMetaTypeEngine Engine;

        [SetUp]
        public void Init()
        {
            Engine = new MetaTypeEngine();
        }
    }
}