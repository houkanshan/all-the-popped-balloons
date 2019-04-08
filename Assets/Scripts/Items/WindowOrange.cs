using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowOrange : MonoBehaviour {
  public InfoSpot[] ShowIfClicked;
  public GameObject targetHotSpot;

  SpriteRenderer spriteRenderer;

  private void Awake() {
    spriteRenderer = GetComponent<SpriteRenderer>();
  }

  private void OnEnable() {
    StartCoroutine(DisableTargetNextFrame());
    spriteRenderer.enabled = false;
  }

  // private void OnDisable() {
  //   print("disable");
  //   StartCoroutine(DisableTargetNextFrame());
  //   spriteRenderer.enabled = false;
  // }

  private void Update() {
    foreach(InfoSpot infoSpot in ShowIfClicked) {
      if (!infoSpot.isClicked) {
        return;
      }
    }
    targetHotSpot.SetActive(true);
    spriteRenderer.enabled = true;
  }

  IEnumerator DisableTargetNextFrame() {
    yield return new WaitForEndOfFrame();
    targetHotSpot.SetActive(false);
  }

}
