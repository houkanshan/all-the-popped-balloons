using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Lean.Localization;

public class InfoSpot: Clickable {
  [LeanPhraseName]
  public string text;
  [HideInInspector] public bool isClicked = false;
  public GameObject targetItem;
  public TMPLeanText tMPLeanText;
  public Renderer hoverTarget;

  SpriteRenderer spriteRenderer;
  Collider2D coll;
  bool unhandledMouseClick = false;

  private void Awake() {
    spriteRenderer = GetComponent<SpriteRenderer>();
    coll = GetComponent<Collider2D>();
  }

  private void Start()
  {
    if (!GameManager.i.showSpot) {
      Color c = spriteRenderer.color;
      c.a = 0;
      spriteRenderer.color = c;
    }
    coll.offset = new Vector2(0, -0.05f / transform.localScale.y);
  }

  private void OnDisable() {
    tMPLeanText.PhraseName = "Empty";
    if (hoverTarget) {
      hoverTarget.enabled = false;
    }
  }

  private void OnMouseUp() {
    unhandledMouseClick = true;
  }

  private void OnMouseEnter() {
    if (hoverTarget) {
      hoverTarget.enabled = true;
    }
  }
  private void OnMouseExit() {
    if (hoverTarget) {
      hoverTarget.enabled = false;
    }
  }

  private void Update() {
    if (unhandledMouseClick) {
      unhandledMouseClick = false;
      UpdateText();
      isClicked = true;
      if (targetItem) {
        targetItem.SetActive(true);
      }
    }
    Render();
  }

  void Render() {
  }

  void UpdateText() {
    tMPLeanText.PhraseName = text;
  }

  #if (UNITY_EDITOR)
  [Button]
  void TestText() {
    if (tMPLeanText.PhraseName == text) {
      tMPLeanText.PhraseName = "Empty";
    } else {
      UpdateText();
    }
  }
  #endif

}
