using System;
using System.IO;
using System.Threading.Tasks;
using Stride.Core;
using Stride.Core.IO;
using Stride.Core.Mathematics;
using Stride.Core.Serialization;
using Stride.Core.Serialization.Contents;
using Stride.Engine;
using Stride.Input;
using Stride.Physics;
using UnityEngine;
using Matrix = BulletSharp.Math.Matrix;

namespace ET;

public class GlobelEngineScript: UpdateMonoBehaviour
{
    private static GlobelEngineScript _engineScript = null!;
    public static GlobelEngineScript Default => _engineScript;

    public float DeltaTime { get; private set; }

    [Display(" ")]
    public Stride.Engine.Entity Global { get; private set; }

    [Display(" ")]
    public Stride.Engine.Entity Unit { get; private set; }

    [Display(" ")]
    public Stride.Engine.Entity UI { get; private set; }

    [Display(" ")]
    public Stride.Engine.CameraComponent Camera { get; private set; }

    private Stride.Engine.Scene? currentScene;

    public override void Start()
    {
        base.Start();
        _engineScript = this;
        DontDestroyOnLoad(this.Entity);

        foreach (Stride.Engine.Entity entity in this.SceneSystem.SceneInstance.RootScene.Entities)
        {
            if (entity.Name.Equals("Global", StringComparison.Ordinal))
            {
                this.Global = entity;
            }

            if (entity.Name.Equals("Unit", StringComparison.Ordinal))
            {
                this.Unit = entity;
            }

            if (entity.Name.Equals("UI", StringComparison.Ordinal))
            {
                this.UI = entity;
            }

            if (entity.Name.Equals("MainCamera", StringComparison.Ordinal))
            {
                Camera = entity.Get<CameraComponent>();
            }
        }

        if (Global == null)
            throw new NullReferenceException(nameof (this.Global));
        if (Unit == null)
            throw new NullReferenceException(nameof (this.Unit));
        if (UI == null)
            throw new NullReferenceException(nameof (this.UI));
        if (Camera == null)
            throw new NullReferenceException(nameof (this.Camera));
    }

    public override void Update()
    {
        DeltaTime = (float)this.Game.UpdateTime.Elapsed.TotalSeconds;
    }

    public T Load<T>(string url, ContentManagerLoaderSettings? settings = null) where T : class
    {
        return this.Content.Load<T>(url, settings);
    }

    public Task<T> LoadAsync<T>(string url, ContentManagerLoaderSettings? settings = null) where T : class
    {
        return this.Content.LoadAsync<T>(url, settings);
    }

    public async ETTask<byte[]> LoadBytes(string url, StreamFlags settings = StreamFlags.None)
    {
        await using var stream = this.Content.OpenAsStream(url, settings);
        long length = stream.Length - stream.Position;
        byte[] buffer = new byte[length];
        _ = await stream.ReadAsync(buffer, (int)stream.Position, (int)length);
        return buffer;
    }

    public string LoadString(string url, StreamFlags settings = StreamFlags.None)
    {
        using var stream = this.Content.OpenAsStream(url, settings);
        using var streamReader = new StreamReader(stream);
        return streamReader.ReadToEnd();
    }

    public async ETTask<Stride.Engine.Scene> LoadSceneAsync(string url)
    {
        if (currentScene != null)
        {
            currentScene.Parent = null;
            Release(this.currentScene);
        }

        currentScene = await this.Content.LoadAsync(new UrlReference<Stride.Engine.Scene>(url));
        this.Entity.Scene.Children.Add(currentScene);
        return currentScene;
    }

    public void Release(object asset)
    {
        this.Content.Unload(asset);
    }

    public Stride.Engine.Entity? GetEntity(string name)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof (name));
        }

        foreach (Stride.Engine.Entity entity in this.SceneSystem.SceneInstance.RootScene.Entities)
        {
            if (name.Equals(entity.Name, StringComparison.Ordinal))
            {
                return entity;
            }
        }

        return null;
    }

    public Simulation GetCurrentSimulation()
    {
        return this.GetSimulation();
    }

    public InputManager GetInputManager()
    {
        return this.Input;
    }

    public void DebugPoint(Vector3 resultPoint)
    {
        Prefab prefab = this.Content.Load<Prefab>("Res/BoxE");
        EngineEntity entity = this.SceneSystem.SceneInstance.RootScene.SpawnPrefabModel(prefab, null, Matrix.Identity);
        entity.Transform.Position = resultPoint;
        Destroy(entity).Coroutine();
    }

    async ETTask Destroy(EngineEntity? entity)
    {
        await Task.Delay(1000);
        if (entity != null)
            entity.Scene = null;
    }
}