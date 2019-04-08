using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "FontSetting", menuName="FontSetting")]
public class FontSetting : ScriptableObject {
  public int fontSize = 1;
  public TMP_FontAsset font;
}
