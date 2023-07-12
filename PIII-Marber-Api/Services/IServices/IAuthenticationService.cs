using Models.Models;
using Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IServices
{
    public interface ICustomAuthenticationService
    {
        Users? ValidateUser(CredentialsViewModel credentialsViewModel);
        string ValidateRole(int id);
    }
}
