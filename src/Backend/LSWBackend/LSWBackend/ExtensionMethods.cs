using System.Runtime.CompilerServices;

namespace LSWBackend;

public static class ExtensionMethods
{
    public static T CopyPropertiesFrom<T>(this T target, object source) => CopyPropertiesFrom<T>(target, source, null);

    public static T CopyPropertiesFrom<T>(this T target, object source, string[]? ignoreProperties) {
        if (target == null) return target;
        ignoreProperties ??= Array.Empty<string>();
        var propsSource = source.GetType().GetProperties().Where(x => x.CanRead && !ignoreProperties.Contains(x.Name));
        var propsTarget = target.GetType().GetProperties().Where(x => x.CanWrite);

        propsTarget
          .Where(prop => propsSource.Any(x => x.Name == prop.Name))
          .ToList()
          .ForEach(prop => {
              var propSource = propsSource.Where(x => x.Name == prop.Name).First();
              prop.SetValue(target, propSource.GetValue(source));
          });
        return target;
    }

    public static void Log(this ControllerBase controller, string msg = "", [CallerMemberName] string callerMethod = "") {
        //Note: Color output requires "launchBrowser": false in launchSettings.json
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write($"{DateTime.Now:HH:mm:ss.ff}");
        Console.BackgroundColor = ConsoleColor.Black;
        string method = controller.Request.HttpContext.Request.Method;
        Console.ForegroundColor = method == "GET" ? ConsoleColor.Green : method == "DELETE" ? ConsoleColor.Red : ConsoleColor.Cyan;
        Console.Write($" {controller.Request.HttpContext.Request.Method} ");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write($"{controller.Request.HttpContext.Request.Path}{controller.Request.QueryString} ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{callerMethod} {msg}");
        Console.ResetColor();
    }
}
