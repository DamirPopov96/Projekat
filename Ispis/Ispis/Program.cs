using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ispis
{
    class Program
    {
        public class config
        { 
            [Name("id")]
            public int Id { get; set;}

            [Name("name")]
            public string Name { get; set; }
        }

        

        static void Main(string[] args)
        {
            var records = new List<config>
            {
                new config {Id = 1, Name = "Marko"},
                new config {Id = 2, Name = "Ruzic"},
            };

            using (var writer = new StreamWriter("C:\\Users\\Windows 10\\Desktop\\projekat\\ispis\\Ispis\\Seba.csv"))
            using (var csv = new CsvWriter(writer))
            {
                csv.WriteRecords(records);
            }
        }
    }
}
