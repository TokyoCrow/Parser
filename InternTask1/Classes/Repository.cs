using CsvHelper;
using InternTask1.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace InternTask1.Classes
{
    public interface IRepository
    {
        void Save(IEnumerable<Website> sites);
    }

    public class CsvFile : IRepository
    {
        public async void Save(IEnumerable<Website> sites)
        {
            string dateTime = DateTime.Now.ToString("HH.mm.ss|dd.MM.yy");
            var csvFileFullPath = $"{Configuration.CsvFilePath}{Configuration.CsvFileName}({dateTime}).csv";
            using (var streamWriter = new StreamWriter(csvFileFullPath))
            using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
            {
                csvWriter.Configuration.Delimiter = ",";
                await csvWriter.WriteRecordsAsync(sites);
            }
        }
    }
}