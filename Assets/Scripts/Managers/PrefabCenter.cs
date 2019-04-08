using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PrefabCenter : MonoBehaviour
{
  public static PrefabCenter i;
  public GameObject HotSpot;

  void Awake() {
    i = this;
  }
#if UNITY_EDITOR
  void Update() {
    if (Application.isPlaying) { return; }
    if (i != null) { return; }
    i = this;
  }
#endif
}