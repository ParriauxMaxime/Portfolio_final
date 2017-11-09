using Xunit;
using DataAccessLayer;
using WebService.Controllers;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;

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

        [Fact]
        public void Comment_Object_HasFields()
        {
            var comment = new Comment();
            Assert.Equal(0, comment.Id);
            Assert.Equal(0, comment.score);
            Assert.Null(comment.text);
            Assert.Equal(0, comment.userId);
        }

        // Database (without Mock)

        [Fact]
        public void GetTags_ReturnsAllTags()
        {
            var service = new DataService();
            var tags = service.GetTagRepository();
            Assert.Equal(1874, tags.Count());
            Assert.Equal("vb.net", tags.GetByID(1).TagName);
        }

        // Web Service (with Mock)

        // GET

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

        // POST

        [Fact]
        public void PostAccount_UpdateWithGivenId_ReturnsOk()
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
        public void PostAccount_Update_ReturnsBadRequest()
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

        [Fact]
        public void PostAccount_Update_ReturnsOk()
        {
            var dataServiceMock = new Mock<IDataService>();
            dataServiceMock
                .Setup(o => o.GetAccountRepository())
                .Returns(new DataAccessLayer.Repository.GenericWritableRepository<Account>(new DatabaseContext()));
            var urlHelperMock = new Mock<IUrlHelper>();

            var ctrl = new AccountController(dataServiceMock.Object);
            ctrl.Url = urlHelperMock.Object;

            var response = ctrl.Post("{ \"id\" : 1 ,\"name\" : \"newaccname\", \"creationDate\" : \"2010-10-15T15:30:25\" }");

            Assert.IsType<OkResult>(response);

        }

        // PUT and DELETE

        [Fact]
        public void PostAccount_CreateAndDelete_ReturnsOk()
        {
            var dataServiceMock = new Mock<IDataService>();
            dataServiceMock
                .Setup(o => o.GetAccountRepository())
                .Returns(new DataAccessLayer.Repository.GenericWritableRepository<Account>(new DatabaseContext()));
            var urlHelperMock = new Mock<IUrlHelper>();

            var ctrl = new AccountController(dataServiceMock.Object);
            ctrl.Url = urlHelperMock.Object;

            var responseCreate = ctrl.Put("{ \"Name\" : \"Account for Delete\", \"CreationDate\" : \"2017-10-15T15:30:25\" }");

            Assert.IsType<CreatedResult>(responseCreate);

            var objectResult = responseCreate as CreatedResult;
            var account = objectResult.Value as GenericModel;

            var responseDelete = ctrl.Delete(account.Id);

            Assert.IsType<OkObjectResult>(responseDelete);
        }

        [Fact]
        public void PostAccount_Create_ReturnsBadRequest()
        {
            var dataServiceMock = new Mock<IDataService>();
            dataServiceMock
                .Setup(o => o.GetAccountRepository())
                .Returns(new DataAccessLayer.Repository.GenericWritableRepository<Account>(new DatabaseContext()));
            var urlHelperMock = new Mock<IUrlHelper>();

            var ctrl = new AccountController(dataServiceMock.Object);
            ctrl.Url = urlHelperMock.Object;

            // incorrect json string (therefor BadRequestResult)
            var response = ctrl.Put("{ null }");

            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void PostAccount_Delete_ReturnsNotFound()
        {
            var dataServiceMock = new Mock<IDataService>();
            dataServiceMock
                .Setup(o => o.GetAccountRepository())
                .Returns(new DataAccessLayer.Repository.GenericWritableRepository<Account>(new DatabaseContext()));
            var urlHelperMock = new Mock<IUrlHelper>();

            var ctrl = new AccountController(dataServiceMock.Object);
            ctrl.Url = urlHelperMock.Object;

            var response = ctrl.Delete(1000);

            Assert.IsType<NotFoundResult>(response);

        }


    }
}
