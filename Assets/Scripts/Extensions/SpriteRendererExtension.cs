using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class SpriteRendererExtension
{
  public static void FadeSprite(this SpriteRenderer renderer,
    MonoBehaviour mono,
    float duration,
    Action<SpriteRenderer> callback = null
  )
  {
    mono.StartCoroutine(FadeCoroutine(renderer, duration, callback));
  }

  private static IEnumerator FadeCoroutine (SpriteRenderer renderer,
    float duration,
    Action<SpriteRenderer> callback
  )
  {
    // Fading animation
    float start = Time.time;
    while (Time.time <= start + duration)
    {
      Color color = renderer.color;
      color.a = 1f - Mathf.Clamp01((Time.time - start) / duration);
      renderer.color = color;
      yield return new WaitForEndOfFrame();
    }

    // Callback
    if (callback != null)
      callback(renderer);
  }
}