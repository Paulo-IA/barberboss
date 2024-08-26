namespace BarberBoss.Exception.ExceptionBase;

public abstract class BarberBossException : SystemException
{
    protected BarberBossException(string message) : base(message) { }
}
