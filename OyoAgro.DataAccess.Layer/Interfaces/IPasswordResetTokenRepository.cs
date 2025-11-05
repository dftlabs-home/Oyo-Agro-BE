using System.Collections.Generic;
using System.Threading.Tasks;
using OyoAgro.DataAccess.Layer.Models.Entities;

namespace OyoAgro.DataAccess.Layer.Interfaces
{
    public interface IPasswordResetTokenRepository
    {
        Task<PasswordResetToken> GetValidToken(string token);
        Task<List<PasswordResetToken>> GetUserTokens(int userId);
        Task SaveForm(PasswordResetToken entity);
        Task UpdateToken(PasswordResetToken entity);
        Task DeleteExpiredTokens();
    }
}
