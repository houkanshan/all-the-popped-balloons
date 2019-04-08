using UnityEngine;

// NOTE: Not using.
// game config should just be put on an MonoBehaviour, add to prefab.
[CreateAssetMenu(fileName = "GameConfig", menuName = "Grandma/GameConfig", order = 0)]
public class GameConfig : ScriptableObject {
  public bool debug = false;
  public Slide initialSlide;

  static GameConfig _i;
  static public GameConfig i {
    get {
      // For singleton
      // if (!_i) {
      //   _i = FindObjectOfType<GameConfig>();
      // }
      // if (_i) {
      //   _i = ScriptableObject.CreateInstance<GameConfig>();
      // }
#if (!UNITY_EDITOR)
      _i.debug = false;
#endif
      return _i;
    }
  }
}