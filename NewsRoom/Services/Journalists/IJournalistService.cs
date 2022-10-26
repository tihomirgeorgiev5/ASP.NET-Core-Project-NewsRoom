namespace NewsRoom.Services.Journalists
{
    public interface IJournalistService
    {
        public bool IsJournalist(string userId);

        public int GetIdByUser(string userId);
    }
}
