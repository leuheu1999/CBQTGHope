using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Module.Framework.Common
{
    //public class CustomRoleProvider : RoleProvider
    //{
    //    public override void AddUsersToRoles(string[] usernames, string[] roleNames)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public override string ApplicationName { get; set; }

    //    public override void CreateRole(string roleName)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public override string[] FindUsersInRole(string roleName, string usernameToMatch)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public override string[] GetAllRoles()
    //    {
    //        throw new System.NotImplementedException();
    //        //using (var usersContext = new UsersContext())
    //        //{
    //        //    return usersContext.Roles.Select(r => r.RoleName).ToArray();
    //        //}
    //    }

    //    public override string[] GetRolesForUser(string username)
    //    {
    //        var userRoles = new List<UserRolesModel>();
    //        var userBus = new UsersServiceClient();
    //        var res = userBus.GetRolesByUser(username);
    //        if (res != null && res.Data != null && res.Data.status == "200")
    //        {
    //            userRoles = res.Data.resultObject;
    //        }
    //        var result = userRoles.Select(item => item.RoleId.ToString().ToUpper()).ToArray();
    //        return result;
    //    }

    //    public Dictionary<string, string> GerPermissionAreas(string username)
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //    public string[] GetCodeCookieForUser(string username)
    //    {
    //        var userRights =new List<UserRightsModel>();
    //        var userBus = new UsersServiceClient();
    //        var res = userBus.GetRightsByUser(username);
    //        if (res != null && res.Data != null && res.Data.status == "200")
    //        {
    //            userRights = res.Data.resultObject;
    //        }
           
    //        //var result = userRights.Select(item => item.RightsId.ToString().ToUpper()).ToArray();
    //        var result = userRights.Select(item => item.CodeCookie).ToArray();
    //        return result;
    //    }
    //    public string[] GetRightsForUser(string username)
    //    {
    //        var userRights = new List<UserRightsModel>();
    //        var userBus = new UsersServiceClient();
    //        var res = userBus.GetRightsByUser(username);
    //        if (res != null && res.Data != null && res.Data.status == "200")
    //        {
    //            userRights = res.Data.resultObject;
    //        }
    //        var result = userRights.Select(item => item.RightsId.ToString().ToUpper()).ToArray();
    //        return result;
    //    }
    //    public UserRolesModel GetRoleLeastByUser(string username)
    //    {
    //        var userRoles = new List<UserRolesModel>();
    //        var userBus = new UsersServiceClient();
    //        var res = userBus.GetRolesByUser(username);
    //        if (res != null && res.Data != null && res.Data.status == "200")
    //        {
    //            userRoles = res.Data.resultObject;
    //            return userRoles.FirstOrDefault();
    //        }
    //        else
    //        {
    //            return null;
    //        }
    //    }

    //    public override string[] GetUsersInRole(string roleName)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public override bool IsUserInRole(string username, string roleName)
    //    {
    //        throw new System.NotImplementedException();
    //        //using (var usersContext = new UsersContext())
    //        //{
    //        //    var user = usersContext.Users.SingleOrDefault(u => u.UserName == username);
    //        //    if (user == null)
    //        //        return false;
    //        //    return user.UserRoles != null && user.UserRoles.Select(u => u.Role).Any(r => r.RoleName == roleName);
    //        //}
    //    }

    //    public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public override bool RoleExists(string roleName)
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}
}
