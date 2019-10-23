using System.Collections.Generic;

namespace MTC.Engine
{
    public interface IQuestionListBuilder
    {
        MtcSettings Settings { get; }

        IReadOnlyCollection<MtcQuestion> BuildQuestions();
    }
}
