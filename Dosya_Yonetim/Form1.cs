using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using FST = FileSystemTree;

namespace Dosya_Yonetim
{
    public partial class Form1 : Form
    {
        private FST.FileSystem fs;
        private FST.Folder currentFolder;
        private readonly string savePath = "fileSystem.json";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!LoadFileSystem())
            {
                fs = new FST.FileSystem();
                currentFolder = fs.GetRoot();
                MessageBox.Show("Yeni dosya sistemi oluşturuldu.");
            }
            UpdateTreeView();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveFileSystem();
        }

        private void SaveFileSystem()
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    PreserveReferencesHandling = PreserveReferencesHandling.All,
                    TypeNameHandling = TypeNameHandling.Auto,
                    Formatting = Formatting.Indented
                };
                string jsonData = JsonConvert.SerializeObject(currentFolder, settings);
                File.WriteAllText(savePath, jsonData);
                MessageBox.Show("Dosya sistemi başarıyla kaydedildi.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kaydetme sırasında hata oluştu: " + ex.Message);
            }
        }

        private bool LoadFileSystem()
        {
            try
            {
                if (File.Exists(savePath))
                {
                    var settings = new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.All,
                        TypeNameHandling = TypeNameHandling.Auto
                    };
                    string jsonData = File.ReadAllText(savePath);
                    FST.Folder loadedFolder = JsonConvert.DeserializeObject<FST.Folder>(jsonData, settings);
                    if (loadedFolder != null)
                    {
                        fs = new FST.FileSystem(loadedFolder);
                        currentFolder = fs.GetRoot();
                        FixParentReferences(currentFolder, null);
                        MessageBox.Show("Dosya sistemi başarıyla yüklendi.");
                        UpdateTreeView();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yükleme sırasında hata oluştu: " + ex.Message);
            }
            return false;
        }

        private void FixParentReferences(FST.Node node, FST.Folder parent)
        {
            if (node is FST.Folder folder)
            {
                foreach (var child in folder.GetChildren())
                {
                    child.Parent = folder;
                    FixParentReferences(child, folder);
                }
            }
            node.Parent = parent;
        }


        private void UpdateTreeView()
        {
            treeView1.Nodes.Clear();
            DisplayNode(fs.GetRoot(), null);
        }

        private void DisplayNode(FST.Node node, TreeNode treeNode)
        {
            string nodeName = node.Name;
            if (node is FST.File && !nodeName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
            {
                nodeName += ".txt";
            }
            TreeNode newNode = new TreeNode(nodeName) { Tag = node };
            if (treeNode == null)
                treeView1.Nodes.Add(newNode);
            else
                treeNode.Nodes.Add(newNode);

            if (node is FST.Folder folder)
            {
                foreach (var child in folder.GetChildren())
                {
                    DisplayNode(child, newNode);
                }
            }
        }

        private FST.Folder GetSelectedFolder()
        {
            if (treeView1.SelectedNode != null)
            {
                var selectedNode = treeView1.SelectedNode;
                if (selectedNode.Tag is FST.Folder folder)
                    return folder;
                MessageBox.Show("Lütfen bir klasör seçiniz.");
            }
            return fs.GetRoot();
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            string folderName = txtName.Text;
            var selectedFolder = GetSelectedFolder();
            if (selectedFolder is FST.File)
            {
                MessageBox.Show("Txt dosyasının altında klasör oluşturulamaz!");
                return;
            }
            if (!string.IsNullOrWhiteSpace(folderName))
            {
                FST.Folder newFolder = new FST.Folder(folderName);
                selectedFolder.AddNode(newFolder);
                MessageBox.Show($"Klasör '{folderName}' başarıyla eklendi!");
                UpdateTreeView();
            }
            else
            {
                MessageBox.Show("Klasör adı boş olamaz!");
            }
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            string fileName = txtName.Text;
            var selectedFolder = GetSelectedFolder();
            if (selectedFolder is FST.File)
            {
                MessageBox.Show("Txt dosyasının altında dosya oluşturulamaz!");
                return;
            }
            if (!fileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
            {
                fileName += ".txt";
            }
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                FST.File newFile = new FST.File(fileName, "txt", 1024);
                selectedFolder.AddNode(newFile);
                MessageBox.Show($"Dosya '{fileName}' başarıyla eklendi!");
                UpdateTreeView();
            }
            else
            {
                MessageBox.Show("Dosya adı boş olamaz!");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                var selectedNode = treeView1.SelectedNode;
                var parentNode = selectedNode.Parent;

                if (parentNode != null && parentNode.Tag is FST.Folder parentFolder)
                {
                    if (parentFolder.RemoveNode(selectedNode.Text))
                    {
                        MessageBox.Show($"'{selectedNode.Text}' başarıyla silindi!");
                        UpdateTreeView();
                    }
                    else
                    {
                        MessageBox.Show("Silme işlemi başarısız.");
                    }
                }
                else if (selectedNode.Tag is FST.Folder rootFolder)
                {
                    if (fs.GetRoot().RemoveNode(selectedNode.Text))
                    {
                        MessageBox.Show($"Kök klasör '{selectedNode.Text}' başarıyla silindi!");
                        UpdateTreeView();
                    }
                    else
                    {
                        MessageBox.Show("Kök klasör silinemedi.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek için bir dosya veya klasör seçin!");
            }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            string itemName = txtName.Text;
            if (!string.IsNullOrWhiteSpace(itemName))
            {
                FST.Node foundNode = SearchNode(fs.GetRoot(), itemName);
                if (foundNode != null)
                {
                    string fullPath = GetFullPath(foundNode);
                    MessageBox.Show($"Bulundu: {foundNode.Name} - Yol: {fullPath}");
                }
                else
                {
                    MessageBox.Show("Aradığınız dosya/klasör bulunamadı.");
                }
            }
            else
            {
                MessageBox.Show("Arama yapmak için bir isim giriniz!");
            }
        }

        private string GetFullPath(FST.Node node)
        {
            if (node == null)
                return string.Empty;
            List<string> pathElements = new List<string>();
            FST.Node currentNode = node;
            while (currentNode != null)
            {
                pathElements.Insert(0, currentNode.Name);
                currentNode = currentNode.Parent;
            }
            return string.Join("/", pathElements);
        }

        private FST.Node SearchNode(FST.Node node, string itemName)
        {
            if (node.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
                return node;

            if (node is FST.Folder folder)
            {
                foreach (var child in folder.GetChildren())
                {
                    FST.Node result = SearchNode(child, itemName);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }


        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            string itemName = txtName.Text;
            FST.Node foundNode = SearchNode(fs.GetRoot(), itemName);
            if (foundNode != null)
            {
                string details = $"Ad: {foundNode.Name}\nBoyut: {foundNode.Size} bytes\nOluşturulma Tarihi: {foundNode.CreationDate}\nYol: {GetFullPath(foundNode)}";
                MessageBox.Show(details, "Detaylar");
            }
            else
            {
                MessageBox.Show("Detay gösterilemedi. Dosya/Klasör bulunamadı.");
            }
        }
    }
}
