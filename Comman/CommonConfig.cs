using System;
using System.Globalization;


namespace BluSignalHelpMethod
{
    public static class CommonConfig
    {
        public static string EMailId
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["EMailId"].ToString(CultureInfo.InvariantCulture); }
        }
        public static string SmtpHostName
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["SMTPHostName"].ToString(CultureInfo.InvariantCulture); }
        }
        public static string EMailName
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["EMailName"].ToString(CultureInfo.InvariantCulture); }
        }
        public static string SmtpPassword
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["SMTPPassword"].ToString(CultureInfo.InvariantCulture); }
        }
        public static string SmtpUserName
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["SMTPUserName"].ToString(CultureInfo.InvariantCulture); }
        }
        public static int PortNumber
        {
            get { return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PortNo"].ToString(CultureInfo.InvariantCulture)); }
        }
        public static bool EnableSsl
        {
            get { return Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["EnableSsl"].ToString(CultureInfo.InvariantCulture)); }
        }


        public static string AdminEmailID
        {
            get { return System.Configuration.ConfigurationManager.AppSettings["AdminEmailID"].ToString(CultureInfo.InvariantCulture); }
        }

    }
}
