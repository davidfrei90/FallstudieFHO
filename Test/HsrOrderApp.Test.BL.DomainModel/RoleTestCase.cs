#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace HsrOrderApp.Test.BL.DomainModel
{
    /// <summary>
    /// Summary description for RoleTestCase
    /// </summary>
    [TestClass]
    public class RoleTestCase
    {
        private Role role;

        [TestInitialize]
        public void SetUp()
        {
            role = new Role();
        }

        [TestCleanup]
        public void TearDown()
        {
            role = null;
        }

        #region ctors tests

        [TestMethod]
        public void Build()
        {
            role = new Role();
            Helper.AssertEmptiness(role, "RoleId", "RoleName");
        }

        #endregion

        #region Property Assignment

        [TestMethod]
        public void PropsAssignments()
        {
            Helper.TestProperty<int>(123, "RoleId", role);
            Helper.TestProperty<string>("FakeRoleName", "RoleName", role);
            role.Users = new List<User>() {new User()}.AsQueryable();
            Assert.AreEqual<int>(1, role.Users.Count());
        }

        [TestMethod]
        public void Validation()
        {
            role = new Role()
                       {
                           RoleId = 1,
                           RoleName = "FakeRole",
                           Version = 0
                       };
            Assert.IsTrue(role.IsValid, "Should be valid");
        }

        [TestMethod]
        public void ValidationFails()
        {
            role = new Role();
            Assert.IsFalse(role.IsValid, "Should be invalid");
        }

        #endregion
    }
}