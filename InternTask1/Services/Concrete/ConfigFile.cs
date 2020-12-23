using InternTask1.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternTask1.Services.Concrete
{
    public class ConfigFile : IConfiguration
    {
        public void SetCsvFileName(string name)
        {
            Configuration.CsvFileName = name;
        }

        public void SetCsvFilePath(string path)
        {
            Configuration.CsvFilePath = path;
        }

        public void SetExpEmail(string mail)
        {
            Configuration.ExpEmail = mail;
        }

        public void SetNestingDegree(byte degree)
        {
            Configuration.NestingDegree = degree;
        }
    }
}
