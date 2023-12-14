﻿using EXTool;
using UnityEditor;

namespace GAS.Core
{
    public static class GasDefine
    {
        public const string GAS_VERSION = "1.0.0";
        
        public const string GAS_MENU_ROOT = "Gameplay Ability System/";
        
        public const int GAS_TAG_MAX_GENERATIONS = 5;
        
        public const string GAS_ASSET_FOLDER_NAME = "exGAS_Setting";

        public static string GAS_ASSET_PATH => $"Assets/{GAS_ASSET_FOLDER_NAME}";
        public static string GAS_TAG_ASSET_PATH => $"{GAS_ASSET_PATH}/GameplayTagsAsset.asset";

        public static void CheckGasAssetFolder()
        {
            if (!AssetDatabase.IsValidFolder(GAS_ASSET_PATH))
            {
                AssetDatabase.CreateFolder("Assets", GAS_ASSET_FOLDER_NAME);
                EXLog.Log($"{GAS_ASSET_FOLDER_NAME} folder created!");
            }
        }
    }
}