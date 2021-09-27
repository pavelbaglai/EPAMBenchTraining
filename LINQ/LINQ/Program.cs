using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] strings =
              {
                "You only live forever in the lights you make",
                "When we were young we used to say",
                "That you only hear the music when your heart begins to break",
                "Now we are the kids from yesterday"
              };
            char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
            //word count of each sentences
            var counts = strings.Select(s => s.Split(' ').Count());
            //words starting with a vowel
            var vowelFirst = strings.SelectMany(s => s.Split(' ')).Where(a => vowels.Contains(a.ToLower().First()));
            //longest word
            var longest = strings.SelectMany(s => s.Split(' ')).OrderByDescending(o=>o.Length).First();
            //Average word count
            var avg = strings.Average(s => s.Split(' ').Count());
            //alpabetically ordered without duplicates
            var ordered = strings.SelectMany(s => s.ToLower().Split(' ')).OrderBy(o => o).Distinct();
            
        }
    }
}
