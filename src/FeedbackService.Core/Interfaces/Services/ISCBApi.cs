using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackService.Core.Interfaces.Services
{
    public interface ISCBApi
    {
        Task<string> GetToken(string client_id, string client_secretkey);
    }
}
