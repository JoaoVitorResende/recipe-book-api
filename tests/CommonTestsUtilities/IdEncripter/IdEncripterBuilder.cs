using Sqids;

namespace CommonTestsUtilities.IdEncripter
{
    public class IdEncripterBuilder
    {
        public static SqidsEncoder<long> Build()
        {
            return new SqidsEncoder<long>(new()
            {
                MinLength = 3,
                Alphabet = "AaBbCcDdEeFfGgHhIiJjKkLlMmnNOo012345"
            });
        }
    }
}
