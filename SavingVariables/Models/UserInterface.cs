using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavingVariables.DAL;
using SavingVariables;
using SavingVariables.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SavingVariables.Models
{
    public class UserInterface
    {
        public void UserInterfaceFunctions()
        {
            while (true)
            {
                Console.WriteLine(">>");
                string input = Console.ReadLine();

                //x=5 pattern
                string constSetPattern = @"^(?<constantLetter>[a-zA-Z])(\s?)\=(\s?)(?<constantNumber>\d+)$";
                Match addToDBMatch = Regex.Match(input, constSetPattern);

                //x pattern
                string constGetPattern = @"^(?<constLetter>[a-zA-Z])$";
                Match showSingleMatch = Regex.Match(input, constGetPattern);

                //clear x pattern
                string clearXPattern = @"^clear[\s](?<constLetter>[a-zA-Z])";
                Match clearSingleMatch = Regex.Match(input.ToLower(), clearXPattern);

                //quit/exit
                if ((input.ToLower() == "quit") | (input.ToLower() == "exit"))
                {
                    Console.WriteLine("Bye!");
                    break;
                }
                //remove/delete/clear all
                else if ((input.ToLower() == "remove all") | (input.ToLower() == "delete all") | (input.ToLower() == "clear all"))
                {
                    SavingVariablesRepository clearAll = new SavingVariablesRepository();
                    clearAll.ClearAllFunction();
                    Console.WriteLine("All the records are removed.");

                }
                //show all
                else if ((input.ToLower() == "showall") | (input.ToLower() == "show all"))
                {
                    SavingVariablesRepository showAll = new SavingVariablesRepository();
                    List<charValue> selectedCtx = showAll.showItAll();

                    if (selectedCtx != null)
                    {
                        foreach (var element in selectedCtx)
                        {
                            Console.WriteLine(element.charName + " --> " + element.value);
                        }
                    }
                }
                //show single record
                else if (showSingleMatch.Success == true)
                {
                    SavingVariablesRepository showSingle = new SavingVariablesRepository();
                    var record = showSingle.showSingleRecord(input.ToLower());

                    if (record != null)
                    {
                        Console.WriteLine(record.charName + " --> " + record.value);
                    }
                    else
                    {
                        Console.WriteLine(input + " doesn't have a value.");
                    }

                }
                //save into database
                else if (addToDBMatch.Success == true)
                {
                    string consLetter = addToDBMatch.Groups["constantLetter"].Value.ToLower();
                    int consNumber = int.Parse(addToDBMatch.Groups["constantNumber"].Value);

                    SavingVariablesRepository string1 = new SavingVariablesRepository();
                    string1.SavingAValueToDB(consLetter, consNumber);

                    Console.WriteLine("Save " + consLetter + " as " + consNumber);
                }
                //clear x record
                else if (clearSingleMatch.Success == true)
                {
                    var inputString = clearSingleMatch.Groups["constLetter"].Value.ToLower();

                    SavingVariablesRepository clearSingle = new SavingVariablesRepository();
                    string clearAlert = clearSingle.ClearSingleRecord(inputString);
                    Console.WriteLine(clearAlert);

                }
            }
        }
        
        
        
    }
}
