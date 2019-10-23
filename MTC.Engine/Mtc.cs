using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FluentAssertions;

namespace MTC.Engine
{
    public class Mtc : IMtc
    {
        public MtcSettings Settings { get; }
        private readonly IReadOnlyCollection<MtcQuestion> _questions;
        private MtcStats _stats;
        private readonly Timer _questionTimer;
        private readonly Timer _testTimer;

        public Mtc(MtcSettings mtcSettings, IQuestionListBuilder questionListBuilder)
        {
            mtcSettings.Should().NotBeNull();
            questionListBuilder.Should().NotBeNull();

            Settings = mtcSettings;
            _questions = questionListBuilder.BuildQuestions();
            _stats = new MtcStats(_questions);

            _questionTimer = Settings.SecondsPerQuestions > 0 
                ? new Timer(QuestionTimeElapsedHandler, this, -1, Settings.SecondsPerQuestions * 1000) 
                : null;

            _testTimer = Settings.SecondsPerTest> 0
                ? new Timer(TestTimeElapsedHandler, this, -1, Settings.SecondsPerTest * 1000)
                : null;
        }

        private void QuestionTimeElapsedHandler(object state)
        {
            OnQuestionTimeElapsed?.Invoke(state, null);
        }

        private void TestTimeElapsedHandler(object state)
        {
            OnTestTimeElapsed?.Invoke(state, null);
        }

        public void RegisterAnswer(byte questionId, byte proposedAnswer, out bool status)
        {
            questionId.Should().BeInRange(1, (byte) _questions.Count);

            _stats.RegisterAnswer(questionId, proposedAnswer, out status);
        }

        public MtcStats GetStatistics()
        {
            return _stats;
        }

        public event EventHandler OnQuestionTimeElapsed;
        public event EventHandler OnTestTimeElapsed;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<MtcQuestion> IEnumerable<MtcQuestion>.GetEnumerator()
        {
            return GetEnumerator();
        }

        private MtcEnumerator GetEnumerator()
        {
            return new MtcEnumerator(_questions);
        }
    }
}
