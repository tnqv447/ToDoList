namespace AppCore.Services
{
    public enum SORT_ORDER
    {
        ASCENDING,
        DESCENDING,
    }
    public enum SEARCH_SORT_TYPE
    {
        //GENERALL

        ID,
        NAME,
        STATUS,

        //FOR USER
        ROLE,

        //FOR TASK
        SCOPE,

        //FOR LOG
        EXEC_DATE,
        TASK_NAME,
        EXEC_USER_NAME,
        TASK_ID,
        EXEC_USER_ID,
        ACTION
    }
}