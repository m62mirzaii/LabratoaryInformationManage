using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class SystemMethod
    {
        [Key]
        public int Id { get; set; }
        public int SystemsId { get; set; }
        public string? MethodName { get; set; } 
        public virtual Systems System { get; set; }
    }
}
