using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Win32;

    public class ProtocolHandlerRegistrationService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken) 
    {
        RegisterProtocolHandler();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public void RegisterProtocolHandler() 
    {
        var key = Registry.ClassesRoot.CreateSubKey("myapp");
        key.SetValue("", "URL:myapp Protocol");
        key.SetValue("URL Protocol", "");
        var commandKey = key.CreateSubKey("shell");
        var openKey = commandKey.CreateSubKey("open");
        var commandKey2 = openKey.CreateSubKey("command");

        /*commandKey2.SetValue("", $"\"C:\\Path\\To\\Application\\Executable.exe\" \"%1\"");*/
        commandKey2.SetValue("", $"C:\\Users\\Administrator\\Downloads\\WebApplication2\\WebApplication2\\Executable.exe\" \"%1\"");


    }
}
