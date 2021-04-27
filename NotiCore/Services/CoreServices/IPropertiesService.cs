
namespace NotiCore.API.Services.CoreServices
{
    public interface IPropertiesService
    {
        string GetProperty(string propertyName);
        bool PropertyExist(string propertyName);
        void SaveProperty(string propertyName, string propertyValue);
        void SaveRawProperty(string propertyName, string propertyValue);
    }
}
