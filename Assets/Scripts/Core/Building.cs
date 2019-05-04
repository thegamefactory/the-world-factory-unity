namespace TWF
{
    public class Building : IEntity
    {
        public int Seed { get; }

        public Building(int seed)
        {
            Seed = seed;
        }
    }
}
