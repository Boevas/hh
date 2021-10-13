using System;
using Xunit;


using Moq;
using WebApplication2.Models;
using WebApplication2.Controllers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using WebApplication2.MiddleWare.LoggerManager;
namespace TestProject2
{
    public class UsersControllerTests
    {
        private readonly Mock<IRepository<User>> mockRepository;
        private readonly UsersController Controller;

        public UsersControllerTests()
        {
            mockRepository = new Mock<IRepository<User>>();
            Controller = new UsersController(mockRepository.Object, new LoggerManagerNLog());
        }

        #region Test_Get
        
        [Fact]
        //Test Get() return Type 
        public void Test1()
        {
            var result = Controller.Get();
            Assert.IsType<Task<IEnumerable<User>>>(result);
        }

        [Fact]
        //Test Get() return Type and Count
        async public void Test2()
        {
            
            mockRepository.Setup(repo => repo.Get())
                .ReturnsAsync(new List<User>() { new User(), new User() });

            var result = await Controller.Get();
            var actionResult = Assert.IsType<List<User>>(result);
            var users = Assert.IsType<List<User>>(actionResult);

            Assert.Equal(2, users.Count);
        }
        #endregion Test_Get

        #region Test_Get(int)

        [Fact]
        //Test Get(id) return Type
        public void Test3()
        {
            var result = Controller.Get(It.IsAny<int>());
            Assert.IsType<Task<ActionResult<User>>>(result);
        }

        [Fact]
        //Test Get(id) return Type and Object
        async public void Test4()
        {
            User user = new ();
            user.Id = 1;
            user.Name = "Иванов";
            user.DepartmentId = 5;

            mockRepository
            .Setup(repo => repo.Get(It.IsAny<int>()))
            .ReturnsAsync(user);

            var result = await Controller.Get(user.Id);
            var actionResult = Assert.IsType<ActionResult<User>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var testuser = Assert.IsType<User>(okResult.Value);

            Assert.Equal(user.Id, testuser.Id);
            Assert.Equal(user.Name, testuser.Name);
            Assert.Equal(user.DepartmentId, testuser.DepartmentId);
        }

        [Fact]
        //Test Get(id) return Object not Found
        async public void Test5()
        {
            var result = await Controller.Get(2);
            var actionResult = Assert.IsType<ActionResult<User>>(result);
            Assert.IsType<NotFoundResult>(actionResult.Result);
        }
        #endregion Test_Get(int)

