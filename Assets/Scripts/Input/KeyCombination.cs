namespace TWF.Input
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    /// <summary>
    /// A wrapper to determine if a certain combination of keys is active or not.
    /// </summary>
    public partial class KeyCombination : IKeyCombination
    {
        private readonly LinkedList<KeyCode> keys;

        private KeyCombination(LinkedList<KeyCode> keys)
        {
            this.keys = keys;
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