using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Projeto.WebAPI.Data;
using Projeto.WebAPI.Services;
using FluentValidation.AspNetCore;
using FluentValidation;
using Projeto.WebAPI.Model;
using Projeto.WebAPI.Validators;
using AutoMapper;

namespace Projeto.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(db => db.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvc().AddFluentValidation().ConfigureApiBehaviorOptions(
                options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                }
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<ICompraService, CompraService>();
            services.AddTransient<IValidator<Produto>, ProdutoValidator>();
            services.AddTransient<IValidator<Solicitacao>, SolicitacaoValidator>();
            var mapperConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
