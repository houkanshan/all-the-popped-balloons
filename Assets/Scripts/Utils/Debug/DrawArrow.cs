#if UNITY_EDITOR
using System.Collections;
using UnityEngine;
using UnityEditor;

public enum LineStyle {
  Solid,
  Dotted,
}

// From http://wiki.unity3d.com/index.php/DrawArrow

public static class DrawArrow
{
    // Runs inside OnGizmo
    public static void ForGizmo(
        Vector3 from, Vector3 direction,
        LineStyle lineStyle = LineStyle.Solid,
        float arrowHeadLength = 0.1f, float arrowHeadAngle = 20.0f
        )
    {
        if (lineStyle == LineStyle.Solid) {
            Gizmos.DrawRay(from, direction);
        } else {
            Handles.DrawDottedLine(from, from + direction, 2f);
        }

        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180+arrowHeadAngle,0) * new Vector3(0,0,1);
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180-arrowHeadAngle,0) * new Vector3(0,0,1);
        Vector3 top = Quaternion.LookRotation(direction) * Quaternion.Euler(180+arrowHeadAngle,0,0) * new Vector3(0,0,1);
        Vector3 bottom = Quaternion.LookRotation(direction) * Quaternion.Euler(180-arrowHeadAngle,0,0) * new Vector3(0,0,1);
        Gizmos.DrawRay(from + direction, right * arrowHeadLength);
        Gizmos.DrawRay(from + direction, left * arrowHeadLength);
        Gizmos.DrawRay(from + direction, top * arrowHeadLength);
        Gizmos.DrawRay(from + direction, bottom * arrowHeadLength);
    }

    // Runs inside OnGizmo
    public static void ForGizmo(
        Vector3 from, Vector3 direction,
        Color color,
        LineStyle lineStyle = LineStyle.Solid,
        float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f
        )
    {
        Gizmos.color = color;
        Handles.color = color;
        ForGizmo(from, direction, lineStyle, arrowHeadAngle, arrowHeadAngle);
    }

    /**
     * Run in update
     */
    public static void ForDebug(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
    {
        Debug.DrawRay(pos, direction);

        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180+arrowHeadAngle,0) * new Vector3(0,0,1);
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180-arrowHeadAngle,0) * new Vector3(0,0,1);
        Debug.DrawRay(pos + direction, right * arrowHeadLength);
        Debug.DrawRay(pos + direction, left * arrowHeadLength);
    }
    /**
     * Run in update
     */
    public static void ForDebug(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
    {
        Debug.DrawRay(pos, direction, color);

        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180+arrowHeadAngle,0) * new Vector3(0,0,1);
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180-arrowHeadAngle,0) * new Vector3(0,0,1);
        Debug.DrawRay(pos + direction, right * arrowHeadLength, color);
        Debug.DrawRay(pos + direction, left * arrowHeadLength, color);
    }
}
#endif