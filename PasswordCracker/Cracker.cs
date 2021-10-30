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
        public const string Characters = @"ąęźżćłśĄĘĆŚŹŻŁabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789~!@#$%^&*()_+}{?>.<*-,./';][\|`~";
    }

    public class Cracker : IEnumerable
    {
        private readonly string _password;
        private readonly string _charSet;
        private StringBuilder sb = new StringBuilder();

        private ulong _lenght;
        private uint _minLenght;
        private uint _maxLenght;

        public Cracker(string password, string charSet)
        {
            _password = password;
            _charSet = charSet;
            _maxLenght = 16;
            _minLenght = 1;
        }

        public IEnumerator GetEnumerator()
        {
            _lenght = (ulong)_charSet.Length;
            for (var x = _minLenght; x <= _maxLenght; x++)
            {
                ulong total = (ulong)Math.Pow((double)_charSet.Length, (double)x);
                ulong counter = 0;
                while (counter < total)
                {
                    string a = Factor(counter, x - 1);
                    yield return a;
                    counter++;
                }
            }
        }
        private string Factor(ulong counter, double power)
        {
            sb.Length = 0;
            while (power >= 0)
            {
                sb = sb.Append(_charSet[(int)(counter % _lenght)]);
                counter /= _lenght;
                power--;
            }
            return sb.ToString();
        }
    }
}
