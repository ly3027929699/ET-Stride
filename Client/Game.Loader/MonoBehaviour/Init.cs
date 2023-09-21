using System;
using System.Diagnostics.CodeAnalysis;
using CommandLine;
using Stride.Core;
using Stride.Engine;
using UnityEngine;

namespace ET
{
	public class Init: UpdateMonoBehaviour, ISingletonAwake
	{
		[Display]
		private Stride.Engine.Entity? _global;
		[Display]
		private Stride.Engine.Entity? _unit;
		[Display]
		private Stride.Engine.Entity? _ui;
		[Display]
		private Stride.Engine.Entity? _camera;


		void ISingletonAwake.Awake()
		{
			
		}

		public override void Start()
		{
			this.StartAsync().Coroutine();
		}
		
		private async ETTask StartAsync()
		{
			DontDestroyOnLoad(this.Entity);
			
			AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
			{
				Log.Error(e.ExceptionObject.ToString());
			};

			// 命令行参数
			string[] args = "".Split(" ");
			Parser.Default.ParseArguments<Options>(args)
				.WithNotParsed(error => throw new Exception($"命令行格式错误! {error}"))
				.WithParsed((o)=>World.Instance.AddSingleton(o));
			Options.Instance.StartConfig = $"StartConfig/Localhost";
			
			World.Instance.AddSingleton<Logger>().Log = new StrideGameLogger();
			ETTask.ExceptionHandler += ET.Log.Error;
			
			World.Instance.AddSingleton<TimeInfo>();
			World.Instance.AddSingleton<FiberManager>();
			
			World.Instance.AddSingleton<GlobalInitContext,GlobalInitContextData>(new GlobalInitContextData(this._global,this._unit,this._ui,this._camera));
			World.Instance.AddSingleton<ReourceExtension,Stride.Engine.Scene>(this.Entity.Scene);

			await World.Instance.AddSingleton<ResourcesLoader>().CreatePackageAsync("DefaultPackage", true);
			
			CodeLoader codeLoader = World.Instance.AddSingleton<CodeLoader>();
			await codeLoader.DownloadAsync();
			
			codeLoader.StartAsync().Coroutine();
		}

		public override void Update()
		{
			TimeInfo.Instance.Update();
			FiberManager.Instance.Update();
		}

		private void LateUpdate()
		{
			FiberManager.Instance.LateUpdate();
		}

		private void OnApplicationQuit()
		{
			World.Instance.Dispose();
		}
	}

	public record GlobalInitContextData(Stride.Engine.Entity? Global, Stride.Engine.Entity? Unit, Stride.Engine.Entity? UI,Stride.Engine.Entity? Camera);

	public class GlobalInitContext: Singleton<GlobalInitContext>,ISingletonAwake<GlobalInitContextData>
	{
		private GlobalInitContextData _data = null!;

		public GlobalInitContextData Data => this._data;


		public CameraComponent Camera { get; private set; }

		public void Awake(GlobalInitContextData a)
		{
			this._data = a ?? throw new ArgumentNullException(nameof (a));
			var cameraComponent = this._data.Camera?.Get<CameraComponent>();
			this.Camera = cameraComponent ?? throw new NullReferenceException(nameof (cameraComponent));
			
		}
	}
}