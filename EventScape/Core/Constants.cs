namespace EventScape.Core
{
    public static class Constants
    {
        public static class Roles
        {
            public const string NonRegisteredUser   = "NonRegisteredUser";
            public const string UserRole = "UserRole";
            public const string Administrator = "Administrator";
            public const string BlockedUser = "BlockedUser";
        }

        public static class Policies
        {
            public const string RequireAdmin = "RequireAdmin";
            public const string RequireNonUser = "RequireNonUser";
            public const string RequireUser = "RequireUser";
            public const string RequireBlockedUser = "RequireBlockedUser";
        }
    }
}
