using Infrastructure.Implemtation.Cian;
using NUnit.Framework;
using OfficeOpenXml;
using System.IO;
using System.Threading.Tasks;

namespace Tests.Cian
{
    public class CianExcelParserTest
    {
        private CianExcelParser _parser;
        [SetUp]
        public void Setup()
        {
            _parser = new CianExcelParser(default,default);
        }

        [Test]
        public async Task Parse()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            var pathToFile = @"C:\Users\timof\OneDrive\Рабочий стол\offers.xlsx";

            using (var fileStream = new FileStream(pathToFile, FileMode.Open))
            {
                var bytes = new byte[fileStream.Length];

                await fileStream.ReadAsync(bytes, 0, bytes.Length);
                await _parser.ParseAsync(bytes);
            }
        }
    }
}
