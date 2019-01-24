using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Templates.Core.Repositories.Users;
using Templates.EntityFrameworkCore.Models;

namespace Templates.Application.Users
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;

        public UserAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public User Get(int id)
        {
            return _userRepository.Get(id);
        }

        public IQueryable<User> Get(Expression<Func<User, bool>> predicate)
        {
            return _userRepository.Get(predicate);
        }

        public IQueryable<User> Get()
        {
            return _userRepository.Get();
        }

        public User Create(User user)
        {
            return _userRepository.Insert(user);
        }

        public User Update(User user)
        {
            return _userRepository.Update(user);
        }

        public void Delete(int id)
        {
            _userRepository.Delete(_userRepository.Get(id));
        }

        public void Delete(IEnumerable<int> ids)
        {
            _userRepository.Delete(_userRepository.Get(u => ids.Contains(u.Id)));
        }
    }
}
