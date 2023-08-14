namespace NewsRoom.Data
{
    public class MessageConstants
    {
        public static class AllError
        {
            public const string EmptyField = "The field should not be empty. It is required to have a content.";
        }
        public static class Faq
        {
            public const string FaqAlreadyExist = "Faq with question {0} and answer {1} already exists.";

            public const string FaqNotFound = "Faq with id {0} is not found.";
        }
    }
}
