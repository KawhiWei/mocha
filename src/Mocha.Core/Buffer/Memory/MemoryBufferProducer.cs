// Licensed to the .NET Core Community under one or more agreements.
// The .NET Core Community licenses this file to you under the MIT license.

namespace Mocha.Core.Buffer.Memory;

internal sealed class MemoryBufferProducer<T> : IBufferProducer<T>
{
    private readonly MemoryBufferPartition<T>[] _partitions;
    private uint _partitionIndex;

    public MemoryBufferProducer(string topicName, MemoryBufferPartition<T>[] partitions)
    {
        TopicName = topicName;
        _partitions = partitions;
    }

    public string TopicName { get; }

    public ValueTask ProduceAsync(T item)
    {
        var partition = SelectPartition();
        partition.Enqueue(item);
        return ValueTask.CompletedTask;
    }

    private MemoryBufferPartition<T> SelectPartition()
    {
        var index = (Interlocked.Increment(ref _partitionIndex) - 1) % _partitions.Length;
        return _partitions[index];
    }
}