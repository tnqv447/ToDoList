using System.Collections.Generic;
using AppCore.Models;
namespace MvcClient.Models
{
    public class LogViewModel
    {
        public IList<DbLog> Logs { get; set; }
    }
}