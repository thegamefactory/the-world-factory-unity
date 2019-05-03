using UnityEngine;
using System.Collections.Generic;

//TODO: Consider using predicates
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

    public bool Evaluate()
    {
        foreach (KeyCode key in keys)
        {
            if (!Input.GetKey(key)) return false;
        }
        return true;
    }
}
