using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MTC.Engine.UnitTest
{
    [TestClass]
    public class MtcQuestionBuilderTest
    {
        private MtcSettings _settings;
        private IQuestionListBuilder _questionListBuilder;
        private IReadOnlyCollection<MtcQuestion> _questions;

        private void InitTestWithMultOp()
        {
            _settings = new MtcSettings(5, 9, 10, 0, 0, false, MtcOperation.Multiplication, MtcDifficultyLevel.Normal);
            _questionListBuilder = new MtcQuestionListBuilder(_settings);
            _questions = _questionListBuilder.BuildQuestions();
        }

        private void InitTestWithMultOpAndIncludeMultByOneOn()
        {
            _settings = new MtcSettings(5, 9, 100, 0, 0, true, MtcOperation.Multiplication, MtcDifficultyLevel.Normal);
            _questionListBuilder = new MtcQuestionListBuilder(_settings);
            _questions = _questionListBuilder.BuildQuestions();
        }

        [TestMethod]
        public void Should_ReturnCorrectNumberOfQuestions()
        {
            InitTestWithMultOp();

            _questions.Should().NotBeNull();

            _questions.Count
                .Should()
                .Be(_settings.NumOfQuestions);
        }

        [TestMethod]
        public void Should_ReturnAtLeastOne2ndOperandEqualTo1_ForMultOpAndIfIncludeMultByOneIsOn()
        {
            InitTestWithMultOpAndIncludeMultByOneOn();

            _questions
                .Any(q => q.Operand2 == 1)
                .Should()
                .BeTrue();
        }

        [TestMethod]
        public void Should_ReturnCorrect1stOperand_ForMultOp()
        {
            InitTestWithMultOp();

            _questions
                .All(q => q.Operand1 == _settings.RangeFrom)
                .Should()
                .BeTrue();
        }

        [TestMethod]
        public void Should_ReturnValidRangeOf2ndOperands_ForMultOp()
        {
            InitTestWithMultOp();

            _questions
                .All(q => q.Operand2 >= _settings.RangeFrom && q.Operand2 <= _settings.RangeTo)
                .Should()
                .BeTrue();
        }

        [TestMethod]
        public void Should_ReturnDifferent2ndOperands_ForMultOp()
        {
            InitTestWithMultOp();

            _questions
                .Select(q => q.Operand2)
                .Distinct()
                .Count()
                .Should()
                .BeGreaterThan(1);
        }

        [TestMethod]
        public void Should_ReturnCorrectOp_ForMultOp()
        {
            InitTestWithMultOp();

            _questions
                .All(q => q.Operation == MtcOperation.Multiplication)
                .Should()
                .BeTrue();
        }
    }
}
