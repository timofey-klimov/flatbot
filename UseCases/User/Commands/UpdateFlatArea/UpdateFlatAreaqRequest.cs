using UseCases.User.Base;

namespace UseCases.User.Commands.UpdateFlatArea
{
    public class UpdateFlatAreaqRequest : BaseUserRequest
    {
        public int FlatArea { get; }
        public UpdateFlatAreaqRequest(long chatId, int flatArea) 
            : base(chatId)
        {
            FlatArea = flatArea;
        }
    }
}
