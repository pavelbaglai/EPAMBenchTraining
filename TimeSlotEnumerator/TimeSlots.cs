using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace TimeSlotEnumerator
{

    public class TimeSlots : IEnumerable<string>
    {
        private List<string> _slots;
        private string _startingSlot;
        public TimeSlots(List<string> slots, string startingSlot, int count)
        {
            _slots = slots;
            _startingSlot = startingSlot;
        }

        public IEnumerator<string> GetEnumerator()
        {
            int index = _slots.FindIndex(a => a == _startingSlot);
            yield return _slots[index];
            int i = 1;
            while (index - i >= 0 || index + i < _slots.Count)
            {
                if (index - i >= 0)
                {
                    yield return _slots[index - i];
                }
                if (index + i < _slots.Count)
                {
                    yield return _slots[index + i];
                }
                i++;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    /*public class TimeSlots : IEnumerable<string>
    {
        private List<string> _slots;
        private string _startingSlot;
        private int _count;
        public TimeSlots(List<string> slots, string startingSlot, int count)
        {
            _slots = slots;
            _startingSlot = startingSlot;
            _count = count;
        }

        public IEnumerator<string> GetEnumerator()
        {
            int index = _slots.FindIndex(a=>a==_startingSlot);
            yield return _slots[index];
            int i = 1;
            int count = 1;
            while ((index - i >= 0 || index + i < _slots.Count) && count < _count)
            {
                if (index - i >= 0)
                {
                    yield return _slots[index - i];
                    count++;
                }
                if (index + i < _slots.Count && count < _count)
                {
                    yield return _slots[index + i];
                    count++;
                }
                i++;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }*/
}
