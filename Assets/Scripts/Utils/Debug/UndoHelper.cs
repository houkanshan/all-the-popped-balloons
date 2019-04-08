#if (UNITY_EDITOR)
using UnityEngine;
using UnityEditor;

static public class UndoHelper {
  static public void StartUndoRecord(Object target, string name = "Custom Undo") {
    Undo.RecordObject(target, name);
  }
}
#endif