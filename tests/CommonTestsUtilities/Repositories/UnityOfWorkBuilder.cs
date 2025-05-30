﻿using Moq;
using MyRecipeBook.Domain.Repositories.UnityOfWork;

namespace CommonTestsUtilities.Repositories
{
    public class UnityOfWorkBuilder
    {
        public static IUnityOfWork Build()
        {
            var mock = new Mock<IUnityOfWork>();
            return mock.Object;
        }
    }
}
