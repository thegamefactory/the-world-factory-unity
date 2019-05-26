using System.Collections.Generic;
using System.Linq;

namespace TWF
{
    /// <summary>
    /// A definition of all the tool behaviors.
    /// </summary>
    public class ToolBehaviors
    {
        public static Dictionary<string, IToolBehavior> Zoners(IEnumerable<Zone> allZones)
        {
            var zoners = new List<IToolBehavior>();
            foreach (Zone z in allZones.Where((z) => z.HasTrait(ManuallyZonable.Instance.GetType())))
            {
                zoners.Add(z.Zoner());
            }
            return zoners.ToDictionary((z) => z.Name);
        }
    }
}
