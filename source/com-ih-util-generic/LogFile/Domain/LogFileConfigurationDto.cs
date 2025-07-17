using System;

namespace com.ih.util.generic.LogFile.Domain
{
    [Serializable]
    public class LogFileConfigurationDto
    {
        public string Path { get; set; }
        public string FileNamePrefix { get; set; }
        public string DatetimeFormat { get; set; }
    }
}