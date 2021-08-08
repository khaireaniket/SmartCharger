using Microsoft.AspNetCore.Mvc;
using Moq;
using SmartCharger.API.Tests.Base;
using SmartCharger.Application.Groups.DTOs;
using SmartCharger.Application.Groups.Queries.GetAllGroups;
using SmartCharger.Application.Groups.Queries.GetGroupById;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SmartCharger.API.Tests
{
    public class GroupControllerTests : BaseTests
    {
        [Fact]
        public async Task GetAllGroups_Returns_Ok_With_ListOfGroups()
        {
            // Arrange
            _mediator.Setup(x => x.Send(It.IsAny<GetAllGroupsQuery>(), It.IsAny<CancellationToken>()))
                           .Returns(Task.FromResult(_listOfGroups));

            // Act
            var result = await _groupController.Get();

            // Assert
            Assert.NotNull(result);

            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.NotNull(okObjectResult.Value);

            var allGroups = (List<Group>)okObjectResult.Value;

            Assert.NotNull(allGroups);
            Assert.Equal(_listOfGroups.Count, allGroups.Count);
            Assert.Equal(_listOfGroups.First().Id, allGroups.First().Id);
        }

        [Fact]
        public async Task GetAllGroups_Returns_NoContent_With_EmptyResponse()
        {
            // Arrange
            _mediator.Setup(x => x.Send(It.IsAny<GetAllGroupsQuery>(), It.IsAny<CancellationToken>()))
                           .Returns(Task.FromResult(_emptyListOfGroups));

            // Act
            var result = await _groupController.Get();

            // Assert
            Assert.NotNull(result);

            var noContentObjectResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentObjectResult.StatusCode);
        }

        [Fact]
        public async Task GetGroupById_Returns_Ok_With_GroupDetails()
        {
            // Arrange
            _mediator.Setup(x => x.Send(It.IsAny<GetGroupByIdQuery>(), It.IsAny<CancellationToken>()))
                           .Returns(Task.FromResult(_group));

            // Act
            var result = await _groupController.Get(new GetGroupByIdQuery { Id = _groupId });

            // Assert
            Assert.NotNull(result);

            var okObjectResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okObjectResult.StatusCode);
            Assert.NotNull(okObjectResult.Value);

            var singleGroup = (Group)okObjectResult.Value;

            Assert.NotNull(singleGroup);
            Assert.Equal(_group.Id, singleGroup.Id);
        }

        [Fact]
        public async Task GetGroupById_Returns_NotFound_With_EmptyResponse()
        {
            // Arrange
            _mediator.Setup(x => x.Send(It.IsAny<GetGroupByIdQuery>(), It.IsAny<CancellationToken>()))
                           .Returns(Task.FromResult(_nullGroup));

            // Act
            var result = await _groupController.Get(new GetGroupByIdQuery());

            // Assert
            Assert.NotNull(result);

            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundObjectResult.StatusCode);
        }
    }
}
