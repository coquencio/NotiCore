using Python.Deployment;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.CoreServices.Implementation
{
    public class PythonService : IPythonService
    {
        public static void SetupModules(params string[] wheelNames)
        {
            const string libsPath = @"Infraestructure/PythonLibs/";
            const string tempPath = @"Infraestructure/";
            Installer.SetupPython().GetAwaiter().GetResult();
            Installer.TryInstallPip();
            foreach (var wheelName in wheelNames)
            {
                File.Copy(libsPath + wheelName, tempPath + wheelName, true);
                Installer.InstallWheel(wheelName).GetAwaiter().GetResult();
                File.Move(tempPath + wheelName, libsPath + wheelName, true);
            }
        }

        public Dictionary<string, string> ExecutePythonCode(string[] libraries, string code, string[] values = null)
        {

            PythonEngine.Initialize();
            using (Py.GIL())
            {
                // create a Python scope
                using (PyScope scope = Py.CreateScope())
                {
                    foreach (var lib in libraries)
                    {
                        scope.Import(lib);
                    }
                    scope.Exec(code);
                    Dictionary<string, string> returned = new Dictionary<string, string>();
                    if (values != null)
                    {
                        foreach (var value in values)
                        {
                            returned.Add(value, scope.Get(value).ToString());
                        }
                    }
                    scope.Dispose();
                    return returned;
                }
            }
        }
    }
}
