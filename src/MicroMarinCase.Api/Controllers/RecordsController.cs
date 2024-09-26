using Azure.Core;
using FluentValidation;
using MediatR;
using MicroMarinCase.Api.Filters;
using MicroMarinCase.Application.UseCases.RecordUseCases.Commands;
using MicroMarinCase.Application.UseCases.RecordUseCases.Queries;
using MicroMarinCase.Domain.AggregateRootModels.RecordModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MicroMarinCase.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecordsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{recordType}")]
        public async Task<IActionResult> Create(string recordType, [FromBody] JsonObject request, CancellationToken cancellationToken = default)
        {
            var command = new CreateRecordCommand() { Data = request, RecordType = recordType };

            await _mediator.Send(command);

            return Ok();
        }

        [HttpPost("records/filter")]
        public async Task<IActionResult> GetRecords([FromBody] GetRecordQuery query, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id,JsonObject data, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new UpdateRecordCommand() { Id=id,Data=data});

            if (result.IsSuccess)
                return NoContent();
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id,CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(new DeleteRecordCommand() { Id = id });

            if(result.IsSuccess)
                return NoContent();
            return BadRequest(result);

        }
    }   
}
