using Test.NoAuth.EntityFrameworkCore;

namespace Test.NoAuth.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly NoAuthDbContext _context;

        public TestDataBuilder(NoAuthDbContext context)
        {
            _context = context;
        }

        public void Build()
        {
            //create test data here...
        }
    }
}