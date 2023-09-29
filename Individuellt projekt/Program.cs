using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Individuellt_projekt
{// ===== Eddie Halling SUT23 =====
    internal class Program
    {
        static void Main(string[] args)
        {
            List<BankAccount> AlphaAccounts = new List<BankAccount>
            {

            };
            List<BankAccount> BravoAccounts = new List<BankAccount>
            {

            };
            List<BankAccount> CharlieAccounts = new List<BankAccount>
            {

            };
            List<BankAccount> DeltaAccounts = new List<BankAccount>
            {

            };
            List<BankAccount> EchoAccounts = new List<BankAccount>
            {

            };

            List<User> users = new List<User>
            {
                new User("Alpha", 1111, AlphaAccounts),
                new User("Bravo", 2222, BravoAccounts),
                new User("Charlie", 3333, CharlieAccounts),
                new User("Delta", 4444, DeltaAccounts),
                new User("Echo", 5555, EchoAccounts),
            };

            while (true)
            {
                WelcomeMenu();
                User loggedInUser = LogIn(users);
                if (loggedInUser != null)
                {
                    MainMenu(loggedInUser);
                }
            }
        }
        static void WelcomeMenu()
        {
            Console.Clear();
            Console.WriteLine("\n===== CONSOLE-BANKEN =====" +
                "\n\nHej och välkommen till Console-Banken, den enda banken du behöver i ditt liv!" +
                "\n\nVänligen tryck ENTER för att forsätta...");
            Console.ReadLine();
        }
        static User LogIn(List<User> users)
        {
            try
            {
                int loginAttempts = 3;
                
                
                while (loginAttempts != 0)
                {
                    Console.Clear();
                    Console.Write("\nAnvändarnamn: ");
                    string enterName = Console.ReadLine().ToUpper();
                    Console.Write("Pinkod: ");
                    int enterPincode;
                    if (int.TryParse(Console.ReadLine(), out enterPincode))
                    {
                        User loggedInUser = users.FirstOrDefault(u => u.UserName == enterName && u.UserPinCode == enterPincode);

                        if (loggedInUser != null)
                        {
                            Console.Clear();
                            Console.WriteLine($"\nInloggningen lyckades, varmt välkommen {loggedInUser.UserName.ToUpper()}!" +
                                $"\nVänligen vänta medan jag hämtar dina konton...");
                            Thread.Sleep(3000);
                            return loggedInUser;
                        }
                        else
                        {
                            loginAttempts--;
                            Console.WriteLine($"\nOjdå... Inloggningen misslyckades. Fel användarnamn eller pinkod.\nDu har {loginAttempts} försök kvar!\nTryck ENTER för att fortsätta");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        loginAttempts--;
                        Console.WriteLine($"\nOjdå... Inloggningen misslyckades. Pinkod kan bara vara siffror.\nDu har {loginAttempts} försök kvar!\nTryck ENTER för att fortsätta");
                        Console.ReadLine();
                    }
                }
                if (loginAttempts == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Försök är slut! Programmet stängs ner...");
                    Thread.Sleep(3000);
                    Environment.Exit(0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
            return null;
        }
        static void MainMenu(User loggedInUser)
        {
            while (true)
            {
                Console.Clear();
                Console.Write($"\n===== Du är inloggad som {loggedInUser.UserName.ToUpper()} ===== " +
                    "\n\nVad vill du göra för något? Välj alternativ 1-4." +
                    "\n\n[1] Visa konton och saldo" +
                    "\n[2] För över pengar" +
                    "\n[3] Ta ut pengar" +
                    "\n[4] Logga ut" +
                    "\n\nVÄLJ: ");
                string userChoise = Console.ReadLine();
                switch (userChoise)
                {
                    case "1":
                        Console.Clear();
                        break;
                    case "2":

                        break;
                    case "3":

                        break;
                    case "4":
                        Console.Clear();
                        Console.WriteLine("\nDu loggas nu ut...");
                        Thread.Sleep(3000);
                        return;
                    default:
                        Console.WriteLine("\nDu måste välja alternativ 1-4!");
                        Thread.Sleep(2000);
                        break;
                }
            }
        }
    }
}
