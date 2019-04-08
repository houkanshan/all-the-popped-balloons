#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

public static class GizmoHelper {
  public static void DrawArrowedLine(
    Vector3 from, Vector3 to,
    float dotRadius = 0.01f, LineStyle lineStyle = LineStyle.Solid
  ) {
    DrawArrow.ForGizmo(from, to - from, lineStyle);
    Gizmos.DrawWireSphere(from, dotRadius);
  }
}
#endif