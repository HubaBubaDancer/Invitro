using Invitro;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Invitro.Areas.Identity.Data;
using Invitro.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AuthDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthDbContextConnection' not found.");

var mainConnectionString = "Server=localhost;Port=5432;User Id=postgres;Password=5656;Database=Invitro;";
var authConnectionString = "Server=localhost;Port=5432;User Id=postgres;Password=5656;Database=InvitroAuth;";

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(mainConnectionString));
builder.Services.AddDbContext<AuthDbContext>(options => options.UseNpgsql(authConnectionString));
builder.Services.AddDefaultIdentity<ApplicationUser>(o => o.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddTransient<IAppointmentHandler, AppointmentHandler>();
builder.Services.AddTransient<IDepartmentHandler, DepartmentHandler>();
builder.Services.AddTransient<IFamilyHandler, FamilyHandler>();
builder.Services.AddTransient<IProcedureHandler, ProcedureHandler>();
builder.Services.AddTransient<IRegistrationHandler, RegistrationHandler>();



builder.Services.AddControllersWithViews();
    
builder.Services.AddRazorPages();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = " DentalOffice",
        Description = "(:",
        TermsOfService = new Uri("https://github.com/HubaBubaDancer"),
        Contact = new OpenApiContact
        {
            Name = "LinkedIn",
            Url = new Uri("https://www.linkedin.com/in/hubabubadancer/")
        }
    });
});






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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.UseSwagger();
app.UseSwaggerUI();

app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Doctor", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string email = "demouser@test.test";
    string password = "Test123!";

    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new ApplicationUser()
        {
            UserName = email, 
            Email = email, 
            EmailConfirmed = true, 
            FirstName = "Demo", 
            LastName = "User"
        };
        await userManager.CreateAsync(user, password);
        await userManager.AddToRoleAsync(user, "Admin");
    }
}



app.Run();