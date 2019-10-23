using System.ComponentModel;
using FluentAssertions;

namespace MTC.Engine
{
    public class MtcStatsItem
    {
        public readonly MtcQuestion Question;
        public readonly byte CorrectAnswer;
        public byte? ProposedAnswer { get; internal set; }
        public bool? IsInTime { get; private set; }

        public MtcStatsItem(MtcQuestion question, byte? proposedAnswer, bool? isInTime)
        {
            question.Should().NotBeNull();

            Question = question;
            ProposedAnswer = proposedAnswer;
            CorrectAnswer = ClalculateCorrectAnswer();
            IsInTime = isInTime;
        }

        public bool IsPassed() => (!IsInTime.HasValue || IsInTime.Value) && 
                                  ProposedAnswer.HasValue && ProposedAnswer.Value == CorrectAnswer;

        public void MarkAsDelayed() => IsInTime = false;

        private byte ClalculateCorrectAnswer()
        {
            int result;

            switch (Question.Operation)
            {
                case MtcOperation.Addition:
                    result = Question.Operand1 + Question.Operand2;
                    break;
                case MtcOperation.Division:
                    result = Question.Operand1 / Question.Operand2;
                    break;
                case MtcOperation.Multiplication:
                    result = Question.Operand1 * Question.Operand2;
                    break;
                case MtcOperation.Subtraction:
                    result = Question.Operand1 - Question.Operand2;
                    break;
                default:
                    throw new InvalidEnumArgumentException(
                        nameof(Question.Operation), (int) Question.Operation, Question.Operation.GetType());
            }

            return (byte) result;
        }
    }
}
