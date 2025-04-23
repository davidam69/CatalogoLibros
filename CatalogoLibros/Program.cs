var builder = WebApplication.CreateBuilder(args);

// Añadir servicios al contenedor.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar la canalización de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // El valor predeterminado de HSTS es de 30 días. Puede cambiarlo para escenarios de producción; consulte https://aka.ms/aspnetcore-hsts.
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
