using System.Collections.Generic;
using System.Linq;

namespace Model.Configuration
{
    public class ConfigModel
    {
        private readonly IDictionary<int, int> _rules;
        public IDictionary<int, int> Rules { get { return _rules; } }

        private readonly int _size;
        public int Size { get { return _size; } }


        public ConfigModel(IDictionary<int, int> rules, int size)
        {
            _rules = rules;
            _size = size;
        }

        internal bool Validate()
        {
            //no ambiguity since using Dictionary
            if(_rules != null && _size > 0)
            {
                var hasOutOfBoundsValues = _rules.Where
                    (kv => !IsInRange(1, _size, kv.Key) || !IsInRange(1, _size, kv.Value)).Count() > 0;
                return !hasOutOfBoundsValues;
            }
            return false;
        }

        private bool IsInRange(int start, int end, int num)
        {
            return Enumerable.Range(start, end).Contains(num);
        }
    }
}
