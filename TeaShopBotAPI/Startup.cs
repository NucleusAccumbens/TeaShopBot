using DATABASE.DataContext;
using DATABASE.Entityes;
using DATABASE.Intarfaces;
using DATABASE.Interfaces;
using DATABASE.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeaShopBLL;
using TeaShopBLL.Interfaces;
using TeaShopBLL.Services;
using TeaShopBotAPI.Models;
using Telegram.Bot;

namespace TeaShopBotAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration;

        // назейр IServiceCollection опедярюбкъер йнккейжхч яепбхянб б опхкнфемхх.
        // я онлныэч лернднб пюяьхпемхъ щрнцн назейрю оюпнхгбндхряъ йнмтхцспюжхъ опхкнфемхъ
        // дкъ хяонкэгнбюмхъ щрху яепбхянб
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            services.AddDbContext<ShopContext>
                (options => options.UseSqlServer
                (_configuration.GetConnectionString("ShopContext"),                          
                 optionBilder => optionBilder.MigrationsAssembly("TeaShopBotAPI")));        // оЕПЕНОПЕДЕКЕМХЕ ОПНЕЙРЮ, Б ЙНР. ЯНГДЮЧРЯЪ ЛХЦПЮЖХХ

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IService<UserDTO>, UserService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TeaShopBotAPI", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TeaShopBotAPI v1"));
            }

            app.UseSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
