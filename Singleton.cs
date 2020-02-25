using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T _instance;

    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            lock (_lock)
            {
                if(null == _instance)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        print("Never be more than 1 singleton.");
                        return _instance;
                    }

                    if(null == _instance)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton)" + typeof(T).ToString();

                        DontDestroyOnLoad(singleton);
                    }
                }

                return _instance;
            }
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnRuntimeMethodLoad()
    {
        T _instance = Instance;
        print("Singleton.OnRuntimeMethodLoad");
    }

    public virtual void OnDestroy()
    {
        _instance = null;
    }
}
