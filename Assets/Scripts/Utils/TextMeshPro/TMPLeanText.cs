using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Localization;
using TMPro;

[ExecuteInEditMode]
[RequireComponent(typeof(TextMeshPro))]
public class TMPLeanText : LeanLocalizedBehaviour {
  public override void UpdateTranslation(LeanTranslation translation) {
    FontSetting font = null;
    if (translation == null) {
      // Try default language
      translation = LeanLocalization.GetDefaultTranslation(PhraseName);
      var fontTrans = LeanLocalization.GetDefaultTranslation("Font");
      if (fontTrans != null) {
        font = fontTrans.Object as FontSetting;
      }
    } else {
      font = LeanLocalization.GetTranslationObject("Font") as FontSetting;
    }

    if (translation == null) { return; }

    // Update text
    var text = GetComponent<TextMeshPro>();
    text.text = translation.Text;

    // Update font
    if (font != null) {
      text.font = font.font;
      text.fontSize = font.fontSize;
    }
  }
}
