using System.ComponentModel.DataAnnotations;

namespace Models.Model
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        public string? TestName { get; set; }
        public int TestConditionId { get; set; }
        public int TestImportanceId { get; set; }
        public int LabratoaryToolId { get; set; }
        public long Amount { get; set; }
        public decimal Minimum { get; set; }
        public decimal Maximum { get; set; }
        public int StandardId { get; set; }
        public int MeasureId { get; set; } 
        public int? TestDescriptionId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime EndDate { get; set; }


        public virtual TestCondition TestCondition { get; set; }
        public virtual TestImportance TestImportance { get; set; }
        public virtual LabratoaryTool LabratoaryTool { get; set; }
        public Standard Standard { get; set; }
        public Measure Measure { get; set; } 
        public TestDescription TestDescription { get; set; }
    }
}
