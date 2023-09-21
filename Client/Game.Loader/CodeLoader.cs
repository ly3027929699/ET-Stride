using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

#pragma warning disable CS0162

namespace ET
{
    public class CodeLoader: Singleton<CodeLoader>, ISingletonAwake
    {
        private Assembly assembly;

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
                assBytes =  await ReourceExtension.Instance.LoadBytesAsync("Res/Dlls/Game.Model.dll");
                pdbBytes =  await ReourceExtension.Instance.LoadBytesAsync("Res/Dlls/Game.Model.pdb");
            }
            else
            {
                
            }

            this.assembly = Assembly.Load(assBytes, pdbBytes);
            Assembly hotfixAssembly =await this.LoadHotfixAsync();

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
                assBytes =  await ReourceExtension.Instance.LoadBytesAsync("Res/Dlls/Game.Hotfix.dll");
                pdbBytes =  await ReourceExtension.Instance.LoadBytesAsync("Res/Dlls/Game.Hotfix.pdb");
            }
            else
            {
            }

            Assembly hotfixAssembly = Assembly.Load(assBytes, pdbBytes);

            return hotfixAssembly;
        }

        public async ETTask Reload()
        {
            Assembly hotfixAssembly =await this.LoadHotfixAsync();

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