using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Turkcell.Updater.Utility
{
    /// <summary>
    ///     ms-appx///AppSetup --> InstallationFolder
    ///     ms-appdata///Local --> LocalFolder
    ///     ms-appdata///Roaming --> RoamingFolder
    ///     Reference Documentation: http://msdn.microsoft.com/en-us/library/windowsphone/develop/ff402541(v=vs.105).aspx
    /// </summary>
    public static class IsolatedStorageHelper
    {
        public static async Task WriteToFile(StorageFolder folder, string filePath, string content)
        {
            StorageFile storageFile = await folder.CreateFileAsync(filePath, CreationCollisionOption.ReplaceExisting);
            Stream stream = await storageFile.OpenStreamForWriteAsync();

            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                await writer.WriteAsync(content);
            }
        }

        public static async Task WriteToStreamAsync(Stream stream, string content)
        {
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                await writer.WriteAsync(content);
            }
        }

        public static async Task WriteToLocalFileAsync(string path, string content)
        {
            await WriteToFile(ApplicationData.Current.LocalFolder, path, content);
        }

        public static async Task WriteToRoamingFile(string path, string content)
        {
            await WriteToFile(ApplicationData.Current.RoamingFolder, path, content);
        }

        public static async Task<string> ReadFileFromLocalFolder(StorageFolder folder, string filePath)
        {
            string content = String.Empty;
            StorageFile storageFile = await folder.GetFileAsync(filePath);
            // Interestingly, there is no way to check whether a file exist or not without having an exception. 

            if (storageFile != null)
            {
                Stream stream = await storageFile.OpenStreamForReadAsync();
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    content = await reader.ReadToEndAsync();
                }
            }
            return content;
        }

        public static async Task<string> ReadFromStreamAsync(Stream stream)
        {
            string content = String.Empty;
            if (stream != null)
            {
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                {
                    content = await reader.ReadToEndAsync();
                }
            }
            return content;
        }

        /// <summary>
        ///     Reads the contents of the given file using UTF-8 Encoding.
        /// </summary>
        /// <param name="filePath">Relative path to the file from LocalFolder.</param>
        /// <exception cref="FileNotFoundException">Throwed if the given file does not exist.</exception>
        /// <returns>
        ///     Contents of the file as <see cref="string" />.
        /// </returns>
        public static async Task<string> ReadFileFromLocalFolder(string filePath)
        {
            return await ReadFileFromLocalFolder(ApplicationData.Current.LocalFolder, filePath);
        }

        public static async Task<bool> FileExistsUnderLocalFolderAsync(string path)
        {
            try
            {
                await ApplicationData.Current.LocalFolder.GetFileAsync(path);
                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }

        public static async Task<StorageFile> GetLocalFileAsync(string filename)
        {
            return await ApplicationData.Current.LocalFolder.GetFileAsync(filename);
        }

        #region ExtensionMethods

        public static async Task Write(this StorageFile file, string content)
        {
            await WriteToStreamAsync(await file.OpenStreamForWriteAsync(), content);
        }

        public static async Task<string> Read(this StorageFile file)
        {
            return await ReadFromStreamAsync(await file.OpenStreamForReadAsync());
        }

        #endregion
    }
}