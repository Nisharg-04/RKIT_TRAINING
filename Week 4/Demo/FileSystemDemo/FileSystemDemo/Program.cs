namespace FileSystemDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "example.txt";

            //FileProperties.ShowFileProperties(path);
            //FileMethods.ShowFileMethods(path);

            //FileInfoProperties.ShowFileInfoProperties(path);
            //FileInfoMethods.ShowFileInfoMethods(path);

            StreamReaderProperties.ShowStreamReaderProperties(path);
            StreamReaderMethods.ShowStreamReaderMethods(path);

            path = @"C:\Temp";  
            //DirectoryProperties.ShowDirectoryProperties(path);
            //DirectoryMethods.ShowDirectoryMethods(path);

            //DirectoryInfoProperties.ShowDirectoryInfoProperties(path);
            //DirectoryInfoMethods.ShowDirectoryInfoMethods(path);
        }
    }
}
