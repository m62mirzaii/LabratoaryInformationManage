using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class TestImportance
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }

    }
}
