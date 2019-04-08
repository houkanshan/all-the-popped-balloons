#if (UNITY_EDITOR)
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;


public class PixelAligner : MonoBehaviour
{
	[MenuItem("2D/Align to pixel #&i")]
  public static void Align() {
    if (Application.isPlaying) { return; }
    foreach(var trans in Selection.transforms) {
      UndoHelper.StartUndoRecord(trans);
      Vector3 pos = trans.localPosition;
      pos.x = Mathf.Round(pos.x * 100) / 100;
      pos.y = Mathf.Round(pos.y * 100) / 100;
      trans.localPosition = pos;
    }
  }
}
#endif
