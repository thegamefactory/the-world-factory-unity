using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace TWF.Input
{
    /// <summary>
    /// A wrapper to determine if a certain combination of keys is active or not.
    /// </summary>
    public class KeyCombination
    {
        LinkedList<KeyCode> keys;

        private KeyCombination(LinkedList<KeyCode> keys)
        {
            this.keys = keys;
        }

        public class Builder
        {
            LinkedList<KeyCode> keys = new LinkedList<KeyCode>();

            public Builder And(KeyCode key)
            {
                keys.AddLast(key);
                return this;
            }

            public KeyCombination build()
            {
                return new KeyCombination(keys);
            }
        }

        public static Builder builder(KeyCode key)
        {
            return new Builder().And(key);
        }

        public bool IsActive()
        {
            return keys.All(k => UnityEngine.Input.GetKey(k));
        }
    }
}