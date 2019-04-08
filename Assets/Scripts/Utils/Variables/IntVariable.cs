// ----------------------------------------------------------------------------
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using UnityEngine;

[CreateAssetMenu]
public class IntVariable : ScriptableObject
{
#if UNITY_EDITOR
  [Multiline]
  public string DeveloperDescription = "";
#endif
  public int DefaultValue;

  public int value;
  public int Value {
    get { return value; }
    set {
      this.value = value;
    }
  }

  private void OnEnable() {
    Value = DefaultValue;
  }
  private void OnDisable() {
    Value = DefaultValue;
  }

  public void SetValue(int value)
  {
    Value = value;
  }

  public void SetValue(IntVariable value)
  {
    Value = value.Value;
  }

  public void ApplyChange(int amount)
  {
    Value += amount;
  }

  public void ApplyChange(IntVariable amount)
  {
    Value += amount.Value;
  }
}