﻿using FeedbackService.Core.Interfaces.Repositories;
using FeedbackService.Core.Interfaces.Services;
using FeedbackService.Core.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackService.Core.Services
{
    public class FeedbacksService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly ILogger<FeedbacksService> _logger;
        public FeedbacksService(IFeedbackRepository feedbackRepository, ILogger<FeedbacksService> logger)
        {
            _feedbackRepository = feedbackRepository ?? throw new ArgumentNullException(nameof(feedbackRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Feedback> CreateFeedback(Feedback feedback)
        {
            try
            {
                return await _feedbackRepository.CreateFeedback(feedback);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while trying to call CreateFeedback in service class, Error Message = {ex}.");
                throw;
            }
        }

        public async Task<bool> DeleteFeedback(int id)
        {
            try
            {
                return await _feedbackRepository.DeleteFeedback(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while trying to call DeleteFeedback in service class, Error Message = {ex}.");
                throw;
            }
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbacks()
        {
            try
            {                
                return await _feedbackRepository.GetAllFeedbacks();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while trying to call GetAllFeedbacks in service class, Error Message = {ex}.");
                throw;
            }
        }

        public async Task<Feedback> GetFeedbackById(int id)
        {
            try
            {
                return await _feedbackRepository.GetFeedbackById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while trying to call GetFeedbackById in service class, Error Message = {ex}.");
                throw;
            }
        }

        public async Task<bool> UpdateFeedback(int id, Feedback feedback)
        {
            try
            {
                return await _feedbackRepository.UpdateFeedback(id, feedback);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while trying to call UpdateFeedback in service class, Error Message = {ex}.");
                throw;
            }
        }
    }
}
