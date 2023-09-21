using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof(UI))]
    [FriendOf(typeof(UI))]
    public static partial class UISystem
    {
        [EntitySystem]
        private static void Awake(this UI self, string name, Stride.Engine.Entity gameObject)
        {
            self.nameChildren.Clear();
            self.Name = name;
            self.GameObject = gameObject;
        }
		
        [EntitySystem]
        private static void Destroy(this UI self)
        {
            foreach (UI ui in self.nameChildren.Values)
            {
                ui.Dispose();
            }

            self.GameObject.Scene = null;
            self.nameChildren.Clear();
        }

        public static void SetAsFirstSibling(this UI self)
        {
            throw new NotImplementedException();
        }

        public static void Add(this UI self, UI ui)
        {
            self.nameChildren.Add(ui.Name, ui);
        }

        public static void Remove(this UI self, string name)
        {
            UI ui;
            if (!self.nameChildren.TryGetValue(name, out ui))
            {
                return;
            }
            self.nameChildren.Remove(name);
            ui.Dispose();
        }

        public static UI? Get(this UI self, string name)
        {
            if (self.nameChildren.TryGetValue(name, out UI? child))
            {
                return child;
            }
            Stride.Engine.Entity? childGameObject = self.GameObject.Transform.Find(name)?.Entity;
            if (childGameObject == null)
            {
                return null;
            }
            child = self.AddChild<UI, string, Stride.Engine.Entity>(name, childGameObject);
            self.Add(child);
            return child;
        }
    }
    
    [ChildOf()]
    public sealed class UI: Entity, IAwake<string, Stride.Engine.Entity>, IDestroy
    {
        public Stride.Engine.Entity GameObject { get; set; }
		
        public string Name { get; set; }

        public Dictionary<string, UI> nameChildren = new Dictionary<string, UI>();
    }
}