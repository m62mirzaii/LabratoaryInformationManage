using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class SystemMethodUser
    {
        [Key]
        public int Id { get; set; }
        public int SystemMethodId { get; set; }
        public int UserId { get; set; }
        public virtual SystemMethod SystemMethod { get; set; }
    }
}
