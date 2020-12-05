using System;
using System.Linq.Expressions;
namespace AppCore.Models
{
    public class EnumConverter{
        public static string Convert(ROLE role)
        {
            switch(role)
            {
                case ROLE.MANAGER: return "Quản lý";
                case ROLE.WORKER: return "Nhân viên";
                default: return "";
            }
        }
        public static string Convert(SCOPE scope)
        {
            switch(scope)
            {
                case SCOPE.PRIVATE: return "Private";
                case SCOPE.PUBLIC: return "Public";
                default: return "";
            }
        }
        public static string Convert(USER_STATUS status)
        {
            switch(status)
            {
                case USER_STATUS.ACTIVE: return "Mở";
                case USER_STATUS.DISABLED: return "Khóa";
                default: return "";
            }
        }
        public static string Convert(SEX sex)
        {
            switch(sex)
            {
                case SEX.FEMALE: return "Nữ";
                case SEX.MALE: return "Nam";
                case SEX.OTHER: return "Khác";
                default: return "";
            }
        }
        public static string Convert(STATUS status)
        {
            switch(status)
            {
                case STATUS.NEW: return "Mới";
                case STATUS.ON_PROGRESS: return "Đang tiến hành";
                case STATUS.DONE: return "Hoàn thành";
                default: return "";
            }
        }
    }
    public enum ROLE
    {
        WORKER,
        MANAGER,
        ALL
    }
    public enum SCOPE
    {
        PUBLIC,
        PRIVATE,
        ALL
    }
    public enum STATUS{
        NEW,
        ON_PROGRESS,
        DONE,
        ALL
    }
    public enum SEX
    {
        MALE,
        FEMALE,
        OTHER,
        ALL
    }
    public enum USER_STATUS
    {
        ACTIVE,
        DISABLED,
        ALL
    }
    public enum ACTION
    {
        ADD,
        DELETE,
        UPDATE,
        CHANGE_STATUS,
        CHANGE_SCOPE
    }
    public enum ACTION_TARGET
    {
        USER,
        TASK,
        COMMENT
    }
    public enum CHANGE_FIELD
    {
        NONE,
        TITLE,
        DESCRIPTION,
        TIME,
        REGISTERED_USER,
        JOINT_USERS
    }
    

}