using AutoMapper;
using FeedbackService.Core.Interfaces.Repositories;
using FeedbackService.Core.Models;
using FeedbackService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackService.Infrastructure.Repositories
{    
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly FeedbackDbContext _dbContext;
        private readonly IMapper _mapper;
        public FeedbackRepository(FeedbackDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Feedback> CreateFeedback(Feedback feedback)
        {
            var dbFeedback = _mapper.Map<Entities.Feedback>(feedback);
            await _dbContext.Feedbacks.AddAsync(dbFeedback);
            await _dbContext.SaveChangesAsync();

            return feedback;
        }

        public async Task<bool> DeleteFeedback(int id)
        {
            var feedback = await _dbContext.Feedbacks.FindAsync(id);
            if (feedback != null)
            {
                _dbContext.Feedbacks.Remove(feedback);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbacks()
        {
            var dbFeedbacks = await _dbContext.Feedbacks.ToListAsync().ConfigureAwait(false);
            if (dbFeedbacks.Count > 0)
            {
                return _mapper.Map<IEnumerable<Feedback>>(dbFeedbacks);
            }

            return Enumerable.Empty<Feedback>();
        }

        public async Task<Feedback> GetFeedbackById(int id)
        {
            var dbFeedbacks = await _dbContext.Feedbacks.FindAsync(id);
            if (dbFeedbacks != null)
            {
                return _mapper.Map<Feedback>(dbFeedbacks);
            }

            return null;
        }

        public async Task<bool> UpdateFeedback(int id, Feedback feedback)
        {
            var _feedback = await _dbContext.Feedbacks.FindAsync(id);
            if (_feedback == null || _feedback.Id != id)
            {
                return false;
            }

            _feedback.Subject = feedback.Subject;
            _feedback.Message = feedback.Message;
            _feedback.Rating = feedback.Rating;
            _feedback.CreatedBy = feedback.CreatedBy;
            _feedback.CreatedDate = feedback.CreatedDate;

            if (feedback != null)
            {
                _dbContext.Feedbacks.Update(_feedback);
                await _dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
