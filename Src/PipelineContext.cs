using System;
using System.Collections.Generic;

namespace Pipeline
{
    public sealed class PipelineContext
    {
        private enum State
        { Pending, Running, Stopped }

        private DateTime m_startTime, m_stopTime;
        private State m_state;

        private readonly ICollection<KeyValuePair<DateTime, string>> m_logs;

        public DateTime CreationTime
        { get; }

        public DateTime StartTime
            => (m_state == State.Running || m_state == State.Stopped) ? m_startTime : throw new Exception("Context start time not set, because the context has not been started");

        public DateTime EndTime
            => (m_state == State.Stopped) ? m_stopTime : throw new Exception("Context end time not set, because the context has not been stoped");

        public IEnumerator<KeyValuePair<DateTime, string>> Logs
            => m_logs.GetEnumerator();

        public PipelineContext()
        {
            CreationTime = DateTime.Now;
            m_state = State.Pending;
            m_logs = new List<KeyValuePair<DateTime, string>>();
        }

        public void Start()
        {
            if (m_state == State.Pending)
            {
                m_state = State.Running;
                m_startTime = DateTime.Now;
            }
            else
            {
                //Error cannot start when ended or is running
            }
        }

        public void Stop()
        {
            if (m_state == State.Running)
            {
                m_state = State.Stopped;
                m_stopTime = DateTime.Now;
            }
            else
            {
                //Error cannot stop when pending of ended
            }
        }

        public void Log(string log)
        {
            m_logs.Add(new KeyValuePair<DateTime, string>(DateTime.Now, log));
        }
    }
}
