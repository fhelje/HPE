using System.IO.Compression;

namespace FSSystem.ContentAdapter.HPEAndHPInc {
    internal static class ZipHelper {
        public static void Zip(string directory, string zipFile) {
            FileHelpers.DeleteIfExists(zipFile);
            ZipFile.CreateFromDirectory(directory, zipFile);
        }
    }
}