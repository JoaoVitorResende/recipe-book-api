﻿using Moq;
using MyRecipeBook.Domain.Repositories.User;

namespace CommonTestsUtilities.Repositories
{
    public class UserUpdateOnlyRepositoryBuilder
    {
        public static IUserUpdateOnlyRepository Build()
        {
            var mock = new Mock<IUserUpdateOnlyRepository>();
            return mock.Object;
        }
    }
}
