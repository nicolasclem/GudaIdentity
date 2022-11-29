using GudaIdentity.Data;
using GudaIdentity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//  config  conect  DB

builder.Services.AddDbContext<ApplicationDbContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("SQL"))

);

// Identity   a applicacion

builder.Services.AddIdentity<IdentityUser,IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();


// url de retorno
builder.Services.ConfigureApplicationCookie(options =>
 {
     options.LoginPath = new PathString("/Cuentas/Acceso");
     options.AccessDeniedPath = new PathString("/Cuentas/Bloqueado");
 });


//Opciones  de configuracion de  IDENTITY
builder.Services.Configure<IdentityOptions>(options =>
{

    options.Password.RequiredLength = 5;
    options.Password.RequireLowercase= true;    
    options.Lockout.DefaultLockoutTimeSpan= TimeSpan.FromMinutes(1);// para probar
    options.Lockout.MaxFailedAccessAttempts= 3;


});

//se agrega el servico  de IEmailSender

builder.Services.AddTransient<IEmailSender, MailJetEmailSender>();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//agrego autenticacion!!
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
