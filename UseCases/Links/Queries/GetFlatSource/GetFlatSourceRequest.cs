using UseCases.Links.Queries.GetFlatSource.Dto;
using UseCases.User.Base;

namespace UseCases.Links.Queries.GetFlatSource
{
    public class GetFlatSourceRequest : BaseUserRequest<SourceUrlDto>
    {
        public int FlatId { get; }

        public GetFlatSourceRequest(long chatId, int flatId) 
            : base(chatId)
        {
            FlatId = flatId;
        }
    }
}
