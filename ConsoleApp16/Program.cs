// See https://aka.ms/new-console-template for more information
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Collections.Generic;


namespace A 
{
    class program
    {

        //ASCII CISLA POLE
        private static readonly Dictionary<char, string[]> mapapoli = new Dictionary<char, string[]>()
        {
            { '0', new string[5] { "  __   ", " /  \\  ", "| () | ", " \\__/  ", "       " } },
            { '1', new string[5] { " _  ", "/ | ", "| | ", "|_| ", "    " } },
            { '2', new string[5] { " ___  ", "(_  ) ", " / /  ", "/___| ", "      " } },
            { '3', new string[5] { " ____ ", "|__ / ", " |_ \\ ", "|___/ ", "      " } },
            { '4', new string[5] { " _ _   ", "| | |  ", "|_  _| ", "  |_|  ", "       " } },
            { '5', new string[5] { " ___  ", "| __| ", "|__ \\ ", "|___/ ", "      " } },
            { '6', new string[5] { "  __  ", " / /  ", "/ _ \\ ", "\\___/ ", "      " } },
            { '7', new string[5] { " ____  ", "|__  | ", "  / /  ", " /_/   ", "       " } },
            { '8', new string[5] { " ___  ", "( _ ) ", "/ _ \\ ", "\\___/ ", "      " } },
            { '9', new string[5] { " ___  ", "/ _ \\ ", "\\_, / ", " /_/  ", "      " } },
            { 'R', new string[5] { "\u001b[34m ___  \u001b[0m", "\u001b[34m| _ \\ \u001b[0m", "\u001b[34m|   / \u001b[0m", "\u001b[34m|_|_\\ \u001b[0m", "      \u001b[0m" } },
            { 'O', new string[5] { "  ___ ", " / _ \\", "| (_) ", " \\___/", "      " } },
            { 'h', new string[5] { " _   ", "| |_ ", "| ' \\", "|_||_", "     " } },
            { 'm', new string[5] { "       ", " _ __  ", "| '  \\ ", "|_|_|_|", "       " } },
            { 'I', new string[5] { "\u001b[92m ___  \u001b[0m", "\u001B[92m|_ _| \u001b[0m", "\u001B[92m | |  \u001b[0m", "\u001B[92m|___| \u001b[0m", "\u001B[92m      \u001b[0m" } },
            { 'A', new string[5] { "   _    ", "  /_\\   ", " / _ \\  ", "/_/ \\_\\ ", "        " } },
            { 'U', new string[5] { "\u001b[31m _   _  \u001b[0m", "\u001b[31m| | | | \u001b[0m", "\u001b[31m| |_| | \u001b[0m", "\u001b[31m \\___/  \u001b[0m", "\u001b[31m        \u001b[0m" } },
            { 'V', new string[5] { "__   __ ", "\\ \\ / / ", " \\ V /  ", "  \\_/   ", "        " } },
            { '=', new string[5] { "      ", " ___  ", "|___| ", "|___| ", "      " } },
            { ' ', new string[5] { "   ", "   ", "   ", "   ", "   " } },
            { ',', new string[5] { "    ", "    ", " _  ", "( ) ", "|/   " } }

        };


        //BARVENI TEXTU
        static void ColorWrite(ConsoleColor color, string text, bool endLine)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();

            if (endLine)
                Console.WriteLine();
        }


        //PRIRAZENI INPUT CISEL K VELICINAM

        static bool retezjecislo(string input)
        {
            int intValue;
            float floatValue;
            double doubleValue;
            if (int.TryParse(input, out intValue) || float.TryParse(input, out floatValue) || double.TryParse(input, out doubleValue))
            {
                return true;
            }
            else
                return false;
        }
        static string minusposledni (string input)
        {
            return input.Substring(0, input.Length - 1);
        }
        static string posledni(string input)
        {
            return input.Substring(input.Length - 1);
        }

        static bool spatnyinput (string input)
        {
            string povolene = "UuVvIiAaRrOoOhmohmOmom";
            if (input.Split(' ').Length == 2)
            {
                string prvni = input.Split(' ')[0];
                string druhy = input.Split(' ')[1];
                if (!(povolene.Contains(posledni(prvni)) && povolene.Replace(posledni(prvni), "").Contains(posledni(druhy))
                    || !(retezjecislo(minusposledni(prvni)) && retezjecislo(minusposledni(druhy)))))
                {
                    return true;
                }
                return false;

            }
            return true;

        }

