using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavingVariables;
using SavingVariables.DAL;

namespace SavingVariables.Models
{
    public class ClearAll
    {
        public string ClearAllFunction()
        {
            var ctx = new SavingVariablesDatabase();
            while(ctx.charValueDb.Count()!=0)
            {
                ctx.charValueDb.Remove(ctx.charValueDb.FirstOrDefault());
                ctx.SaveChanges();
            }

            return "Cleared all the records.";
        }
    }
}
