namespace MadeLine.Core.Settings
{
    using MadeLine.Data.Models;
    using System.Collections.Generic;

    public class AppSettings
    {
        public string ApplicationName { get; set; }

        public string Version { get; set; }

        public string Environment { get; set; }

        public string AdminRole { get; set; }

        public string UserRole { get; set; }

        public string BrandRole { get; set; }
        
        public TranslationLanguage DefaultLanguage { get; set; }

        public DbSettings DbSettings { get; set; }

        public URLSettings URLS { get; set; }

        public EmailSettings EmailSettings { get; set; }

        public JwtIssuerOptions JwtIssuerOptions { get; set; }

        public FacebookAuthSettings FacebookAuthSettings { get; set; }
    }

    public class DbSettings
    {
        public string LocalConnectionName { get; set; }

        public string AppHarborConnectionName { get; set; }
    }

    public class URLSettings
    {
        public string BaseURL { get; set; }
    }
    
    public class FacebookAuthSettings
    {
        public string AppId { get; set; }

        public string AppSecret { get; set; }
    }

    public class EmailSettings
    {
        public string PrimaryDomain { get; set; }

        public int PrimaryPort { get; set; }

        public string UsernameEmail { get; set; }

        public string From { get; set; }

        public IEnumerable<string> CCList { get; set; }
    }
}
