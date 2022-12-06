namespace ParentalSight.GarbageCollection
{
    using System;
    using System.IO;
    using System.Linq;

    public interface IGarbageCollector
    {
        void DeleteFiles(string directory, DateTime? minCreationTime = null);

        void Archive(string directory, string outputDirectory);
    }

    public class GarbageCollector : IGarbageCollector
    {
        public void DeleteFiles(string directory, DateTime? minCreationTime = null)
        {
            foreach (var file in Directory.GetFiles(directory))
            {
                if (minCreationTime == null
                 || minCreationTime.Value < File.GetCreationTime(file))
                {
                    File.Delete(file);
                }
            }

            var internalDirectories = Directory.EnumerateDirectories(directory);

            if (internalDirectories?.Any() == true)
            {
                foreach (var internalDirectory in internalDirectories)
                {
                    DeleteFiles(internalDirectory, minCreationTime);
                }
            }
        }

        public void Archive(string directory, string outputDirectory)
        {
        }
    }
}