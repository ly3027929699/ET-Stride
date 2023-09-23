using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using UnityEngine;

#pragma warning disable CS0162

namespace ET
{
    public class CodeLoader: Singleton<CodeLoader>, ISingletonAwake
    {
        private Assembly assembly;
        private AssemblyLoadContext assemblyLoadContext;

        public void Awake()
        {
        }

        public async ETTask DownloadAsync()
        {
            if (!Define.IsEditor)
            {
                // this.dlls = await ResourcesComponent.Instance.LoadAssetAsync<TextAsset>($"Assets/Bundles/Code/Model.dll.bytes");
                // this.aotDlls = await ResourcesComponent.Instance.LoadAssetAsync<TextAsset>($"Assets/Bundles/AotDlls/mscorlib.dll.bytes");
            }
        }

        public async ETTask StartAsync()
        {
            byte[] assBytes = null!;
            byte[] pdbBytes = null!;
            if (Define.IsEditor)
            {
            }
            else
            {
            }
            assBytes = await ReourceExtension.Instance.LoadBytesAsync("Res/Dlls/Game.Model.dll");
            pdbBytes = await ReourceExtension.Instance.LoadBytesAsync("Res/Dlls/Game.Model.pdb");

            this.assemblyLoadContext = new AssemblyLoadContext("ClientDomain", true);
            using Stream dllStream = new MemoryStream(assBytes);
            using Stream pdbStream = new MemoryStream(pdbBytes);
            this.assembly = this.assemblyLoadContext.LoadFromStream(dllStream, pdbStream);
            Assembly hotfixAssembly = await this.LoadHotfixAsync();

            World.Instance.AddSingleton<CodeTypes, Assembly[]>(new[]
            {
                typeof (World).Assembly, typeof (Init).Assembly, this.assembly, hotfixAssembly
            });

            IStaticMethod start = new StaticMethod(this.assembly, "ET.Entry", "Start");
            start.Run();
        }

        private async ETTask<Assembly> LoadHotfixAsync()
        {
            
            byte[] assBytes = null!;
            byte[] pdbBytes = null!;
            if (Define.IsEditor)
            {
            }
            else
            {
            }
            assBytes = await ReourceExtension.Instance.LoadBytesAsync("Res/Dlls/Game.Hotfix.dll");
            pdbBytes = await ReourceExtension.Instance.LoadBytesAsync("Res/Dlls/Game.Hotfix.pdb");

            using Stream dllStream = new MemoryStream(assBytes);
            using Stream pdbStream = new MemoryStream(pdbBytes);
            Assembly hotfixAssembly = assemblyLoadContext.LoadFromStream(dllStream, pdbStream);

            return hotfixAssembly;
        }

        public async ETTask Reload()
        {
            assemblyLoadContext?.Unload();
            GC.Collect();
            assemblyLoadContext = new AssemblyLoadContext("ClientDomain", true);
            Assembly hotfixAssembly = await this.LoadHotfixAsync();

            CodeTypes codeTypes =
                    World.Instance.AddSingleton<CodeTypes, Assembly[]>(new[]
                    {
                        typeof (World).Assembly, typeof (Init).Assembly, this.assembly, hotfixAssembly
                    });
            codeTypes.CreateCode();

            Log.Debug($"reload dll finish!");
        }
    }
}