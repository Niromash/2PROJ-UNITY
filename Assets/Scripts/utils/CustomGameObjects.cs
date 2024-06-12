using System.Linq;
using UnityEngine;

public class CustomGameObjects
{
    public static GameObject FindMaybeDisabledGameObjectByName(string notDisabledParentName, string gameObjectName)
    {
        GameObject go = GameObject.Find(notDisabledParentName);
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