using AutoMapper;
using NewsRoom.Infrastructure;

namespace NewsRoom.Test.Mocks
{
    public static class MapperMock
    {
        public static IMapper Instance
        {
            get 
            {
                var mapperConfiguration = new MapperConfiguration(config =>
                {
                    config.AddProfile<MappingProfile>();
                });

                return new Mapper(mapperConfiguration);
            }
        }
    }
}
