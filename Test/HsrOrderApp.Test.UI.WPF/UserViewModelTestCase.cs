#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.PresentationLogic;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock2;

#endregion

namespace HsrOrderApp.Test.UI.WPF
{
    /// <summary>
    /// Summary description for UserViewModelTestCase
    /// </summary>
    [TestClass]
    public class UserViewModelTestCase
    {
        private Mockery mockBuilder;
        private IServiceFacade serviceFacade;

        private TestContext testContextInstance;

        public UserViewModelTestCase()
        {
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

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        [TestInitialize()]
        public void Initialize()
        {
            mockBuilder = new Mockery();
            serviceFacade = mockBuilder.NewMock<IServiceFacade>();
        }

        [TestMethod]
        public void TestUserViewModel()
        {
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.Service = serviceFacade;
            UserListDTO user = new UserListDTO() {Id = 1};
            IList<UserListDTO> users = new List<UserListDTO>() {user};
            Expect.Once.On(serviceFacade).Method("GetAllUsers").Will(Return.Value(users));
            userViewModel.LoadCommand.Command.Execute(null);

            Assert.AreEqual<int>(1, userViewModel.Items.Count);
            Assert.AreEqual(user, userViewModel.SelectedItem);
            Assert.AreEqual(Strings.UserViewModel_DisplayName, userViewModel.DisplayName);
        }

        [TestMethod]
        public void TestUserDetailModel()
        {
            RoleDTO roleDTO = new RoleDTO() {Id = 1, RoleName = "FakeName", Version = 0};
            UserDTO dto = new UserDTO() {Id = 1, UserName = "FakeUsername", Password = "FakePassword", Version = 1};
            dto.Roles.Add(roleDTO);

            UserDetailViewModel userDetailViewModel = new UserDetailViewModel(dto, false);
            userDetailViewModel.Service = serviceFacade;

            Expect.Once.On(serviceFacade).Method("StoreUser").With(dto);
            userDetailViewModel.SaveCommand.Command.Execute(null);

            Assert.AreEqual(dto, userDetailViewModel.Model);
            Assert.AreEqual(Strings.UserDetailViewModel_DisplayName, userDetailViewModel.DisplayName);

            RoleViewModel roleViewModel = (RoleViewModel) userDetailViewModel.ListViewModel;
            Assert.AreEqual(roleDTO, roleViewModel.Items.FirstOrDefault());
        }

        [TestMethod]
        public void TestRoleDetailViewModel()
        {
            RoleDTO roleDTO = new RoleDTO() {Id = 1, RoleName = "FakeName", Version = 0};
            RoleDetailViewModel viewModel = new RoleDetailViewModel(roleDTO, false);
            Assert.AreEqual(roleDTO, viewModel.Model);
            Assert.AreEqual(Strings.RoleDetailViewModel_DisplayName, viewModel.DisplayName);
        }
    }
}