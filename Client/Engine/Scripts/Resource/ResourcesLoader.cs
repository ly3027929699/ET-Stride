using System;
using System.Collections.Generic;
using System.IO;
using Stride.Engine;
using UnityEngine;

namespace ET
{
    public class ResourcesLoader: Singleton<ResourcesLoader>, ISingletonAwake
    {
        private GlobalConfig? loadGlobalConfig;

        public void Awake()
        {
            
        }

        protected override void Destroy()
        {
            
        }

        /// <summary>
        /// 主要用来加载dll config aotdll，因为这时候纤程还没创建，无法使用ResourcesLoaderComponent。
        /// 游戏中的资源应该使用ResourcesLoaderComponent来加载
        /// </summary>
        public async ETTask<T> LoadAssetAsync<T>(string location) where T : class
        {
            //todo: 用外置资源系统加载
            return await ReourceExtension.Instance.LoadAsync<T>(location);
        }

        public GlobalConfig LoadGlobalConfig()
        {
            if (loadGlobalConfig != null)
                return loadGlobalConfig;
            Prefab loadBuiltin = this.LoadBuiltin<Prefab>("Res/GlobalConfig");
            Stride.Engine.Entity entity = loadBuiltin.Entities[0];
            this.loadGlobalConfig = entity.Get<GlobalConfig>();
            this.Release(loadBuiltin);
            return this.loadGlobalConfig;
        }

        public T LoadBuiltin<T>(string globalconfig) where T : class
        {
            return ReourceExtension.Instance.Load<T>(globalconfig);
        }
        public async ETTask<byte[]> LoadBytes(string globalconfig)
        {
            return await ReourceExtension.Instance.LoadBytesAsync(globalconfig);
        }
        public string LoadString(string globalconfig) 
        {
            return ReourceExtension.Instance.LoadString(globalconfig);
        }

        public async ETTask<Stride.Engine.Scene> LoadSceneAsync(string url)
        {
            return await ReourceExtension.Instance.LoadSceneAsync(url);
        }


        public void Release(object asset)
        {
             ReourceExtension.Instance.Release(asset);
        }
        

    }
}