using UnityEngine;

public class ComponentUtilities
{
    public static T safelyGetComponent<T>(GameObject gameObject) where T : Component
    {
        T component = gameObject.GetComponent<T>();
        if (component == null) component = gameObject.AddComponent<T>();
        return component;
    }
}
