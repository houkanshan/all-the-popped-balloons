using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WireFrame: Editor {

  [MenuItem("Utility/Show WireFrame %h")]
  static void Show() {
    foreach(GameObject h in Selection.gameObjects) {
      var rend = h.GetComponent<Renderer>();
      if (rend)
        EditorUtility.SetSelectedRenderState(rend, EditorSelectedRenderState.Highlight | EditorSelectedRenderState.Wireframe);
    }
  }

  [MenuItem("Utility/Show WireFrame %h", true)]
  static bool CheckShow() {
    return Selection.activeGameObject != null;
  }

  [MenuItem("Utility/Hide WireFrame %h")]
  static void Hide() {
    foreach(GameObject h in Selection.gameObjects) {
      var rend = h.GetComponent<Renderer>();
      if (rend)
        EditorUtility.SetSelectedRenderState(rend, EditorSelectedRenderState.Hidden);
    }
  }

  [MenuItem("Utility/Hide WireFrame %h", true)]
  static bool CheckHide() {
    return Selection.activeGameObject != null;
  }
}
