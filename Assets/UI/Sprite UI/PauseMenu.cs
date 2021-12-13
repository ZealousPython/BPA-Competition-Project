using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void pause() {
        GameManager.instance.unPause();
    }
    public void quit() {
        GameManager.instance.Quit();
    }
    public void save() {
        GameManager.instance.saveFile();
    }
    public void kill() {
        Destroy(gameObject);
    }
}
