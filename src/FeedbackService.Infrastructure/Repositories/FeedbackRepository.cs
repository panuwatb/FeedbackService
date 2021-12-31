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

        public async Task<bool> CreateFeedback(Feedback feedback)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteFeedback(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbacks()
        {
            var dbFeedbacks = await _dbContext.Feedbacks.ToListAsync().ConfigureAwait(false);

            return _mapper.Map<IEnumerable<Feedback>>(dbFeedbacks);
        }

        public async Task<Feedback> GetFeedbackById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
