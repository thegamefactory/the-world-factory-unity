namespace TWF.Input
{
    using System.Collections.Generic;
    using UnityEngine;

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