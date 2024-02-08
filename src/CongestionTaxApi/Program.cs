using CongestionTaxApi.Api;
using CongestionTaxApi.Domain;


var builder = WebApplication.CreateSlimBuilder(args);

var services = builder.Services;

// No fancy DI here, just a simple service until more is needed
services.AddSingleton<CongestionTaxCalculator>();

// Default some kind of "man page" as default for the API

var app = builder.Build();

app.MapEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Skip these for now
// app.UseHttpsRedirection();
// app.UseStaticFiles();
// app.UseRouting();
// app.UseAuthorization();

app.Run();