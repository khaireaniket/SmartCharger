using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCharger.Application.Connectors.Commands.CreateConnector;
using SmartCharger.Application.Connectors.Commands.DeleteConnector;
using SmartCharger.Application.Connectors.Commands.UpdateConnector;
using SmartCharger.Application.Connectors.Queries.GetAllConnectors;
using SmartCharger.Application.Connectors.Queries.GetConnectorById;
using System.Threading.Tasks;

namespace SmartCharger.API.Controllers
{
    /// <summary>
    /// Controller to delegate calls to Connector handler
    /// </summary>
    [ApiController]
    [Route("api/connectors")]
    public class ConnectorController : ControllerBase
    {
        private IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator"></param>
        public ConnectorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Endpoint used to fetch all the Connectors
        /// </summary>
        /// <returns>List of Connectors</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get()
        {
            var listOfConnectors = await _mediator.Send(new GetAllConnectorsQuery());

            if (listOfConnectors == null || listOfConnectors.Count <= 0)
                return NoContent();

            return Ok(listOfConnectors);
        }

        /// <summary>
        /// Endpoint used to fetch a Connector by ConnectorId
        /// </summary>
        /// <param name="query">ConnectorId</param>
        /// <returns>Connector details</returns>
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] GetConnectorByIdQuery query)
        {
            var connector = await _mediator.Send(query);

            if (connector == null)
                return NotFound(query.Id);

            return Ok(connector);
        }

        /// <summary>
        /// Endpoint will be used to create a new Connector
        /// </summary>
        /// <param name="command">Create Connector details</param>
        /// <returns>Id of created Connector</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateConnectorCommand command)
        {
            var createdConnectorId = await _mediator.Send(command);
            return Ok(createdConnectorId);
        }

        /// <summary>
        /// Endpoint used to update an existing Connector
        /// </summary>
        /// <param name="command">Update Connector details</param>
        /// <returns>Id of updated Connector</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromBody] UpdateConnectorCommand command)
        {
            var updatedConnectorId = await _mediator.Send(command);

            if (updatedConnectorId == 0)
                return NotFound(command.Id);

            return Ok(updatedConnectorId);
        }

        /// <summary>
        /// Endpoint used to remove a Connector
        /// </summary>
        /// <param name="command">Connector Id</param>
        /// <returns>No data</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteConnectorCommand command)
        {
            var isDeleted = await _mediator.Send(command);

            if (isDeleted == false)
                return NotFound(command.Id);

            return NoContent();
        }
    }
}
