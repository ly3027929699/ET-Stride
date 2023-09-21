using Stride.Core.Mathematics;

namespace ET;

public readonly struct Line
{
    public Vector3 Start { get; }
    public Vector3 End { get; }

    public Line(Vector3 start, Vector3 end)
    {
        this.Start = start;
        this.End = end;
    }
}