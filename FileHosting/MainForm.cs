using FileHosting.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileHosting.ServiceReference1;

namespace FileHosting
{
    public partial class MainForm : Form
    {
        public User currentUser;
        public ServiceReference1.File currFile;
        ServiceReference1.FileHostingServiceClient client = new ServiceReference1.FileHostingServiceClient();
        public bool addNewFile;
        byte[] buff;
        int indxSelectFile = -1;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            LogonForm lf = new LogonForm();
            lf.Owner = this;
            lf.Show();
        }

        private void btnAddNewFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                this.textBoxDescription.Text = string.Empty;
                this.labelDateAdd.Text = string.Empty;
                currFile = new ServiceReference1.File();
                addNewFile = true;
                this.labelFileName.Text = System.IO.Path.GetFileName(ofd.FileName);
                currFile.FileName = this.labelFileName.Text;

                buff = System.IO.File.ReadAllBytes(ofd.FileName);

                this.labelSize.Text = FileSizeFormat(buff.Length);
                currFile.Size = buff.Length;
            }
        }

        private void listViewAllFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewAllFiles.SelectedIndices == null
                ||
                listViewAllFiles.SelectedIndices.Count == 0)
                return;

            indxSelectFile = listViewAllFiles.SelectedIndices[0];
            ServiceReference1.File selectFile = currentUser.Files[indxSelectFile];
            this.textBoxDescription.Text = selectFile.Description;
            this.labelDateAdd.Text = selectFile.DateAdd.ToString();
            this.labelFileName.Text = selectFile.FileName;
            this.labelSize.Text = FileSizeFormat(selectFile.Size);

            currFile = selectFile;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (currentUser != null && currentUser.Files != null && currentUser.Files.Count() != 0)
            {
                ListViewItem listViewItem;
                foreach (FileHosting.ServiceReference1.File f in currentUser.Files)
                {
                    listViewItem = new ListViewItem(f.FileName);
                    this.listViewAllFiles.Items.Add(listViewItem);
                }
            }
        }

        private async void btnSaveChanges_Click(object sender, EventArgs e)
        {
            if (currFile == null)
                return;

            currFile.Description = this.textBoxDescription.Text;
            currFile.UserId = currentUser.UserId;
            if (addNewFile)
            {
                currFile.DateAdd = DateTime.Now;
                currFile.Bytes = buff;
                ServiceReference1.File newFile = await client.AddNewFileAsync(currFile);

                listViewAllFiles.Items.Add(new ListViewItem(currFile.FileName));

                List<ServiceReference1.File> files = currentUser.Files.ToList();
                files.Add(newFile);
                currentUser.Files = files.ToArray();

                currFile = currentUser.Files[currentUser.Files.Length - 1];

                addNewFile = false;
            }
            else
            {
                await client.UpdateFileInfoAsync(currFile);
            }

            MessageBox.Show("Successfully");
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (currFile == null || addNewFile)
                return;

            await client.DeleteFileAsync(currFile.FileId, this.labelFileName.Text, currentUser.UserId);

            if (indxSelectFile != -1 && indxSelectFile < currentUser.Files.Length)
            {
                List<ServiceReference1.File> files = currentUser.Files.ToList();
                files.RemoveAt(indxSelectFile);
                currentUser.Files = files.ToArray();
                listViewAllFiles.Items.RemoveAt(indxSelectFile);
            }

            this.textBoxDescription.Text = string.Empty;
            this.labelFileName.Text = string.Empty;
            this.labelSize.Text = string.Empty;
            this.labelDateAdd.Text = string.Empty;

            currFile = null;

            MessageBox.Show("File deleted successfully");
        }

        public string FileSizeFormat(long lSize)
        {
            double size = lSize;
            int index = 0;
            for (; size > 1024; index++)
                size /= 1024;
            return size.ToString("0.0 " + new[] { "B", "KB", "MB", "GB", "TB" }[index]);
        }

        private async void btnDownloadFile_Click(object sender, EventArgs e)
        {
            if (currFile == null || addNewFile)
                return;

            byte[] buffer = await client.DownloadFileAsync(this.labelFileName.Text, currentUser.UserId);
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = this.labelFileName.Text;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.WriteAllBytes(sfd.FileName, buffer);
            }

            MessageBox.Show("Download complete");
        }
    }
}