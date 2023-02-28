using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tickeing_system.models;
namespace tickeing_system.Services
{
    public class UserService : IUserService
    {
        private TicketingSystemContex _context;
        public UserService(TicketingSystemContex context) {
        _context = context;
        }
        // Service to get all users
        public List<User> GetUserList()
        {
            List < User > userList;
            try {
                userList = _context.Set < User > ().ToList();
            } catch (Exception) {
                throw;
            }
            return userList;
        }

        public User GetUserById(int userId)
        {
            User? user;
            try {
                user = _context.Find < User > (userId);
            } catch (Exception) {
                throw;
            }
            return user;
        }

        // service to save user 
        public ResponseModel SaveUser(User userModel)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                _context.Add<User> (userModel);
                model.Message = "User Inserted Successfully";
                _context.SaveChanges();
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }
    }
}