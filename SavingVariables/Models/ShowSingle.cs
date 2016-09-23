using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavingVariables;
using SavingVariables.DAL;

namespace SavingVariables.Models
{
    public class ShowSingle
    {
        public charValue showSingleRecord(string input)
        {
            //need error proofing, if x doesn't exist
            try
            {
                var ctx = new SavingVariablesDatabase();
                var selectedCtx = (from d in ctx.charValueDb
                                   where d.charName == input
                                   select d).FirstOrDefault();
                
                return selectedCtx;
            }
            catch(InvalidOperationException)
            {
                return null;
            }
        }
    }
}
