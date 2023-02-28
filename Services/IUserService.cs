using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tickeing_system.models;

namespace tickeing_system.Services
{
    public interface IUserService
    {
        List<User> GetUserList();

        User GetUserById(int userId);
        ResponseModel SaveUser(User employeeModel);

        public bool VerifyUser(string UserName, string UserPassword);
    }
}