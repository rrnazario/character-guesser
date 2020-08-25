namespace CharacterGuesser.Domain.Constants
{
    public class MessageConstants
    {
        public static readonly string SUFIX_ANY_KEY_MESSAGE = "(Press any key to continue)";
        public static readonly string SUFIX_ENTER_MESSAGE = "(Press ENTER when finish): ";

        public static readonly string SUCCESS_MESSAGE_FIRST_TIME = $"Yes! {SUFIX_ANY_KEY_MESSAGE}";
        public static readonly string SUCCESS_MESSAGE = $"One more for me! {SUFIX_ANY_KEY_MESSAGE}";
    }
}
