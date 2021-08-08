using MediatR;
using Moq;
using SmartCharger.API.Controllers;
using SmartCharger.Application.Groups.DTOs;
using System;
using System.Collections.Generic;

namespace SmartCharger.API.Tests.Base
{
    public abstract class BaseTests
    {
        protected readonly Mock<IMediator> _mediator;
        protected readonly GroupController _groupController;
        protected readonly int _groupId = new Random().Next();
        protected Group _group;
        protected List<Group> _listOfGroups;
        protected Group _nullGroup;
        protected List<Group> _emptyListOfGroups;

        protected BaseTests()
        {
            _mediator = new Mock<IMediator>();
            _groupController = new GroupController(_mediator.Object);

            _group = new Group
            {
                Id = _groupId,
                Name = "Group 1",
                CapacityInAmps = 100F
            };

            _listOfGroups = new List<Group> { _group };
            _emptyListOfGroups = new List<Group>();
        }
    }
}
