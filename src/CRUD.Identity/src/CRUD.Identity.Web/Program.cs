using CRUD.Identity.Web.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder
    .Services
    .ConfigureDatabase(builder.Configuration.GetConnectionString("DefaultConnection"))
    .ConfigureIdentity()
    .ConfigureOpenIddict()
    .ConfigureHostedServices();

// below is not production ready, should limit the origins
builder.Services.AddCors(setup =>
{
    setup.DefaultPolicyName = "Default";
    setup.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app
    .UseRouting()
    .UseStaticFiles()
    .UseCors("Default")
    .UseAuthentication()
    .UseAuthorization();

app.MapControllers();
app.MapDefaultControllerRoute();
app.MapRazorPages();

app.Run();