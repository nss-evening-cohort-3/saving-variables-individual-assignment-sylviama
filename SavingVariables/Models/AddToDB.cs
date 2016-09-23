using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavingVariables.Models;
using SavingVariables.DAL;
using SavingVariables;
using System.Text.RegularExpressions;


namespace SavingVariables.Models
{
    public class AddToDB
    {
        public string AddToDBFunction(string input)
        
        {
            //parsing
            string constSetPattern = @"^(?<constantLetter>[a-zA-Z])(\s?)\=(\s?)(?<constantNumber>\d+)$";
            Match constantOperation = Regex.Match(input, constSetPattern);
            
            string consLetter = constantOperation.Groups["constantLetter"].Value.ToLower();
            int consNumber = int.Parse(constantOperation.Groups["constantNumber"].Value);

            //save if not have a record
            var ctx = new SavingVariablesDatabase();

            try
            {
                var testIfHasValue =ctx.charValueDb.SingleOrDefault(c => c.charName == consLetter);

                if (testIfHasValue == null)
                {
                    charValue Record = new charValue() { charName = consLetter, value = consNumber };
                    ctx.charValueDb.Add(Record);
                    ctx.SaveChanges();
                    return "Save " + consLetter + " as " + consNumber;
                }
                else
                {
                    return consLetter + " already has a value.";
                }
            }
            catch(InvalidOperationException)
            {
                return "Ah Oh.. seems like we have duplicate records.";
            }
            

            
                
            
            
            
        }
    }
}
