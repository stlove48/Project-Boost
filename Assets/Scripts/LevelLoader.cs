using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    [SerializeField] float sceneDelay = 4f;

    private void OnEnable()
    {
        EventManager.LevelCompleted += LoadNextLevel;
        EventManager.LevelFailed += ReloadCurrentLevel;
    }

    private void OnDisable()
    {
        EventManager.LevelCompleted -= LoadNextLevel;
        EventManager.LevelFailed -= ReloadCurrentLevel;
    }

    public void ReloadCurrentLevel(int sceneIndex)
    {
        StartCoroutine(DelaySceneLoad(sceneIndex));
    }

    public void LoadNextLevel(int sceneIndex)
    {
        int nextSceneIndex = sceneIndex + 1;

        // If the next scene index is the number of total scenes, restart from first level
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 1;
        }
        StartCoroutine(DelaySceneLoad(nextSceneIndex));
    }

    IEnumerator DelaySceneLoad(int sceneIndex)
    {
        yield return new WaitForSeconds(sceneDelay);
        SceneManager.LoadScene(sceneIndex);
        EventManager.OnLevelLoaded(true);
    }
}
