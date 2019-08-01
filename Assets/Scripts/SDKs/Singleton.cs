/**
 * Singleton.cs
 * Created by: Joao Borks
 * Created on: 08/04/17 (dd/mm/yy)
 */

using UnityEngine;

/// <summary>
/// Abstract class for any singleton
/// </summary>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// The active instance of this <see cref="Singleton{T}"/>
    /// </summary>
    public static T Instance
    {
        get
        {
#if UNITY_EDITOR
            if (isApplicationQuitting)
                return null;
#endif
            if (!instance) // Local instance is null
            {
                instance = FindObjectOfType<T>();
                if (!instance) // No object was found, so create one
                    instance = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
            }
            return instance;
        }
    }

    /// <summary>
    /// Local reference of the active instance of this <see cref="Singleton{T}"/>
    /// </summary>
    static T instance;

#if UNITY_EDITOR
    /// <summary>
    /// Used to identify when the editor is closing the game application to avoid scene errors
    /// </summary>
    static bool isApplicationQuitting;
#endif

    protected virtual void Awake()
    {
        if (!instance)
            instance = GetComponent<T>();
        else if (instance && instance != this)
            Destroy(gameObject); // Destroys any duplicate of the singleton object
        DontDestroyOnLoad(gameObject);
    }

    protected virtual void OnDestroy()
    {
#if UNITY_EDITOR
        isApplicationQuitting = true;
#endif
    }
}