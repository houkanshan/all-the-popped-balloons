using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[ExecuteInEditMode]
public class GameManager : MonoBehaviour {
  public static GameManager i;

  [BoxGroup("Debug")]
  public bool debug = true;
  [BoxGroup("Debug")]
  public bool showSpot = true;
  [BoxGroup("Debug")]
  public bool setInitialSlide = true;
  [BoxGroup("Debug")]
  public Color normalLink = Color.green;
  [BoxGroup("Debug")]
  public Color fadeLink = Color.red;

  public Slide initialSlide;
  public IntReference loopedTimes;
  public IntReference helloTimes;

  public AudioSource audioSourceEnvHuman;
  public AudioSource audioSourceEnvNature;
  public AudioSource audioSourceInteraction;

  void Awake() {
    i = this;
#if (!UNITY_EDITOR)
    debug = false;
#endif
    if (!debug) {
      showSpot = false;
      setInitialSlide = false;
    }
  }
  void Update() {
#if (UNITY_EDITOR)
    if (i == null) {
      i = this;
    }
#endif
  }
  private void Start() {
    loopedTimes.Variable.Value = 0;
  }

  public void SetAudioEnvHuman(AudioObject audio, float volume) {
    if (audio == null) {
      if (audioSourceEnvHuman.isPlaying) {
        audioSourceEnvHuman.Stop();
      }
      audioSourceEnvHuman.clip = null;
    } else {
      audioSourceEnvHuman.volume = volume >= 0 ? volume : audio.volume;
      if (audio.clip != audioSourceEnvHuman.clip) {
        if (audio.onlyPlayOnce && GameManager.i.loopedTimes.Value > 0) {
          return;
        }
        audioSourceEnvHuman.clip = audio.clip;
        audioSourceEnvHuman.time = audio.getStartPoint();
        audioSourceEnvHuman.loop = audio.loop;
        audioSourceEnvHuman.Play();
      }
    }
  }

  public void SetAudioEnvNature(AudioObject audio, float volume) {
    if (audio == null) {
      if (audioSourceEnvNature.isPlaying) {
        audioSourceEnvNature.Stop();
      }
      audioSourceEnvNature.clip = null;
    } else {
      audioSourceEnvNature.volume = volume >= 0 ? volume : audio.volume;
      if (audio.clip != audioSourceEnvNature.clip) {
        if (audio.onlyPlayOnce && GameManager.i.loopedTimes.Value > 0) {
          return;
        }
        audioSourceEnvNature.clip = audio.clip;
        audioSourceEnvNature.time = audio.getStartPoint();
        audioSourceEnvNature.loop = audio.loop;
        audioSourceEnvNature.Play();
      }
    }
  }

  public void PlayInteraction(AudioObject audio) {
    if (audio) {
      AudioObject obj = audio.GetAudio();
      if (obj && obj.clip) {
        audioSourceInteraction.clip = obj.clip;
        audioSourceInteraction.time = obj.getStartPoint();
        audioSourceInteraction.PlayOneShot(obj.clip, obj.volume);
      }
    }
  }
#if UNITY_EDITOR
  private void OnDrawGizmos() {
    if (debug && setInitialSlide) {
      var size = Vector3.one * 1.4f;
      Gizmos.color = Color.red;
      Gizmos.DrawWireCube(
        initialSlide.transform.position,
        size
      );
    }
  }
#endif
}
