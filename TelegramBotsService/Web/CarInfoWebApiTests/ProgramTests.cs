using CarInfoWebApi;
using Xunit;

namespace CarInfoWebApiTests
{
    public class ProgramTests
    {
        [Fact]
        public void Test()
        {
            Program.CreateHostBuilder(new[] {""});
        }
    }
}