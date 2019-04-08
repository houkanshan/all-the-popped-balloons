using UnityEngine;
using System.Collections;
using System.Collections.Generic;

static public class MouseHelper {

  static public Collider2D[] FindCollider2DUnderMouse(Camera cam) {
    Vector3 mousePosition = Input.mousePosition;
    mousePosition.z = - cam.transform.position.z;

    Vector2 v = cam.ScreenToWorldPoint(mousePosition);

    Collider2D[] cols = Physics2D.OverlapPointAll(v);
    return cols;
  }

  static public T FindUnderMouse<T>(Camera cam) {
    Collider2D[] cols = FindCollider2DUnderMouse(cam);
    if (cols.Length > 0) {
      foreach (Collider2D c in cols) {
        T target = c.gameObject.GetComponent<T>();
        if (target != null) {
          return target;
        }
      }
    }
    return default(T);
  }
  static public List<T> FindAllUnderMouse<T>(Camera cam) {
    Collider2D[] cols = FindCollider2DUnderMouse(cam);
    List<T> all = new List<T>();
    if (cols.Length > 0) {
      foreach (Collider2D c in cols) {
        T target = c.gameObject.GetComponent<T>();
        if (target != null) {
          all.Add(target);
        }
      }
    }
    return all.Count == 0 ? null : all;
  }
}
