using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GYMONE.Models;

namespace GYMONE.Repository
{
    public interface IAccountData
    {
        IEnumerable<Role> GetRoles();
        IEnumerable<Users> GetAllUsers();
        string GetRoleByUserID(string UserId);
        string GetUserID_By_UserName(string UserName);
        string Get_checkUsernameExits(string username);
        bool Get_CheckUserRoles(string UserId);
        string GetUserName_BY_UserID(string UserId);
        IEnumerable<AllroleandUser> DisplayAllUser_And_Roles();
    }
}