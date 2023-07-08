using Uniteds.CaseStudy.Domain.Models;

namespace Uniteds.CaseStudy.Domain.Interfaces
{
    public interface IUserRepository
    {
        User GetUserByEmail(string email);
        void AddUser(User user);
    }
}
