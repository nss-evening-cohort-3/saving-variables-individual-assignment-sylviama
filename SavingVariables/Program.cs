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
            UserInterface UI = new UserInterface();
            UI.UserInterfaceFunctions();
            
        }
    }
}
