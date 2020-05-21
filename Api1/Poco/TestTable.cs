using Dapper.Contrib.Extensions;

namespace Api1.Poco
{
    [Table("TestTable")]
    public class TestTable
    {
        public int Id { get; set; }
        public string A { get; set; }
        public string B { get; set; }
    }
}
