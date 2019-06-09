namespace TWF.Input
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// A wrapper to determine if a certain combination of keys is active or not.
    /// </summary>
    public class KeyCombination : IKeyCombination
    {
        private readonly IEnumerable<KeyCode> keys;

        public KeyCombination(IEnumerable<KeyCode> keys)
        {
            this.keys = new LinkedList<KeyCode>(keys);
        }

        public static KeyCombinationBuilder Builder(KeyCode key)
        {
            return new KeyCombinationBuilder().And(key);
        }

        public bool IsActive()
        {
            return this.keys.All(k => UnityEngine.Input.GetKey(k));
        }

        public override string ToString()
        {
            return this.keys.ToReadableString(10);
        }
    }
}