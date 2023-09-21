using System.Runtime.CompilerServices;
using Stride.Core.Mathematics;
using Unity.Mathematics;

namespace ET;

public static class Vector3Extension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe float3 ToFloat3(this Vector3 value)
    {
        return *(float3*)&value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Vector3 ToVector3(this float3 value)
    {
        return *(Vector3*)&value;
    }
}
public static class QuaternionExtension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe quaternion ToQuaternion(this Quaternion value)
    {
        return *(quaternion*)&value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe Quaternion ToQuaternion(this quaternion value)
    {
        return *(Quaternion*)&value;
    }
}