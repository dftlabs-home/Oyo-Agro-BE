using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Interfaces;
using OyoAgro.DataAccess.Layer.Models.Entities;
using OyoAgro.DataAccess.Layer.Repositories.Base;

namespace OyoAgro.DataAccess.Layer.Repositories
{
    public class PasswordResetTokenRepository : RepositoryFactory, IPasswordResetTokenRepository
    {
        public async Task<PasswordResetToken> GetValidToken(string token)
        {
            return await BaseRepository().FindEntity<PasswordResetToken>(x => 
                x.Token == token && 
                !x.IsUsed && 
                x.ExpiresAt > DateTime.UtcNow);
        }

        public async Task<List<PasswordResetToken>> GetUserTokens(int userId)
        {
            var tokens = await BaseRepository().FindList<PasswordResetToken>(x => x.UserId == userId);
            return tokens.ToList();
        }

        public async Task SaveForm(PasswordResetToken entity)
        {
            await BaseRepository().Insert(entity);
        }

        public async Task UpdateToken(PasswordResetToken entity)
        {
            await BaseRepository().Update(entity);
        }

        public async Task DeleteExpiredTokens()
        {
            var expiredTokens = await BaseRepository().FindList<PasswordResetToken>(x => 
                x.ExpiresAt < DateTime.UtcNow || x.IsUsed);
            
            foreach (var token in expiredTokens)
            {
                await BaseRepository().Delete(token);
            }
        }
    }
}
