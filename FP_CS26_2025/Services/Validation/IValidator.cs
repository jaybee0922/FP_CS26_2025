using System.Collections.Generic;

namespace FP_CS26_2025.Services.Validation
{
    public interface IValidator<T>
    {
        bool Validate(T entity, out List<string> errors);
    }
}
