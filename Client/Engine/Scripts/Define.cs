namespace ET
{
	public static class Define
	{
		public const string CodeDir = "Assets/Bundles/Code/";
		public const string BuildOutputDir = "Temp/Bin/Debug";
#if DEBUG
		public static bool IsDebug = true;
#else
		public static bool IsDebug = false;
#endif
		
#if UNITY_EDITOR
		public static bool IsEditor = true;
#else
        public static bool IsEditor = false;
#endif
		
#if ENABLE_DLL
		public static bool EnableDll = true;
#else
        public static bool EnableDll = false;
#endif
		
#if ENABLE_VIEW
		public static bool EnableView = true;
#else
		public static bool EnableView = false;
#endif
		
#if ENABLE_IL2CPP
		public static bool EnableIL2CPP = true;
#else
		public static bool EnableIL2CPP = false;
#endif
	}
}