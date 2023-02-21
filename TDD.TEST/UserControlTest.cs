using Moq;
using TDD.API.Contracts;
using TDD.API.Entity;
using TDD.API.Controllers;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace TDD.TEST
{
    public class TestUserController
    {
        private readonly Mock<IUserService> _userService;
        private Fixture _fixture;
        public TestUserController()
        {
            _userService = new Mock<IUserService>();
            _fixture = new Fixture();
        }
        #region GetUserTest
        [Fact]
        public async Task Get_User_ReturnOk()
        {
            //arrange
            _fixture.Customize<User>(c => c.With(x => x.Id, 5 )
                                           .With(x=>x.Name, "dfdf")
                                           .With(x => x.Family, "nututu"));
            var user = _fixture.Create<User>();
            _userService.Setup(x => x.Get(5)).Returns(user);
            var userController = new UserController(_userService.Object);
            //Act
            var result = await userController.GetUser(5);
            var obj = result as ObjectResult;
            Assert.Equal(200, obj.StatusCode);

        }

        [Fact]
        public async Task Get_User_ReturnExeption()
        {
            //arrange
            _userService.Setup(x => x.Get(5)).Throws(new Exception());
            var userController = new UserController(_userService.Object);
            //Act
            var result = await userController.GetUser(5);
            var obj = result as ObjectResult;
            //Assert
            Assert.Equal(400, obj.StatusCode);

        }

        [Fact]
        public async Task Get_User_ReturnNotFound()
        {
            //arrange
            _userService.Setup(x => x.Get(900)).Returns<User>(null);
            var userController = new UserController(_userService.Object);
            //Act
            var result = await userController.GetUser(900);
            var obj = result as ObjectResult;
            //Assert
            Assert.Equal(404, obj.StatusCode);

        }
        #endregion

        #region GetAllUsersTest
        [Fact]
        public async Task GetAll_Users_ReturnOk()
        {
            //arrange
            var userList = _fixture.CreateMany<User>(3).ToList();
            _userService.Setup(x=>x.GetAll()).Returns(userList);
            var userController = new UserController(_userService.Object);
            //Act
            var result = await userController.GetUsers();
            var obj = result as ObjectResult;
            Assert.Equal(200,obj.StatusCode);
            
        }

        [Fact]
        public async Task GetAll_Users_ReturnExeption()
        {
            //arrange
            _userService.Setup(x => x.GetAll()).Throws(new Exception());
            var userController = new UserController(_userService.Object);
            //Act
            var result = await userController.GetUsers();
            var obj = result as ObjectResult;
            //Assert
            Assert.Equal(400, obj.StatusCode);

        }

        [Fact]
        public async Task GetAll_Users_ReturnNotFound()
        {
            //arrange
            _userService.Setup(x => x.GetAll()).Returns<User>(null);
            var userController = new UserController(_userService.Object);
            //Act
            var result = await userController.GetUsers();
            var obj = result as ObjectResult;
            //Assert
            Assert.Equal(404, obj.StatusCode);

        }
        #endregion

        [Fact]
        public async Task Post_User_ReturnOk()
        {
            //arrange
            var user = _fixture.Create<User>();
            _userService.Setup(x => x.Add(It.IsAny<User>())).Returns(user);
            var userController = new UserController(_userService.Object);
            //Act
            var result = await userController.AddUser(user);
            var obj = result as ObjectResult;
            Assert.Equal(200, obj.StatusCode);

        }

        [Fact]
        public async Task Post_User_ReturnExeption()
        {
            //arrange
            var user = _fixture.Create<User>();
            _userService.Setup(x => x.Add(user)).Throws(new Exception());
            var userController = new UserController(_userService.Object);
            //Act
            var result = await userController.AddUser(user);
            var obj = result as ObjectResult;
            //Assert
            Assert.Equal(400, obj.StatusCode);

        }

        [Fact]
        public async Task Delete_User_ReturnOk()
        {
            //arrange
            _userService.Setup(x => x.Delete(4)).Returns(true);
            var userController = new UserController(_userService.Object);
            //Act
            var result = await userController.DeleteUser(4);
            var obj = result as ObjectResult;
            //Assert
            Assert.Equal(200, obj.StatusCode);

        }

        [Fact]
        public async Task Delete_User_NotFound()
        {
            //arrange
            _userService.Setup(x => x.Delete(555)).Returns(false);
            var userController = new UserController(_userService.Object);
            //Act
            var result = await userController.DeleteUser(555);
            var obj = result as ObjectResult;
            //Assert
            Assert.Equal(404, obj.StatusCode);

        }
    }
}