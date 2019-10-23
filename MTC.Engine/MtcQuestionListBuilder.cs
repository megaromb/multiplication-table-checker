using System;
using System.Collections.Generic;
using System.ComponentModel;
using FluentAssertions;

namespace MTC.Engine
{
    public class MtcQuestionListBuilder : IQuestionListBuilder
    {
        public MtcQuestionListBuilder(MtcSettings settings)
        {
            settings.Should().NotBeNull();

            Settings = settings;
        }

        public MtcSettings Settings { get; }

        public IReadOnlyCollection<MtcQuestion> BuildQuestions()
        {
            var result = new List<MtcQuestion>(Settings.NumOfQuestions);
            var rnd = new Random();

            for (int i = 0; i < Settings.NumOfQuestions; ++i)
            {
                switch (Settings.Operation)
                {
                    case MtcOperation.Multiplication:
                        
                        var question = new MtcQuestion(
                            Settings.RangeFrom,
                            (byte)rnd.Next(Settings.IncludeMultByOne ? 1 : Settings.RangeFrom, Settings.RangeTo + 1),
                            Settings.Operation, 
                            (byte)(i + 1));

                        result.Add(question);

                        break;
                    case MtcOperation.Addition:
                    case MtcOperation.Division:
                    case MtcOperation.Subtraction:
                        throw new NotImplementedException();
                    default:
                        throw new InvalidEnumArgumentException(
                            nameof(Settings.Operation), (int)Settings.Operation, Settings.Operation.GetType());
                }
            }

            return result.AsReadOnly();
        }
    }
}
