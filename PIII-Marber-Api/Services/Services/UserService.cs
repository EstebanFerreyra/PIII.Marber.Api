using AutoMapper;
using Models.DTO;
using Models.Models;
using Models.ViewModel;
using Services.IServices;
using Services.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class UserService : IUserService
    {
        private readonly Marber_BBDDContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(Marber_BBDDContext _context)
        {
            _dbContext = _context;
            _mapper = AutoMapperConfig.Configure();
        }

        public List<UserDTO> GetUsers()
        {
            try
            {
                return _mapper.Map<List<UserDTO>>(_dbContext.Users.ToList());
            }
            catch (Exception exe)
            {
                string error = exe.Message;
                return null;
            }
        }

        public UserDTO GetUserById(int id)
        {
            try
            {
                return _mapper.Map<UserDTO>(_dbContext.Users.Where(x => x.Id == id).First());
            }
            catch (Exception exe)
            {
                string error = exe.Message;
                return null;
            }
        }

        public UserDTO AddUser(UserViewModel user)
        {
            try
            {
                _dbContext.Users.Add(new Users()
                {
                    IdRole = _dbContext.Roles.First(f => f.Id == user.IdRole).Id,
                    UserName = user.UserName,
                    UserEmail = user.UserEmail,
                    UserPassword = user.UserPassword,
                });
                _dbContext.SaveChanges();
                return _mapper.Map<UserDTO>(_dbContext.Users.OrderBy(o => o.Id).Last());
            }
            catch (Exception exe)
            {
                string error = exe.Message;
                return null;
            }
        }

        public UserDTO UpdateUser(UserViewModel user)
        {
            try
            {
                Users userDB = _dbContext.Users.Single(i => i.Id == user.Id);
                userDB.IdRole = _dbContext.Roles.First(r => r.Id == user.IdRole).Id;
                userDB.UserName = user.UserName;
                userDB.UserEmail = user.UserEmail;
                userDB.UserPassword = user.UserPassword;
                //_dbContext.Users.Add(_mapper.Map<Users>(user));
                _dbContext.SaveChanges();
                return _mapper.Map<UserDTO>(_dbContext.Users.FirstOrDefault(f => f.Id == userDB.Id));
            }
            catch (Exception exe)
            {
                string error = exe.Message;
                return null;
            }
        }

        public bool DeleteUser(int id)
        {
            try
            {
                Users user = _dbContext.Users.Where(w => w.Id == id).FirstOrDefault();
                if (user != null)
                {
                    _dbContext.Users.Remove(user);
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception exe)
            {
                string error = exe.Message;
                return false;
            }
        }
    }
}
