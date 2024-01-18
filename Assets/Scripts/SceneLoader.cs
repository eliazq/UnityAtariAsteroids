using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadActiveSceneAfterTime(float time)
    {
        StartCoroutine(LoadActiveSceneTime(time));
    }

    IEnumerator LoadActiveSceneTime(float time)
    {
        if (time >= 0)
        {
            yield return new WaitForSeconds(time);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Debug.LogError("Invalid time value"); // time cant be - makes no sense 
        }
    }
}
