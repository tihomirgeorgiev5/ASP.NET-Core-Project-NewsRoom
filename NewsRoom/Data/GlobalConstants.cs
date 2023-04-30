namespace NewsRoom.Data
{
    public static class GlobalConstants
    {
        public class User
        {
            public const int FullNameMinLength = 5;
            public const int FullNameMaxLength = 40;
            public const int PasswordMinLength = 5;
            public const int PasswordMaxLength = 100;
        
        }
        public static class ANews
        {
            public const int AreaMaxLength = 30;
            public const int AreaMinLength = 2;

            public const int TitleMaxLength = 500;
            public const int TitleMinLength = 5;

            public const int DescriptionMaxLength = 10000;
            public const int DescriptionMinLength = 10;

            public const int DateMaxLength = 01 - 01 - 2025;
        }

        public static class Category
        {
            public const int NameMaxLength = 20;
        }

        public static class Journalist
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 30;
            public const int PhoneNumberMinLength = 5;
            public const int PhoneNumberMaxLength = 30;
        }

        public static class Images
        {
            public const string Error404 = "https://img.freepik.com/free-vector/oops-404-error-with-broken-robot-concept-illustration_114360-5529.jpg?w=740&t=st=1682870909~exp=1682871509~hmac=9096d64eb44e1d8600800a00ced997dc173d8d3b6191c4a09f36ac11bab84b6d";
            public const string Error500 = "https://img.freepik.com/free-vector/500-internal-server-error-concept-illustration_114360-1905.jpg?w=740&t=st=1682870991~exp=1682871591~hmac=3079b6c4fa0cef3808bb565d620a0b1a83fe397166f5879b8c8dbe8b0f64e16c";
        }
       

        
    }
}