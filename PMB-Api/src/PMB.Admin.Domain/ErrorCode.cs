namespace PMB.Admin.Domain
{
    public enum ErrorCode
    {
        None,
        InternalServerError,
        UserNotFound,
        KeyNotFound,
        AlreadyFrozen,
        NoFrozenKey
    }
}