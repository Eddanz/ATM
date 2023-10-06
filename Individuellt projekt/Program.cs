using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Channels;

namespace Individuellt_projekt
{// ===== Eddie Halling SUT23 =====
    internal class Program
    {
        static void Main(string[] args)
        {
            List<BankAccount> AlphaAccounts = new List<BankAccount>
            {
                new BankAccount("Lönekonto", 20157)
            };
            List<BankAccount> BravoAccounts = new List<BankAccount>
            {
                new BankAccount("Lönekonto", 14537),
                new BankAccount("Sparkonto", 10000),
            };
            List<BankAccount> CharlieAccounts = new List<BankAccount>
            {
                new BankAccount("Lönekonto", 25600),
                new BankAccount("Sparkonto", 20000),
                new BankAccount("Matkonto", 3000)
            };
            List<BankAccount> DeltaAccounts = new List<BankAccount>
            {
                new BankAccount("Lönekonto", 32881),
                new BankAccount("Sparkonto", 30000),
                new BankAccount("Matkonto", 4000),
                new BankAccount("Resa", 10000)
            };
            List<BankAccount> EchoAccounts = new List<BankAccount>
            {
                new BankAccount("Lönekonto", 58923),
                new BankAccount("Sparkonto", 60000),
                new BankAccount("Matkonto", 7000),
                new BankAccount("Resa", 15000),
                new BankAccount("Elektronik", 30000)
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
            Console.WriteLine("\n===== CONSOLE-BANKEN =====");
            RandomMessage();
            Console.WriteLine("\nVänligen tryck ENTER för att forsätta...");
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
                    if (int.TryParse(Console.ReadLine(), out int enterPincode))
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
                        foreach (var account in loggedInUser.Accounts)
                        {
                            Console.WriteLine($"\nKonto: {account.AccountName}\nSaldo: {account.AccountBalance:C}");
                        }
                        Console.WriteLine("\nVänligen tryck ENTER för att forsätta...");
                        Console.ReadLine();
                        break;

                    case "2":
                        Console.Clear();
                        int accountNumber = 0;
                        foreach (var account in loggedInUser.Accounts)
                        {
                            accountNumber++;
                            Console.WriteLine($"\nKonto {accountNumber}: {account.AccountName}\nSaldo: {account.AccountBalance:C}");
                        }
                        if (accountNumber <= 1)
                        {
                            Console.WriteLine("\nDu måste ha minst två konton för denna funktionen!\nTar dig tillbaka...");
                            Thread.Sleep(3000);
                            break;
                        }
                        else
                        {
                            Console.Write($"\n\nVilket konto vill du föra över pengar från?\nVälj 1-{accountNumber}: ");
                            if (int.TryParse(Console.ReadLine(), out int accountChoiseFrom))
                            {
                                if (accountChoiseFrom > accountNumber)
                                {
                                    Console.WriteLine($"\nDu måste välja alternativ 1-{accountNumber}!");
                                    Thread.Sleep(2000);
                                    break;
                                }
                                Console.Write($"\nVilket konto vill du föra över pengar till?\nVälj 1-{accountNumber}: ");
                                if (int.TryParse(Console.ReadLine(), out int accountChoiseTo))
                                {
                                    if (accountChoiseTo > accountNumber)
                                    {
                                        Console.WriteLine($"\nDu måste välja alternativ 1-{accountNumber}!");
                                        Thread.Sleep(2000);
                                        break;
                                    }
                                    else if (accountChoiseTo == accountChoiseFrom)
                                    {
                                        Console.WriteLine("\nDu kan inte föra över pengar mellan samma konto, du måste välja två olika!");
                                        Thread.Sleep(2000);
                                        break;
                                    }
                                }
                                Transfer(loggedInUser, accountChoiseFrom - 1, accountChoiseTo - 1);
                            }
                            else
                            {
                                Console.WriteLine($"\nDu kan inte skriva bokstäver, välj alternativ 1-{accountNumber}!");
                                Thread.Sleep(2000);
                            }
                            break;
                        }

                    case "3":
                        Console.Clear();
                        accountNumber = 0;
                        foreach (var account in loggedInUser.Accounts)
                        {
                            accountNumber++;
                            Console.WriteLine($"\nKonto {accountNumber}: {account.AccountName}\nSaldo: {account.AccountBalance:C}");
                        }
                        Console.Write($"\n\nVilket konto vill du ta ut pengar från?\nVälj 1-{accountNumber}: ");
                        if (int.TryParse(Console.ReadLine(), out int accountChoise))
                        {
                            if (accountChoise > accountNumber)
                            {
                                Console.WriteLine($"\nDu måste välja alternativ 1-{accountNumber}!");
                                Thread.Sleep(2000);
                            }
                            Withdraw(loggedInUser, accountChoise-1);
                        }
                        else
                        {
                            Console.WriteLine($"\nDu kan inte skriva bokstäver, välj alternativ 1-{accountNumber}!");
                            Thread.Sleep(2000);
                        }
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

        static void RandomMessage()
        {
            Random random = new Random();
            string[] Messages = new string[]
            { "\nHej och välkommen till Console-Banken, den enda banken du behöver i ditt liv!",
              "\nVarmt välkommen till Console-Banken, köp lågt, sälj högt!",
              "\nVälkommen till Console-Banken, varje krona är den första till miljonen!"
            };

            string randomMessage = Messages[random.Next(Messages.Length)];
            Console.Write(randomMessage);
        }

        static void Withdraw(User user, int accountIndex)
        {
            Console.Clear();

            if (accountIndex < user.Accounts.Count)
            {
                Console.Write($"\nHur mycket vill du ta från {user.Accounts[accountIndex].AccountName}?\nAnge: ");
                if (double.TryParse(Console.ReadLine(), out double amount))
                {
                    if (amount <= 0)
                    {
                        Console.WriteLine("Det måste vara större än 0!");
                        Thread.Sleep(2000);
                    }
                    else if (amount > user.Accounts[accountIndex].AccountBalance)
                    {
                        Console.WriteLine("Du har inte tillräckligt med pengar på kontot!");
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        user.Accounts[accountIndex].AccountBalance -= amount;
                        Console.WriteLine($"Du tog ut {amount:C} från kontot: {user.Accounts[accountIndex].AccountName}");
                        Thread.Sleep(3000);
                    }
                }
                else
                {
                    Console.WriteLine("Du måste ange det i siffror!");
                    Thread.Sleep(2000);
                }
            }
        }
        static void Transfer(User user, int accountIndexFrom, int accountIndexTo)
        {
            Console.Clear();

            if (accountIndexFrom < user.Accounts.Count)
            {
                Console.Write($"\nHur mycket vill du föra över från {user.Accounts[accountIndexFrom].AccountName} till {user.Accounts[accountIndexTo].AccountName}?\nAnge: ");
                if (double.TryParse(Console.ReadLine(), out double amount))
                {
                    if (amount <= 0)
                    {
                        Console.WriteLine("Det måste vara större än 0!");
                        Thread.Sleep(2000);
                    }
                    else if (amount > user.Accounts[accountIndexFrom].AccountBalance)
                    {
                        Console.WriteLine("Du har inte tillräckligt med pengar på kontot!");
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        user.Accounts[accountIndexFrom].AccountBalance -= amount;
                        user.Accounts[accountIndexTo].AccountBalance += amount;
                        Console.WriteLine($"Du förde över {amount:C} från kontot: {user.Accounts[accountIndexFrom].AccountName} till kontot: {user.Accounts[accountIndexTo].AccountName}");
                        Thread.Sleep(3000);
                    }
                }
                else
                {
                    Console.WriteLine("Du måste ange det i siffror!");
                    Thread.Sleep(2000);
                }
            }
        }
    }
}
