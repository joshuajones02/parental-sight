namespace ParentalSight
{
    using System;

    public interface IGarbageCollector
    {
        void DeleteFiles(string directory, DateTime? minCreationTime = null);

        void Archive(string directory, string outputDirectory);
    }
}