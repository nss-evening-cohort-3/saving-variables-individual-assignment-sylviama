using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Web;
using SavingVariables.Models;


namespace SavingVariables.DAL
{
    public class SavingVariablesDatabase: DbContext
    {
        public virtual DbSet<charValue> charValueDb { get; set; }

    }
    
}
