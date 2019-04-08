using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Audio", menuName = "Inventory/AudioGroup", order = 1)]
public class AudioGroup: AudioObject {
  public AudioObject[] audioObjects;

  AudioObject lastAudioObject = null;
  public override AudioObject GetAudio() {
    AudioObject ao = RandomHelper.RandomChoose(audioObjects, new AudioObject[] { lastAudioObject });
    lastAudioObject = ao;
    return ao;
  }
}