using NUnit.Framework;

namespace HtmlAgilityPack.Tests
{
    [TestFixture]
    public class HtmlEntityFacts
    {
        [Test]
        public void DeEntitizeDoesNotThrowExceptionIfEntityNameNotFound()
        {
            string deEntitize = null;
            Assert.DoesNotThrow(() =>
                                    {
                                        deEntitize = HtmlEntity.DeEntitize("&nbsp1;");
                                    });
            Assert.AreEqual("&nbsp1;", deEntitize);
        }
    }
}