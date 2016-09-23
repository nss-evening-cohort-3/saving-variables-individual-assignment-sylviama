using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavingVariables;
using SavingVariables.DAL;

namespace SavingVariables.Models
{
    public class ShowAll
    {
        public IQueryable<charValue> showItAll()
        {
            try
            {
                var ctx = new SavingVariablesDatabase();
                var selectedCtx = from d in ctx.charValueDb
                                  select d;
                return selectedCtx;
            }
            catch(InvalidOperationException)
            {
                return null;
            }
        }
    }
}
