using Python.Deployment;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.Implementation
{
    public class PythonService : IPythonService
    {
        public static void SetupModules(params string[] wheelPaths)
        {
            Installer.SetupPython().GetAwaiter().GetResult();
            Installer.TryInstallPip();
            foreach (var wheelPath in wheelPaths)
            {
                Installer.InstallWheel(wheelPath).GetAwaiter().GetResult();
            }
            PythonEngine.Initialize();
        }

        public Dictionary<string, string> ExecutePythonCode(string[] libraries, string code, string[] values = null)
        {
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
