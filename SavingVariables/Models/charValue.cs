using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SavingVariables.Models
{
    public class charValue
    {
        [Key]
        public int charId { get; set; }
        
        [Required]
        public string charName { get; set; }
        
        [Required]
        public int value { get; set; }
    }
}
