using System;
using System.IO;
using System.Threading.Tasks;
using com.ih.util.custom.errors.Domain;
using com.ih.util.generic.LogFile.Domain;
using Newtonsoft.Json;

namespace com.ih.util.generic.LogFile.Service
{
    public interface ILogFileService
    {
        Task New(CustomErrorException error);
    }
    
    public class LogFileService : ILogFileService
    {
        private readonly LogFileConfigurationDto _logFileConfiguration;

        public LogFileService(LogFileConfigurationDto logFileConfiguration)
        {
            this._logFileConfiguration = logFileConfiguration;
        }

        public async Task New(CustomErrorException error)
        {
            try
            {
                _ = Task.Run(() =>
                {
                    StreamWriter man;

                    string directoryName = _logFileConfiguration.Path;
                    string fileName = directoryName + _logFileConfiguration.FileNamePrefix + "_" + DateTime.Now.ToString("yyyy-MM-dd") + ".log";

                    CreateFile(directoryName, fileName);

                    man = File.AppendText(fileName);

                    man.WriteLine("-----------------------------------------------------------------------------");
                    man.WriteLine("Date : " + DateTime.Now.ToString(this._logFileConfiguration.DatetimeFormat));
                    man.WriteLine();
                    man.WriteLine("Status:" + error.status);
                    man.WriteLine("Message:" + error.Message);
                    man.WriteLine("StackTrace: " + error.StackTrace);
                    man.WriteLine("ContentBodyRequest: " + error.contentBodyRequest);
                    man.WriteLine("ContentBodyResponse: " + error.contentBodyResponse);
                    man.WriteLine("Details: " + JsonConvert.SerializeObject(error.details));
                    man.WriteLine();

                    man.Close();
                });
            }
            catch { }
        }

        private void CreateFile(string directoryName, string fileName)
        {
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName!);
            }

            if (!File.Exists(fileName))
            {
                var file = File.Create(fileName!);

                file.Close();
            }
        }
    }
}