using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using SavingVariables.Models;
using System.Data.Entity;

namespace SavingVariables.DAL
{
    public class SavingVariablesRepository
    {
        public SavingVariablesDbContext Context { get; set; }

        //set the value
        public SavingVariablesRepository()
        {
            Context = new SavingVariablesDbContext();
        }

        //dependancy Injection: repository depenses on context
        public SavingVariablesRepository(SavingVariablesDbContext _context)
        {
            Context = _context;
        }
          
        
        //save a value
        public void SavingAValueToDB(string consLetter, int consNumber)
        {
            charValue Record = new charValue() { charName = consLetter, value = consNumber };
            Context.charValueDb.Add(Record);
            Context.SaveChanges();

        }

        //clear all
        public void ClearAllFunction()
        {
            while (Context.charValueDb.Count() != 0)
            {
                Context.charValueDb.Remove(Context.charValueDb.FirstOrDefault());
                Context.SaveChanges();
            }
            
        }

        //clear single
        public string ClearSingleRecord(string inputString)
        {
            try
            {
                //DbSet<T> inherence from IQueryable<T> and IEnumberable<T>
                charValue selectedRecord = (from d in Context.charValueDb
                                            where d.charName == inputString
                                            select d).SingleOrDefault();
                if (selectedRecord != null)
                {
                    Context.charValueDb.Remove(selectedRecord);
                    Context.SaveChanges();
                    return "='" + inputString + "' is now free!";
                }else
                {
                    return null;
                }

                
            }
            catch (InvalidOperationException)
            {
                return "Ah oh, seems like we have more than one " + inputString + " record...";
            }
        }

        //show all
        public List<charValue> showItAll()
        {
            IQueryable<charValue> selectedCtx = from d in Context.charValueDb
                                                select d;
            return selectedCtx.ToList(); 
        }

        //same as show all, getVariable, this is the simplied way, is not being used
        public List<charValue> GetVariable()
        {
            return Context.charValueDb.ToList();
        }

        //show single
        public charValue showSingleRecord(string input)
        {
            
            try
            {
                charValue selectedCtx = (from d in Context.charValueDb
                                   where d.charName == input
                                   select d).FirstOrDefault();

                return selectedCtx;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }
    }
}