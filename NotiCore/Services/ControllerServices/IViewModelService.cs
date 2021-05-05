using Microsoft.AspNetCore.Http;
using NotiCore.API.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Services.ControllerServices
{
    public interface IViewModelService
    {
        SourceSetupViewModel GetUserSourceSetupModel(string values);
        void SaveUserChanges(IFormCollection values);
    }
}
