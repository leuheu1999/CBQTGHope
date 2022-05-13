using System.Web.Security;
using System.Linq;
namespace Module.Framework.Common
{
    public class CustomRoleProvider : RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override string ApplicationName { get; set; }

        public override void CreateRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new System.NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new System.NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new System.NotImplementedException();
            //using (var usersContext = new UsersContext())
            //{
            //    return usersContext.Roles.Select(r => r.RoleName).ToArray();
            //}
        }

        public override string[] GetRolesForUser(string username)// lấy danh sách nhóm quyền by usernam
        {
            UsersServiceClient _UsersSrv = new UsersServiceClient();
            var userRoles = _UsersSrv.GetRolesByUser(username);// lấy danh sách nhóm quyền by usernam
            var result = new string[0];
            if(userRoles!=null && userRoles.Data!=null && userRoles.Data.resultObject.Count>0)
            {
                result = userRoles.Data.resultObject.Select(item => item.Id.ToString().ToUpper()).ToArray();
            }
            return result;
        }

        public string[] GetCodeCookieForUser(string username)// lấy danh sách cookied quyền chức năng của user trong quyền chức năng
        {
            UsersServiceClient _UsersSrv = new UsersServiceClient();
            var userRights = _UsersSrv.GetRightsByUser(username); //lấy danh sách quyền chức năng của user
            var result = new string[0];
            if (userRights != null && userRights.Data != null && userRights.Data.resultObject!=null && userRights.Data.resultObject.Count > 0)
            {
                result = userRights.Data.resultObject.Select(item => item.CodeCookie).ToArray();
            }
            return result;
        }
        public string[] GetRightsForUser(string username)
        {
            UsersServiceClient _UsersSrv = new UsersServiceClient();
            var userRights = _UsersSrv.GetRightsByUser(username);
            var result = new string[0];
            if (userRights != null && userRights.Data != null && userRights.Data.resultObject!=null&& userRights.Data.resultObject.Count > 0)
            {
                result = userRights.Data.resultObject.Select(item => item.Id.ToString()).ToArray();
            }
            return result;
        }
        public Business.Entities.Domain.Roles GetRoleLeastByUser(string username)
        {
            UsersServiceClient _UsersSrv = new UsersServiceClient();
            var userRoles = _UsersSrv.GetRolesByUser(username);
            if (userRoles == null || userRoles.Data==null || userRoles.Data.resultObject==null)
            {
                return null;
            }
            return userRoles.Data.resultObject.FirstOrDefault();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new System.NotImplementedException();
            //using (var usersContext = new UsersContext())
            //{
            //    var user = usersContext.Users.SingleOrDefault(u => u.UserName == username);
            //    if (user == null)
            //        return false;
            //    return user.UserRoles != null && user.UserRoles.Select(u => u.Role).Any(r => r.RoleName == roleName);
            //}
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new System.NotImplementedException();
        }
    }
}