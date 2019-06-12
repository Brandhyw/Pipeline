using System;
using System.Collections.Generic;
using System.Text;

namespace Pipeline
{
    public delegate T Process<T>(T input, PipelineContext context);
}
