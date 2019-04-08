using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Lean.Localization;

public class LinkSpot: MonoBehaviour {

	[MenuItem("Grandma/Link Selected Spot to Slide %l")]
	public static void LinkSpotToSlide()
	{
    List<HotSpot> spots = new List<HotSpot>();
    Slide slide = null;
    foreach(var trans in Selection.transforms) {
      var spot = trans.GetComponent<HotSpot>();
      if (spot) {
        spots.Add(spot);
      }
      if (slide == null) {
        slide = trans.GetComponent<Slide>();
      }
    }
    if (spots.Count != 0 && slide != null) {
      foreach(var spot in spots) {
        Undo.RecordObject(spot, "Link HotSpot to Slide");
        spot.targetSlide = slide;
        PrefabHelper.SavePrefabComponent(spot);
      }
    }
    Selection.activeGameObject = null;
	}

	[MenuItem("Grandma/Link Selected Slides #&l")]
	public static void LinkSlides()
	{
    List<Slide> slides = new List<Slide>();
    foreach(var trans in Selection.transforms) {
      var slide = trans.GetComponent<Slide>();
      if (slide)  {
        slides.Add(slide);
      }
    }
    slides.Sort((x, y) => {
      if (y == null) return 1;
      return x.transform.position.x.CompareTo(y.transform.position.x);
    });

    Slide prevSlide = null;
    foreach(Slide slide in slides) {
      if (prevSlide != null)  {
        prevSlide.LinkToSlide(slide);
      }
      prevSlide = slide;
    }

    Selection.activeGameObject = null;
	}

  [MenuItem("Grandma/Fadeout #&f")]
  public static void ToggleSlideFadeout() {
    var slide = SelectionHelper.GetComponent<Slide>();
    PrefabHelper.StartUndoRecord(slide);
    slide.fadeOut = !slide.fadeOut;
  }

  [MenuItem("Grandma/Start from selected slide")]
  public static void StartFromSlide() {
    var slide = SelectionHelper.GetComponent<Slide>();
    if (slide != null) {
      GameManager.i.initialSlide = slide;
    }
    PrefabHelper.SavePrefabComponent(GameManager.i);
    UnityEditor.EditorApplication.isPlaying = true;
  }

  [MenuItem("Grandma/Update Text #&u")]
  public static void UpdateText() {
    LeanLocalization.AllLocalizations[0]
      .GetComponent<LeanLocalizationMultiLoader>().LoadFromSource();
  }

}