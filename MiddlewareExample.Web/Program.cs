var builder = WebApplication.CreateBuilder(args);

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


app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Before 1. Middleware\n");

    await next();

    await context.Response.WriteAsync("After 1. Middleware");
});

app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("Before 2. Middleware\n");

    await next();

    await context.Response.WriteAsync("After 2. Middleware");
});

app.Run(async context =>
{
    await context.Response.WriteAsync("Terminal 3. Middleware\n");
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
