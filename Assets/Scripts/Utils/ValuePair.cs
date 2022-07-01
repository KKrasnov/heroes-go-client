namespace Assets.Scripts.Utils
{
    public class ValuePair<T1, T2>
    {
        public T1 First;
        public T2 Second;

        public ValuePair(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }
    }
}