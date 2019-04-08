using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NaughtyAttributes;
#if (UNITY_EDITOR)
using UnityEditor;
#endif



public class Slide : MonoBehaviour {
  const float baseX = 0;
  const float baseY = 0;
  const float stepX = 1.5f;
  const float defaultFadeDuration = 0.8f;

  public Slider slider;
  [TextArea(1, 3)]
  public string note;
  public AudioObject audioEnvHuman;
  public AudioObject audioEnvNature;
  public float volumeEnvHuman = -1f;
  public float volumeEnvNature = -1f;
  public bool fadeOut = false;
  public bool markRemoved = false;
  public bool FadeOut {
    get {
      return fadeOut && GameManager.i.loopedTimes.Value == 0;
    }
  }
  public float fadeDuration = defaultFadeDuration;

  Vector3 centerPosition = new Vector3(0, 0, 0);
  bool show = false;
  bool lastShow = true;
  SpriteRenderer selfRenderer;

  void Awake() {
    selfRenderer = GetComponent<SpriteRenderer>();
  }

  void Start () {

  }

  void Update () {
    if (slider == null) { return; }
    if (slider.currentSlide == this)
    {
      if (!show) {
        transform.position = centerPosition;
        show = true;
      }
    } else {
      if (show) {
        show = false;
      }
    }

    Render();

    lastShow = show;
  }

  void Render() {
    if (lastShow != show) {
      ToggleChildren(show);
      if (show) {
        GameManager.i.SetAudioEnvHuman(audioEnvHuman, volumeEnvHuman);
        GameManager.i.SetAudioEnvNature(audioEnvNature, volumeEnvNature);
        selfRenderer.enabled = true;
        selfRenderer.color = Color.white;
#if UNITY_EDITOR
        Debug.Log("Current Slide: " + name);
#endif
      } else {
        if (FadeOut) {
          selfRenderer.sortingOrder = 1;
          selfRenderer.FadeSprite(this, fadeDuration, (renderer) => {
            renderer.enabled = false;
            renderer.sortingOrder = 0;
          });
        } else {
          selfRenderer.enabled = false;
        }
      }
    }
  }

  void ToggleChildren(bool show) {
    foreach(Transform t in transform) {
      if (show) {
        var comparer = t.GetComponent<CompareVariables>();
        if (comparer) {
          t.gameObject.SetActive(comparer.Predicate());
        } else {
          t.gameObject.SetActive(true);
        }
      } else {
        t.gameObject.SetActive(false);
      }
    }
  }

#if (UNITY_EDITOR)
  private void OnDrawGizmos() {
    // Label Gizmo
    Vector3 pos = transform.position;
    pos.x -= 0.64f;
    pos.y -= 0.64f;
    Handles.Label(pos, name);

    GUIStyle style = new GUIStyle();
    style.normal.textColor = Color.yellow;
    pos.y += 1.28f + 0.12f;
    Handles.Label(pos, note, style);

    // Fade Gizmo
    if (fadeOut && fadeDuration != defaultFadeDuration) {
      style = new GUIStyle();
      style.normal.textColor = Color.green;
      style.alignment = TextAnchor.LowerRight;
      pos = transform.position;
      pos.x += 0.64f;
      pos.y -= 0.64f;
      Handles.Label(pos, fadeDuration.ToString(), style);
    }
  }
  [Button]
  public void UpdateMap() {
    if (Application.isPlaying) { return; }
    // Load Sprite
    Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Slides/" + name + ".png");
    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    spriteRenderer.sprite = sprite;
  }
  [Button]
  public HotSpot AddHotSpot() {
    if (Application.isPlaying) { return null; }
    GameObject g = GeneratorHelper.genFromPrefab(PrefabCenter.i.HotSpot, transform);
    return g.GetComponent<HotSpot>();
  }
  // [Button]
  public void RePosition() {
    if (Application.isPlaying) { return; }
    Undo.RecordObject(transform, "reposition");
    if (transform.position == Vector3.zero) {
      int index = Convert.ToInt32(name) - 1;
      transform.position = new Vector3(baseX + stepX * index, baseY, 0);
    } else {
      transform.position = Vector3.zero;
    }
  }
  [Button]
  public void Duplicate() {
    var g = GeneratorHelper.CopyToSibling(gameObject);
    g.transform.position += new Vector3(1, -1, 0);
  }
  public void LinkToSlide(Slide slide) {
    var spots = GetComponentsInChildren<HotSpot>();
    if (slide) {
      foreach(var spot in spots) {
        PrefabHelper.StartUndoRecord(spot, "Link HotSpot to Slide");
        spot.targetSlide = slide;
        PrefabHelper.SavePrefabComponent(spot);
      }
    }
  }
  [Button]
  public void LinkToNext() {
    var currentIndex = transform.GetSiblingIndex();
    var nextSlide = transform.parent.GetChild(currentIndex + 1);
    if (nextSlide) {
      var slide = nextSlide.GetComponent<Slide>();
      LinkToSlide(slide);
    }
  }
#endif
}
