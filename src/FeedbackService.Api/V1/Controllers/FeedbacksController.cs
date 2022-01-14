using FeedbackService.Core.Interfaces.Services;
using FeedbackService.Core.Models;
using FeedbackService.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedbackService.Api.V1.Controllers
{
    [Route("api/" + ApiConstants.ServiceName + "/v{api-version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IDateTimeService _dateTimeService;
        private readonly ISCBApi _scbApi;
        public FeedbacksController(IFeedbackService feedbackService, IDateTimeService dateTimeService, ISCBApi scbApi)
        {
            _feedbackService = feedbackService ?? throw new ArgumentNullException(nameof(feedbackService));
            _dateTimeService = dateTimeService ?? throw new ArgumentNullException(nameof(dateTimeService));
            _scbApi = scbApi;
        }

        /// <summary>
        /// Get all the feedbacks.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// - Tables used. => Feedback
        /// </remarks>        
        [HttpGet(Name = "GetFeedbacks")]
        //[SwaggerOperation("GetFeedbacks")]
        //[Route("Getfeedbacks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacks()
        {
            var response = await _feedbackService.GetAllFeedbacks().ConfigureAwait(false);
            var xxxx = await _scbApi.GetToken("xxx", "xxx");
            if (response == null)
            {
                return NoContent();
            }
            return Ok(response);
        }

        /// <summary>
        /// Get feedback by Id.
        /// </summary>
        /// <returns>Feedback</returns>
        /// <remarks>
        /// - Tables used. => Feedback
        /// </remarks> 
        [HttpGet("{id}", Name = "GetFeedbackById")]
        //[SwaggerOperation("GetFeedbacks")]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Feedback>> GetFeedbackById(int id)
        {
            var response = await _feedbackService.GetFeedbackById(id).ConfigureAwait(false);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
        }

        /// <summary>
        /// Create feedback.
        /// </summary>
        /// <returns>Feedback</returns>
        /// <remarks>
        /// - Tables used. => Feedback
        /// </remarks> 
        [HttpPost]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Feedback>> CreateFeedback(Feedback feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var response = await _feedbackService.CreateFeedback(feedback).ConfigureAwait(false);

            return CreatedAtRoute(nameof(GetFeedbackById), new { id = response.Id }, response);
        }

        /// <summary>
        /// DeleteFeedback
        /// </summary>
        /// <param name="id"></param>
        /// <returns>true/false</returns>
        /// <remarks>
        /// - Tables used => Feedback
        /// </remarks>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteFeedback(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            
            return await _feedbackService.DeleteFeedback(id).ConfigureAwait(false);            
        }

        /// <summary>
        /// Update feedback.
        /// </summary>
        /// <returns>true/false</returns>
        /// <remarks>
        /// - Tables used. => Feedback
        /// </remarks> 
        [HttpPut("{id}", Name = "UpdateFeedback")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> UpdateFeedback(int id, Feedback feedback)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (id <= 0)
            {
                return BadRequest();
            }

            return await _feedbackService.UpdateFeedback(id, feedback).ConfigureAwait(false);            
        }     
    }
}