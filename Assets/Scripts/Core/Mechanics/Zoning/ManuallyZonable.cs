namespace TWF
{
    /// <summary>
    /// A trait for zones which can be manually zoned (e.g by the action of a tool controlled by the player). 
    /// </summary>
    public class ManuallyZonable : ITrait
    {
        private ManuallyZonable()
        {

        }

        public static ManuallyZonable Instance { get; } = new ManuallyZonable();
    }
}
