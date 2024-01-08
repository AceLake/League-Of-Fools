using League_Of_Fools.Models;

namespace League_Of_Fools.Service
{
    public class SecurityService
    {
        SecurityDAO securityDAO = new SecurityDAO();
        public bool IsValid(UserModel user)
        {
            return securityDAO.GetByNameAndPassword(user);
        }

        
    }
}
