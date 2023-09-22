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
            
                // change the scene
                await resourcesLoaderComponent.LoadSceneAsync($"Scenes/{currentScene.Name}", LoadSceneMode.Single);

                currentScene.AddComponent<OperaComponent>();
                if (currentScene.Name == "Map1")
                {
                    TransformComponent transform = GlobelEngineScript.Default.Camera.Entity.Transform;
                    transform.Position = new Vector3(-4.606f,27.51f,11.088f);
                    transform.Rotation = Quaternion.RotationX(MathUtil.DegreesToRadians(-45));
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }

        }
    }
}