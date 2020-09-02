using AbBus.BAL.ViewModels;
using AbBus.BAL.DTO;
using AbBus.DAL;
using System.Data;
using System.Linq;
using System;

namespace AbBus.BAL
{
    public class UserManagement
    {
        private readonly UnitOfWork _uow;
        private readonly UserMapper _userMapper;
        public UserManagement()
        {
            _uow = new UnitOfWork();
            _userMapper = new UserMapper();
        }
        public UserVM SignIn(string email, string password)
        {
            return _userMapper.VMMapper(_uow.Users.Get(filter: x => x.email.Equals(email) && x.password.Equals(password)).FirstOrDefault());
        }

        public bool EditProfile(string name, string email)
        {
            try
            {
                user currUserObj = _uow.Users.Get(filter: x => x.email.Equals(email)).FirstOrDefault();
                if (currUserObj == null)
                {
                    return false;
                }
                currUserObj.name = name;
                currUserObj.email = email;
                currUserObj.updatedon = DateTime.UtcNow;
                _uow.Users.Update(currUserObj);
                return true;

            }
            catch (DataException ex)
            {
                return false;
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.)
            }
        }

        public bool SignUp(UserVM userVMObj)
        {
            if (!UserExists(userVMObj.Email)) // user cannot have multiple email accounts. If that is the case, invalidate the sign-up
            {
                try
                {
                    userVMObj.RegisteredOn = DateTime.UtcNow;
                    userVMObj.UpdatedOn = DateTime.UtcNow;
                    _uow.Users.Insert(_userMapper.DataMapper(userVMObj));
                    return true;
                }
                catch (DataException ex)
                {
                    return false;
                }
            }
            return false;
    }

    public bool UserExists(string email)
    {
        user currUserObj = _uow.Users.Get(filter: x => x.email.Equals(email)).FirstOrDefault();
        if (currUserObj == null)
        {
            return false;
        }
        return true;
    }
}
}
