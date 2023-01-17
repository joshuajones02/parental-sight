namespace ParentalSight.Common.Builders
{
    using System;

    public class FilenameBuilder
    {
        private readonly string _dateTime;

        public FilenameBuilder() : this(DateTime.Now)
        {
        }

        public FilenameBuilder(DateTime dateTime)
        {
            _dateTime = dateTime.ToString("yyyyMMddHHmmss");
        }

        private string _prefix;
        public FilenameBuilder WithPrefix(string prefix)
        {
            _prefix = prefix;

            return this;
        }

        private string _fileExtension;
        public FilenameBuilder WithExtension(string fileExtension)
        {
            _fileExtension = fileExtension.StartsWith('.') ?
                fileExtension : string.Concat('.', fileExtension);

            return this;
        }

        public string Build() =>
            $"{_prefix}{_dateTime}{_fileExtension}";
    }
}