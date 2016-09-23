using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SavingVariables.Models;
using SavingVariables.DAL;
using System.Text.RegularExpressions;

namespace SavingVariables
{
    public class Program
    {
        static void Main(string[] args)
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
                else if((input.ToLower()=="remove all")|(input.ToLower()=="delete all")|(input.ToLower()=="clear all"))
                {
                    ClearAll clearAll = new ClearAll();
                    var clearedAllAlert = clearAll.ClearAllFunction();
                    Console.WriteLine(clearedAllAlert);
                
                }
                //show all
                else if ((input.ToLower() == "showall")| (input.ToLower() == "show all"))
                {
                    ShowAll showAll = new ShowAll();
                    IQueryable<charValue> selectedCtx= showAll.showItAll();

                    if (selectedCtx != null)
                    {
                        foreach (var element in selectedCtx)
                        {
                            Console.WriteLine(element.charName + " --> " + element.value);
                        }
                    }
                }
                //show single record
                else if(showSingleMatch.Success==true)
                {
                    ShowSingle showSingle = new ShowSingle();
                    var record = showSingle.showSingleRecord(input.ToLower());

                    if (record != null)
                    {
                        Console.WriteLine(record.charName + " --> " + record.value);
                    } else
                    {
                        Console.WriteLine(input + " doesn't have a value.");
                    }

                }
                //save into database
                else if(addToDBMatch.Success==true)
                {
                    AddToDB string1 = new AddToDB();
                    string savedOutput = string1.AddToDBFunction(input);

                    Console.WriteLine(savedOutput);
                }
                //clear x record
                else if(clearSingleMatch.Success==true)
                {
                    ClearSingle clearSingle = new ClearSingle();
                    string clearAlert = clearSingle.ClearSingleRecord(input);
                    Console.WriteLine(clearAlert);

                }
            }
            
            //testing for all
          
        }
    }
}
