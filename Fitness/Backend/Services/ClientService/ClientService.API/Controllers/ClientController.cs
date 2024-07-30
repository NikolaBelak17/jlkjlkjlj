﻿using Amazon.Util;
using ClientService.API.Entities;
using ClientService.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ClientService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IRepository _repository;

        public ClientController(IRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Client>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            var clients = await _repository.GetClients();
            return Ok(clients);
        }

        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClientSchedule>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ClientSchedule>>> GetClientsSchedules()
        {
            var schedules = await _repository.GetClientsSchedules();
            return Ok(schedules);
        }

        [HttpGet("{id}", Name = "GetClient")]
        [ProducesResponseType(typeof(IEnumerable<Client>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Client>> GetClientById(string id)
        {
            var result = await _repository.GetClientById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Route("[action]/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClientSchedule>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClientSchedule>> GetClientScheduleByClientId(string id)
        {
            var result = await _repository.GetClientScheduleByClientId(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Route("[action]/{clientId}/{weekId}")]
        [HttpGet]
        [ProducesResponseType(typeof(WeeklySchedule), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WeeklySchedule>> GetClientWeekSchedule(string clientId, int weekId)
        {
            var result = await _repository.GetClientWeekSchedule(clientId, weekId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Route("[action]/{name}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Client>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientsByName(string name)
        {
            var results = await _repository.GetClientsByName(name);
            return Ok(results);
        }

        [Route("[action]/{surname}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Client>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientsBySurname(string surname)
        {
            var results = await _repository.GetClientsBySurname(surname);
            return Ok(results);
        }

        [Route("[action]/{trainerId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Client>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Client>>> GetClientsByTrainerId(string trainerId)
        {
            var results = await _repository.GetClientsByTrainerId(trainerId);
            return Ok(results);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<Client>), StatusCodes.Status201Created)]
        public async Task<ActionResult<Client>> CreateClient([FromBody] Client client)
        {
            await _repository.CreateClient(client);
            return CreatedAtRoute("GetClient", new { id = client.Id }, client);

        }

        [HttpPut]
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateClient([FromBody] Client client)
        {
            return Ok(await _repository.UpdateClient(client));
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(typeof(ClientSchedule),StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateClientSchedule([FromBody] ClientSchedule clientSchedule)
        {
            return Ok(await _repository.UpdateClientSchedule(clientSchedule));
            
        }

        [Route("[action]")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> TrainingBooking([FromQuery]string clientId,[FromQuery] string trainerId,
                                                         [FromQuery]string trainingType,[FromQuery]int weekId,
                                                         [FromQuery]string dayId,[FromQuery] int startTime, 
                                                         [FromQuery]bool isBooking)
        {
            var result = await _repository.TrainingBooking(clientId,trainerId,trainingType,weekId,dayId,startTime,isBooking);
            if(result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}", Name = "DeleteClient")]
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteClient(string id)
        {
            return Ok(await _repository.DeleteClient(id));
        }

        [Route("[action]")]
        [HttpDelete]
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteAllClients()
        {
            await _repository.DeleteAllClients();
            return Ok();
        }
    }
}
