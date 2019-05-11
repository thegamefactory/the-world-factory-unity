namespace TWF.State.Building
{
    /// <summary>
    /// A building usage is an integer tuple (capacity, occupation) that can be used as building trait
    /// </summary>
    public class Usage
    {
        public int Capacity { get; }
        public int Occupation { get; }

        public Usage(int capacity, int occupation)
        {
            Capacity = capacity;
            Occupation = occupation;
        }

        public Usage(int capacity)
        {
            Capacity = capacity;
            Occupation = 0;
        }
    }
}
