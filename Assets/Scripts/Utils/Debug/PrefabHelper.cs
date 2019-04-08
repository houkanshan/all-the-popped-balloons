#if (UNITY_EDITOR)
using UnityEngine;
using UnityEditor;

static public class PrefabHelper {
  static public void StartUndoRecord(Object target, string name = "Custom Undo") {
    UndoHelper.StartUndoRecord(target, name);
  }
  static public void SavePrefabComponent(Object target) {
    PrefabUtility.RecordPrefabInstancePropertyModifications(target);
  }
}
#endif