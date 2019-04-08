
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(AudioObject))]
public class MusicObjectEditor: Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AudioObject musicObject = (AudioObject)target;
        if(GUILayout.Button("Update Name")) {
          string assetPath = AssetDatabase.GetAssetPath(musicObject.GetInstanceID());
          AssetDatabase.RenameAsset(assetPath, musicObject.clip.name);
          AssetDatabase.SaveAssets();
        }
    }
}
