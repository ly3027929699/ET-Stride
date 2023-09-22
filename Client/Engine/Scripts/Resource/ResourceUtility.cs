using System;
using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Physics;

namespace ET;

public static class ResourceUtility
{
    public static EngineEntity SpawnPrefabModel(this EngineScene rootScene, Prefab source, EngineEntity? attachEntity, Matrix localMatrix)
    {
        if (rootScene == null)
        {
            throw new ArgumentNullException(nameof (rootScene));
        }

        if (source == null)
        {
            throw new ArgumentNullException(nameof (source));
        }

        // Clone
        var spawnedEntities = source.Instantiate();

        // Add
        foreach (var prefabEntity in spawnedEntities)
        {
            prefabEntity.Transform.UpdateLocalMatrix();
            var entityMatrix = prefabEntity.Transform.LocalMatrix * localMatrix;
            entityMatrix.Decompose(out prefabEntity.Transform.Scale, out prefabEntity.Transform.Rotation, out prefabEntity.Transform.Position);

            if (attachEntity != null)
            {
                attachEntity.AddChild(prefabEntity);
            }
            else
            {
                rootScene.Entities.Add(prefabEntity);
            }
        }
        return spawnedEntities[0];
    }
}