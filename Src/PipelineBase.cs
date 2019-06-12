using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Pipeline
{
    public class PipelineBase<T> : IPipeline<T>
    {
        private readonly List<Process<T>> m_processors;

        public PipelineBase()
        {
            m_processors = new List<Process<T>>();
        }

        public T Process(T input, out PipelineContext context)
        {
            context = new PipelineContext();
            return Process(input, context);
        }

        public T Process(T input, PipelineContext context)
        {
            foreach (Process<T> processor in this)
            {
                input = processor(input, context);
            }
            return input;
        }

        public IPipeline<T> Register(Process<T> processor)
        {
            m_processors.Add(processor);
            return this;
        }

        public IEnumerator<Process<T>> GetEnumerator()
        {
            return m_processors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
