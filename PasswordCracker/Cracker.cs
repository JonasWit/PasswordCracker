using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordCracker
{
    public struct CharSet
    {
        public const string Numbers = @"0123456789";
        public const string LowerCases = @"ąęźżćłśabcdefghijklmnopqrstuvwxyz";
        public const string UpperCases = @"ĄĘĆŚŹŻŁABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string Specials = @"!@#$%^&*()_+}{?>.<*-,./';][\|`~";
    }

    public class CrackerEventArgs : EventArgs
    {
        public ulong CombinationsChecked { get; set; }
        public int LenghtsChecked { get; set; }
    }

    public class Cracker : IEnumerable<string>
    {
        private readonly string _password;
        private bool _passwordFound;
        private StringBuilder sb = new();

        private ulong _lenght;

        public int MaxLen { get; set; } = 2;
        public bool UseNumbers { get; set; } 
        public bool UseLowerCases { get; set; }
        public bool UseUpperCases { get; set; } 
        public bool UseSpecials { get; set; }

        private string CreateCharSet()
        {
            var sBuilder = new StringBuilder();
            if (UseNumbers) sBuilder.Append(CharSet.Numbers);
            if (UseLowerCases) sBuilder.Append(CharSet.LowerCases);
            if (UseUpperCases) sBuilder.Append(CharSet.UpperCases);
            if (UseSpecials) sBuilder.Append(CharSet.Specials);

            return sBuilder.ToString();
        }

        public Cracker(string password)
        {
            _password = password;
        }

        public IEnumerator<string> GetEnumerator() 
        {
            var cSet = CreateCharSet();
            _lenght = (ulong)cSet.Length;
            for (var x = 1; x <= MaxLen; x++)
            {
                if (_passwordFound) break;
      
                ulong total = (ulong)Math.Pow((ulong)cSet.Length, (ulong)x);
                ulong counter = 0;

                while ((counter < total) && !_passwordFound)
                {
                    string str = Factor(counter, x - 1, cSet);
                    if (str.Equals(_password)) _passwordFound = true;
                    yield return str;
                    counter++;
                }
            }
        }

        private string Factor(ulong counter, double power, string charSet)
        {
            sb.Length = 0;
            while (power >= 0)
            {
                sb = sb.Append(charSet[(int)(counter % _lenght)]);
                counter /= _lenght;
                power--;
            }
            return sb.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)GetEnumerator()).GetEnumerator();
        }
    }
}
