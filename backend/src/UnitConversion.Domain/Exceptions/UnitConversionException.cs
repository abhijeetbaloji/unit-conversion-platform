namespace UnitConversion.Domain.Exceptions;

public class UnitConversionException : Exception
{
    public UnitConversionException(string message)
        : base(message)
    {
    }
}
