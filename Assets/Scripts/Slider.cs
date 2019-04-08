using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using NaughtyAttributes;

[ExecuteInEditMode]
public class Slider : MonoBehaviour {
  public List<Slide> slides;
  public Slide currentSlide;

  public static Slider i;

  void Awake() {
    i = this;

    slides = new List<Slide>();
    foreach (Transform c in transform) {
      Slide s = c.GetComponent<Slide>();
      slides.Add(s);
      s.slider = this;
    }
    if (GameManager.i.setInitialSlide && GameManager.i.initialSlide != null) {
      currentSlide = GameManager.i.initialSlide;
    } else {
      currentSlide = slides[0];
    }
  }

  public static void TurnToSlide(Slide slide) {
    i.currentSlide = slide;
  }

#if (UNITY_EDITOR)
  // [Button]
  public void AppendTo() {
    int last = 94;
    int maxIndex = 0;
    foreach(Transform t in transform) {
      int index = Convert.ToInt32(t.name) - 1;
      maxIndex = Math.Max(index, maxIndex);
    }

    if (last <= maxIndex) { return; }

    int i;
    for (i = maxIndex + 1; i <= last; i ++) {
      GameObject slideGO = GeneratorHelper.genGameObject((i + 1).ToString(), transform);
      slideGO.AddComponent<SpriteRenderer>();
      Slide slide = slideGO.AddComponent<Slide>();
      slide.UpdateMap();
      slide.RePosition();
    }
  }
  [Button]
  public void AutoLinkHotSpot() {
    HotSpot prevHotSpot = null;
    foreach(Transform t in transform) {
      Slide slide = t.GetComponent<Slide>();
      HotSpot hotSpot;

      // Make link
      if (prevHotSpot && prevHotSpot.targetSlide == null) {
        prevHotSpot.targetSlide = slide;
      }

      // Get hot spot
      Transform hotSpotTransform = transform.Find("HotSpot");
      if (hotSpotTransform == null) {
        hotSpot = slide.AddHotSpot();
      } else {
        hotSpot = hotSpotTransform.GetComponent<HotSpot>();
      }

      prevHotSpot = hotSpot;
    }
  }
#endif
}
