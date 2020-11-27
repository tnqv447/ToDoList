using System;
using System.Linq.Expressions;
namespace AppCore.Models
{
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
        DELAYED,
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
    public enum SORT_ORDER
    {
        ASCENDING,
        DESCENDING,
    }

}