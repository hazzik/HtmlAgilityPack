namespace HtmlAgilityPack.Tests
{
    using Xunit;

    public class HtmlEntityFacts
    {
        [Fact]
        public void DeEntitizeDoesNotThrowExceptionIfEntityNameNotFound()
        {
            string deEntitize = null;
            Assert.DoesNotThrow(() =>
                                    {
                                        deEntitize = HtmlEntity.DeEntitize("&nbsp1;");
                                    });
            Assert.Equal("&nbsp1;", deEntitize);
        }
    }
}