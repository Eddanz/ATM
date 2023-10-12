# Individuellt-projekt
En Console-application som simulerar en bankomat/internetbank.
Programmet är objektorienterat och är uppbyggt med två olika klasser och konstruktorer (BankAccount och User) för att skapa nya unika konton och användare, dessa sparas sedan i listor.
Så varje användare har ett användarnamn, en pinkod och egna konton.
Sedan finns det en switch-sats flera if else-statements, while-loopar och villkor därefter.
Det finns sex stycken egna metoder som används för att köra välkomstmenyn (WelcomeMenu), huvudmenyn (MainMenu), Logga in (LogIn), slumpmässigt meddelande (RandomMessage), för att ta ut pengar från konton (Withdraw) och föra över pengra mellan konton (Transfer).
Så när användaren startar programmet så välkomnas den till Console-Banken och får sedan logga in. Den har 3 försök på sig att logga in, om den misslyckas 3 gånger så kommer programmet att stängas av.
Lyckas den logga in så kommer användaren till huvudmenyn och har 4 olika val, se konton och saldon, föra över pengar mellan konton, ta ut pengar från konton och logga ut.

=== Reflektion ===
När jag började bygga programmet använde jag mig först av flera arrayer för konton, användarnamn, pinkoder och så vidare. Märkte snabbt att det blev väldigt rörigt och svårt att läsa koden. 
Så jag bestämde mig för att börja om och gjorde det objektorienterat, använde mig av klasser och listor, då det blir betyligt snyggare och enklare att jobba med och mer lättläst. Och gjort flera egna medtoder för att inte ha en massa kod i Main-kodblocket.
Skulle jag ha gjort om det så hade jag nog försökt få in en del metoder i mina egna klasserer istället för att skriva dem i Program-klassen.
