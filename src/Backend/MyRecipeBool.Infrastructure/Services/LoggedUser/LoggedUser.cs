﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Infrastructure.DataAccess;

namespace MyRecipeBook.Infrastructure.Services.LoggedUser
{
    public class LoggedUser : IloggedUser
    {
        private readonly MyrecipeBookDbContext _dbContext;
        private readonly ITokenProvider _tokenProvider;
        public LoggedUser(MyrecipeBookDbContext dbContext, ITokenProvider tokenProvider)
        {
            _dbContext = dbContext;
            _tokenProvider = tokenProvider;
        }
        public async Task<User> User()
        {
            var token = _tokenProvider.Value();
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
            var identifier = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;
            var userIdentifier = Guid.Parse(identifier);
            return await _dbContext.Users.AsNoTracking().FirstAsync(user => user.Active && user.UserIdentifier == userIdentifier);
        }
    }
}
