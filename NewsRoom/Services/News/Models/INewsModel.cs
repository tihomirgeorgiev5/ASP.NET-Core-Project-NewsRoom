using System;

namespace NewsRoom.Services.News.Models
{
    public interface INewsModel
    {
        string Area { get; }

        string Title { get; }

        DateTime Date { get; }
    }
}
