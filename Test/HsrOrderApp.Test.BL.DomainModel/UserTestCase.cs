#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace HsrOrderApp.Test.BL.DomainModel
{
    /// <summary>
    /// Summary description for UserTestCase
    /// </summary>
    [TestClass]
    public class UserTestCase
    {
        private User user;

        [TestInitialize]
        public void SetUp()
        {
            user = new User();
        }

        [TestCleanup]
        public void TearDown()
        {
            user = null;
        }

        #region ctors tests

        [TestMethod]
        public void Build()
        {
            user = new User();
            Helper.AssertEmptiness(user, "UserId", "UserName" /*, "Password"*/, "Roles");
            Assert.AreNotEqual<int>(0, user.Password.ToString().Count());
            Assert.IsNull(user.Customer);
        }

        #endregion

        #region Property Assignment

        [TestMethod]
        public void PropsAssignments()
        {
            Helper.TestProperty<int>(123, "UserId", user);
            Helper.TestProperty<string>("FakeName", "UserName", user);
            Helper.TestProperty<string>("FakePassword", "Password", user);
            user.Customer = new Customer();
            Assert.IsNotNull(user.Customer);
            user.Roles = new List<Role>() {new Role()}.AsQueryable();
            Assert.AreEqual<int>(1, user.Roles.Count());
        }

        [TestMethod]
        public void Validation()
        {
            user = new User()
                       {
                           UserId = 1,
                           UserName = "FakeUserName",
                           Password = "FakePassword",
                           Version = 0
                       };
            Assert.IsTrue(user.IsValid, "Should be valid");
        }

        [TestMethod]
        public void ValidationFails()
        {
            user = new User();
            Assert.IsFalse(user.IsValid, "Should be invalid");
        }

        #endregion
    }
}