using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
[CreateAssetMenu(fileName = "ReloadAlert", menuName = "Inventory/Editor", order = 1)]
public class ReloadAlert : ScriptableObject
{
#if (UNITY_EDITOR)
  [InfoBox("Note: only one object is needed in a project.")]
  public bool alertReload = true;
  public bool clearConsoleOnBuild = true;

  static ReloadAlert() {
    BuildPlayerWindow.RegisterBuildPlayerHandler(OnPreProcessBuild);
  }

  static void OnPreProcessBuild(BuildPlayerOptions _opt) {
    var setting = GetSetting();
    if (setting && setting.clearConsoleOnBuild) {
      CleanConsole.ClearLogConsole();
      Debug.Log("Console cleared");

    }
  }

  [UnityEditor.Callbacks.DidReloadScripts]
  static void OnScriptsReloaded()
  {
    var setting = GetSetting();
    if (setting && setting.alertReload && !EditorApplication.isPlayingOrWillChangePlaymode) {
      UnityEditor.EditorUtility.DisplayDialog("Script Reloaded", "Script reloaded.", "OK");
    }
  }

  static ReloadAlert GetSetting() {
    var settings = ScriptableObjectHelper.GetAllInstances<ReloadAlert>();
    if (settings.Length != 0) {
      return settings[0];
    }
    return null;
  }
#endif
}
