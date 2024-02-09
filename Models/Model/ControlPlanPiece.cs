using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class ControlPlanPiece
    { 
        [Key]
        public int Id { get; set; }
        public int ControlPlanId { get; set; }
        public int PieceId { get; set; } 

        public virtual ControlPlan ControlPlan { get; set; }
        public virtual Piece Piece { get; set; }

    }
}
