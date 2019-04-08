using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

public class RandomText : MonoBehaviour {
  [ReorderableList]
  public string[] texts;
  public bool keepFirstText;
  public bool noRepeat;
  bool isFirstEnable = true;
  string lastText;
  TMPLeanText tMPLeanText;

  private void Awake() {
    tMPLeanText = GetComponent<TMPLeanText>();
  }

  private void OnEnable() {
    if (isFirstEnable && keepFirstText) { return; }
    string[] excludes = null;
    if (noRepeat) {
      excludes = new string[] { lastText, };
    }
    lastText = RandomHelper.RandomChoose(texts, excludes);
    tMPLeanText.PhraseName = lastText;
  }
}
