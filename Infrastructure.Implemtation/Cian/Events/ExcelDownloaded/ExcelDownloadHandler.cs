using Entities.Models;
using Entities.Models.ValueObjects;
using Infrastructure.Interfaces.Bus;
using Infrastructure.Interfaces.Cian;
using Infrastructure.Interfaces.Cian.Events.ExcelDownloaded;
using Infrastructure.Interfaces.DataAccess;
using Infrastructure.Interfaces.Logger;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utils;

namespace Infrastructure.Implemtation.Cian.Events.ExcelDownloaded
{
    public class ExcelDownloadHandler : IEventBusHandler<ExcelDownloadedEvent>
    {
        private readonly ILoggerService _logger;
        private readonly IDbContext _context;

        public ExcelDownloadHandler(
            IDbContext dbContext,
            ILoggerService loggerService)
        {
            _context = dbContext;
            _logger = loggerService;
        }

        public async Task HandleAsync(ExcelDownloadedEvent @event)
        {
            try
            {
                using (var package = new ExcelPackage(new MemoryStream(@event.Data)))
                {
                    var workSheet = package.Workbook.Worksheets.FirstOrDefault();

                    for (int i = 2; i <= int.MaxValue; i++)
                    {
                        if (workSheet.Cells[i, 1].GetValue<string>().IsEmpty())
                            break;

                        var cianID = workSheet.Cells[i, 1]?.GetValue<long>();
                        var flatCountInfo = workSheet.Cells[i, 2]?.GetValue<string>();
                        var metro = workSheet.Cells[i, 4]?.GetValue<string>();
                        var address = workSheet.Cells[i, 5]?.GetValue<string>();
                        var flatSquare = workSheet.Cells[i, 6]?.GetValue<string>();
                        var floorNumber = workSheet.Cells[i, 7]?.GetValue<string>();
                        var price = workSheet.Cells[i, 9].GetValue<string>();
                        var phones = workSheet.Cells[i, 10].GetValue<string>();
                        var floorHeight = workSheet.Cells[i, 21]?.GetValue<string>();
                        var reference = workSheet.Cells[i, 24]?.GetValue<string>();

                        var metroEntity = new Metro(metro);

                        var addressEntity = new Address(address);

                        var priceEntity = new Price(price);
                        var floors = floorNumber.Split('/', ',');

                        var currentFloor = int.Parse(floors.ElementAtOrDefault(0));
                        var maxFloor = int.Parse(floors.ElementAtOrDefault(1));

                        var roomArea = flatSquare
                            .Split('/')
                            .FirstOrDefault()
                            .ToDouble();

                        var flatCount = flatCountInfo.Split(',').FirstOrDefault();

                        var flat = new Flat(
                            cianId: cianID == null ? default : cianID.Value,
                            roomCount: flatCount == null ? default : int.Parse(flatCount),
                            metro: metroEntity,
                            address: addressEntity,
                            roomArea: roomArea,
                            currentFloor: currentFloor,
                            lastFloor: maxFloor,
                            price: priceEntity,
                            phone: phones,
                            cellingHeight: floorHeight == null ? default : floorHeight.ToDouble(),
                            reference: reference);

                        var entity = await _context.Flats.FirstOrDefaultAsync(x => x.CianId == flat.CianId);

                        if (entity != null)
                        {
                            entity = flat;
                        }
                        else
                        {
                            await _context.Flats.AddAsync(flat);
                            _logger.Info($"Create new Flat CianId {flat.CianId}");
                        }

                        await _context.SaveChangesAsync(default);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{ex.GetType().Name} {ex.Message}");
            }
        }
    }
}
