using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Chathub;
using SocialMedia.Dbcontext;

namespace SocialMedia
{
    public class Startup
    {
        //同源政策名稱
        public readonly string MyCorsPolicy = "MyCorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //註冊json方法
            services.AddControllers().AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
            });


            //註冊資料庫
            services.AddDbContext<MemberContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //signalR
            services.AddSignalR();

            services.AddMvc();

            //允許同源
            services.AddCors(options =>
            {
                //自訂義同源政策
                options.AddPolicy(MyCorsPolicy, policy =>
                {
                    policy.WithOrigins("允許的網域")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //使用同源服務
            app.UseCors(MyCorsPolicy);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //2.x 版本
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");

            //});

            //配置SignalR
            //app.UseSignalR(routes =>
            //{
            //    //客戶端只要用 連上這個路由就可以連接signalr server
            //    routes.MapHub<ChatHub>("/chathub");
            //});
            //3.1 後可以用endpoint
            app.UseRouting();
            app.UseEndpoints(endpoints=> 
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapHub<ChatHub>("/chathub");
            });

        }
    }
}
