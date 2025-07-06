using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MiniDownloadManager
{
    public partial class Form1 : Form
    {
        private FileOption? selectedFile;

        public Form1()
        {
            InitializeComponent();
            progressBar.Style = ProgressBarStyle.Blocks;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            progressBar.Value = 10;
            var files = await FetchFilesAsync();
            progressBar.Value = 40;

            selectedFile = SelectBestFile(files);
            if (selectedFile == null)
            {
                MessageBox.Show("No compatible files found.");
                progressBar.Value = 0;
                return;
            }
            progressBar.Value = 70;

            await DisplayFileAsync(selectedFile);
            progressBar.Value = 100;
        }

        private async Task<List<FileOption>> FetchFilesAsync()
        {
            string url = "https://4qgz7zu7l5um367pzultcpbhmm0thhhg.lambda-url.us-west-2.on.aws/";
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<FileOption>>(response) ?? new List<FileOption>();
            }
        }

        private FileOption? SelectBestFile(List<FileOption> files)
        {
            if (files == null || files.Count == 0)
                return null;

            return files!
                .Where(f => f != null && CheckValidators(f))
                .OrderByDescending(f => f!.Score)
                .FirstOrDefault();
        }

        private bool CheckValidators(FileOption file)
        {
            if (file.Validators == null) return true;

            // RAM check (MB)
            if (file.Validators.Ram.HasValue)
            {
                var availableRam = GetAvailableRAMInMB();
                if (availableRam < (ulong)file.Validators.Ram.Value)
                    return false;
            }

            // OS version check
            if (!string.IsNullOrEmpty(file.Validators.Os))
            {
                Version requiredVersion = new Version(file.Validators.Os);
                Version currentVersion = Environment.OSVersion.Version;

                if (currentVersion < requiredVersion)
                    return false;
            }

            // Disk space check (bytes)
            if (file.Validators.Disk.HasValue)
            {
                var freeSpace = GetFreeDiskSpaceInBytes();
                if (freeSpace < (ulong)file.Validators.Disk.Value)
                    return false;
            }

            return true;
        }

        private static ulong GetFreeDiskSpaceInBytes()
        {
            var drive = Path.GetPathRoot(Environment.SystemDirectory);

            if (string.IsNullOrEmpty(drive))
                return 0;

            var di = new DriveInfo(drive);
            return (ulong)di.AvailableFreeSpace;

        }

        private static ulong GetAvailableRAMInMB()
        {
            var query = new ManagementObjectSearcher("SELECT FreePhysicalMemory FROM Win32_OperatingSystem");
            var ram = query.Get().Cast<ManagementObject>().FirstOrDefault();
            if (ram != null)
            {
                ulong freeKb = (ulong)ram["FreePhysicalMemory"];
                return freeKb / 1024;
            }
            return 0;
        }

        private async Task DisplayFileAsync(FileOption file)
        {
            labelTitle.Text = file.Title;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var imgData = await client.GetByteArrayAsync(file.ImageURL);
                    using (var ms = new MemoryStream(imgData))
                    {
                        pictureBox.Image = Image.FromStream(ms);
                    }
                }
            }
            catch
            {
                pictureBox.Image = null;
            }
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            if (selectedFile == null)
                return;

            btnDownload.Enabled = false;
            string fileName = Path.GetFileName(new Uri(selectedFile.FileURL).LocalPath);

            try
            {
                string filePath = await DownloadFileAsync(selectedFile.FileURL, fileName);
                RunFileAndOpenFolder(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error downloading file: " + ex.Message);
            }
            finally
            {
                btnDownload.Enabled = true;
            }
        }

        private async Task<string> DownloadFileAsync(string fileUrl, string fileName)
        {
            string tempPath = Path.GetTempPath();
            string filePath = Path.Combine(tempPath, fileName);

            if (File.Exists(filePath))
            {
                MessageBox.Show("File already downloaded on this PC.");
                return filePath;
            }

            using (HttpClient client = new HttpClient())
            {
                var data = await client.GetByteArrayAsync(fileUrl);
                await File.WriteAllBytesAsync(filePath, data);
            }
            return filePath;
        }

        private void RunFileAndOpenFolder(string filePath)
        {
            System.Diagnostics.Process.Start(filePath);
            System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{filePath}\"");
        }

        private void labelTitle_Click(object sender, EventArgs e)
        {

        }
    }

   
}