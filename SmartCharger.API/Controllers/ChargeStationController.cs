using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCharger.Application.ChargeStations.Commands.CreateChargeStation;
using SmartCharger.Application.ChargeStations.Commands.DeleteChargeStation;
using SmartCharger.Application.ChargeStations.Commands.UpdateChargeStation;
using SmartCharger.Application.ChargeStations.Queries.GetAllChargeStations;
using SmartCharger.Application.ChargeStations.Queries.GetChargeStationById;
using System.Threading.Tasks;

namespace SmartCharger.API.Controllers
{
    /// <summary>
    /// Controller to delegate calls to Charge Station handler
    /// </summary>
    [ApiController]
    [Route("api/chargestations")]
    public class ChargeStationController : ControllerBase
    {
        private IMediator _mediator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator"></param>
        public ChargeStationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Endpoint used to fetch all the Charge Stations
        /// </summary>
        /// <returns>List of Charge Stations</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get()
        {
            var listOfChargeStations = await _mediator.Send(new GetAllChargeStationsQuery());

            if (listOfChargeStations == null || listOfChargeStations.Count <= 0)
                return NoContent();

            return Ok(listOfChargeStations);
        }

        /// <summary>
        /// Endpoint used to fetch a Charge Station by ChargeStationId
        /// </summary>
        /// <param name="query">ChargeStationId</param>
        /// <returns>Group details</returns>
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] GetChargeStationByIdQuery query)
        {
            var chargeStation = await _mediator.Send(query);

            if (chargeStation == null)
                return NotFound(query.Id);

            return Ok(chargeStation);
        }

        /// <summary>
        /// Endpoint will be used to create a new Charge Station
        /// </summary>
        /// <param name="command">Create Charge Station details</param>
        /// <returns>Id of created Charge Station</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateChargeStationCommand command)
        {
            var createdChargeStationId = await _mediator.Send(command);
            return Ok(createdChargeStationId);
        }

        /// <summary>
        /// Endpoint used to update an existing Charge Station
        /// </summary>
        /// <param name="command">Update Charge Station details</param>
        /// <returns>Id of updated Charge Station</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromBody] UpdateChargeStationCommand command)
        {
            var updatedchargeStationId = await _mediator.Send(command);

            if (updatedchargeStationId == 0)
                return NotFound(command.Id);

            return Ok(updatedchargeStationId);
        }

        /// <summary>
        /// Endpoint used to remove a Charge Station
        /// </summary>
        /// <param name="command">Charge Station Id</param>
        /// <returns>No data</returns>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteChargeStationCommand command)
        {
            var isDeleted = await _mediator.Send(command);

            if (isDeleted == false)
                return NotFound(command.Id);

            return NoContent();
        }
    }
}
