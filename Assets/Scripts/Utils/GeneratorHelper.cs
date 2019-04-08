using UnityEngine;
using System.Collections;
#if (UNITY_EDITOR)
using UnityEditor;
#endif

static public class GeneratorHelper {
  static public GameObject genGameObject(string name, Transform parent, Vector3 position) {
    GameObject g = new GameObject(name);
    g.transform.parent = parent;
    g.transform.localPosition = position;
    return g;
  }
  static public GameObject genGameObject(string name, Transform parent) {
    return genGameObject(name, parent, Vector3.zero);
  }

  static public GameObject genFromPrefab(GameObject prefab, Transform parent, Vector3 position) {
    GameObject ga;
#if (UNITY_EDITOR)
    if (Application.isPlaying) {
      ga = MonoBehaviour.Instantiate(prefab, parent, false);
    } else {
      ga = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
      ga.transform.parent = parent;
    }
#else
    ga = MonoBehaviour.Instantiate(prefab, parent, false);
#endif

    ga.transform.localPosition = position;
    return ga;
  }
  static public GameObject genFromPrefab(GameObject prefab, Transform parent = null) {
    return genFromPrefab(prefab, parent, Vector3.zero);
  }

  static public GameObject CopyToSibling(GameObject g) {
    var trans = g.transform;
    var copy = MonoBehaviour.Instantiate(g, trans.parent);
    var siblingIndex = trans.GetSiblingIndex();
    copy.transform.SetSiblingIndex(siblingIndex + 1);
    return copy;
  }
}