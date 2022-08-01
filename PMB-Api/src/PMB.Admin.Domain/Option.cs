namespace PMB.Admin.Domain
{
    public record Option<TBody>(TBody Body, ErrorCode ErrorCode = ErrorCode.None);
    
    public static class OptionExtensions
    {
        public static Option<TBody> ToOption<TBody>(this TBody body) =>
            new(body);

        public static Option<TBody> ToOption<TBody>(this ErrorCode code) =>
            new(default, code);

        public static bool IsOk<TBody>(this Option<TBody> option) =>
            option.Body != null && option.ErrorCode == ErrorCode.None;
    }
}