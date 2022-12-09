using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class Definition
    {
        [Key]
        public int Id { get; set; }
        public int ProcessId { get; set; }
        public string Name { get; set; }
    }
}
