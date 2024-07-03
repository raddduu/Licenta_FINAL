using Microsoft.AspNetCore.Http;

namespace ManageMe.BusinessLogic
{
    public class ChannelRequesterUserVM : UserMinimalInfo
    {
        public int ChannelId { get; set; }
        public DateTime Date { get; set; }
    }
}
