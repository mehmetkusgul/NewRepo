using FileSystemTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dosya_Yonetim
{
    internal class FileManager
    {
        //Başlangıç ve mevzut klasörlerin tanımlanması
        private Folder root;
        private Folder currentDirectory;

        //Root klasörünün oluşturulması ve mevcut klasör olarak belirlenmesi
        public FileManager()
        {
            root = new Folder("Root-Folder");
            currentDirectory = root;
        }

        //Tüm dizini döndürme
        public List<Node> ListDirectory()
        {
            return currentDirectory.GetChildren();
        }

        //Dizini döndürme
        public Folder GetDirectory()
        {
            return currentDirectory;
        }

        //Dizine klasör ekleme
        public void CreateFolder(string name)
        {
            Folder newFolder = new Folder(name);
            currentDirectory.AddNode(newFolder);
        }

        //Dizine dosya ekleme
        public void CreateFile(string name, string extension, long size)
        {
            FileSystemTree.File newFile = new FileSystemTree.File(name, extension, size);
            currentDirectory.AddNode(newFile);
        }

        //Dizinden dosya veya klasör silme
        public bool Delete(string name)
        {
            return currentDirectory.RemoveNode(name);
        }

        //İsme göre dosya ya klasör arama
        public List<Node> SearchByName(string name)
        {
            List<Node> finding = new List<Node>();
            Search(currentDirectory, name.ToLower(), finding);
            return finding;
        }

        private void Search(Folder folder, string name, List<Node> finding)
        {
            foreach (Node node in folder.GetChildren())
            {
                if (node.Name.ToLower().Contains(name))
                    finding.Add(node);

                if (node is Folder subFolder)
                    Search(subFolder, name, finding);
            }
        }

        //Tüm sistemin klasör yapısı gösterme
        public List<string> GetAllSystemFiles()
        {
            List<string> lines = new List<string>();
            CreateLines(root, 0, lines);
            return lines;
        }

        private void CreateLines(Node node, int level, List<string> lines)
        {
            string spaces = new string(' ', level * 2);
            lines.Add($"{spaces}{node.Name}");

            if (node is Folder folder)
                foreach (Node child in folder.GetChildren())
                    CreateLines(child, level + 1, lines);
        }

        //Dizin değiştirme
        public bool ChangeDirectory(string path)
        {
            if (path == "..")
            {
                if (currentDirectory.Parent != null)
                {
                    currentDirectory = currentDirectory.Parent;
                    return true;
                }
                return false;
            }

            Node? destination;
            if (path.StartsWith("/"))
                destination = root.FindNodeByPath(path);
            else
                destination = currentDirectory.FindNode(path);

            if (destination is Folder folder)
            {
                currentDirectory = folder;
                return true;
            }

            return false;
        }
    }
}
