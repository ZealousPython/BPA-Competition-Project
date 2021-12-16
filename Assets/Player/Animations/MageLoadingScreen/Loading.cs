using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{
    void Start() {
        StartCoroutine(LoadSceneAsync());
    }
    IEnumerator LoadSceneAsync() {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(GameManager.instance.NextScene);
        yield return null;
    }
}
