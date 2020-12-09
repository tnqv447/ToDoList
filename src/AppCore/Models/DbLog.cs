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
        public CHANGE_FIELD ChangeField { get; set; }

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
        public DbLog(DbLog log, int id)
        {
            this.Copy(log);
            this.Id = id;
        }

        public DbLog(DateTime execDate, ACTION_TARGET actionTarget, ACTION action, CHANGE_FIELD changeField, int execUserId, int targetId, string targetName, string targetStatusName, string targetScopeName)
        {
            ExecDate = execDate;
            ActionTarget = actionTarget;
            Action = action;
            ChangeField = changeField;
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
            ChangeField = log.ChangeField;
            ExecUserId = log.ExecUserId;
            TargetId = log.TargetId;
            TargetName = log.TargetName;
            TargetStatusName = log.TargetStatusName;
            TargetScopeName = log.TargetScopeName;
        }

        public override string ToString()
        {
            if (ExecUser == null) return "";
            var res = "";
            if (ExecUser.Role.Equals(ROLE.WORKER))
            {
                res = String.Format("Nhân viên {0} ", ExecUserName);
            }
            else res = String.Format("Quản lí {0} ", ExecUserName);

            switch (ActionTarget)
            {
                case ACTION_TARGET.JOIN_USERS:
                    {
                        res += String.Format("đã || cập nhật || <Danh sách người tham gia> trong công việc '{0}' [ID: {1}]", TargetName, TargetId);
                        break;
                    }
                case ACTION_TARGET.ATTACHED_FILES:
                    {
                        res += String.Format("đã || cập nhật || <File đính kèm> trong task {0} [ID: {1}]", TargetName, TargetId);
                        break;
                    }
                case ACTION_TARGET.TASK:
                    {
                        switch (Action)
                        {
                            case ACTION.ADD: res += String.Format("đã || thêm || công việc '{0}' [ID: {1}], trang thái {2}", TargetName, TargetId, TargetStatusName); break;
                            case ACTION.DELETE: res += String.Format("đã || xóa || công việc '{0}'  [ID: {1}]", TargetName, TargetId); break;
                            case ACTION.UPDATE:
                                string field = "";
                                switch (ChangeField)
                                {
                                    case CHANGE_FIELD.TITLE: field = "<Tiêu đề> "; break;
                                    case CHANGE_FIELD.TIME: field = "<Thời hạn> "; break;
                                    case CHANGE_FIELD.DESCRIPTION: field = "<Mô tả> "; break;
                                    case CHANGE_FIELD.REGISTERED_USER: field = "<Người đảm nhiệm> "; break;

                                    default: break;
                                }
                                res += String.Format("đã || cập nhật || {0} trong công việc '{1}' [ID: {2}]", field, TargetName, TargetId);
                                break;
                            case ACTION.CHANGE_STATUS: res += String.Format("đã || thay đổi || tiến độ công việc '{0}' [ID: {1}] thành {2}", TargetName, TargetId, TargetStatusName); break;
                            case ACTION.CHANGE_SCOPE: res += String.Format("đã || thay đổi || phạm vi công việc '{0}' [ID: {1}] thành {2}", TargetName, TargetId, TargetScopeName); break;
                        }
                        break;
                    }
                case ACTION_TARGET.COMMENT:
                    {
                        switch (Action)
                        {
                            case ACTION.ADD: res += String.Format("đã || thêm || bình luận trong công việc '{0}' [ID: {1}]", TargetName, TargetId); break;
                            case ACTION.DELETE: res += String.Format("đã xóa một bình luận trong công việc '{0}' [ID: {1}]", TargetName, TargetId); break;
                            case ACTION.UPDATE: res += String.Format("đã cập nhật bình luận trong công việc '{0}' [ID: {1}]", TargetName, TargetId); break;
                        }
                        break;
                    }
                default:
                    {
                        switch (Action)
                        {
                            case ACTION.ADD: res += String.Format("đã || thêm || nhân viên {0} [ID: {1}]", TargetName, TargetId); break;
                            case ACTION.DELETE: res += String.Format("đã || xóa || nhân viên {0} [ID: {1}]", TargetName, TargetId); break;
                            case ACTION.UPDATE: res += String.Format("đã || cập nhật || nhân viên {0} [ID: {1}]", TargetName, TargetId); break;
                            case ACTION.CHANGE_STATUS: res += String.Format("đã || thay đổi || trạng thái nhân viên {0} [ID: {1}] thành {2}", TargetName, TargetId, TargetStatusName); break;
                        }
                        break;
                    }
            }
            var template = "{0}: " + res;
            return String.Format(template, ExecDate.ToString("dd/MM/yyyy"));
        }

        public bool GetActionTarget(Type type)
        {
            var check = true;
            if (type.Equals(typeof(DbLog))) check = false;
            else if (type.Equals(typeof(Comment))) ActionTarget = ACTION_TARGET.COMMENT;
            else if (type.Equals(typeof(User))) ActionTarget = ACTION_TARGET.USER;
            else if (type.Equals(typeof(ToDoTask))) ActionTarget = ACTION_TARGET.TASK;
            else if (type.Equals(typeof(JointUser))) ActionTarget = ACTION_TARGET.JOIN_USERS;
            else if (type.Equals(typeof(AttachedFile))) ActionTarget = ACTION_TARGET.ATTACHED_FILES;
            else check = false;

            return check;
        }
    }
}