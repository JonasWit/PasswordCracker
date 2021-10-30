using System;
using System.Threading.Tasks;

namespace PasswordCracker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Input password or leave empty to quit: ");
                var password = Console.ReadLine();

                if (string.IsNullOrEmpty(password)) break;

                var cracker = new Cracker(password, CharSet.Characters);
                ulong passwordsChecked = 0;

                Parallel.ForEach(cracker, p => 
                {
                    Console.Write($"Passwords checked: {passwordsChecked++} - {p}\r");
                    if (p == password)
                    {
                        Console.WriteLine("target found:" + p);
                        return;
                    }
                });

                //foreach (string result in cracker)
                //{
                //    Console.Write($"Passwords checked: {passwordsChecked++} - {result}\r");
                //    if (result == password)
                //    {
                //        Console.WriteLine("target found:" + result);
                //        return;
                //    }
                //}

                Console.WriteLine("Test Finished!");
            }
        }
    }
}
