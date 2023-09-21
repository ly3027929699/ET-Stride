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

		void ISingletonAwake.Awake()
		{
		}

		public override void Start()
		{
			this.StartAsync().Coroutine();
		}
		
		private async ETTask StartAsync()
		{
			Log.Info("init!");
			DontDestroyOnLoad(this.Entity);
			
			AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
			{
				ET.Log.Error(e.ExceptionObject.ToString());
			};

			try
			{
				// 命令行参数
				string[] args = "".Split(" ");
				Parser.Default.ParseArguments<Options>(args)
						.WithNotParsed(error => throw new Exception($"命令行格式错误! {error}"))
						.WithParsed((o)=>World.Instance.AddSingleton(o));
				Options.Instance.StartConfig = $"StartConfig/Localhost";
			}
			catch (Exception e)
			{
				this.Log.Error(e.ToString());
			}
			
			World.Instance.AddSingleton<Logger>().Log = new StrideGameLogger();
			ETTask.ExceptionHandler += ET.Log.Error;
			
			World.Instance.AddSingleton<TimeInfo>();
			World.Instance.AddSingleton<FiberManager>();
			
			World.Instance.AddSingleton<ReourceExtension,Stride.Engine.Scene>(this.Entity.Scene);

			ResourcesLoader resourcesLoader = World.Instance.AddSingleton<ResourcesLoader>();
			GlobalConfig loadGlobalConfig = resourcesLoader.LoadGlobalConfig();
			Options.Instance.AppType = loadGlobalConfig.AppType;
			
			
			CodeLoader codeLoader = World.Instance.AddSingleton<CodeLoader>();
			await codeLoader.DownloadAsync();
			
			codeLoader.StartAsync().Coroutine();
		}

		public override void Update()
		{
			TimeInfo.Instance.Update();
			FiberManager.Instance.Update();
			FiberManager.Instance.LateUpdate();
		}

		public override void Cancel()
		{
			base.Cancel();
			World.Instance.Dispose();
		}
	}
}