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
            List<BankAccount> AlphaAccounts = new List<BankAccount> //Lista med konton som tillhör användaren "Alpha"
            {
                new BankAccount("Lönekonto", 20157) //Skapar nytt objekt från BankAccount-klassen
            };
            List<BankAccount> BravoAccounts = new List<BankAccount> //Lista med konton som tillhör användaren "Bravo"
            {
                new BankAccount("Lönekonto", 14537), //Skapar nya objekt från BankAccount-klassen
                new BankAccount("Sparkonto", 10000),
            };
            List<BankAccount> CharlieAccounts = new List<BankAccount> //Lista med konton som tillhör användaren "Charlie"
            {
                new BankAccount("Lönekonto", 25600), //Skapar nya objekt från BankAccount-klassen
                new BankAccount("Sparkonto", 20000),
                new BankAccount("Matkonto", 3000)
            };
            List<BankAccount> DeltaAccounts = new List<BankAccount> //Lista med konton som tillhör användaren "Delta"
            {
                new BankAccount("Lönekonto", 32881), //Skapar nya objekt från BankAccount-klassen
                new BankAccount("Sparkonto", 30000),
                new BankAccount("Matkonto", 4000),
                new BankAccount("Resa", 10000)
            };
            List<BankAccount> EchoAccounts = new List<BankAccount> //Lista med konton som tillhör användaren "Echo"
            {
                new BankAccount("Lönekonto", 58923), //Skapar nya objekt från BankAccount-klassen
                new BankAccount("Sparkonto", 60000),
                new BankAccount("Matkonto", 7000),
                new BankAccount("Resa", 15000),
                new BankAccount("Elektronik", 30000)
            };

            List<User> users = new List<User> //Lista med användare
            {
                new User("Alpha", 1111, AlphaAccounts), //Skapar nya objekt från User-klassen
                new User("Bravo", 2222, BravoAccounts),
                new User("Charlie", 3333, CharlieAccounts),
                new User("Delta", 4444, DeltaAccounts),
                new User("Echo", 5555, EchoAccounts),
            };

            while (true)
            {
                WelcomeMenu(); //Kör metoden WelcomeMenu
                User loggedInUser = LogIn(users); //Deklarerar en variabel av datatypen User som kallar på LogIn metoden och tar emot lista med användare, returnerar sedan en inloggad användare som tilldelas till loggedInUser
                if (loggedInUser != null) //Om loggedInUser tilldelats en användare så körs MainMenu metoden som tar emot användaren
                {
                    MainMenu(loggedInUser);
                }
            }
        }

        /* === WelcomeMenu metod === 
        Det första användaren ser när programmet startar och hälsar användaren välkommen. */
        static void WelcomeMenu()
        {
            Console.Clear();
            Console.WriteLine("\n===== CONSOLE-BANKEN =====");
            RandomMessage(); //Skriver ut ett slumpmässigt välkomstmeddelande
            Console.WriteLine("\nVänligen tryck ENTER för att forsätta...");
            Console.ReadLine();
        }

        /* === LogIn metod ===
        Metod för att användarna ska kunna logga in på sin egna sida i banken, kontrollerar att användarnamn och pinkod stämmer överrens.
        Om användare lyckas logga in så returnerar metoden den inloggade användaren. Misslyckas man logga in 3 gånger så stängs programmet. */
        static User LogIn(List<User> users)
        {
            int loginAttempts = 3; //Hur många försök användaren har att försöka logga in
            while (loginAttempts != 0) //While-loop som körs så länge försöken som är kvar inte är noll
            {
                Console.Clear();
                Console.Write("\nAnvändarnamn: ");
                string enterName = Console.ReadLine().ToUpper();
                Console.Write("Pinkod: ");
                if (int.TryParse(Console.ReadLine(), out int enterPincode))
                {
                    //Med hjälp av FirstOrDefualt och lambda-uttryck så söker den igenom listan av användare och ser om den hittar matchning med inmatat användar namn och pinkod,
                    //returnerar sedan en inloggad användare som tilldelas till loggedInUser.
                    User loggedInUser = users.FirstOrDefault(u => u.UserName == enterName && u.UserPinCode == enterPincode);

                    if (loggedInUser != null) //Om loggedInUser tilldelats en användare
                    {
                        Console.Clear();
                        Console.WriteLine($"\nInloggningen lyckades, varmt välkommen {loggedInUser.UserName.ToUpper()}!" +
                            $"\nVänligen vänta medan jag hämtar dina konton...");
                        Thread.Sleep(3000);
                        return loggedInUser; //Returnerar den inloggade användaren
                    }
                    else //Om användaren skriver in fel användarnamn eller pinkod
                    {
                        loginAttempts--; //Tar bort ett inloggningsförsök
                        Console.WriteLine($"\nOjdå... Inloggningen misslyckades. Fel användarnamn eller pinkod.\nDu har {loginAttempts} försök kvar!\nTryck ENTER för att fortsätta");
                        Console.ReadLine();
                    }
                }
                else //Om användaren skriver in bokstäver i pinkoden
                {
                    loginAttempts--; //Tar bort ett inloggningsförsök
                    Console.WriteLine($"\nOjdå... Inloggningen misslyckades. Pinkod kan bara vara siffror.\nDu har {loginAttempts} försök kvar!\nTryck ENTER för att fortsätta");
                    Console.ReadLine();
                }
            }
            if (loginAttempts == 0) //Om inloggningsförsöken är slut
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Försök är slut! Programmet stängs ner...");
                Thread.Sleep(3000);
                Environment.Exit(0); //Stänger av programmet
            }
            return null;
        }

        /* === MainMenu metod === 
        Menyn användaren ser när den loggat in på sin sida, där den kan göra fyra olika val. */
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
                    //Skriver ut kontonamn och saldon för konton.
                    case "1":
                        Console.Clear();
                        foreach (var account in loggedInUser.Accounts)
                        {
                            Console.WriteLine($"\nKonto: {account.AccountName}\nSaldo: {account.AccountBalance:C}");
                        }
                        Console.WriteLine("\nVänligen tryck ENTER för att forsätta...");
                        Console.ReadLine();
                        break;

                    //Skriver ut kontonamn och saldon för konton, där användaren först väljer vilket konto pengar ska tas ifrån och sen vilket konto pengar ska gå till.
                    //Måste finnas två konton hos användaren för att använda denna funktion.
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
                                    Transfer(loggedInUser, accountChoiseFrom - 1, accountChoiseTo - 1); //Kör transfer metoden
                                }
                                else
                                {
                                    Console.WriteLine($"\nDu kan inte skriva bokstäver, välj alternativ 1-{accountNumber}!");
                                    Thread.Sleep(2000);
                                }
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"\nDu kan inte skriva bokstäver, välj alternativ 1-{accountNumber}!");
                                Thread.Sleep(2000);
                            }
                            break;
                        }

                    //Skriver ut kontonamn och saldon för konton, där användaren får välja vilket konto den vill ta ut pengar från.
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

                    //Loggar ut användaren och återgår till WelcomeMenu
                    case "4":
                        Console.Clear();
                        Console.WriteLine("\nDu loggas nu ut...");
                        Thread.Sleep(3000);
                        return;

                    //Felhantering om användare försöker välja något annat än siffra 1-4
                    default:
                        Console.WriteLine("\nDu måste välja alternativ 1-4!");
                        Thread.Sleep(2000);
                        break;
                }
            }
        }

        /* === RandomMessage metod === 
        Väljer ett av tre meddelanden att skriva ut i WelcomeMenu när programmet startar eller återgår till WelcomeMenu efter utloggning. */
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

        /* === Withdraw metod === 
        Metod för att ta ut pengar från konton. */
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

        /* === Transfer metod === 
        Metod för att ta föra över pengar mellan konton.*/
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
