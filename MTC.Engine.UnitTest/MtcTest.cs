using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MTC.Engine.UnitTest
{
    [TestClass]
    public class MtcTest
    {
        private MtcSettings _settings;
        private IQuestionListBuilder _questionListBuilder;
        private Mtc _mtc;

        private void InitTestWithMultOp()
        {
            _settings = new MtcSettings(5, 9, 10, 0, 0, false, MtcOperation.Multiplication, MtcDifficultyLevel.Normal);
            _questionListBuilder = new MtcQuestionListBuilder(_settings);
            _mtc = new Mtc(_settings, _questionListBuilder);
        }

        [TestMethod]
        public void Should_HaveCorrectNumOfQuestions()
        {
            InitTestWithMultOp();

            _mtc.Count().Should().Be(_settings.NumOfQuestions);
        }

        [TestMethod]
        public void Should_HaveCorrectNumOfStatItems()
        {
            InitTestWithMultOp();

            _mtc.GetStatistics().GetStatistics().Count.Should().Be(_settings.NumOfQuestions);
        }

        [TestMethod]
        public void Should_HaveProposedAnswers_IfTheyRegistered()
        {
            InitTestWithMultOp();
            
            var proposedAnswers = _mtc.ToDictionary(q => q.Id, q => (byte)(q.Id * 10));

            foreach (MtcQuestion question in _mtc)
            {
                _mtc.RegisterAnswer(question.Id, proposedAnswers[question.Id], out var _);
            }

            var stats = _mtc.GetStatistics().GetStatistics();

            foreach (var proposedAnswer in proposedAnswers)
            {
                var statItem = stats.Single(i => i.Question.Id == proposedAnswer.Key);

                statItem.ProposedAnswer.Should().Be(proposedAnswer.Value);
            }
        }

        // TODO: check pass/fail for correct/incorrect proposed answers
    }
}
