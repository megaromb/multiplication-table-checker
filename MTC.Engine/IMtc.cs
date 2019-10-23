using System;
using System.Collections.Generic;

namespace MTC.Engine
{
    public interface IMtc : IEnumerable<MtcQuestion>
    {
        MtcSettings Settings { get; }

        void RegisterAnswer(byte questionId, byte proposedAnswer, out bool status);
        MtcStats GetStatistics();

        event EventHandler OnQuestionTimeElapsed;
        event EventHandler OnTestTimeElapsed;
    }
}
