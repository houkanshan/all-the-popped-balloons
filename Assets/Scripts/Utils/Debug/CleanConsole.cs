#if (UNITY_EDITOR)
using UnityEditor;
#endif
using System.Reflection;

public static class CleanConsole {

  public static void ClearLogConsole() {
#if UNITY_5
    var logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
#else
    var logEntries = System.Type.GetType("UnityEditor.LogEntries,UnityEditor.dll"); // Unity 2017 - 2018
#endif
    var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
    clearMethod.Invoke(null, null);
  }
}