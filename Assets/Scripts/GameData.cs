using System.Collections.Generic;
using Oozeling.Card;
using UnityEditor;
using UnityEngine;

namespace Oozeling.Helper
{

    public class GameData : Singleton<GameData>
    {
        [SerializeField] GameObject _cardUIPrefab;
        [SerializeField] GameObject _cardPlaceholderPrefab;
        [SerializeField] List<CardTemplate> _allUnitData ;
        public static GameObject CardUIPrefab => Instance._cardUIPrefab;
        public static GameObject CardPlaceHolderPrefab => Instance._cardPlaceholderPrefab;
        public static List<CardTemplate> AllCardData => Instance._allUnitData;


#if UNITY_EDITOR
        void OnValidate()
        {
            _allUnitData = GetAllInstances<CardTemplate>();
        }


        public static List<T> GetAllInstances<T>(string searchPath = null) where T : UnityEngine.Object
        {
            string[]
                guids = AssetDatabase.FindAssets("t:" +
                                                 typeof(T).Name); //FindAssets uses tags check documentation for more info
            if (searchPath != null)
                guids = AssetDatabase.FindAssets("t:" + typeof(T).Name, new[] {searchPath});

            List<T> result = new List<T>();
            for (int i = 0; i < guids.Length; i++) //probably could get optimized 
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                result.Add(AssetDatabase.LoadAssetAtPath<T>(path));
            }

            return result;
        }
#endif
    }

}
