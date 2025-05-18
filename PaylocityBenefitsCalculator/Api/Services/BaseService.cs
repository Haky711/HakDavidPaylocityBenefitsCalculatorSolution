using Api.Data;
using Api.Exceptions;

namespace Api.Services
{
    public abstract class BaseService
    {
        // Maybe this should be in a separated static helper class, but for now, I will keep it here.
        protected void IfFalseTrowNotFoundException(bool condition, string message)
        {
            if (!condition)
            {
                throw new NotFoundException(message);
            }
        }

        // Maybe this should be in a separated static helper class, but for now, I will keep it here.
        protected void IfNullThrowNotFoundException(object? obj, string message)
        {
            if (obj == null)
            {
                throw new NotFoundException(message);
            }
        }
    }
}
