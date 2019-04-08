using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Audio", menuName = "Inventory/Audio", order = 1)]
public class AudioObject: ScriptableObject {
  public AudioClip clip;
  public float volume = 1f;
  public bool randomStartPoint = false;
  public bool loop = true;
  [Tooltip("dirty")]
  public bool onlyPlayOnce = false;
  public virtual AudioObject GetAudio() {
    return this;
  }
  public float getStartPoint() {
    if (randomStartPoint) {
      return Random.Range(0f, clip.length * 0.5f);
    }
    return 0;
  }
}