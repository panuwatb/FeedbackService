using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackService.Core.Models
{
    public class Feedback
    {              
        public string Subject { get; set; }        
        public string Message { get; set; }        
        public int Rating { get; set; }
        public string CreatedBy { get; set; }        
        public DateTime CreatedDate { get; set; }
    }
}
