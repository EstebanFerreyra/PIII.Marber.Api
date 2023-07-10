using Models.Models;
using Models.ViewModel;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class AuthenticationService : ICustomAuthenticationService
    {
        private readonly Marber_BBDDContext _dbContext;

        public AuthenticationService(Marber_BBDDContext context)
        {
            _dbContext = context;
        }

        public Users? ValidateUser(CredentialsViewModel credentialsViewModel)
        {
            return _dbContext.Users.FirstOrDefault(f => f.UserName == credentialsViewModel.UserName && f.UserPassword == credentialsViewModel.UserPassword);
        }
    }
}
