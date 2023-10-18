﻿namespace Mocha.Core.Model.Trace;

public class SpanEvent
{
    public string TraceId { get; private set; }

    public long TimeBucket { get; private set; }

    public string EventName { get; private set; }

}