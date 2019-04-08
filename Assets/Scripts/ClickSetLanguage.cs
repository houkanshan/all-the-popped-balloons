using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Localization;

public class ClickSetLanguage : MonoBehaviour {
  [LeanLanguageName]
  public string language;

  private void OnMouseDown() {
    LeanLocalization.AllLocalizations[0].SetCurrentLanguage(language);
  }

}
