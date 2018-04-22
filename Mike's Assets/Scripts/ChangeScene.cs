using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
    private const string SceneName = "minigame";

    private void Start()
    {
        SceneManager.UnloadSceneAsync(SceneName);
    }
}
