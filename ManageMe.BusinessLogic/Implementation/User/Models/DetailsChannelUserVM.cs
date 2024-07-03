using Microsoft.AspNetCore.Http;

namespace ManageMe.BusinessLogic
{
    public class DetailsChannelUserVM : UserMinimalInfo
    {
        public int ChannelId { get; set; }
        public bool IsModerator { get; set; }
    }
}
