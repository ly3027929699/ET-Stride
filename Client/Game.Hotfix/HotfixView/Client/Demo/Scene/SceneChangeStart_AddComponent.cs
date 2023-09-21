using System;
using Stride.Core.Mathematics;
using Stride.Engine;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class SceneChangeStart_AddComponent: AEvent<Scene, SceneChangeStart>
    {
        protected override async ETTask Run(Scene root, SceneChangeStart args)
        {
            try
            {
                Scene currentScene = root.CurrentScene();

                ResourcesLoaderComponent resourcesLoaderComponent = currentScene.GetComponent<ResourcesLoaderComponent>();
            
                // 加载场景资源
                await resourcesLoaderComponent.LoadSceneAsync($"Scenes/{currentScene.Name}", LoadSceneMode.Single);
                // 切换到map场景

                //await SceneManager.LoadSceneAsync(currentScene.Name);

                currentScene.AddComponent<OperaComponent>();
                if (currentScene.Name == "Map1")
                {
                    TransformComponent transform = GlobelEngineScript.Default.Camera.Entity.Transform;
                    transform.Position = new Vector3(0f,14.015f,13.635f);
                    transform.Rotation = Quaternion.RotationX(MathUtil.DegreesToRadians(-43));
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

        }
    }
}