using OyoAgro.DataAccess.Layer.Models.Dtos;
using OyoAgro.DataAccess.Layer.Models.Params;

namespace OyoAgro.BusinessLogic.Layer.Interfaces
{
    public interface IPasswordResetService
    {
        Task<TData<string>> ForgotPassword(ForgotPasswordParam param);
        Task<TData<string>> ResetPassword(ResetPasswordParam param);
        Task<TData<bool>> ValidateResetToken(string token);
    }
}
