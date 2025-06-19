namespace MyRecipeBook.Communication.Responses
{
    public class ResponseRegistredUserJson
    {
        public string Name { get; set; } = string.Empty;
        public ResponseTokenJson Tokens { get; set; } = default!;
    }
}
