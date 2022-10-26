namespace NewsRoom.Services.News
{
    public class NewsDetailsServiceModel : NewsServiceModel
    
    {
        public string Description { get; init; }

        public int JournalistId { get; init; }

        public string JournalistName { get; init; }

        public string UserId { get; init; }
    }
}
