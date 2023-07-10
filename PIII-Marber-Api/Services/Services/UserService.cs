using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class UserService
    {
        private readonly Marber_BBDDContext _dbContext;

        public UserService(Marber_BBDDContext context)
        {
            _dbContext = context;
        }

        public List<Users> GetListUser()
        {
            var list = _dbContext.Users.ToList();
            return list;
        }

    }
}
