#if (UNITY_EDITOR)
using UnityEngine;
using UnityEditor;

static public class ScriptableObjectHelper {
  static public void Save(Object target) {
    EditorUtility.SetDirty(target);
    AssetDatabase.SaveAssets();
  }

  public static T[] GetAllInstances<T>() where T : ScriptableObject
  {
    string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);  //FindAssets uses tags check documentation for more info
    T[] a = new T[guids.Length];
    for (int i = 0; i < guids.Length; i++)         //probably could get optimized
    {
      string path = AssetDatabase.GUIDToAssetPath(guids[i]);
      a[i] = AssetDatabase.LoadAssetAtPath<T>(path);
    }

    return a;
  }
}
#endif