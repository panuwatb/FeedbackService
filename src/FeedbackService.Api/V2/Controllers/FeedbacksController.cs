using FeedbackService.Core.Interfaces.Services;
using FeedbackService.Core.Models;
using FeedbackService.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FeedbackService.Api.V2.Controllers
{
    [Route("api/" + ApiConstants.ServiceName + "/v{api-version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        public FeedbacksController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService ?? throw new ArgumentNullException(nameof(feedbackService));
        }

        /// <summary>
        /// Get all the feedbacks.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// - Tables used. => Feedback
        /// </remarks>        
        [HttpGet]
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
            if (response == null)
            {
                return NoContent();
            }
            return Ok(response);
        }
    }
}
