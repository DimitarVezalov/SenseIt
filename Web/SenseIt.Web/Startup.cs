namespace SenseIt.Web
{
    using System;
    using System.Reflection;
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using SenseIt.Data;
    using SenseIt.Data.Common;
    using SenseIt.Data.Common.Repositories;
    using SenseIt.Data.Models;
    using SenseIt.Data.Repositories;
    using SenseIt.Data.Seeding;
    using SenseIt.Services.Data;
    using SenseIt.Services.Data.Admin;
    using SenseIt.Services.Mapping;
    using SenseIt.Services.Messaging;
    using SenseIt.Web.ViewModels;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(this.configuration);

            services.AddHttpContextAccessor();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            Account account = new Account(
                            this.configuration["Cloudinary:my_cloud_name"],
                            this.configuration["Cloudinary:my_api_key"],
                            this.configuration["Cloudinary:my_api_secret"]);

            Cloudinary cloudinary = new Cloudinary(account);

            services.AddSingleton(cloudinary);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            // Admin services
            services.AddTransient<IEmailSender>(x => new SendGridEmailSender(this.configuration["SendGrid:my_api_key"]));
            services.AddTransient<IAdminProductsService, AdminProductsService>();
            services.AddTransient<IAdminAppServicesService, AdminAppServicesService>();
            services.AddTransient<IAdminProductCategoriesService, AdminProductCategoriesService>();
            services.AddTransient<IAdminServiceCategoriesService, AdminServiceCategoriesService>();
            services.AddTransient<IAdminUsersService, AdminUsersService>();

            // User Interface services
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IProductCategoriesService, ProductCategoriesService>();
            services.AddTransient<IAppServiceCategoriesService, AppServiceCategoriesService>();
            services.AddTransient<IAppServicesService, AppServicesService>();
            services.AddTransient<IReviewsService, ReviewsService>();
            services.AddTransient<IAppointmentsService, AppointmentsService>();
            services.AddTransient<ICartItemsService, CartItemsService>();
            services.AddTransient<ICartsService, CartsService>();
            services.AddTransient<IUsersService, UsersService>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
