using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//functions used for the the main menu buttons
public class MainMenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }
    public void exit() {
        GameManager.instance.Quit();
    }
    public void load() {
        GameManager.instance.loadFile();
    }
    public void start() {
        GameManager.instance.ChangeScene("Assets/UI/PlayerSelect/CharacterSelect.unity");
    }
    public void LoadTutorial() {
       
        GameManager.instance.ChangeScene("Assets/UI/Tutorial.unity");
    }
}
