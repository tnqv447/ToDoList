using System.Collections;
using System;
using AppCore.Models;
using System.Collections.Generic;
using AppCore.Interfaces;

namespace AppCore.Services
{
    public class AnalysisService : IAnalysisService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AnalysisService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public AnalyzeUserTasks AnalyzeByUser(User user, DateTime start, DateTime end){
            var res = new AnalyzeUserTasks();
            res.TasksCount = user?.ToDoTasks?.Count ?? 0;
            if(res.TasksCount != 0){
                res.UserId = user.Id;
                foreach(ToDoTask task in user.ToDoTasks){
                    if(DateTime.Compare(task.StartDate.Date, start.Date) >= 0 && DateTime.Compare(task.StartDate.Date, end.Date) <= 0){
                        switch(task.Status){
                            case STATUS.DONE: res.TaskDoneCount++; break;
                            case STATUS.ON_PROGRESS: {
                                    res.TaskOnProgressCount++;
                                    if(DateTime.Compare(task.EndDate.Date, DateTime.Today) > 0 ) res.TaskDelayedCount++;
                                    break;
                                }
                            default: {
                                res.TaskNewCount++;
                                if(DateTime.Compare(task.EndDate.Date, DateTime.Today) > 0 ) res.TaskDelayedCount++;
                                break;
                            }
                        }
                    }
                }
            }
            return res;
        }

        public AnalyzeUserTasks AnalyzeByTasks(DateTime start, DateTime end){
            var res = new AnalyzeUserTasks();
            var tasks = _unitOfWork.ToDoTasks.GetAll();
            res.TasksCount = tasks?.Count ?? 0;
            if(res.TasksCount != 0){
                foreach(ToDoTask task in tasks){
                    if(DateTime.Compare(task.StartDate.Date, start.Date) >= 0 && DateTime.Compare(task.StartDate.Date, end.Date) <= 0){
                        switch(task.Status){
                            case STATUS.DONE: res.TaskDoneCount++; break;
                            case STATUS.ON_PROGRESS: {
                                    res.TaskOnProgressCount++;
                                    if(DateTime.Compare(task.EndDate.Date, DateTime.Today) > 0 ) res.TaskDelayedCount++;
                                    break;
                                }
                            default: {
                                res.TaskNewCount++;
                                if(DateTime.Compare(task.EndDate.Date, DateTime.Today) > 0 ) res.TaskDelayedCount++;
                                break;
                            }
                        }
                    }
                }
            }
            return res;
        }
    }
}