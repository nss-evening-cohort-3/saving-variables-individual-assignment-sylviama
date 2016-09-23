using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavingVariables;
using SavingVariables.DAL;
using System.Text.RegularExpressions;

namespace SavingVariables.Models
{
    public class ClearSingle
    {
        public string ClearSingleRecord(string input)
        {
            string clearXPattern = @"^clear[\s](?<constLetter>[a-zA-Z])";
            Match clearSingleMatch = Regex.Match(input.ToLower(), clearXPattern);

            var inputString = clearSingleMatch.Groups["constLetter"].Value.ToLower();

            var ctx = new SavingVariablesDatabase();
            try
            {
                charValue selectedRecord = (from d in ctx.charValueDb
                                            where d.charName == inputString
                                            select d).SingleOrDefault();
                if (selectedRecord != null)
                {
                    ctx.charValueDb.Remove(selectedRecord);
                    ctx.SaveChanges();
                }

                return "='" + inputString + "' is now free!";
            }
            catch(InvalidOperationException)
            {
                return "Ah oh, seems like we have more than one "+inputString+" record...";
            }
        }
    }
}
