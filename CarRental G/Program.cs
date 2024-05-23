using CarRental.Common.Classes;
using CarRental.Business.Classes;
using CarRental.Data.Classes;
using CarRental.Data.Interfaces;
using CarRental_VG;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IData, CollectionData>();
builder.Services.AddSingleton<BookingProcessor>();
builder.Services.AddSingleton<NewEntities>();
await builder.Build().RunAsync();
