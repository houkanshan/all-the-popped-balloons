using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using NaughtyAttributes;

[SelectionBase]
[ExecuteInEditMode]
public class Dialog : MonoBehaviour {
  [OnValueChanged("OnSizeChanged")]
  public Vector2 size = new Vector2(110, 21);
  [OnValueChanged("OnSizeChanged")]
  public bool isLeftSide = true;
  public bool hasArrow = true;
  public bool autoPosition = true;
  public Transform body;
  public Transform down;
  public Transform arrow;

#if UNITY_EDITOR
  void OnSizeChanged() {
    var w = Mathf.Floor(size.x);
    var h = Mathf.Floor(size.y);
    body.localScale = new Vector3(w, h);
    down.localPosition = new Vector3(
      isLeftSide ? 0 : (w / 100f),
      - h / 100f,
      0
    );

    var spriteRenderer = down.GetComponent<SpriteRenderer>();
    spriteRenderer.flipX = !isLeftSide;

    if (autoPosition) {
      transform.localPosition = new Vector3(
        isLeftSide ? -0.62f : 0.62f - w / 100f,
        -0.02f,
        0
      );
    }

    arrow.gameObject.SetActive(hasArrow);
    if (hasArrow) {
      arrow.localPosition = new Vector3((w - 4) / 100, - (h - 3) / 100, 0);
    }
  }
  private void OnDrawGizmos() {
    if (Selection.activeGameObject == gameObject) {
      Gizmos.color = Color.blue;
    } else {
      Gizmos.color = ColorHelper.alphaTo(Color.gray, 0.5f);
    }
    var w = Mathf.Floor(size.x) / 100;
    var h = Mathf.Floor(size.y) / 100;
    Gizmos.DrawWireCube(
      transform.position + new Vector3(w / 2, - h / 2, 0),
      new Vector3(w, h, 0)
    );
  }
#endif
}
