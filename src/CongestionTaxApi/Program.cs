
using Microsoft.AspNetCore.Mvc;

var app = WebApplication
    .CreateSlimBuilder(args)
    .Build();


// Default some kind of "man page" as default for the API
app.MapGet("/", () =>
    Results.Text(
        content: File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "HELP.md")),
        contentType: "text/plain"));

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
