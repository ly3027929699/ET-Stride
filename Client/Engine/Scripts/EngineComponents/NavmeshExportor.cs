using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Stride.Core;
using Stride.Core.Diagnostics;
using Stride.Core.IO;
using Stride.Core.Mathematics;
using Stride.Core.Serialization;
using Stride.Core.Serialization.Contents;
using Stride.Core.Storage;
using Stride.Engine;
using Stride.Engine.Design;
using Stride.Graphics;
using Stride.Physics;
using Stride.Rendering;

namespace ET;

public class NavmeshExportor: StartupScript
{
    private EngineScene? root;

    public string savePath = "E:\\Stride\\Projects\\ET-Stride\\Tools\\RecastNavExportor\\Meshes";
    private bool export;

    public UrlReference<EngineScene> sceneAsset;

    private void Initialise()
    {
        root = this.SceneSystem.SceneInstance.RootScene;
        if (this.root.Entities.Count == 0)
            throw new NullReferenceException("root scene is null");
    }

    public bool Export
    {
        get => this.export;
        set
        {
            if (this.export != value)
            {
            }

            this.export = value;
        }
    }

    public override void Start()
    {
        base.Start();
        ExportObjs();
    }


    readonly record  struct VertexPositionNormalTangentTexture(Vector3 position, Vector3 nomal, Vector2 uv, Vector4 tangent);
    readonly record  struct TrangleIndex(short a,short b,short c);
    private void ExportObjs()
    {
        string logPath = Path.Combine(savePath, "test.log");
        StringBuilder logStringBuilder = new StringBuilder();
        try
        {
            if (this.root == null)
                Initialise();
            var entities = this.root!.Entities;
            string path = Path.Combine(savePath, "test.obj");
            StringBuilder stringBuilder = new StringBuilder();
            List<Vector3> vertexs = new List<Vector3>(10240);
            List<TrangleIndex> triangles = new List<TrangleIndex>(10240);

            GraphicsContext graphicsContext = new GraphicsContext(GraphicsDevice);
            var commandList = graphicsContext.CommandList;
            short vertexIndexBase = 0;
            foreach (EngineEntity rootEntity in entities)
            {
                TransformComponent transform = rootEntity.Get<TransformComponent>();
                ModelComponent model = rootEntity.Get<ModelComponent>();
                if (null != model)
                {
                    vertexIndexBase = (short)vertexs.Count;
                    var modelMeshes = model.Model.Meshes;
                    for (int index = 0; index < modelMeshes.Count; index++)
                    {
                        if(index!=0)
                            continue;
                        Mesh modelMesh = modelMeshes[index];
                        MeshDraw mesh = modelMesh.Draw;
                        int meshDrawCount = mesh.DrawCount;
                        foreach (VertexBufferBinding vertexBufferBinding in mesh.VertexBuffers)
                        {
                            logStringBuilder.AppendLine(mesh.PrimitiveType.ToString());
                            if (vertexBufferBinding.Stride != Unsafe.SizeOf<VertexPositionNormalTangentTexture>())
                            {
                                this.Log.Error($"vertexBufferBinding.Stride:{vertexBufferBinding.Stride}");
                                return;
                            }

                            int offset = vertexBufferBinding.Offset / vertexBufferBinding.Stride;
                            int vertextCount = vertexBufferBinding.Count;

                            var vertexPositionTextures = vertexBufferBinding.Buffer.GetData<VertexPositionNormalTangentTexture>(commandList);
                            for (int i = offset; i < offset + vertextCount; i++)
                            {
                                Vector3 position = vertexPositionTextures[i].position;
                                Matrix identity = Matrix.Identity;
                                identity.M41 = position.X;
                                identity.M42 = position.Y;
                                identity.M43 = position.Z;
                                Matrix multiply = Matrix.Multiply(transform.WorldMatrix, identity);
                                multiply.Decompose(out _, out Quaternion _, out Vector3 translation);
                                vertexs.Add(translation);
                            }
                        }

                        int indexBufferOffset = mesh.IndexBuffer.Offset / sizeof (short);
                        int indexBufferCount = mesh.IndexBuffer.Count;
                        short[] indexs = mesh.IndexBuffer.Buffer.GetData<short>(commandList);
                        for (int i = indexBufferOffset; i < indexBufferOffset + indexBufferCount - 2; i += 3)
                        {
                            triangles.Add(VertexIndex(1,0,2));

                            TrangleIndex VertexIndex(int a, int b, int c)
                            {
                                return new TrangleIndex((short)(vertexIndexBase + indexs[i + a]),
                                    (short)(vertexIndexBase + indexs[i + b]),
                                    (short)(vertexIndexBase + indexs[i + c]));
                            }
                        }
                    }
                }
            }

            foreach (var vector3 in vertexs)
            {
                stringBuilder.AppendLine($"v {vector3.X} {vector3.Y} {vector3.Z}");
            }

            foreach (var trangleIndex in triangles)
            {
                stringBuilder.AppendLine($"f {trangleIndex.a} {trangleIndex.b} {trangleIndex.c}");
            }
            
            File.WriteAllText(path, stringBuilder.ToString());
        }
        catch (Exception e)
        {
            this.Log.Error(e.ToString());
            logStringBuilder.AppendLine(e.ToString());
        }

        File.WriteAllText(logPath, logStringBuilder.ToString());

    }
}