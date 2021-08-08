using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCharger.Application.Groups.Commands.CreateGroup;
using SmartCharger.Application.Groups.Commands.DeleteGroup;
using SmartCharger.Application.Groups.Commands.UpdateGroup;
using SmartCharger.Application.Groups.Queries.GetAllGroups;
using SmartCharger.Application.Groups.Queries.GetGroupById;
using System.Threading.Tasks;

namespace SmartCharger.API.Controllers
{
    /// <summary>
    /// Controller to delegate calls to Group handler
    /// </summary>
    [ApiController]
    [Route("api/groups")]
    public class GroupController : ControllerBase
    {
        private IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator"></param>
        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Endpoint used to fetch all the Groups
        /// </summary>
        /// <returns>List of Groups</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get()
        {
            var listOfGroups = await _mediator.Send(new GetAllGroupsQuery());

            if (listOfGroups == null || listOfGroups.Count <= 0)
                return NoContent();

            return Ok(listOfGroups);
        }

        /// <summary>
        /// Endpoint used to fetch a Group by GroupId
        /// </summary>
        /// <param name="query">GroupId</param>
        /// <returns>Group details</returns>
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] GetGroupByIdQuery query)
        {
            var group = await _mediator.Send(query);

            if (group == null)
                return NotFound(query.Id);

            return Ok(group);
        }

        /// <summary>
        /// Endpoint will be used to create a new Group
        /// </summary>
        /// <param name="command">Create Group details</param>
        /// <returns>Id of created Group</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateGroupCommand command)
        {
            var createdGroupId = await _mediator.Send(command);
            return Ok(createdGroupId);
        }

        /// <summary>
        /// Endpoint used to update an existing Group
        /// </summary>
        /// <param name="command">Update Group details</param>
        /// <returns>Id of updated Group</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromBody] UpdateGroupCommand command)
        {
            var updatedGroupId = await _mediator.Send(command);

            if (updatedGroupId == 0)
                return NotFound(command.Id);

            return Ok(updatedGroupId);
        }

        /// <summary>
        /// Endpoint used to remove a Group
        /// </summary>
        /// <param name="command">Group Id</param>
        /// <returns>No data</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteGroupCommand command)
        {
            var isDeleted = await _mediator.Send(command);

            if (isDeleted == false)
                return NotFound(command.Id);

            return NoContent();
        }
    }
}
