using UnityEngine;

namespace Oozeling.Helper
{
    /// <summary>
    /// MONOBEHAVIOR PSEUDO SINGLETON ABSTRACT CLASS
    /// example	: '''public  class MyClass : Singleton<MyClass> {'''
    /// </summary>
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T _instance;
    
        public static T Instance {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<T>();

                return _instance;
            }
        }
    }

}
