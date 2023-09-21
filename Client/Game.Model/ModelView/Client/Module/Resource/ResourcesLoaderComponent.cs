using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof(ResourcesLoaderComponent))]
    [FriendOf(typeof(ResourcesLoaderComponent))]
    public static partial class ResourcesLoaderComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ResourcesLoaderComponent self)
        {
            
        }
        
        [EntitySystem]
        private static void Awake(this ResourcesLoaderComponent self, string packageName)
        {
            
        }
        
        [EntitySystem]
        private static void Destroy(this ResourcesLoaderComponent self)
        {
            foreach (var kv in self.handlers)
            {
                ResourcesLoader.Instance.Release(kv.Value);
            }
        }

        public static async ETTask<T> LoadAssetAsync<T>(this ResourcesLoaderComponent self, string location)where T : class
        {
            using CoroutineLock coroutineLock = await self.Fiber().CoroutineLockComponent.Wait(CoroutineLockType.ResourcesLoader, location.GetHashCode());
           
            if (!self.handlers.TryGetValue(location, out var handler))
            {
                handler= await ResourcesLoader.Instance.LoadAssetAsync<T>(location);
                self.handlers.Add(location, handler);
            }
            
            return (T)handler;
        }
        
        public static async ETTask LoadSceneAsync(this ResourcesLoaderComponent self, string location, LoadSceneMode loadSceneMode)
        {
            using CoroutineLock coroutineLock = await self.Fiber().CoroutineLockComponent.Wait(CoroutineLockType.ResourcesLoader, location.GetHashCode());

            if (self.handlers.TryGetValue(location, out var handler))
            {
                Log.Error("Load scene fail. Becase of the scene is already added to the handlers");
                return;
            }
            handler= await ResourcesLoader.Instance.LoadSceneAsync(location);
            self.handlers.Add(location, handler);
        }
    }
    
    /// <summary>
    /// 用来管理资源，生命周期跟随Parent，比如CurrentScene用到的资源应该用CurrentScene的ResourcesLoaderComponent来加载
    /// 这样CurrentScene释放后，它用到的所有资源都释放了
    /// </summary>
    [ComponentOf]
    public class ResourcesLoaderComponent: Entity, IAwake, IAwake<string>, IDestroy
    {
        public Dictionary<string, object> handlers = new();
    }
}