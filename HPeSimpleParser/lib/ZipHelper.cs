using System.IO.Compression;

namespace HPeSimpleParser.lib {
    internal static class ZipHelper {
        public static void Zip(string directory, string zipFile) {
            FileHelpers.DeleteIfExists(zipFile);
            ZipFile.CreateFromDirectory(directory, zipFile);
        }
    }
}