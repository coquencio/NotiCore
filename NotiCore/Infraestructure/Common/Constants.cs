using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotiCore.API.Infraestructure.Common
{
    public static class TopicConstants
    {
        public const float MinimumTopicAccuracy = 0.78F;
    }
    public static class PropertyConstants
    {
        public const string MailHost = "MailHost";
        public const string MailPort = "MailPort";
        public const string MailerAddress = "MailerAddress";
        public const string MailerPassword = "MailerPassword";
    }
}
