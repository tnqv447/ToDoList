using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppCore.Models
{
    public class DbLog
    {
        public int Id { get; set; }
        public DateTime ExecDate { get; set; }

        public ACTION_TARGET ActionTarget { get; set; }
        public ACTION Action { get; set; }

        public int ExecUserId { get; set; }
        public virtual User ExecUser { get; set; }
        [NotMapped]
        public string ExecUserName { get { return ExecUser?.Name ?? ""; } }

        public int TargetId { get; set; }
        public string TargetName { get; set; }
        public string TargetStatusName { get; set; }
        public string TargetScopeName { get; set; }

        public DbLog()
        {
            
        }
        public DbLog(DbLog log)
        {
            this.Copy(log);
        }

        public DbLog(DateTime execDate, ACTION_TARGET actionTarget, ACTION action, int execUserId, int targetId, string targetName, string targetStatusName, string targetScopeName)
        {
            ExecDate = execDate;
            ActionTarget = actionTarget;
            Action = action;
            ExecUserId = execUserId;
            TargetId = targetId;
            TargetName = targetName;
            TargetStatusName = targetStatusName;
            TargetScopeName = targetScopeName;
        }

        public void Copy(DbLog log)
        {
            ExecDate = log.ExecDate;
            ActionTarget = log.ActionTarget;
            Action = log.Action;

            ExecUserId = log.ExecUserId;
            TargetId = log.TargetId;
            TargetName = log.TargetName;
            TargetStatusName = log.TargetStatusName;
            TargetScopeName = log.TargetScopeName;
        }

        public override string ToString()
        {
            if(ExecUser == null) return "";
            var res = "";
            if(ExecUser.Role.Equals(ROLE.WORKER)){
                res = String.Format("Nhân viên {0} [ID: {1}] ", ExecUserName, ExecUserId);
            }
            else res = String.Format("Quản lí {0} [ID: {1}] ", ExecUserName, ExecUserId);
            
            switch(ActionTarget){
                case ACTION_TARGET.TASK: {
                        switch(Action){
                            case ACTION.ADD : res =  String.Format("đã thêm task {0} [ID: {1}], trang thái {2}", TargetName, TargetId, TargetStatusName); break;
                            case ACTION.DELETE : res = String.Format("đã xóa task {0} [ID: {1}]", TargetName, TargetId); break;
                            case ACTION.UPDATE : res = String.Format("đã cập nhật task {0} [ID: {1}]", TargetName, TargetId); break;
                            case ACTION.CHANGE_STATUS : res = String.Format("đã thay đổi tiến độ task {0} [ID: {1}] thành {2}", TargetName, TargetId, TargetStatusName); break;
                            case ACTION.CHANGE_SCOPE : res = String.Format("đã thay đổi phạm vi task {0} [ID: {1}] thành {2}", TargetName, TargetId, TargetScopeName); break;
                         }
                        break;
                    }
                case ACTION_TARGET.COMMENT: {
                    switch (Action)
                    {
                        case ACTION.ADD : res = String.Format("đã bình luận trong task {0} [ID: {1}]", TargetName, TargetId); break;
                        case ACTION.DELETE : res = String.Format("đã xóa một bình luận trong task {0} [ID: {1}]", TargetName, TargetId); break;
                        case ACTION.UPDATE : res = String.Format("đã cập nhật bình luận trong task {0} [ID: {1}]", TargetName, TargetId); break;
                    }
                        break;
                    }
                default: {
                    switch(Action)
                    {
                        case ACTION.ADD : res = String.Format("đã thêm user {0} [ID: {1}]", TargetName, TargetId); break;
                        case ACTION.DELETE : res = String.Format("đã xóa user {0} [ID: {1}]", TargetName, TargetId); break;
                        case ACTION.UPDATE : res = String.Format("đã cập nhật user {0} [ID: {1}]", TargetName, TargetId); break;
                        case ACTION.CHANGE_STATUS : res = String.Format("đã thay đổi trạng thái user {0} [ID: {1}] thành {2}", TargetName, TargetId, TargetStatusName); break;
                    }
                    break;
                    }
            }
            var template = "[{0}] || " + res;
            return String.Format(template, ExecDate.ToString());
        }

        public bool GetActionTarget(Type type){
            var check = true;
            if(type.Equals(typeof(DbLog))) check = false;
            else if(type.Equals(typeof(Comment))) ActionTarget = ACTION_TARGET.COMMENT;
            else if(type.Equals(typeof(User))) ActionTarget = ACTION_TARGET.USER;
            else ActionTarget = ACTION_TARGET.TASK;

            return check;
        }
    }
}