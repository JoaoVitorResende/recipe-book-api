using MyRecipeBook.Application.Services.Cryptography;
namespace CommonTestsUtilities.Cryptography
{
    public class PasswordEncripterBuilder
    {
        public static PasswordEncryption Build() => new PasswordEncryption("abc1234");
    }
}
