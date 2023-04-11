#if UNITY_EDITOR
using System;
using UnityEditor;
#endif
using UnityEngine;
namespace Kurisu.AkiPopup
{
    public class PopupSet:ScriptableObject
    {
        [SerializeField]
        private string[] values=new string[0];
        public string[] Values=>values;
        #if UNITY_EDITOR
        public static PopupSet GetOrCreateSettings(Type type)
        {
            var guids=AssetDatabase.FindAssets($"t:{type.FullName}");
            PopupSet setting=null;
            if(guids.Length==0)
            {
                setting = ScriptableObject.CreateInstance(type) as PopupSet;
                string k_SettingsPath = $"Assets/{type.Name}.asset";
                Debug.Log($"New {type.Name} Set Created! Saving Path:{k_SettingsPath}");
                AssetDatabase.CreateAsset(setting, k_SettingsPath);
                AssetDatabase.SaveAssets();
            }
            else setting=AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[0]),type) as PopupSet;
            return setting;
        }
        #endif
        public int GetStateID(string state)
        {
            if(values==null)return -1;
            for(int i=0;i<values.Length;i++)
            {
                if(values[i]==state)return i;
            }
            return -1;
        }
    }
}