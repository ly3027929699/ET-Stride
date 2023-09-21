using Stride.Core.Mathematics;
using Stride.Engine;

namespace ET;

public static class StrideEntityHelper
{
    public static Stride.Engine.TransformComponent? Find(this TransformComponent self, string name)
    {
        if (self == null)
        {
            throw new ArgumentNullException(nameof (self));
        }

        if (name==null)
        {
            throw new ArgumentNullException(nameof (name));
        }

        foreach (TransformComponent transformComponent in self.Children)
        {
            if (transformComponent!=null && name.Equals(transformComponent.Entity.Name, StringComparison.Ordinal))
            {
                return transformComponent;
            }
        }

        return null;
    }
    public static Line ScreenPointToLine(this CameraComponent camera, Vector2 screenPos)
    {
        if (camera == null)
        {
            throw new ArgumentNullException(nameof (camera));
        }

        
        Matrix invViewProj = Matrix.Invert(camera.ViewProjectionMatrix);

        // Reconstruct the projection-space position in the (-1, +1) range.
        //    Don't forget that Y is down in screen coordinates, but up in projection space
        Vector3 sPos;
        sPos.X = screenPos.X * 2f - 1f;
        sPos.Y = 1f - screenPos.Y * 2f;

        // Compute the near (start) point for the raycast
        // It's assumed to have the same projection space (x,y) coordinates and z = 0 (lying on the near plane)
        // We need to unproject it to world space
        sPos.Z = 0f;
        var vectorNear = Vector3.Transform(sPos, invViewProj);
        vectorNear /= vectorNear.W;

        // Compute the far (end) point for the raycast
        // It's assumed to have the same projection space (x,y) coordinates and z = 1 (lying on the far plane)
        // We need to unproject it to world space
        sPos.Z = 1f;
        var vectorFar = Vector3.Transform(sPos, invViewProj);
        vectorFar /= vectorFar.W;

        return new Line(vectorNear.XYZ(), vectorFar.XYZ());
    }
}