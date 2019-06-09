using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace TWF.Input
{
    /// <summary>
    /// A wrapper to determine if a certain combination of keys is active or not.
    /// </summary>
    public class KeyCombination : IKeyCombination
    {
        LinkedList<KeyCode> keys;

        private KeyCombination(LinkedList<KeyCode> keys)
        {
            this.keys = keys;
        }

        public class KeyCombinationBuilder
        {
            private LinkedList<KeyCode> keys = new LinkedList<KeyCode>();

            public KeyCombinationBuilder And(KeyCode key)
            {
                keys.AddLast(key);
                return this;
            }

            public KeyCombination Build()
            {
                return new KeyCombination(keys);
            }
        }

        public static KeyCombinationBuilder Builder(KeyCode key)
        {
            return new KeyCombinationBuilder().And(key);
        }

        public bool IsActive()
        {
            return keys.All(k => UnityEngine.Input.GetKey(k));
        }

        public override string ToString()
        {
            return keys.ToReadableString(10);
        }
    }
}