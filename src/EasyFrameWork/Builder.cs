using Easy.Mvc.Plugin;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.Extensions.Configuration;
using Easy.Mvc.ValueProvider;
using Easy.RepositoryPattern;
using Easy.Modules.User.Service;
using Easy.Modules.Role;
using Easy.Modules.DataDictionary;
using Easy.Modules.MutiLanguage;
using Easy.Mvc.Authorize;
using Easy.Encrypt;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Easy.Logging;

namespace Easy
{
    public static class Builder
    {
        public static IConfigurationRoot Configuration { get; set; }
        public static IServiceCollection ServiceCollection { get; set; }
        public static IPluginLoader UseEasyFrameWork(this IServiceCollection services, IConfigurationRoot configuration, IHostingEnvironment hostingEnvironment)
        {
            services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<RazorViewEngineOptions>, PluginRazorViewEngineOptionsSetup>());
            
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, Mvc.Controllers.ServiceBasedControllerActivator>());
            services.TryAddEnumerable(ServiceDescriptor.Transient<IActionDescriptorProvider, ActionDescriptorProvider>());
            services.TryAddSingleton<IPluginLoader, Loader>();


            services.TryAddTransient<IAuthorizer, DefaultAuthorizer>();

            services.TryAddTransient<ICookie, Cookie>();
            services.TryAddTransient<IUserService, UserService>();
            services.TryAddTransient<IRoleService, RoleService>();
            services.TryAddTransient<IUserRoleRelationService, UserRoleRelationService>();
            services.TryAddTransient<IPermissionService, PermissionService>();
            services.TryAddTransient<IDataDictionaryService, DataDictionaryService>();
            services.TryAddTransient<ILanguageService, LanguageService>();
            services.TryAddTransient<IEncryptService, EncryptService>();
            services.AddTransient<IOnModelCreating, EntityFrameWorkModelCreating>();
            services.AddTransient<IPluginLoader, Loader>();            

            ServiceCollection = services;

            Configuration = configuration;
            return new Loader(hostingEnvironment);
        }

        public static IApplicationBuilder UsePluginStaticFile(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<PluginStaticFileMiddleware>();
            return builder;
        }
        public static void UseFileLog(this ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            loggerFactory.AddProvider(new FileLoggerProvider(env));
        }
    }
}