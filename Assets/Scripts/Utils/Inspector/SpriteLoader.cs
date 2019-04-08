#if (UNITY_EDITOR)
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using NaughtyAttributes;


public class SpriteLoader : MonoBehaviour
{
  public string path = "Assets/Sprites/{name}.png";

  [Button]
  void Load() {
    if (Application.isPlaying) { return; }
    Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path.Replace("{name}", name));
    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.sprite = sprite;
  }
}
#endif