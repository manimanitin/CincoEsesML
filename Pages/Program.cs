using Microsoft.Extensions.ML;
using static CincoEsesML.QualityModel;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPredictionEnginePool<ModelInput, ModelOutput>()
    .FromFile("../CincoEsesML/QualityModel.zip");
// Add services to the container.

builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();
var app = builder.Build();
app.UseStatusCodePagesWithReExecute("/Error");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDeveloperExceptionPage();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
