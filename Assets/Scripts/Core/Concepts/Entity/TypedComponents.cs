﻿using System.Collections.Generic;
using System;

namespace TWF
{
    /// <summary>
    /// Richly typed components that can be attached to entities.
    /// </summary>
    class TypedComponent<T> : IReadOnlyTypedComponents<T>
    {
        List<T> values = new List<T>();
        Func<T> defaultProvider;

        public TypedComponent(string name, Func<T> defaultProvider)
        {
            Name = name;
            this.defaultProvider = defaultProvider;
        }

        public string Name { get; }

        public void AttachComponent(int entityId, T value)
        {
            while (values.Count <= entityId)
            {
                values.Add(defaultProvider());
            }

            values[entityId] = value;
        }

        public T GetComponent(int entityId)
        {
            if (values.Count <= entityId)
            {
                AttachComponent(entityId, defaultProvider());
            }
            return values[entityId];
        }
    }
}