        #region Test_Post
        [Fact]
        //Test Post(obj) return Type
        public void Test6()
        {
            User user = new();
            user.Id = 1;
            user.Name = "Иванов";
            user.DepartmentId = 5;

            var result = Controller.Post(user);
            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        //Test return Post(obj) Type and Object
        async public void Test7()
        {
            List<User> usersList = new();

            //Post
            mockRepository.Setup(repo => repo.Post(It.IsAny<User>()))
            .ReturnsAsync
                (
                    (User user) =>
                    {
                        usersList.Add(user);
                        return true;
                    }
                );

            //Get
            mockRepository.Setup(repo => repo.Get())
                .ReturnsAsync(usersList);

            //Get(int)
            mockRepository.Setup(repo => repo.Get(It.IsAny<int>()))
            .ReturnsAsync
                (
                    (int Id) =>
                    {
                        return usersList.First(x => x.Id == Id);
                    }
                );

            User user = new();
            user.Id = 1;
            user.Name = "Иванов";
            user.DepartmentId = 5;

            //Post user
            {
                var result = await Controller.Post(user);
                var okResult = Assert.IsType<OkObjectResult>(result);
            }

            //Get users
            {
                var result = await Controller.Get();
                var actionResult = Assert.IsType<List<User>>(result);
                var users = Assert.IsType<List<User>>(actionResult);
                Assert.Single(users);
            }

            //Check user
            {
                var result = await Controller.Get(user.Id);
                var actionResult = Assert.IsType<ActionResult<User>>(result);
                var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                var testuser = Assert.IsType<User>(okResult.Value);

                Assert.Equal(user.Id, testuser.Id);
                Assert.Equal(user.Name, testuser.Name);
                Assert.Equal(user.DepartmentId, testuser.DepartmentId);
            }
        }

        [Fact]
        //Test Post(obj) return Type and Objects already exist
        async public void Test8()
        {
            List<User> usersList = new();

            User user = new();
            user.Id = 1;
            user.Name = "Иванов";
            user.DepartmentId = 5;

            //Post
            mockRepository.Setup(repo => repo.Post(It.IsAny<User>()))
            .ReturnsAsync
                (
                    (User user) =>
                    {
                        if(null != usersList.Find(x => x == user))
                            return false;

                        usersList.Add(user);
                        return true;
                    }
                );
            //Get(obj)
            mockRepository.Setup(repo => repo.Get(It.IsAny<User>()))
            .ReturnsAsync
                (
                    (User user) =>
                    {
                        return usersList.FirstOrDefault(x => x.Id == user.Id);
                    }
                );

            {
                var result = await Controller.Post(user);
                Assert.IsType<OkObjectResult>(result);
            }
            {
                var result = await Controller.Post(user);
                Assert.IsType<ConflictObjectResult>(result);
            }
        }
        #endregion Test_Post

        #region Test_Put
        [Fact]
        //Test Put(obj) return Type
        public void Test9()
        {
            User user = new();
            user.Id = 1;
            user.Name = "Иванов";
            user.DepartmentId = 5;

            var result = Controller.Put(user);
            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        //Test Put(obj) return Type and Object
        async public void Test10()
        {
            List<User> usersList = new();

            User user1 = new();
            user1.Id = 1;
            user1.Name = "Иванов";
            user1.DepartmentId = 5;

            usersList.Add(user1);

            //Put
            mockRepository.Setup(repo => repo.Put(It.IsAny<User>()))
            .ReturnsAsync
                (
                    (User user) =>
                    {
                        User u = usersList.FirstOrDefault(x => x.Id == user.Id);
                        if (null == u)
                            return false;

                        u.Name = user.Name;
                        u.Department = user.Department;

                        return true;
                    }
                );
            //Get(obj)
            mockRepository.Setup(repo => repo.Get(It.IsAny<User>()))
            .ReturnsAsync
                (
                    (User user) =>
                    {
                        return usersList.FirstOrDefault(x => x.Id == user.Id);
                    }
                );
            //Get(int)
            mockRepository.Setup(repo => repo.Get(It.IsAny<int>()))
            .ReturnsAsync
                (
                    (int Id) =>
                    {
                        return usersList.FirstOrDefault(x => x.Id == Id);
                    }
                );

            user1.Name = "Петров";
            user1.DepartmentId = 2;

            //Put
            {
                var result = await Controller.Put(user1);
                var okResult = Assert.IsType<OkObjectResult>(result);
            }

            //Get
            {
                var result = await Controller.Get(user1.Id);
                var actionResult = Assert.IsType<ActionResult<User>>(result);
                var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                var testuser = Assert.IsType<User>(okResult.Value);

                Assert.Equal(user1.Id, testuser.Id);
                Assert.Equal(user1.Name, testuser.Name);
                Assert.Equal(user1.DepartmentId, testuser.DepartmentId);
            }
        }

        [Fact]
        //Test return Put(obj) Type and Object not found
        async public void Test11()
        {
            User user1 = new();
            user1.Id = 1;
            user1.Name = "Иванов";
            user1.DepartmentId = 5;

            var resultPut = await Controller.Put(user1);
            Assert.IsType<NotFoundResult>(resultPut);
        }
        #endregion Test_Put

        #region Test_Delete
        [Fact]
        //Test Delete(obj) return Type
        public void Test12()
        {
            User user = new();
            user.Id = 1;
            user.Name = "Иванов";
            user.DepartmentId = 5;

            var result = Controller.Delete(user.Id);
            Assert.IsType<Task<IActionResult>>(result);
        }

        [Fact]
        //Test Delete(obj) return Type and Object
        async public void Test13()
        {
            List<User> usersList = new();

            User user1 = new();
            user1.Id = 1;
            user1.Name = "Иванов";
            user1.DepartmentId = 5;
            usersList.Add(user1);

            User user2 = new();
            user2.Id = 2;
            user2.Name = "Петров";
            user2.DepartmentId = 9;
            usersList.Add(user2);

            User user3 = new();
            user3.Id = 3;
            user3.Name = "Сидоров";
            user3.DepartmentId = 3;
            usersList.Add(user3);

            //Delete
            mockRepository.Setup(repo => repo.Delete(It.IsAny<User>()))
            .ReturnsAsync
                (
                    (User user) =>
                    {
                        User u = usersList.FirstOrDefault(x => x.Id == user.Id);
                        if (null == u)
                            return false;

                        usersList.Remove(u);

                        return true;
                    }
                );
            //Get(obj)
            mockRepository.Setup(repo => repo.Get(It.IsAny<User>()))
            .ReturnsAsync
                (
                    (User user) =>
                    {
                        return usersList.FirstOrDefault(x => x.Id == user.Id);
                    }
                );
            //Get(int)
            mockRepository.Setup(repo => repo.Get(It.IsAny<int>()))
            .ReturnsAsync
                (
                    (int Id) =>
                    {
                        return usersList.FirstOrDefault(x => x.Id == Id);
                    }
                );
            //Get
            mockRepository.Setup(repo => repo.Get())
                .ReturnsAsync(usersList);

            //Delete
            {
                var result = await Controller.Delete(user2.Id);
                var okResult = Assert.IsType<OkObjectResult>(result);
            }


            //Count users
            {
                var result = await Controller.Get();
                var actionResult = Assert.IsType<List<User>>(result);
                var users = Assert.IsType<List<User>>(actionResult);
                Assert.Equal(2, users.Count);
            }


            //Get user1
            {
                var result = await Controller.Get(user1.Id);
                var actionResult = Assert.IsType<ActionResult<User>>(result);
                var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                var testuser = Assert.IsType<User>(okResult.Value);

                Assert.Equal(user1.Id, testuser.Id);
                Assert.Equal(user1.Name, testuser.Name);
                Assert.Equal(user1.DepartmentId, testuser.DepartmentId);
            }

            //Get user3
            {
                var result = await Controller.Get(user3.Id);
                var actionResult = Assert.IsType<ActionResult<User>>(result);
                var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
                var testuser = Assert.IsType<User>(okResult.Value);

                Assert.Equal(user3.Id, testuser.Id);
                Assert.Equal(user3.Name, testuser.Name);
                Assert.Equal(user3.DepartmentId, testuser.DepartmentId);
            }

        }
        #endregion Test_Delete
    }
}
