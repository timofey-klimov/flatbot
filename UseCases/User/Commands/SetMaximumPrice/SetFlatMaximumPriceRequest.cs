using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.User.Base;

namespace UseCases.User.Commands.SetMaximumPrice
{
    public class SetFlatMaximumPriceRequest : BaseUserRequest
    {
        public decimal MaximumPrice { get; }
        public SetFlatMaximumPriceRequest(long chatId, decimal price)
            : base(chatId)
        {
            MaximumPrice = price;
        }
    }
}
