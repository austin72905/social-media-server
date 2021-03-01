using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;
using SocialMedia.Common;
using SocialMedia.Dbcontext;
using SocialMedia.Interface;
using SocialMedia.Interface.Repository;
using SocialMedia.Service;
using SocialMedia.Service.Repository;
using System;

namespace SocialMedia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();
            var host = CreateWebHostBuilder(args).Build();
            //新增資料庫
            CreateDb(host);
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                //-----
                //註冊方法
                services
                .AddScoped<IRegister, Register>()
                .AddScoped<ILogin, Login>()
                .AddScoped<IMemberinfo, MemberInfo>()
                .AddScoped<IDirectory, Service.Directory>()
                .AddScoped<IPersonal, Service.Personal>()
                .AddScoped<IProfile, Service.Profile>()
                .AddScoped<IMessage, Service.Message>()
                .AddScoped<IChat, Service.Message>()
                //----- 
                //.AddSingleton<IChat, Service.Message>()
                // 資料庫實作
                .AddScoped<IInterestRepository, InterestRepository>()
                .AddScoped<IPersonalRepository, PersonalRepository>()
                .AddScoped<ILogMan,LogMan>()
                .AddScoped<IErrorHandler,ErrorHandler>()
                )

                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders(); //會移除預設的DebugProvider 、 ConsoleProvider
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                .UseNLog(); //setup NLog for Dependercy injection



        private static void CreateDb(IWebHost host)
        {
            //Scope 物件的存留期 會跟Request  存留期 一致(指接到瀏覽器請求到回傳結果前的執行期間)
            //結束時會自動呼叫 Dispose 方法 自動關閉資料庫連線
            using (var scope = host.Services.CreateScope())
            {
                //DI容器最终体现为一个IServiceProvider接口，我们将所有实现了该接口的类型及其实例统称为ServiceProvider
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<MemberContext>();
                    Dbinit.Initialize(context);
                }
                catch (Exception ex)
                {
                    //發生錯誤時打印
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

    }
}
