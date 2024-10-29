using LogInOut.Inc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//database연결을 위한 DbContext 
builder.Services.AddDbContext<DBConnContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

//services.AddSession();//세션 사용
builder.Services.AddSession(option => {
    // 세션 이름
    option.Cookie.Name = "loginSession";

    // 세션 지속 시간(1시간)
    option.IdleTimeout = TimeSpan.FromHours(1);
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

app.UseSession();//세션 사용 30분

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
