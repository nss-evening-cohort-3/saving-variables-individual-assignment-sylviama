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
    public class charValueRepository
    {
      
        //after Add, need to SaveChanges
        public void AddChar(charValue charvalue)
        {
            SavingVariablesDatabase Context = new SavingVariablesDatabase();

            Context.charValueDb.Add(charvalue);
            Context.SaveChanges();
        }
    }
}