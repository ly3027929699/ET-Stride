using TrueSync;
using System.Reflection;
using Stride.Core.Mathematics;
using UnityEngine;

/**
* @brief Extensions added by TrueSync.
**/
public static class TrueSyncExtensions {

    public static TSVector ToTSVector(this Vector3 vector) {
        return new TSVector(vector.X, vector.Y, vector.Z);
    }

    public static TSVector2 ToTSVector2(this Vector3 vector) {
        return new TSVector2(vector.X, vector.Y);
    }

    public static TSVector ToTSVector(this Vector2 vector) {
        return new TSVector(vector.X, vector.Y, 0);
    }

    public static TSVector2 ToTSVector2(this Vector2 vector) {
        return new TSVector2(vector.X, vector.Y);
    }

    public static Vector3 Abs(this Vector3 vector) {
		return new Vector3(MathF.Abs(vector.X), MathF.Abs(vector.Y), MathF.Abs(vector.Z));
	}

    public static TSQuaternion ToTSQuaternion(this Quaternion rot) {
        return new TSQuaternion(rot.X, rot.Y, rot.Z, rot.W);
    }

    public static Quaternion ToQuaternion(this TSQuaternion rot) {
        return new Quaternion((float)rot.x, (float)rot.y, (float)rot.z, (float)rot.w);
    }

    public static TSMatrix ToTSMatrix(this Quaternion rot) {
        return TSMatrix.CreateFromQuaternion(rot.ToTSQuaternion());
    }

    public static Vector3 ToVector(this TSVector jVector) {
        return new Vector3((float) jVector.x, (float) jVector.y, (float) jVector.z);
    }

    public static Vector3 ToVector(this TSVector2 jVector) {
        return new Vector3((float)jVector.x, (float)jVector.y, 0);
    }

    public static void Set(this TSVector jVector, TSVector otherVector) {
        jVector.Set(otherVector.x, otherVector.y, otherVector.z);
    }

    public static Quaternion ToQuaternion(this TSMatrix jMatrix) {
        return TSQuaternion.CreateFromMatrix(jMatrix).ToQuaternion();
    }

}