using System;
using System.Collections.Generic;
using System.Text;

namespace FileSystemTree
{
    public class File : Node
    {
        private string extension;
        private Queue<string> versionHistory;


    public File(string name, string extension, long size) : base(name + "." + extension, size)
        {
            this.extension = extension;
            this.versionHistory = new Queue<string>();
            AddVersionHistory("Created file");
        }

        public string Extension
        {
            get { return extension; }
        }

        public void AddVersionHistory(string change)
        {
            string entry = $"{DateTime.Now}: {change}";
            versionHistory.Enqueue(entry);

            if (versionHistory.Count > 10)
                versionHistory.Dequeue();
        }

        public Queue<string> GetVersionHistory()
        {
            return new Queue<string>(versionHistory);
        }

        public void Update(long newSize)
        {
            Size = newSize;
            AddVersionHistory($"Updated file. New size: {newSize} bytes");
            attributes["modified"] = DateTime.Now.ToString();
        }

        public override void Display(int level)
        {
            StringBuilder indent = new StringBuilder();
            for (int i = 0; i < level; i++)
            {
                indent.Append("  ");
            }
            Console.WriteLine($"{indent} {Name} ({Size} bytes)");
        }
    }


}
