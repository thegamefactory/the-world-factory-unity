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

        public class KeyCombinationBuilder
        {
            private readonly LinkedList<KeyCode> keys = new LinkedList<KeyCode>();

            public KeyCombinationBuilder And(KeyCode key)
            {
                this.keys.AddLast(key);
                return this;
            }

            public KeyCombination Build()
            {
                return new KeyCombination(this.keys);
            }
        }
    }
}