namespace NewsRoom.Data
{
    public class DataConstants
    {
        public class User
        {
            public const int FullNameMinLength = 5;
            public const int FullNameMaxLength = 40;
            public const int PasswordMinLength = 5;
            public const int PasswordMaxLength = 100;
        
        }
        public class ANews
        {
            public const int AreaMaxLength = 30;
            public const int AreaMinLength = 2;

            public const int TitleMaxLength = 500;
            public const int TitleMinLength = 5;

            public const int DescriptionMaxLength = 10000;
            public const int DescriptionMinLength = 10;

            public const int DateMaxLength = 01 - 01 - 2025;
        }

        public class Category
        {
            public const int NameMaxLength = 20;
        }

        public class Journalist
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 30;
            public const int PhoneNumberMinLength = 5;
            public const int PhoneNumberMaxLength = 30;
        }
       

        
    }
}