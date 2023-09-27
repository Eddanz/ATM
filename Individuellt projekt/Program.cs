using System;

namespace Individuellt_projekt
{// ===== Eddie Halling SUT23 =====
    internal class Program
    {
        static void Main(string[] args)
        {
            WelcomeMenu();
            LogIn();
        }
        static void WelcomeMenu()
        {
            Console.WriteLine("\n===== CONSOLE-BANKEN =====" +
                "\n\nHej och välkommen till Console-Banken, den enda banken du behöver i ditt liv!" +
                "\n\nVänligen tryck ENTER för att forsätta...");
            Console.ReadLine();
        }
        static void LogIn()
        {
            try
            {
                int loginAttempts = 3;
                string[] userName = new string[] { "Alpha", "Bravo", "Charlie", "Delta", "Echo" };
                int[] userPincode = new int[] { 1111, 2222, 3333, 4444, 5555 };
                
                while (loginAttempts !=0)
                {
                    Console.Clear();
                    Console.Write("\nAnvändarnamn: ");
                    string enterName = Console.ReadLine();
                    Console.Write("Pinkod: ");
                    int enterPincode;
                    if (int.TryParse(Console.ReadLine(), out enterPincode))
                    {
                        int userIndex = Array.IndexOf(userName, enterName);

                        if (userIndex != -1 && userPincode[userIndex] == enterPincode)
                        {
                            Console.WriteLine($"Inloggningen lyckades, varmt välkommen {enterName}!");
                            break;
                        }
                        else
                        {
                            loginAttempts--;
                            Console.WriteLine($"Ojdå... Inloggningen misslyckades. Fel användarnamn eller pinkod.\nDu har {loginAttempts} försök kvar!\nTryck ENTER för att fortsätta");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        loginAttempts--;
                        Console.WriteLine($"Ojdå... Inloggningen misslyckades. Pinkod kan bara vara siffror.\nDu har {loginAttempts} försök kvar!\nTryck ENTER för att fortsätta");
                        Console.ReadLine();
                    }
                }
                if (loginAttempts == 0)
                {
                    Console.WriteLine("Försök är slut");
                    
                }
                else
                {
                    Console.WriteLine("Nästa meny");
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
        }
    }
    
}
