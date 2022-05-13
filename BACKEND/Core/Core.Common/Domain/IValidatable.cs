namespace Core.Common.Domain
{
    public interface IValidatable
    {
        bool IsValid { get; }

        ValidationErrors ValidationErrors { get; }
    }
}