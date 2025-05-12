using System;

namespace FileSystemTree
{
    public class FileSystem
    {
        private Folder root;

        public FileSystem()
        {
            // Kök klasörü oluştur
            root = new Folder("Root");
        }
        public FileSystem(Folder rootFolder)
        {
            root = rootFolder;
        }

        public Folder GetRoot()
        {
            return root;
        }

        // Klasör oluşturma fonksiyonu
        public void CreateFolder(string name)
        {
            Folder newFolder = new Folder(name);
            root.AddNode(newFolder);
            Console.WriteLine($"Klasör '{name}' oluşturuldu.");
        }

        // Dosya oluşturma fonksiyonu
        public void CreateFile(string name, string extension, long size)
        {
            File newFile = new File(name, extension, size);
            root.AddNode(newFile);
            Console.WriteLine($"Dosya '{name}.{extension}' oluşturuldu.");
        }

        // Ağacı görüntüleme
        public void DisplayTree()
        {
            root.Display(0);
        }

        public void SetRoot(Folder rootFolder)
        {
            root = rootFolder;
        }

    }
}
