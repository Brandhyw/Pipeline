using System.Collections;
using System.Collections.Generic;

namespace Pipeline
{
    public interface IPipeline<T> : IEnumerable<Process<T>>, IEnumerable
    {
        IPipeline<T> Register(Process<T> processor);

        T Process(T input, PipelineContext context);
        T Process(T input, out PipelineContext context);
    }
}
