using DesafioLin.API.Middleware;
using DesafioLin.API.Security;
using DesafioLin.Domain.Interfaces;
using DesafioLin.DomainServices.Interfaces;
using DesafioLin.DomainServices.Services;
using DesafioLin.Infraestructure.Context;
using DesafioLin.Infraestructure.Repository;
using DesafioLin.Infraestructure.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace DesafioLin.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region SEGURANÇA DE AUTENTICAÇÃO VIA TOKEN

            // Configurando a dependência para a classe de validação
            // de credenciais e geração de tokens
            services.AddScoped<AccessManager>();

            // Autenticação via Token, usando Bearer.
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations
            {
                Seconds = 86400,
                Audience = "localhost",
                Issuer = "Bruno Conti"
            };

            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            // Aciona a extensão que irá configurar o uso de
            // autenticação e autorização via tokens
            services.AddJwtSecurity(signingConfigurations, tokenConfigurations);

            IdentityModelEventSource.ShowPII = true; // Usado para mostrar as propriedades do Token na pilha de erros. Quando se tratar de um erro com o token.

            #endregion SEGURANÇA DE AUTENTICAÇÃO VIA TOKEN

            services.AddMvc(options => options.EnableEndpointRouting = false)
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)

                // Essa opção remove todas as propriedades do Json que forem nulas.
                // Ou seja, nos retornos das API's, caso alguma propriedade seja nula, a mesma não será enviada.
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            // CORS (Cross-Origin Resource Sharing) é uma especificação do W3C que,
            // quando implementado pelo navegador, permite que um site acesse recursos de outro site mesmo estando em domínios diferentes.
            // https://medium.com/@alexandremjacques/entendendo-o-cors-parte-8331d0a777e1
            services.AddCors();

            #region MIDDLEWARES PRÓPRIOS

            services.AddTransient<ErrorHandlerMiddleware>();

            #endregion MIDDLEWARES PRÓPRIOS

            // Contexto para se conectar à base TenantAdmin.
            services.AddDbContext<DesafioLinContext>();

            // Adiciona Repositórios
            services.AddScoped<LoginRepository>();

            // Configura a injeção de dependência do projeto.
            // Por exemplo, essa linha abaixo diz que:
            // Sempre que alguém quiser usar IUserRepository, o sistema vai injetar a classe UserRepository.
            services.AddScoped<IRepository<IEntityBase>, RepositoryEF<IEntityBase>>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IServiceBase, ServiceBase>();
            services.AddScoped<IUserService, UserService>();

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "DesafioLin.API", Version = "v1" }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == Microsoft.Extensions.Hosting.Environments.Development)
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //Realiza as Migrations do Ef Core.
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DesafioLinContext>();
                context.Database.Migrate();
            }

            app.UseCors(config =>
            {
                config.AllowAnyOrigin();
                config.AllowAnyMethod();
                config.AllowAnyHeader();
                config.WithExposedHeaders("Paging-Headers");
            });

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseMvc();

            //Ativa o Swagger.
            app.UseSwagger();

            // Ativa o Swagger UI
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "DesafioLinApi V1");
                // Faz com que a página principal da aplicação seja a do Swagger.
                opt.RoutePrefix = string.Empty;
            });
        }
    }
}