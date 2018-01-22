namespace _02.SliceFile
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    public class Startup
    {
        public static void Main(string[] args)
        {
            string videoPath = Console.ReadLine();
            string destinationPath = Console.ReadLine();
            int pieces = int.Parse(Console.ReadLine());

            SliceAsync(videoPath, destinationPath, pieces);

            Console.WriteLine("Anything else?");
            while (true)
            {
                Console.ReadLine();
            }
        }

        private static void SliceAsync(string sourcePath, string destinationPath, int parts)
        {
            Task.Run(() =>
            {
                Slice(sourcePath, destinationPath, parts);
            });
        }

        private static void Slice(string sourcePath, string destinationPath, int parts)
        {
            if (Directory.Exists(destinationPath))
            {
                Directory.Delete(destinationPath, true);
            }

            Directory.CreateDirectory(destinationPath);

            using (var source = new FileStream(sourcePath, FileMode.Open))
            {
                FileInfo fileInfo = new FileInfo(sourcePath);

                long partsLength = (source.Length / parts) + 1;
                long currentBytes = 0;

                for (int currentPart = 1; currentPart <= parts; currentPart++)
                {
                    string filePath = string.Format("{0}/Part-{1}{2}",
                        destinationPath, currentPart, fileInfo.Extension);


                    using (var destination = new FileStream(filePath, FileMode.Create))
                    {
                        byte[] buffer = new byte[1024];

                        while (currentBytes <= partsLength * currentPart)
                        {
                            int readBytesCount = source.Read(buffer, 0, buffer.Length);

                            if (readBytesCount == 0)
                            {
                                break;
                            }

                            destination.Write(buffer, 0, readBytesCount);
                            currentBytes += readBytesCount;
                        }
                    }
                }

            }

            Console.WriteLine("Slice complete.");
        }
    }
}
