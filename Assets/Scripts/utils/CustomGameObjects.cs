using System.Linq;
using UnityEngine;

public class CustomGameObjects
{
    public static GameObject FindMaybeDisabledGameObjectByName(string parentName, string gameObjectName)
    {
        GameObject go = GameObject.Find(parentName);
        if (go == null)
        {
            return null;
        }

        Transform childTransform
            = go.transform.GetComponentsInChildren<Transform>(true).FirstOrDefault(
                t => t.name == gameObjectName
            );
        if (childTransform == null)
        {
            return null;
        }

        return childTransform.gameObject;
    }
}