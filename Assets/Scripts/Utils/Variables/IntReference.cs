// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
//
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

using System;

[Serializable]
public class IntReference
{
  public bool UseConstant = true;
  public int ConstantValue;
  public IntVariable Variable;

  public IntReference()
  { }

  public IntReference(int value)
  {
    UseConstant = true;
    ConstantValue = value;
  }

  public int Value
  {
    get {
      if (UseConstant) {
        return ConstantValue;
      }
#if UNITY_EDITOR
      if (Variable == null) {
        return ConstantValue;
      }
#endif
      return Variable.Value;
    }
  }

  public static implicit operator int(IntReference reference)
  {
    return reference.Value;
  }
}