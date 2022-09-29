Microsoft.aspnetcore.identity.EntityFramework
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.sqlserver


add this package

inherit appdbcontext with IdentityDbContext


add in program.cs

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

add in program .cs builder services section 

builder.Services.AddDbContext<ShareYourRoutineContext>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ShareYourRoutineContext>();

add this line in
in on Model Creating funcotion of dbcontext
 base.OnModelCreating(modelBuilder);

<<<<<<< HEAD
Now run migration and update database



