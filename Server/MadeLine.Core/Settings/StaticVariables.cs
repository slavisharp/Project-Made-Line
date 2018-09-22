namespace MadeLine.Core.Settings
{
    using System;
    using System.Collections.Generic;

    public static class StaticVariables
    {
        public const string JWT_ROL = "roles";
        public const string JWT_ID = "id";
        public const string JWT_AdminRole = "Admin";
        public const string JWT_BrandRole = "BrandUser";
        public const string JWT_UserRole = "User";
        public const string JWT_AdminRole_Policy = "RequireAdminRole";
        public const string JWT_UserRole_Policy = "RequireUserRole";
        public const string JWT_BrandRole_Policy = "RequireBrandUserRole";

        public static readonly HashSet<string> ImageExtensions = new HashSet<string>(new string[]{ "jpg", "jpe", "bmp", "gif", "png", "jpeg" });
    }
}
