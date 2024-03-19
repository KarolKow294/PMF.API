using CsvHelper;
using CsvHelper.Configuration;
using PMF.API.Models;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace PMF.API.Services
{
    public class CsvService
    {
        public List<CsvPartDto> GetDataFromCsv(IFormFile file)
        {
            List<CsvPartDto> records = new();
            try
            {
                using (var streamReader = new StreamReader(file.OpenReadStream()))
                {
                    using (var csvReader = new CsvReader(streamReader, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";", Encoding = Encoding.UTF8 }))
                    {
                        records = csvReader.GetRecords<CsvPartDto>().ToList();
                    }
                }
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
            return records;
        }
    }
}
