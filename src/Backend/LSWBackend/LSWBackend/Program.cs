string corsKey = "_myCorsKey";

var builder = WebApplication.CreateBuilder(args);

#region -------------------------------------------- ConfigureServices
builder.Services.AddControllers();
builder.Services.AddCors(options => options.AddPolicy(
    corsKey,
    x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
  ));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("LSWDb_Relative");
string location = System.Reflection.Assembly.GetEntryAssembly()!.Location;
string dataDirectory = Path.GetDirectoryName(location)!;
connectionString = connectionString.Replace("|DataDirectory|", dataDirectory + Path.DirectorySeparatorChar);
builder.Services.AddDbContext<LSWContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddHostedService<DatabaseBackgroundService>();

#endregion

var app = builder.Build();

#region -------------------------------------------- Middleware pipeline
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsKey);

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseExceptionHandler(config => config.Run(async context => {
    context.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError; //500
    context.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json; //"application/json"
    var error = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
    if (error != null) {
        await context.Response.WriteAsync(
          $"Exception: {error.Error?.Message} {error.Error?.InnerException?.Message}");
    }
}));

app.MapControllers();
#endregion

app.Run();
