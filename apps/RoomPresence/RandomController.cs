using System;
using System.Collections.Generic;

namespace Presence
{
    public class RandomController : IRandomController
    {
        private readonly int       _minDuration;
        private readonly int       _maxDuration;
        private readonly Random    _rnd         = new(Guid.NewGuid().GetHashCode());
        private          List<int> _usedNumbers = new();

        public RandomController(int minDuration, int maxDuration)
        {
            _minDuration = minDuration;
            _maxDuration = maxDuration + 1;
        }

        public int GetRandomDuration()
        {
            var randomDuration = _rnd.Next(_minDuration, _maxDuration);
            if (AllNumbersUsed())
                _usedNumbers.Clear();

            while (_usedNumbers.Contains(randomDuration))
                randomDuration = _rnd.Next(_minDuration, _maxDuration);
            _usedNumbers.Add(randomDuration);
            return randomDuration;
        }

        private bool AllNumbersUsed()
        {
            return _usedNumbers.Count == _maxDuration - _minDuration;
        }
    }
}