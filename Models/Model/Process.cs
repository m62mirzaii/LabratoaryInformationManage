using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    public class Process
    {
        [Key]
        public int Id { get; set; }
        public string? ProcessName { get; set; }
        public int DefinitionId { get; set; }
        public bool IsActive { get; set; }

    }
}
