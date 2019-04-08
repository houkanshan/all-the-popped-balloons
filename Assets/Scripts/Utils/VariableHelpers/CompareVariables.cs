using UnityEngine;

public enum ComparisonPredicate
{
    Equal,
    Unequal,
    Less,
    LessEqual,
    Greater,
    GreaterEqual
}

public class CompareVariables: MonoBehaviour {
  public IntReference a;
  public ComparisonPredicate be;
  public IntReference b;

#if UNITY_EDITOR
  [SHOW_IN_HIER]
  string Label {
    get {
      string l;
      if (be == ComparisonPredicate.Equal) {
        l = "=";
      } else if (be == ComparisonPredicate.Greater) {
        l = ">";
      } else if (be == ComparisonPredicate.GreaterEqual) {
        l = ">=";
      } else if (be == ComparisonPredicate.Less) {
        l = "<";
      } else if (be == ComparisonPredicate.LessEqual) {
        l = "<=";
      } else {
        l = "!=";
      }
      return l + b.Value.ToString();
    }
  }
#endif

  public bool Predicate() {
    if (a > b) {
      return be == ComparisonPredicate.Greater ||
        be == ComparisonPredicate.GreaterEqual ||
        be == ComparisonPredicate.Unequal;
    }
    if (a < b) {
      return be == ComparisonPredicate.Less ||
        be == ComparisonPredicate.LessEqual ||
        be == ComparisonPredicate.Unequal;
    }
    return be == ComparisonPredicate.Equal ||
        be == ComparisonPredicate.LessEqual ||
        be == ComparisonPredicate.GreaterEqual;
  }
}