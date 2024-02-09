using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class TestCondition
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
    }
}
