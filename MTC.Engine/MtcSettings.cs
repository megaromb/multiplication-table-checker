using FluentAssertions;

namespace MTC.Engine
{
    public enum MtcOperation : byte
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    public enum MtcDifficultyLevel : byte
    {
        Easy,
        Normal,
        Hard
    }

    public class MtcSettings
    {
        public static readonly MtcSettings Default = 
            new MtcSettings(2, 9, 10, 0, 0, false, MtcOperation.Multiplication, MtcDifficultyLevel.Normal);
        
        /// <summary>
        /// including
        /// </summary>
        public byte RangeFrom { get; private set; }

        /// <summary>
        /// including
        /// </summary>
        public byte RangeTo { get; private set; }

        public MtcOperation Operation { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public byte NumOfQuestions { get; private set; }

        /// <summary>
        /// 0 for infinite
        /// </summary>
        public byte SecondsPerQuestions { get; private set; }

        public MtcDifficultyLevel DifficultyLevel { get; private set; }

        /// <summary>
        /// 0 for infinite
        /// </summary>
        public byte SecondsPerTest { get; private set; }

        public bool IncludeMultByOne { get; set; }

        public MtcSettings(
            byte rangeFrom, byte rangeTo, byte numOfQuestions, 
            byte secondsPerQuestions, byte secondsPerTest, bool includeMultByOne, 
            MtcOperation operation, MtcDifficultyLevel difficultyLevel)
        {
            rangeFrom.Should().BePositive();
            rangeTo.Should().BePositive();
            numOfQuestions.Should().BePositive();

            rangeTo.Should().BeGreaterOrEqualTo(rangeTo);

            RangeFrom = rangeFrom;
            RangeTo = rangeTo;
            NumOfQuestions = numOfQuestions;
            SecondsPerQuestions = secondsPerQuestions;
            SecondsPerTest = secondsPerTest;
            IncludeMultByOne = includeMultByOne;
            Operation = operation;
            DifficultyLevel = difficultyLevel;
        }
    }
}
