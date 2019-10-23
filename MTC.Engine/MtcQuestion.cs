namespace MTC.Engine
{
    public class MtcQuestion
    {
        public readonly byte Id;
        public readonly byte Operand1;
        public readonly byte Operand2;
        public readonly MtcOperation Operation;

        public MtcQuestion(byte operand1, byte operand2, MtcOperation operation, byte id)
        {
            Id = id;
            Operand1 = operand1;
            Operand2 = operand2;
            Operation = operation;
        }
    }
}
