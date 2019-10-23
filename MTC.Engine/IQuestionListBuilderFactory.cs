namespace MTC.Engine
{
    public interface IQuestionListBuilderFactory
    {
        MtcSettings Settings { get; }

        IQuestionListBuilder Create();
    }
}
