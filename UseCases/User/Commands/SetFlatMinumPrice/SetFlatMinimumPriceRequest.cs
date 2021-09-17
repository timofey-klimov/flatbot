using UseCases.User.Base;

namespace UseCases.User.Commands.SetFlatMinumPrice
{
    public class SetFlatMinimumPriceRequest : BaseUserRequest
    {
        public decimal MinimumPrice { get; }

        public SetFlatMinimumPriceRequest(long chatId, decimal minimumPrice)
             : base(chatId)
        {
            MinimumPrice = minimumPrice;
        }
    }
}
