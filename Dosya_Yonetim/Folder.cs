using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace FileSystemTree
{
    public class Folder : Node
    {
        private List<Node> children;
        private HashSet<string> tags;

        public Folder(string name) : base(name, 0)
        {
            this.children = new List<Node>();
            this.tags = new HashSet<string>();
        }

        public void AddTag(string tag)
        {
            tags.Add(tag.ToLower());
        }

        public bool RemoveTag(string tag)
        {
            return tags.Remove(tag.ToLower());
        }

        public bool HasTag(string tag)
        {
            return tags.Contains(tag.ToLower());
        }

        public HashSet<string> GetTags()
        {
            return new HashSet<string>(tags);
        }

        public void AddNode(Node node)
        {
            foreach (Node existingNode in children)
            {
                if (existingNode.Name.Equals(node.Name))
                {
                    Console.WriteLine($"Error: A file or folder named '{node.Name}' already exists.");
                    return;
                }
            }

            children.Add(node);
            node.Parent = this;
            UpdateSize();
        }

        public bool RemoveNode(string name)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].Name.Equals(name))
                {
                    children.RemoveAt(i);
                    UpdateSize();
                    return true;
                }
            }
            return false;
        }

        public Node FindNode(string name)
        {
            foreach (Node node in children)
            {
                if (node.Name.Equals(name))
                {
                    return node;
                }
            }
            return null;
        }

        public Node FindNodeByPath(string path)
        {
            if (path.Equals("/") || path.Equals(""))
            {
                return this;
            }

            string[] parts = path.Split('/');
            Folder currentFolder = this;

            int startIndex = path.StartsWith("/") ? 1 : 0;

            for (int i = startIndex; i < parts.Length; i++)
            {
                if (string.IsNullOrEmpty(parts[i])) continue;

                Node found = currentFolder.FindNode(parts[i]);
                if (found == null)
                {
                    return null;
                }

                if (i == parts.Length - 1)
                {
                    return found;
                }

                if (found is Folder)
                {
                    currentFolder = (Folder)found;
                }
                else
                {
                    return null;
                }
            }

            return currentFolder;
        }

        private void UpdateSize()
        {
            long total = 0;
            foreach (Node node in children)
            {
                total += node.Size;
            }
            Size = total;

            if (Parent != null)
            {
                Parent.UpdateSize();
            }
        }

        public List<Node> GetChildren()
        {
            return new List<Node>(children);
        }

        public override void Display(int level)
        {
            StringBuilder indent = new StringBuilder();
            for (int i = 0; i < level; i++)
            {
                indent.Append("  ");
            }

            string tagString = tags.Count > 0 ? $" [Tags: {string.Join(", ", tags)}]" : "";
            Console.WriteLine($"{indent} {Name} ({children.Count} items, {Size} bytes){tagString}");

            foreach (Node node in children)
            {
                node.Display(level + 1);
            }
        }
    }
}








