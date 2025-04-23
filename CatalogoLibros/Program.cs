var builder = WebApplication.CreateBuilder(args);

// A�adir servicios al contenedor.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar la canalizaci�n de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // El valor predeterminado de HSTS es de 30 d�as. Puede cambiarlo para escenarios de producci�n; consulte https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Libro}/{action=Index}/{id?}");

app.Run();
