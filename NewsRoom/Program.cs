using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace NewsRoom
{
    public class Program
    {
        public static void Main(string[] args)
            => CreateWebHostBuilder(args)
              .Build()
              .Run();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) 
             => WebHost
                .CreateDefaultBuilder(args)
                .UseUrls("http://*:80") // listen on port 80
               // .ConfigureWebHostDefaults(webBuilder 
               // => webBuilder
                .UseStartup<Startup>();
                
    }
}
