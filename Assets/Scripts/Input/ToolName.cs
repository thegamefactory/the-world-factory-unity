using System;
using System.Collections.Generic;

namespace TWF.Input
{
    /// <summary>
    /// An identifier for a user action that can be triggered by a key combination.
    /// </summary>
    public class ToolName : IEquatable<ToolName>
    {
        public ToolName(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as ToolName);
        }

        public bool Equals(ToolName other)
        {
            return other != null &&
                   Name == other.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public static bool operator ==(ToolName name1, ToolName name2)
        {
            return EqualityComparer<ToolName>.Default.Equals(name1, name2);
        }

        public static bool operator !=(ToolName name1, ToolName name2)
        {
            return !(name1 == name2);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
