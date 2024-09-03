using VetHub02.Core.Entities;

namespace VetHub02.Helpers
{
    public interface IMailSettings
    {
        public void sendMail(MailSettingsModel email);
      
    }
}
