#region

using System;
using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.BusinessComponents;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.DAL.Data.Repositories;
using HsrOrderApp.SharedLibraries.SharedEnums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock2;

#endregion

namespace HsrOrderApp.Test.BL.BusinessComponents
{
    /// <summary>
    /// Summary description for UserServiceTestCase
    /// </summary>
    [TestClass]
    public class SecurityBusinessComponentTestCase
    {
        private ISecurityRepository context;
        private Mockery mockBuilder;
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestInitialize()]
        public void Initialize()
        {
            mockBuilder = new Mockery();
            context = mockBuilder.NewMock<ISecurityRepository>();
        }

        [TestMethod]
        public void TestGetUserById()
        {
            SecurityBusinessComponent service = new SecurityBusinessComponent(this.context);
            User user = new User() {UserId = 123};

            Expect.Once.On(context).Method("GetUserById").Will(Return.Value(user));
            User resultUser = service.GetUserById(123);
            Assert.AreEqual<decimal>(user.UserId, resultUser.UserId);
            mockBuilder.VerifyAllExpectationsHaveBeenMet();
        }


        [TestMethod]
        public void TestGetUserByCriteria()
        {
            SecurityBusinessComponent service = new SecurityBusinessComponent(this.context);
            Role role = new Role() {RoleId = 12345, RoleName = "FakeRoleName"};
            User user = new User {UserId = 456, UserName = "FakeUserName", Roles = new List<Role> {role}.AsQueryable()};
            IList<User> users = new List<User>();
            users.Add(user);

            foreach (UserSearchType type in Enum.GetValues(typeof (UserSearchType)))
            {
                Expect.Once.On(context).Method("GetAllUsers").Will(Return.Value(users.AsQueryable()));
                IQueryable<User> resultUsers = service.GetUsersByCriteria(type, "FakeUserName", "FakeRoleName");
                Assert.AreEqual<decimal>(1, resultUsers.Count());
                Assert.AreEqual<decimal>(user.UserId, resultUsers.First().UserId);
            }

            mockBuilder.VerifyAllExpectationsHaveBeenMet();
        }

        [TestMethod]
        public void TestStoreUser()
        {
            int userId = 123;
            SecurityBusinessComponent service = new SecurityBusinessComponent(this.context);
            User user = new User() {UserId = userId};
            List<ChangeItem> changeItems = new List<ChangeItem>
                                               {
                                                   new ChangeItem(ChangeType.ChildInsert, new Role()),
                                                   new ChangeItem(ChangeType.ChildUpate, new Role()),
                                                   new ChangeItem(ChangeType.ChildDelete, new Role())
                                               };

            Expect.Once.On(context).Method("SaveUser").Will(Return.Value(userId));
            Expect.Once.On(context).Method("AddUserToRole");
            Expect.Once.On(context).Method("AddUserToRole");
            Expect.Once.On(context).Method("RemoveUserFromRole");
            int resultUserId = service.StoreUser(user, changeItems);
            Assert.AreEqual<int>(userId, resultUserId);

            mockBuilder.VerifyAllExpectationsHaveBeenMet();
        }


        [TestMethod]
        public void TestDeleteUser()
        {
            SecurityBusinessComponent service = new SecurityBusinessComponent(this.context);
            Expect.Once.On(context).Method("DeleteUser").With(1);
            service.DeleteUser(1);
            mockBuilder.VerifyAllExpectationsHaveBeenMet();
        }
    }
}