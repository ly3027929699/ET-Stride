using Stride.Core.IO;
using Stride.Core.Serialization.Contents;

namespace ET;

public class ReourceExtension: Singleton<ReourceExtension>,ISingletonAwake<Stride.Engine.Scene>
{
	private GlobelEngineScript _reourceLoader = null!;
	public void Awake(Stride.Engine.Scene a)
	{
		var entity = new Stride.Engine. Entity("ReourceLoader");
		_reourceLoader = new GlobelEngineScript();
		entity.Add(_reourceLoader);
		entity.Scene = a;
	}
	
	public T Load<T>(string url, ContentManagerLoaderSettings? settings = null) where T : class
	{
		return _reourceLoader.Load<T>(url,settings);
	}
	public Task<T> LoadAsync<T>(string url, ContentManagerLoaderSettings? settings = null) where T : class
	{
		return _reourceLoader.LoadAsync<T>(url,settings);
	}
	public async ETTask<byte[]> LoadBytesAsync(string url, StreamFlags streamFlags = StreamFlags.None)
	{
		return await _reourceLoader.LoadBytes(url,streamFlags);
	}
	
	public string LoadString(string url, StreamFlags streamFlags = StreamFlags.None)
	{
		return _reourceLoader.LoadString(url,streamFlags);
	}
	
	public async ETTask<Stride.Engine.Scene> LoadSceneAsync(string url)
	{
		return await _reourceLoader.LoadSceneAsync(url);
	}

	public void Release(object asset)
	{
		_reourceLoader.Release(asset);
	}
}