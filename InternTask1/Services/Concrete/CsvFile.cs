using CsvHelper;
using InternTask1.Models;
using InternTask1.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace InternTask1.Services.Concrete
{
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
                try
                {
                    await csvWriter.WriteRecordsAsync(sites);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}