namespace MyRecipeBook.Domain.Security.Cryptography
{
    public interface IPasswordEncripter
    {
        public string Encript(string password);
    }
}
