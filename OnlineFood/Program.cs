using Microsoft.EntityFrameworkCore;
using OnlineFood.Data;
using OnlineFood.Models.Repositories;
using OnlineFood.Models.Services;

var builder = WebApplication.CreateBuilder(args);

// Thêm Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug(); // Hiển thị log chi tiết trong cửa sổ Debug
// Add services to the container.
builder.Services.AddControllersWithViews();

//Đăng ký Repo
builder.Services.AddScoped<IFoodCategoryRepo, FoodCategoryRepo>();
builder.Services.AddScoped<IFoodRepo, FoodRepo>();
builder.Services.AddScoped<ICartRepo, CartRepo>();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IPromotionRepo, PromotionRepo>();

//Đăng ký Service
builder.Services.AddScoped<IFoodCategoryService, FoodCategoryService>();
builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPromotionService, PromotionService>();

//Kết nối database
builder.Services.AddDbContext<OnlineFoodContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("OnlineFoodDatabase")));

// Thêm dịch vụ Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian tồn tại của Session
    options.Cookie.HttpOnly = true; // Bảo mật hơn khi chỉ cho phép truy cập Session từ phía server
    options.Cookie.IsEssential = true; // Đảm bảo cookie hoạt động ngay cả khi người dùng không đồng ý cookie
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



// Sử dụng Session
app.UseSession();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Foods}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "admin",
    pattern: "admin",
    defaults: new { controller = "Admin", action = "Index" });
app.Run();
