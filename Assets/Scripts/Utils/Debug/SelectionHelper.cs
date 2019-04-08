#if (UNITY_EDITOR)
using System.Collections.Generic;
using UnityEditor;

public static class SelectionHelper {
  static public T GetComponent<T>() {
    T t = default(T);
    foreach(var trans in Selection.transforms) {
      t = trans.GetComponent<T>();
      if (t != null) {
        return t;
      }
    }
    return t;
  }

  static public List<T> GetComponents<T>() {
    List<T> ts = new List<T>();
    foreach(var trans in Selection.transforms) {
      T t = trans.GetComponent<T>();
      if (t != null) {
        ts.Add(t);
      }
    }
    return ts;
  }
}
#endif