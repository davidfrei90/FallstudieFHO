#region

using System;
using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.BusinessComponents;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.BL.DtoAdapters;
using HsrOrderApp.SharedLibraries.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace HsrOrderApp.Test.BL.Services.DtoAdapters
{
    /// <summary>
    /// Summary description for UserAdapterTestCase
    /// </summary>
    [TestClass]
    public class SecurityAdapterTestCase
    {
        private TestContext testContextInstance;

        public SecurityAdapterTestCase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        [TestMethod]
        public void TestUsersToDtos()
        {
            User user = new User() {UserId = 1, UserName = "FakeUserName", Password = "FakePassword", Version = 0};
            Assert.AreEqual(true, user.IsValid);

            IQueryable<User> users = new List<User>() {user}.AsQueryable();
            IList<UserListDTO> userDtos = SecurityAdapter.UsersToDtos(users);
            Assert.AreEqual<int>(1, userDtos.Count());

            UserListDTO dto = userDtos.First();
            Assert.AreEqual<int>(user.UserId, dto.Id);
            Assert.AreEqual<string>(user.UserName, dto.UserName);
            //Assert.AreEqual<string>(user.Customer.ToString(), dto.CustomerName);
            Assert.AreEqual(true, dto.IsValid);
        }

        [TestMethod]
        public void TestUserToDto()
        {
            User user = new User() {UserId = 1, UserName = "FakeName", Password = "Password", Version = 0};
            Role role = new Role() {RoleId = 1, RoleName = "FakeRoleName", Version = 0};
            user.Roles = new List<Role>() {role}.AsQueryable();
            Assert.AreEqual(true, user.IsValid);
            Assert.AreEqual(true, role.IsValid);

            UserDTO dto = SecurityAdapter.UserToDTO(user);
            Assert.AreEqual<int>(user.UserId, dto.Id);
            Assert.AreEqual<string>(user.UserName, dto.UserName);
            Assert.AreEqual<string>(user.Password, dto.Password);
            Assert.AreEqual(user.Version, dto.Version);
            Assert.AreEqual<int>(1, dto.Roles.Count());

            RoleDTO dtoRole = dto.Roles.First();
            Assert.AreEqual<int>(role.RoleId, dtoRole.Id);
            Assert.AreEqual<String>(role.RoleName, dtoRole.RoleName);
            Assert.AreEqual(role.Version, dtoRole.Version);
            Assert.AreEqual(true, dto.IsValid);
            Assert.AreEqual(true, dtoRole.IsValid);
        }


        [TestMethod]
        public void TestDtoToUser()
        {
            RoleDTO roleDTO = new RoleDTO() { Id = 1, RoleName = "FakeName", Version = 0 };
            UserDTO dto = new UserDTO() { Id = 1, UserName = "FakeUsername", Password = "FakePassword", Version = 1 };
            dto.Roles.Add(roleDTO);
            Assert.AreEqual(true, dto.IsValid);
            Assert.AreEqual(true, roleDTO.IsValid);

            User user = SecurityAdapter.DtoToUser(dto);
            Assert.AreEqual<int>(dto.Id, user.UserId);
            Assert.AreEqual<string>(dto.UserName, user.UserName);
            Assert.AreEqual<string>(dto.Password, user.Password);
            Assert.AreEqual(dto.Version, user.Version);
            Assert.AreEqual(true, user.IsValid);
        }

        [TestMethod]
        public void TestGetChangeItems()
        {
            UserDTO userDTO = new UserDTO();
            userDTO.MarkChildForInsertion(new RoleDTO { Id = 1, RoleName = "FakeRoleName", Version = 0 });
            userDTO.MarkChildForUpdate(new RoleDTO { Id = 2, RoleName = "FakeRoleName", Version = 0 });
            userDTO.MarkChildForDeletion(new RoleDTO { Id = 3, RoleName = "FakeRoleName", Version = 0 });

            IEnumerable<ChangeItem> changeItems = SecurityAdapter.GetChangeItems(userDTO);
            Assert.AreEqual<int>(3, changeItems.Count());
        }
    }
}