using Android.OS;
using AndroidX.Core.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Uno.UI;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace XplatStorageSpace
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void BtnClick(object sender, RoutedEventArgs args)
        {
#if __ANDROID__
            OutputInternalStorage();
            OutputExternalStorage();
#endif
        }


#if __ANDROID__
        private void OutputInternalStorage()
        {
            var path = Android.OS.Environment.DataDirectory;
            StatFs stat = new StatFs(path.Path);
            long blockSize = stat.BlockSizeLong;
            long availableBlocks = stat.AvailableBlocksLong;

            long totalBlocks = stat.BlockCountLong;
            System.Diagnostics.Debug.WriteLine("Internal free: " + formatSize(availableBlocks * blockSize));
            System.Diagnostics.Debug.WriteLine("Internal total: " + formatSize(totalBlocks * blockSize));
        }

        private void OutputExternalStorage()
        {
            if (!ExternalMemoryAvailable())
            {
                return;
            }

            var path = ContextCompat.GetExternalFilesDirs(ContextHelper.Current, null)[1];
            StatFs stat = new StatFs(path.Path);
            long blockSize = stat.BlockSizeLong;
            long availableBlocks = stat.AvailableBlocksLong;
            long totalBlocks = stat.BlockCountLong;

            System.Diagnostics.Debug.WriteLine("External free: " + formatSize(availableBlocks * blockSize));
            System.Diagnostics.Debug.WriteLine("External total: " + formatSize(totalBlocks * blockSize));
        }

        private static bool ExternalMemoryAvailable()
        {
            return Android.OS.Environment.MediaMounted.Equals(Android.OS.Environment.ExternalStorageState, StringComparison.OrdinalIgnoreCase);
        }

        private static string formatSize(float size)
        {
            string suffix = null;

            if (size >= 1024)
            {
                suffix = "KB";
                size /= 1024;
                

            }

            if (size >= 1024)
            {
                suffix = "MB";
                size /= 1024;
            }

            if (size >= 1024)
            {
                suffix = "GB";
                size /= 1024;
            }

            return size.ToString() + " " + suffix;
        }

#endif
    }
}
