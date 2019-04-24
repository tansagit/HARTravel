using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace HARTravel.Models
{
    public class MyProvider : RoleProvider
    {
        public override string ApplicationName { get; set; }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            if (!username.StartsWith("e")) return new string[] { };
            using (HARTravelDBEntities db = new HARTravelDBEntities())
            {
                int eid = int.Parse(username.Substring(1));     //sub string bo ki tu dau c hoac e

                var user = (from e in db.Employees
                            where e.Id == eid
                            select new
                            {
                                e.Role.RoleName
                            }
                           ).SingleOrDefault();

                return new string[] { user.RoleName };
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            if (!username.StartsWith("e")) return false;    //neu user ko phai la employee thi tra ve false
            using (HARTravelDBEntities db = new HARTravelDBEntities())
            {
                int eid = int.Parse(username.Substring(1));
                var user = (from e in db.Employees
                            where e.Id == eid
                            select new
                            {
                                e.Role.RoleName
                            }
                           ).FirstOrDefault();
                return user.RoleName.Equals(roleName);
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}