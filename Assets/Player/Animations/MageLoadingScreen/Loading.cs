using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//script used for the loading scene
public class Loading : MonoBehaviour
{
    void Start() {
        //makes sure timescale is correct and starts loading the next level
        Time.timeScale = 1;
        StartCoroutine(LoadSceneAsync());
    }
    IEnumerator LoadSceneAsync() {
        //starts an asyncronous operation to load the next scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(GameManager.instance.NextScene);
        yield return null;
    }
}
