using Infrastructure.Interfaces.Cian.HttpClient;
using Infrastructure.Interfaces.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UseCases.Flats.Queries
{
    public class GetFlatsHandler : IRequestHandler<GetFlatsRquest, string>
    {
        private IDbContext _dbContext;
        public GetFlatsHandler(
            IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<string> Handle(GetFlatsRquest request, CancellationToken cancellationToken)
        {
            var user = await _dbContext
                .Users
                .Include(x => x.UserContext)
                .FirstOrDefaultAsync(x => x.ChatId == request.ChatId);

            var flats = await _dbContext.Flats
                .Where(x => x.Price <= user.UserContext.MaximumPrice
                        && x.Price >= user.UserContext.MinimumPrice
                        && !user.UserContext.Notifications.Value.Contains(x.CianId))
                .Take(10)
                .ToListAsync();

            var builder = new StringBuilder();

            foreach (var item in flats)
            {
                var pledge = item.Pledge == null ? "Нет" : item.Pledge.ToString();
                var comission = item.Comission == null ? "Нет" : item.Comission.ToString();

                builder.AppendLine("Новая квартира")
                    .AppendLine($"Цена {item.Price}")
                    .AppendLine($"Залог {pledge}")
                    .AppendLine($"Комиссия {comission}%")
                    .AppendLine($"Метро: {item.Metro}")
                    .AppendLine($"Адрес:{item.Address}")
                    .AppendLine($"Этаж: {item.CurrentFloor}")
                    .AppendLine($"Этажей в доме: {item.MaxFloor}")
                    .AppendLine($"Ссылка: {item.CianReference}")
                    .AppendLine(string.Empty);

                user.UserContext.Notifications.Value.Add(item.CianId);
            }

            return builder.ToString();
        }
    }
}
