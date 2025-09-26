using BL;
using BookStore.Bl;
using BookStore.BL;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

#region EntityFramwork

builder.Services.AddDbContext<BookStoreContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
{
    option.Password.RequiredLength = 8;
    option.Password.RequireNonAlphanumeric = true;
    option.Password.RequireUppercase = true;
    option.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<BookStoreContext>();

#endregion

#region Custom Services
builder.Services.AddScoped<ICategory, ClsCategory>();
builder.Services.AddScoped<IBook, ClsBook>();
builder.Services.AddScoped<ISupplier, ClsSupplier>();
builder.Services.AddScoped<IPublisher, ClsPublisher>();
builder.Services.AddScoped<IAuthor, ClsAuthor>();
builder.Services.AddScoped<IGovernorate, ClsGovernorate>();
builder.Services.AddScoped<IDeliveryMan, ClsDeliveryMan>();
builder.Services.AddScoped<ISalesInvoice, ClsSalesInvoice>();
builder.Services.AddScoped<ISalesInvoiceBooks, ClsSalesInvoiceBook>();
builder.Services.AddScoped<ICustomerInvoiceInfo, ClsCustomerInvoiceInfo>();
builder.Services.AddScoped<IDiscount, ClsDiscount>();
builder.Services.AddScoped<IPurchaseInvoice, ClsPurchaseInvoice>();
builder.Services.AddScoped<IPurchaseInvoiceBooks, ClsPurchaseInvoiceBook>();
builder.Services.AddScoped<ISliders, ClsSliders>();
builder.Services.AddScoped<ISettings, ClsSettings>();
builder.Services.AddScoped<IPages, ClsPages>();
#endregion

#region Session and Cookies
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();
builder.Services.ConfigureApplicationCookie(option =>
{
    option.AccessDeniedPath = "/Error/E403";
    option.Cookie.Name = "Cookie";
    option.Cookie.HttpOnly = true;
    option.ExpireTimeSpan = TimeSpan.FromMinutes(720);
    option.LoginPath = "/User/Login";
    option.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    option.SlidingExpiration = true;
});
#endregion

#region Swagger
//builder.Services.AddSwaggerGen(option =>
//{
//option.SwaggerDoc("V1", new OpenApiInfo
//{
//Version = "V1",
//Title = "BookSstore API",
//Description = "Api to access books and related categories",
//Contact = new OpenApiContact
//{
//    Email = "info@gmail.com",
//    Name = "Moaz Alaa",
//},
//License = new OpenApiLicense
//{
//    Name = "BookStore License",
//}

//});
//var xmlComment = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//var fullPath = Path.Combine(AppContext.BaseDirectory, xmlComment);
//option.IncludeXmlComments(fullPath);

//}); 
#endregion

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/E404");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

#region Swagger UI
//app.UseSwagger();
//app.UseSwaggerUI(options =>
//{
//    options.SwaggerEndpoint("/swagger/V1/swagger.json", "V1");
//    options.RoutePrefix = string.Empty;
//}); 
#endregion

#region Routing
app.UseEndpoints(endpoints =>
{
endpoints.MapControllerRoute(
name: "admin",
pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
endpoints.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");

}); 
#endregion


app.Run();

