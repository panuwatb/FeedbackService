using FeedbackService.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackService.Infrastructure.Context
{
    public class FeedbackDbContext : DbContext
    {
        public FeedbackDbContext(DbContextOptions<FeedbackDbContext> option): base(option)
        {
            //FeedData();
        }

        public virtual DbSet<Feedback> Feedbacks { get; set; }

        //private void FeedData()
        //{
        //    var feedbacks = new List<Feedback>()
        //    {
        //        new Feedback() { Id = 1, Subject = ".NET Core 5 Web API", Message = "x", Rating = 1, CreatedBy = "x", CreatedDate = DateTime.Now },
        //        new Feedback() { Id = 2, Subject = "Microservices", Message = "xx", Rating = 2, CreatedBy = "x", CreatedDate = DateTime.Now },
        //        new Feedback() { Id = 3, Subject = "Azure Devops", Message = "xxx", Rating = 3, CreatedBy = "x", CreatedDate = DateTime.Now },
        //        new Feedback() { Id = 4, Subject = "Azure Clound", Message = "xxxx", Rating = 4, CreatedBy = "x", CreatedDate = DateTime.Now }
        //    };

        //    this.Feedbacks.AddRange(feedbacks);
        //    this.SaveChanges();
        //}
    }
}
