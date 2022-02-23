using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using PrepTeach;
using PrepTeach.Extensions;
using PrepTeach.Services;
using System.Diagnostics;
using System.IO.Compression;
using ZNetCS.AspNetCore.Compression;
using ZNetCS.AspNetCore.Compression.Compressors;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlite("Filename=Database.db"));

builder.Services.AddMyAuthentication(builder.Configuration);

builder.Services.AddSingleton<IFilesStorePath, FilesStorePath>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddCompression(options => { options.Compressors = new List<ICompressor> { new GZipCompressor(CompressionLevel.Fastest), new DeflateCompressor(CompressionLevel.Fastest), new BrotliCompressor(CompressionLevel.Fastest) }; });


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllHeaders",
            bd =>
            {
                bd.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
            });
});

//if (app.Environment.IsDevelopment())
//{
builder.Services.AddMySwagger();
//}

var app = builder.Build();
app.UseMySwagger();

if (!app.Environment.IsDevelopment())
{
    try
    {
        var processes = Process.GetProcessesByName("Chrome");
        var path = processes.FirstOrDefault()?.MainModule?.FileName;
        if (path != null)
            Process.Start(path, "http://localhost:5000/swagger");
    }
    catch { }
}

using (var serviceScope = app.Services.CreateScope())
{
    serviceScope.ServiceProvider.GetService<MyDbContext>()?.Database.Migrate();
}

app.UseCompression();

var options = new DefaultFilesOptions();
options.DefaultFileNames.Clear();
options.DefaultFileNames.Add("index.html");
app.UseDefaultFiles(options);
app.UseStaticFiles();

var FileStorePath = AppDomain.CurrentDomain.BaseDirectory + $"wwwroot{Path.DirectorySeparatorChar}media";
if (!Directory.Exists(FileStorePath)) Directory.CreateDirectory(FileStorePath);
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx => { ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age=604800"); },
    FileProvider = new PhysicalFileProvider(FileStorePath),
    RequestPath = "/media"
});

app.UseCors("AllowAllHeaders");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
