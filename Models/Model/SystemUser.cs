using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class SystemUser
    {
        [Key]
        public int Id { get; set; }
        public int SystemsId { get; set; }
        public int UserId { get; set; }

        public virtual Systems Systems { get; set; }
        public virtual Users User { get; set; }
    }
}
