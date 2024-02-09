using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class TestLabratoaryTool
    {
        [Key]
        public int Id { get; set; }
        public int TestId { get; set; }
        public int LabratoaryToolId { get; set; }

        public Test Test { get; set; }
        public LabratoaryTool LabratoaryTool { get; set; }
    }
}
