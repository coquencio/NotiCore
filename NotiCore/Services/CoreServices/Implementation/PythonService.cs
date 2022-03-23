using Microsoft.Extensions.Logging;
using Python.Deployment;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static Python.Runtime.Py;

namespace NotiCore.API.Services.CoreServices.Implementation
{
    public class PythonService : IPythonService
    {
        private readonly ILogger<PythonService> _logger;
        public PythonService(ILogger<PythonService> logger)
        {
            _logger = logger;
        }
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
            PythonEngine.Initialize();
            PythonEngine.BeginAllowThreads();
        }

        public Dictionary<string, string> ExecutePythonCode(string[] libraries, string code, string[] values = null)
        {
            Dictionary<string, string> returned = new Dictionary<string, string>();
            // PythonEngine.Initialize();
            // create a Python scope
            try
            {
                using (GIL())
                {
                    PyScope scope = CreateScope();
                    try
                    {
                        foreach (var lib in libraries)
                        {
                            scope.Import(lib);
                        }
                        scope.Exec(code);
                        if (values != null)
                        {
                            foreach (var value in values)
                            {
                                returned.Add(value, scope.Get(value).ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Python code error: {code}", code);
                    }
                    finally
                    {

                        scope.Dispose();
                        //_gilState.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not create a python scope: " + ex.Message);
            }
            return returned;
            
        }
    }
}
