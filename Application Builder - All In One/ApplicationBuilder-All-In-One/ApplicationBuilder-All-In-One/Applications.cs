using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationBuilder_All_In_One
{
    public partial class Applications : Form
    {
        public Applications()
        {
            InitializeComponent();
        }

        Dictionary<string, string> applicationLinks = new Dictionary<string, string>
{
    { "Google Chrome", "https://dl.google.com/chrome/install/ChromeSetup.exe" },
    { "Brave", "https://referrals.brave.com/latest/BraveBrowserSetup.exe" },
    { "Opera", "https://net.geo.opera.com/opera/stable/windows" },
    { "Mozilla FireFox", "https://download.mozilla.org/?product=firefox-latest&os=win&lang=en-US" },
    { "VLC Media Player", "https://get.videolan.org/vlc/last/win64/vlc-3.0.20-win64.exe" },
    { "7-Zip", "https://www.7-zip.org/a/7z2301-x64.exe" },
    { "WinRAR", "https://www.rarlab.com/rar/winrar-x64-623.exe" },
    { "Spotify", "https://download.scdn.co/SpotifySetup.exe" },
    { "Zoom", "https://zoom.us/client/latest/ZoomInstaller.exe" },
    { "Notepad++", "https://github.com/notepad-plus-plus/notepad-plus-plus/releases/latest/download/npp.8.8.3.Installer.x64.exe" },
    { "Adobe Reader", "https://ardownload2.adobe.com/pub/adobe/reader/win/AcrobatDC/2300820413/AcroRdrDC2300820413_en_US.exe" },
    { "Everything", "https://www.voidtools.com/Everything-1.4.1.1024.x64-Setup.exe" },
    { "Steam", "https://cdn.akamai.steamstatic.com/client/installer/SteamSetup.exe" },
    { "ShareX", "https://github.com/ShareX/ShareX/releases/latest/download/ShareX-15.0.0-setup.exe" },
    { "Discord", "https://discord.com/api/download?platform=win" },
    { "CCleaner", "https://download.ccleaner.com/ccsetup611.exe" },
    { "IrfanView", "https://www.fosshub.com/IrfanView.html?dwl=iview462_setup.exe" },
    { "WhatsApp", "https://web.whatsapp.com/desktop/windows/release/x64/WhatsAppSetup.exe" },

    // Development Tools
    { "Visual Studio 2022", "https://aka.ms/vs/17/release/vs_community.exe" },
    { "Visual Studio Code", "https://code.visualstudio.com/sha/download?build=stable&os=win32-x64-user" },
    { "C++ Redistributable 2015-2022", "https://aka.ms/vs/17/release/vc_redist.x64.exe" },
    { "Microsoft .NET Framework 4.8", "https://dotnet.microsoft.com/download/dotnet-framework/net48" },
    { "Microsoft .NET 6.0 Runtime", "https://dotnet.microsoft.com/download/dotnet/6.0/runtime" },
    { "Microsoft .NET 7.0 Runtime", "https://dotnet.microsoft.com/download/dotnet/7.0/runtime" },
    { "Microsoft .NET 8.0 Runtime", "https://dotnet.microsoft.com/download/dotnet/8.0/runtime" },
    { "Git", "https://github.com/git-for-windows/git/releases/latest/download/Git-2.44.0-64-bit.exe" },
    { "GitHub Desktop", "https://central.github.com/deployments/desktop/desktop/latest/win32" },
    { "Node.js", "https://nodejs.org/dist/v20.11.1/node-v20.11.1-x64.msi" },
    { "Python", "https://www.python.org/ftp/python/3.12.2/python-3.12.2-amd64.exe" },
    { "Postman", "https://dl.pstmn.io/download/latest/win64" },
    { "MySQL Workbench", "https://dev.mysql.com/get/Downloads/MySQLGUITools/mysql-workbench-community-8.0.36-winx64.msi" },
    { "Docker Desktop", "https://desktop.docker.com/win/main/amd64/Docker%20Desktop%20Installer.exe" },
    { "XAMPP", "https://www.apachefriends.org/xampp-files/8.2.12/xampp-windows-x64-8.2.12-0-VS16-installer.exe" },
    { "MongoDB Compass", "https://downloads.mongodb.com/compass/mongodb-compass-1.41.4-win32-x64.exe" },
    { "Fiddler", "https://telerik-fiddler.s3.amazonaws.com/fiddler/FiddlerSetup.exe" },
    { "JetBrains Toolbox", "https://download.jetbrains.com/toolbox/jetbrains-toolbox-1.28.1.15213.exe" },
    { "Java JDK", "https://download.oracle.com/java/17/latest/jdk-17_windows-x64_bin.exe" },
    { "Android Studio", "https://redirector.gvt1.com/edgedl/android/studio/install/2023.2.1.17/android-studio-2023.2.1.17-windows.exe" },
    { "Unity Hub", "https://public-cdn.cloud.unity3d.com/hub/prod/UnityHubSetup.exe" },
    { "Visual Studio Installer Projects", "https://marketplace.visualstudio.com/items?itemName=MadsKristensen.VisualStudioInstallerProjects" }
};

        private async void button1_Click(object sender, EventArgs e)
        {
            var selectedApplications = new List<string>();
            foreach (TabPage tab in tabControl1.TabPages)
                foreach (Control ctrl in tab.Controls)
                    if (ctrl is CheckBox cb && cb.Checked)
                        selectedApplications.Add(cb.Text);

            foreach (string app in selectedApplications)
                await DownloadAndInstallAsync(app, applicationLinks[app]);
        }

        private async Task DownloadAndInstallAsync(string appName, string url)
        {
            try
            {
                string folder = Path.Combine(Path.GetTempPath(), "Applications");
                Directory.CreateDirectory(folder);
                string filePath = Path.Combine(folder, $"{appName}.exe");

                using (var client = new WebClient())
                {
                    client.DownloadProgressChanged += (s, e) =>
                    {
                        progressBar1.Maximum = 100;
                        progressBar1.Value = e.ProgressPercentage;
                        lblStatus.Text = $"{appName} downloading... {e.ProgressPercentage}%";
                        Application.DoEvents();
                    };
                    await client.DownloadFileTaskAsync(new Uri(url), filePath);
                }

                lblStatus.Text = $"{appName} starting installation...";
                Application.DoEvents();

                var psi = new ProcessStartInfo
                {
                    FileName = filePath,
                    WorkingDirectory = folder,
                    UseShellExecute = true,
                    Verb = "runas",
                    WindowStyle = ProcessWindowStyle.Normal
                };

                var proc = Process.Start(psi);
                if (proc != null)
                    await Task.Run(() => proc.WaitForExit());
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"{appName} failed: {ex.Message}";
                MessageBox.Show($"Error installing {appName}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Applications_Load(object sender, EventArgs e)
        {
            MessageBox.Show(
                "This application is designed to download and install various applications automatically. Please ensure you have a stable internet connection and sufficient permissions to install software on your system.",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
