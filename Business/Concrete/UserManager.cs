using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Mapping;
using Core.Utilities.Mapping.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserRepository _userDal;
        IMapper _mapper;

        public UserManager(IUserRepository userDal, IMapper mapper)
        {
            _userDal = userDal;
            _mapper = mapper;
        }


        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            var result = _userDal.GetClaims(user);

            return new SuccessDataResult<List<OperationClaim>>(result, Messages.CreateUserSuccess);
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);

            return new SuccessResult(Messages.CreateUserSuccess);
        }

        public IDataResult<User> GetByMail(string email)
        {
            var result = _userDal.Get(u => u.Email == email);

            return new SuccessDataResult<User>(result, Messages.CreateUserSuccess);
        }

        public IResult Update(int id, User user)
        {
            User currentUser = _userDal.Get(e => e.UserId == id);

            var result = _mapper.SelfMap<User>(user, currentUser);

            _userDal.Update(result);

            return new SuccessResult(Messages.UpdateUserSuccess);
        }
    }
}
