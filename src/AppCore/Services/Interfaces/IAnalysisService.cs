using System;
using AppCore.Models;

namespace AppCore.Services
{
    public interface IAnalysisService
    {
        AnalyzeUserTasks AnalyzeByUser(User user, DateTime start, DateTime end);
        AnalyzeUserTasks AnalyzeByTasks(DateTime start, DateTime end);
    }
}