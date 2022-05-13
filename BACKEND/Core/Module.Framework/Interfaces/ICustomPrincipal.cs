using System.Collections.Generic;
using System.Dynamic;
using System.Security.Principal;

namespace Module.Framework.Interfaces
{ interface ICustomPrincipal : System.Security.Principal.IPrincipal
    {
        string UserName { get; set; }
        string UserId { get; set; }
        string Right { get; set; }
        string UserFullName { get; set; }
        string Role { get; set; }
        string DefaultController { get; set; }
        string DefaultAction { get; set; }
        int NgonNguID { get; set; }
        string Area { get; set; }
    }
   
    public class CustomPrincipal : ICustomPrincipal
    {
        public IIdentity Identity { get; private set; }

        public CustomPrincipal(string username)
        {
            this.Identity = new GenericIdentity(username);
        }

        public bool IsInRole(string role)
        {
            return Identity != null && Identity.IsAuthenticated &&
                   !string.IsNullOrWhiteSpace(role) && this.Role.Contains(role);
        }

        public string UserName { get; set; }
        public string UserId { get; set; }
        public long WebsiteID { get; set; }
        public int NgonNguID { get; set; }
        public string Right { get; set; }
        public string Role { get; set; }
        public string DefaultController { get; set; }
        public string DefaultAction { get; set; }
        public string Area { get; set; }
        public string UserFullName { get; set; }
        public int? IdBranch { get; set; }
        public bool WorkingHour { get; set; }
        public long UserPortalID { get; set; }
        public long PhongBanID { get; set; }
        public long DonViID { get; set; }
    }

    public class CustomPrincipalSerializedModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string Right { get; set; }
        public string Role { get; set; }
        public string DefaultController { get; set; }
        public string DefaultAction { get; set; }
        public bool WorkingHour { get; set; }
        public long WebsiteID { get; set; }
        public int NgonNguID { get; set; }
        public long UserPortalID { get; set; }
        public long PhongBanID { get; set; }
        public long DonViID { get; set; }
    }
}
