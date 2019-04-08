using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickRestart : Clickable {
    private void OnMouseUp() {
        SceneManager.LoadScene(0);
    }
}
