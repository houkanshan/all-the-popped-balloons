using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotSpot : Clickable {
  public Slide targetSlide;
  public AudioObject audioInteract;
  public IntVariable add1WhenClick;
  public bool handleWhenMouseDown = false;
  public SpriteRenderer hoverTarget;

  SpriteRenderer spriteRenderer;
  Collider2D coll;
  bool preparingMouseClick = false;
  bool unhandledMouseClick = false;

#if UNITY_EDITOR
  [SHOW_IN_HIER(width: 11)]
  string Label {
    get {
      return add1WhenClick == null ? "" : "+";
    }
  }
#endif

  private void Awake() {
    spriteRenderer = GetComponent<SpriteRenderer>();
    coll = GetComponent<Collider2D>();
    if (hoverTarget) {
      hoverTarget.enabled = false;
    }
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

  private void OnMouseDown() {
    preparingMouseClick = true;
    if (handleWhenMouseDown) {
      unhandledMouseClick = true;
    }
  }
  private void OnMouseUp() {
    if (handleWhenMouseDown) { return; }
    if (preparingMouseClick) {
      unhandledMouseClick = true;
    }
    preparingMouseClick = false;
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
  private void OnDisable() {
    if (hoverTarget) {
      hoverTarget.enabled = false;
    }
  }

  private void Update() {
    if (unhandledMouseClick) {
      unhandledMouseClick = false;
      if (targetSlide) {
        Slider.TurnToSlide(targetSlide);
      } else {
        Debug.LogError(transform.parent.name + "'s hotSpot hasn't set targetSlide");
      }
      if (add1WhenClick) {
        add1WhenClick.ApplyChange(1);
      }
      GameManager.i.PlayInteraction(audioInteract);
    }
    Render();
  }

  void Render() {
  }

  #if (UNITY_EDITOR)
  public void OnDrawGizmos() {
    var slideTrans = transform.parent;
    var from = slideTrans.GetComponent<Slide>();
    if (targetSlide) {
      var comparer = GetComponent<CompareVariables>();
      var inCurrent = true;
      if (comparer && !comparer.Predicate()) {
        inCurrent = false;
      }
      DrawSlideLink(from, targetSlide, inCurrent);
    }
  }
  void DrawSlideLink(Slide from, Slide to, bool inCurrent) {
    var fromTrans = from.transform;
    if (GameManager.i == null) { return; }

    Gizmos.color = GameManager.i.normalLink;
    if (from.fadeOut) {
      Gizmos.color = GameManager.i.fadeLink;
    }
    if (from.markRemoved) {
      Gizmos.color = ColorHelper.alphaTo(Gizmos.color, 0.2f);
    } else {
      Gizmos.color = ColorHelper.alphaTo(Gizmos.color, inCurrent ? 1f : 0.5f);
    }

    Vector3 targetPos = to.transform.position;
    if (Mathf.Abs(targetPos.x - fromTrans.position.x) > Mathf.Abs(targetPos.y - fromTrans.position.y)) {
      targetPos.x += (targetPos.x < fromTrans.position.x ? 1f : -1f) * 0.64f; // 128 / 2 / 100;
    } else {
      targetPos.y += (targetPos.y < fromTrans.position.y ? 1f : -1f) * 0.64f; // 128 / 2 / 100;
    }
    GizmoHelper.DrawArrowedLine(
      transform.position, targetPos,
      lineStyle: inCurrent ? LineStyle.Solid : LineStyle.Dotted
      );
  }
#endif
}
