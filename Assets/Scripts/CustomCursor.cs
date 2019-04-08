using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
  public static CustomCursor i;
  public Sprite defaultCursor;
  public Vector2 offset;

  Sprite cursorImage;
  // int cursorWidth = 6;
  // int cursorHeight = 8;
  SpriteRenderer spriteRenderer;
  Camera mainCam;

  void Awake()
  {
    i = this;
    UnityEngine.Cursor.visible = false;
    cursorImage = defaultCursor;
    spriteRenderer = GetComponent<SpriteRenderer>();
    mainCam = Camera.main;
  }

  private void Update() {
    RenderCursor();
  }

  private void LateUpdate() {
    spriteRenderer.sprite = cursorImage;
    Vector3 mousePosition = Input.mousePosition;
    mousePosition.z = - mainCam.transform.position.z;
    Vector3 pos = mainCam.ScreenToWorldPoint(mousePosition);
    transform.position = pos;
  }

  public void RenderCursor()
  {
    Clickable clickable = MouseHelper.FindUnderMouse<Clickable>(mainCam);
    if (clickable) {
      cursorImage = clickable.cursorSprite;
    } else {
      cursorImage = defaultCursor;
    }
  }

  //   void OnGUI()
  //   {
  //     GUI.DrawTexture(
  //       new Rect(
  //         Input.mousePosition.x + offset.x,
  //         Screen.height - Input.mousePosition.y + offset.y,
  //         cursorWidth,
  //         cursorHeight
  //       ),
  //        cursorImage
  //     );
  //   }
}
