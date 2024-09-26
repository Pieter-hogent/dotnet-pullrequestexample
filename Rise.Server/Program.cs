using Microsoft.EntityFrameworkCore;
using Rise.Persistence;
using Rise.Persistence.Triggers;
using Rise.Services.Products;
using Rise.Shared.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddRazorComponents().AddInteractiveWebAssemblyComponents();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
    options.EnableDetailedErrors();
    options.EnableSensitiveDataLogging();
    options.UseTriggers(options => options.AddTrigger<EntityBeforeSaveTrigger>());
});

builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAntiforgery();

app.MapRazorPages();
app.MapControllers();
app.MapRazorComponents<Rise.Client.Host>().AddInteractiveWebAssemblyRenderMode();

using (var scope = app.Services.CreateScope())
{ // Require a DbContext from the service provider and seed the database.
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    Seeder seeder = new(dbContext);
    seeder.Seed();
}

app.Run();