        static bool DobryInput(string input)
        {

            if (spatnyinput(input))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        static string ZiskejVysledek(string input) 
        { 
            string pattern = @"(\d+)([UuVvIiAaRrOoOhmohmOmom])";
            Regex regex = new Regex(pattern);

            MatchCollection matches = regex.Matches(input);

            //veliciny
            double u = 0;
            double i = 0;
            double r = 0;

    


            //prirazeni
            foreach (Match match in matches)
            {
                double value = Double.Parse(match.Groups[1].Value);
                string unit = match.Groups[2].Value;

                if (unit == "U" || unit == "u" || unit == "V" || unit == "v")
                {
                   u = value;
                }

                else if (unit == "I" || unit == "i" || unit == "A" || unit == "a")
                {
                    i = value;
                }

                else if (unit == "R" || unit == "r" || unit == "O" || unit == "o" || unit == "Ohm" || unit == "ohm" || unit == "Om" || unit == "om")
                {
                    r = value;
                }
            }

            //ZJISTENI VELICIN
            //napeti
            string vysledek = "";
            if (i != 0 && r != 0)
            {
                vysledek = " U = " + (i * r).ToString() + " V";
            }
            //proud
            else if (u != 0 && r != 0)
            {
                vysledek = " I = " + (u / r).ToString() + " A";
            }
            //odpor
            else if (u != 0 && i != 0)
            {
                vysledek = " R = " + (u / i).ToString() + " Ohm";
            }
            return vysledek.Replace('.', ',');
        }
        




            //VYPOCET - FUNKCE
        //napeti
        static string napeti(double y, double z)
        {
            return (y * z).ToString();
        }
        //proud
        static string proud(double x, double z)
        {
            return (x / z).ToString();
        }
        //odpor
        static string odpor(double x, double y)
        {
            return (x / y).ToString();
        }




            //VYPIS V ASCII CISLECH
        static string[] vypis(string s)
        {
            string[] result = new string[5];

            for (int y = 0; y < 5; y++)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    result[y] += mapapoli[s[i]][y];
                }
            }
            return result;

        }




        
        static void Main(string[] args)
        {
            bool spust = true;
            while (spust)
            {

                //NADPIS A ZISKANI PROMENNYCH
                Console.Clear();
                ColorWrite(ConsoleColor.White, " ", true);
                ColorWrite(ConsoleColor.White, " ┌├──", false);
                ColorWrite(ConsoleColor.DarkGray, "────────────────────────────────────────────────────────────────────────────", false);
                ColorWrite(ConsoleColor.White, "──┤┐", true);
                ColorWrite(ConsoleColor.White, "  │ Zadejte prosím minimálně dvě, ze tří veličin: ", false);
                ColorWrite(ConsoleColor.White, "\u001b[31mNapětí (U)\u001b[0m", false);
                ColorWrite(ConsoleColor.White, ", ", false);
                ColorWrite(ConsoleColor.White, "\u001b[92mProud (I)\u001b[0m", false);
                ColorWrite(ConsoleColor.White, ", ", false);
                ColorWrite(ConsoleColor.White, "\u001b[34mOdpor (R) \u001b[0m", false);
                ColorWrite(ConsoleColor.White, "│", true);
                ColorWrite(ConsoleColor.White, " └├──", false);
                ColorWrite(ConsoleColor.DarkGray, "────────────────────────────────────────────────────────────────────────────", false);
                ColorWrite(ConsoleColor.White, "──┤┘", true);
                ColorWrite(ConsoleColor.White, " ", true);
                ColorWrite(ConsoleColor.DarkGray, " >>  ", false);

                string? input = Console.ReadLine();

                //VYPSANI VYSLEDKU
                ColorWrite(ConsoleColor.White, " ", true);
                ColorWrite(ConsoleColor.White, " ┌├", false);
                ColorWrite(ConsoleColor.DarkGray, "─────────────────────────────────────────────────", false);
                ColorWrite(ConsoleColor.White, ">=<-O0O->=<", false);
                ColorWrite(ConsoleColor.DarkGray, "─────────────────────────────────────────────────", false);
                ColorWrite(ConsoleColor.White, "┤┐", true);

                if (DobryInput(input))
                {
                    string[] vypis1 = vypis(ZiskejVysledek(input));

                    for (int i = 0; i < vypis1.Length; i++)
                    {
                        Console.WriteLine(vypis1[i]);

                    }
                }
                else
                {
                    
                    Console.WriteLine("\u001b[31m                  ___   ___     _     _____   _  _  __   __    ___   _  _   ___   _   _   _____ \r\n                 / __| | _ \\   /_\\   |_   _| | \\| | \\ \\ / /   |_ _| | \\| | | _ \\ | | | | |_   _|\r\n                 \\__ \\ |  _/  / _ \\    | |   | .` |  \\ V /     | |  | .` | |  _/ | |_| |   | |  \r\n                 |___/ |_|   /_/ \\_\\   |_|   |_|\\_|   |_|     |___| |_|\\_| |_|    \\___/    |_|   \u001b[0m");
                    Console.WriteLine("");

                }
                ColorWrite(ConsoleColor.White, " └├", false);
                ColorWrite(ConsoleColor.DarkGray, "─────────────────────────────────────────────────", false);
                ColorWrite(ConsoleColor.White, ">=<-O0O->=<", false);
                ColorWrite(ConsoleColor.DarkGray, "─────────────────────────────────────────────────", false);
                ColorWrite(ConsoleColor.White, "┤┘", true);

                //UKONCENI PROGRAMU
                ColorWrite(ConsoleColor.White, " ", true);
                ColorWrite(ConsoleColor.Magenta, "    ", false);
                ColorWrite(ConsoleColor.Magenta, "\u001b[7m!\u001b[0m", false);
                ColorWrite(ConsoleColor.White, " Pokud si ", false);
                ColorWrite(ConsoleColor.Magenta, "NEPŘEJEŠ ", false);
                ColorWrite(ConsoleColor.White, "pokračovat zmáčkni", false);
                ColorWrite(ConsoleColor.Magenta, " Q ", true);
                ColorWrite(ConsoleColor.DarkGray, " >>  ", false);

                string konec = Console.ReadKey().KeyChar.ToString();
                if (konec == "Q" || konec == "q")
                {
                    spust = false;
                }
                
            }

        }
    
    }
}