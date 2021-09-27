using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeSlotEnumerator
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSlots slots = new TimeSlots(new List<string> { "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45" }, "11:15", 5);
            
            foreach (var slot in slots)
            {
                Console.WriteLine(slot);
            }
            foreach (var slot in slots.Take(5))
            {
                Console.WriteLine(slot);
            }
            Console.Read();
        }
    }
}
