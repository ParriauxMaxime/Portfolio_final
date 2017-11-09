using System;
using Xunit;
using DataAccessLayer;
using WebService.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Net;
using Newtonsoft.Json;

namespace Tests
{
    public class Tests
    {

        // Model

        [Fact]
        public void Tag_Object_HasIdAndTagName()
        {
            var tag = new Tag();
            Assert.Equal(0, tag.Id);
            Assert.Null(tag.TagName);
        }

        // Database

        [Fact]
        public void GetTags_ReturnsAllTags()
        {
            var service = new DataService();
            var tags = service.GetTagRepository();
            Assert.Equal(1874, tags.Count());
            Assert.Equal("vb.net", tags.GetByID(1).TagName);
        }

        // Web Service

        [Fact]
        public void GetUser_ValidId_ReturnsOk()
        {
            var dataServiceMock = new Mock<IDataService>();
            dataServiceMock
                .Setup(o => o.GetUserRepository())
                .Returns(new DataAccessLayer.Repository.GenericReadableRepository<User>(new DatabaseContext()));
            var urlHelperMock = new Mock<IUrlHelper>();

            var ctrl = new UserController(dataServiceMock.Object);
            ctrl.Url = urlHelperMock.Object;

            var response = ctrl.GetByID(1);

            Assert.IsType<OkObjectResult>(response);

        }

        [Fact]
        public void GetUser_InvalidId_ReturnsNotFound()
        {
            var dataServiceMock = new Mock<IDataService>();
            dataServiceMock
                .Setup(o => o.GetUserRepository())
                .Returns(new DataAccessLayer.Repository.GenericReadableRepository<User>(new DatabaseContext()));
            var urlHelperMock = new Mock<IUrlHelper>();

            var ctrl = new UserController(dataServiceMock.Object);
            ctrl.Url = urlHelperMock.Object;

            var response = ctrl.GetByID(2);

            Assert.IsType<NotFoundObjectResult>(response);

        }

        [Fact]
        public void GetPosts_ReturnsOk()
        {
            var dataServiceMock = new Mock<IDataService>();
            dataServiceMock
                .Setup(o => o.GetPostRepository())
                .Returns(new DataAccessLayer.Repository.GenericReadableRepository<Post>(new DatabaseContext()));
            var urlHelperMock = new Mock<IUrlHelper>();

            var ctrl = new PostController(dataServiceMock.Object);
            ctrl.Url = urlHelperMock.Object;

            var response = ctrl.Get();

            Assert.IsType<OkObjectResult>(response);

        }

        // This test fails
        [Fact]
        public void PostAccount_ReturnsOk()
        {
            var dataServiceMock = new Mock<IDataService>();
            dataServiceMock
                .Setup(o => o.GetAccountRepository())
                .Returns(new DataAccessLayer.Repository.GenericWritableRepository<Account>(new DatabaseContext()));
            var urlHelperMock = new Mock<IUrlHelper>();

            var ctrl = new AccountController(dataServiceMock.Object);
            ctrl.Url = urlHelperMock.Object;

            var response = ctrl.Post(1, "{ \"name\" : \"newacc\", \"creationDate\" : \"2010-10-15T15:30:25\" }");

            Assert.IsType<OkResult>(response);

        }

        [Fact]
        public void PostAccount_ReturnsBadRequest()
        {
            var dataServiceMock = new Mock<IDataService>();
            dataServiceMock
                .Setup(o => o.GetAccountRepository())
                .Returns(new DataAccessLayer.Repository.GenericWritableRepository<Account>(new DatabaseContext()));
            var urlHelperMock = new Mock<IUrlHelper>();

            var ctrl = new AccountController(dataServiceMock.Object);
            ctrl.Url = urlHelperMock.Object;

            var response = ctrl.Post("{ null }");

            Assert.IsType<BadRequestResult>(response);

        }


    }
}
