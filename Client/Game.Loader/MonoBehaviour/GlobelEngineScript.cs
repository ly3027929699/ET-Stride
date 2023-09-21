using Stride.Core.IO;
using Stride.Core.Serialization;
using Stride.Core.Serialization.Contents;
using Stride.Engine;
using Stride.Input;
using Stride.Physics;
using UnityEngine;

namespace ET;

public class GlobelEngineScript: UpdateMonoBehaviour
{
    private static GlobelEngineScript _engineScript = null!;
    public static GlobelEngineScript Default => _engineScript;
    public float DeltaTime { get; private set; }

    public override void Start()
    {
        base.Start();

        _engineScript = this;
        DontDestroyOnLoad(this.Entity);
    }

    public override void Update()
    {
        DeltaTime = (float)this.Game.UpdateTime.Elapsed.TotalSeconds;
    }

    public T Load<T>(string url, ContentManagerLoaderSettings? settings = null) where T : class
    {
        return this.Content.Load<T>(url,settings);
    }
    public Task<T> LoadAsync<T>(string url, ContentManagerLoaderSettings? settings = null) where T : class
    {
        return this.Content.LoadAsync<T>(url,settings);
    }
	
    public async ETTask<byte[]> LoadBytes(string url, StreamFlags settings = StreamFlags.None)
    {
        await using var stream = this.Content.OpenAsStream(url,settings);
        long length = stream.Length-stream.Position;
        byte[] buffer = new byte[length];
        _ = await stream.ReadAsync(buffer, (int)stream.Position, (int)length);
        return buffer;
    }
	
    public string LoadString(string url, StreamFlags settings = StreamFlags.None)
    {
        using var stream = this.Content.OpenAsStream(url,settings);
        using var streamReader = new StreamReader(stream);
        return streamReader.ReadToEnd();
    }

    public async ETTask<Stride.Engine.Scene> LoadSceneAsync(string url)
    {
         Stride.Engine.Scene scene = await this.Content.LoadAsync(new UrlReference<Stride.Engine.Scene>(url));
        this.Entity.Scene.Children.Add(scene);
        return scene;
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
}