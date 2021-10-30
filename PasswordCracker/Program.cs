using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordCracker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var stopWatch = new Stopwatch();

            while (true)
            {
                Console.WriteLine("Input password or leave empty to quit: ");
                var password = Console.ReadLine();

                try
                {
                    if (string.IsNullOrEmpty(password)) throw new Exception("Password cannot be empty!");
                    var cracker = new Cracker(password);

                    //Cracker.Status += Cracker_Status;

                    if (string.IsNullOrEmpty(password)) break;

                    Console.WriteLine($"Maximum password lenght: ");
                    var setup = Console.ReadLine();
                    if (!string.IsNullOrEmpty(setup) && setup.All(char.IsDigit) && int.Parse(setup) > 2) cracker.MaxLen = int.Parse(setup);

                    Console.WriteLine($"Check Numbers [y/n]: ");
                    setup = Console.ReadLine();
                    if (!string.IsNullOrEmpty(setup) && setup.Equals("y", StringComparison.OrdinalIgnoreCase)) cracker.UseNumbers = true;

                    Console.WriteLine($"Check Lower Case [y/n]: ");
                    setup = Console.ReadLine();
                    if (!string.IsNullOrEmpty(setup) && setup.Equals("y", StringComparison.OrdinalIgnoreCase)) cracker.UseLowerCases = true;

                    Console.WriteLine($"Check Upper Case [y/n]: ");
                    setup = Console.ReadLine();
                    if (!string.IsNullOrEmpty(setup) && setup.Equals("y", StringComparison.OrdinalIgnoreCase)) cracker.UseUpperCases = true;

                    Console.WriteLine($"Check Special Characters [y/n]: ");
                    setup = Console.ReadLine();
                    if (!string.IsNullOrEmpty(setup) && setup.Equals("y", StringComparison.OrdinalIgnoreCase)) cracker.UseSpecials = true;

                    Console.WriteLine("Processing...");
                    stopWatch.Start();

                    var res = cracker
                        .AsParallel()
                        .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
                        .FirstOrDefault(el => el == password);

                    if (string.IsNullOrEmpty(res)) Console.WriteLine(">>> Password not found! <<<");
                    else Console.WriteLine($">>> Password is: {res} <<<");

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");

                }
                finally
                {
                    stopWatch.Stop();
                    var ts = stopWatch.Elapsed;
                    string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",ts.Hours, ts.Minutes, ts.Seconds,ts.Milliseconds / 10);
                    Console.WriteLine($"Time to crack: {elapsedTime}");
                }
            }
        }
    }
}
