using ElectronNET.API;                  //<<= Add this line

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseElectron(args);      //<<-- Add this line

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Begin Add this line
//Add this to the end of Configure method in Startup.cs
if(HybridSupport.IsElectronActive)
{
    CreateElectronWindow();
}
//End Add this line

app.Run();

//Begin Add this line
async void CreateElectronWindow()
{
    var window = await Electron.WindowManager.CreateWindowAsync();
    window.OnClosed += () => Electron.App.Quit();
    
    window.OnMaximize += Window_OnMaximize; 
}

void Window_OnMaximize()
{
    Electron.Dialog.ShowErrorBox("Maximized Event", "Hi, You just maximized your Electron App!");
    //Dialog Note https://github.com/ElectronNET/Electron.NET/blob/master/ElectronNET.API/Dialog.cs
}
//End Add this line