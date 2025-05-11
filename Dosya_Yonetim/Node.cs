using System;
using System.Collections.Generic;

namespace FileSystemTree
{
    public abstract class Node
    {
        protected string name;
        protected Folder parent;
        protected long size;
        protected string creationDate;
        protected Dictionary<string, string> attributes;

        public Node(string name, long size)
        {
            this.name = name;
            this.size = size;
            this.creationDate = DateTime.Now.ToString();
            this.parent = null;
            this.attributes = new Dictionary<string, string>();

            attributes["created"] = DateTime.Now.ToString();
            attributes["modified"] = DateTime.Now.ToString();
            attributes["owner"] = Environment.UserName;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Folder Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public long Size
        {
            get { return size; }
            set { size = value; }
        }

        public string CreationDate
        {
            get { return creationDate; }
        }

        public void SetAttribute(string key, string value)
        {
            attributes[key] = value;
            attributes["modified"] = DateTime.Now.ToString();
        }

        public string GetAttribute(string key)
        {
            if (attributes.ContainsKey(key))
                return attributes[key];
            return null;
        }

        public Dictionary<string, string> Attributes
        {
            get { return new Dictionary<string, string>(attributes); }
        }

        public string GetPath()
        {
            if (parent == null)
            {
                return "/" + name;
            }
            else
            {
                return parent.GetPath() + "/" + name;
            }
        }

        public abstract void Display(int level);
    }
}