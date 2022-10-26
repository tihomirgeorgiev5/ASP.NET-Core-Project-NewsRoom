namespace NewsRoom.Services.Journalists
{
    public interface IJournalistService
    {
        public bool IsJournalist(string userId);

        public int IdByUser(string userId);
    }
}
