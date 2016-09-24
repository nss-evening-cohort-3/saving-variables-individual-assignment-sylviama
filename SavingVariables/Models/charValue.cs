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
        public virtual int charId { get; set; }
        
        [Required]
        public virtual string charName { get; set; }
        
        [Required]
        public virtual int value { get; set; }
    }
}
