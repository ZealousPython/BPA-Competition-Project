using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//manages pause menu buttons
public class PauseMenu : MonoBehaviour
{
    public void pause() {
        GameManager.instance.unPause();
    }
    public void quit() {
        GameManager.instance.Quit();
    }
    public void MainMenu() {
        GameManager.instance.paused = false;
        GameManager.instance.ChangeScene("Assets/UI/Main Menu.unity");
    }
    public void kill() {
        Destroy(gameObject);
    }
}
