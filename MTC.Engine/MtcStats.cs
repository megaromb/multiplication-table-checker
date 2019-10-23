using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace MTC.Engine
{
    public class MtcStats
    {
        private readonly List<MtcStatsItem> _statsItems;
        public bool IsInTime { get; internal set; }

        public MtcStats(IReadOnlyCollection<MtcQuestion> questions)
        {
            questions.Should().NotBeNull();

            IsInTime = true;
            _statsItems = questions.Select(q => new MtcStatsItem(q, null, true)).ToList();
        }

        public IReadOnlyCollection<MtcStatsItem> GetStatistics() => _statsItems.AsReadOnly();

        public bool IsPassed() => IsInTime && _statsItems.All(item => item.IsPassed());

        public MtcStatsItem this[byte questionId] => _statsItems.Single(item => item.Question.Id == questionId);

        internal void MarkAsDelayed() => IsInTime = false;

        internal void RegisterAnswer(byte questionId, byte proposedAnswer, out bool status)
        {
            MtcStatsItem item = _statsItems.Single(i => i.Question.Id == questionId);
            item.ProposedAnswer = proposedAnswer;
            status = item.IsPassed();
        }
    }
}
