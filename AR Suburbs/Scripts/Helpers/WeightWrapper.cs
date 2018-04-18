namespace Assets.Scripts.Helpers
{
    public class WeightWrapper<T>
    {
        public T Item { get; private set; }
        public int Weight { get; private set; }

        public WeightWrapper(T item, int weight)
        {
            Item = item;
            Weight = weight;
        }
    }
}