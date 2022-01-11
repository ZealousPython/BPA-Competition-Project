using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//manage the game over screen buttons
public class GameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public void Load()
    {
        GameManager.instance.loadFile();
    }
    public void MainMenu() {
        GameManager.instance.ChangeScene("Assets/UI/Main Menu.unity");
    }
    public void Quit() {
        GameManager.instance.Quit();
    }
}
