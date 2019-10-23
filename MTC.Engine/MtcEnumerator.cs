using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MTC.Engine
{
    public class MtcEnumerator : IEnumerator<MtcQuestion>
    {
        private int _position;
        private readonly IEnumerable<MtcQuestion> _questions;

        public MtcQuestion Current => _questions.ElementAt(_position);
        object IEnumerator.Current => Current;

        public MtcEnumerator(IEnumerable<MtcQuestion> questions)
        {
            _position = -1;
            _questions = questions;
        }

        public bool MoveNext()
        {
            return ++_position < _questions.Count();
        }

        public void Reset()
        {
            _position = -1;
        }

        public void Dispose()
        {
        }
    }
}
