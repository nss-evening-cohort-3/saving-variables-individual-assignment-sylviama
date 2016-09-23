using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavingVariables.DAL;
using SavingVariables;
using System.ComponentModel.DataAnnotations;

namespace SavingVariables.Models
{
    public class Parsing
    {
        public void parse(string parseString)
        {
            string lowerParseString = parseString.ToLower();
            switch(parseString)
            {
                case "quit":
                    //temperary write it here, should be in Program
                    Console.WriteLine("Bye");
                    break;

                
                //save into database
                //clearall: delete the entire database
                //showall: show the database
            }
        }
        
        
        
    }
}
