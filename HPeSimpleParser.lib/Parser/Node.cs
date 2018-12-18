using System;
using System.Collections.Concurrent;

namespace HPeSimpleParser.lib.Parser {
    public class Node : IEquatable<Node> {
        public bool Equals(Node other) {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool Equals(object obj) {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Node)obj);
        }

        public override int GetHashCode() {
            return (Name != null ? StringComparer.InvariantCultureIgnoreCase.GetHashCode(Name) : 0);
        }

        public static bool operator ==(Node left, Node right) {
            return Equals(left, right);
        }

        public static bool operator !=(Node left, Node right) {
            return !Equals(left, right);
        }

        public Node(string name, Node parent) {
            Name = name;
            Parent = parent;
            Children = new ConcurrentDictionary<string, Node>();
        }

        public Node GetOrAddChild(string name, Node current) {
            var item = new Node(name, current);
            var node = Children.GetOrAdd(item.Name, item);
            return node;
        }
        public string Name { get; }
        public Node Parent { get; }

        public ConcurrentDictionary<string, Node> Children { get; }
    }
}