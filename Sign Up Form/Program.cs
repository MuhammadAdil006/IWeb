using Microsoft.AspNetCore.Identity;
using Sign_Up_Form;
using Sign_Up_Form.Models.Interfaces;
using Sign_Up_Form.Models.Repository;
using Sign_Up_Form.Models.ViewModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. you can add startup file setup here
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<INotificationRepository, NotificationRepository>();
builder.Services.AddSingleton<IPostRepository, PostRepository>();
builder.Services.AddSingleton<IUserNotificationRepository, UserNotificationRepository>();
builder.Services.AddSingleton<IUserPostRepository, UserPostRepository>();
//builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddDbContext<ShareYourRoutineContext>();
builder.Services.AddIdentity<SignUpUser, IdentityRole>().AddEntityFrameworkStores<ShareYourRoutineContext>();
//adding session

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(option => option.IdleTimeout = TimeSpan.FromMinutes(10));
builder.Services.AddAutoMapper(typeof(Program));
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
app.UseSession();
app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
