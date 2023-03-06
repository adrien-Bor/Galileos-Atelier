using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;
using FMOD;

/// <summary>
/// This class contains all the methods to move though levels.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    [SerializeField] private EventReference UI_start;
    [SerializeField] private EventReference music_lvl1;
    public EventInstance musicEventInstance;

    // Cached reference
    GameSession gameStatus;

    public void LoadNextScene()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        print("current scene index load next scene " +currentSceneIndex);
        if (currentSceneIndex == 0 || currentSceneIndex == 1)
            SceneManager.LoadScene(currentSceneIndex + 1);

        else if (currentSceneIndex == 2)
            LoadStartScene();
        //musicEventInstance.setParameterByNameWithLabel("Menu_parameter", "OpentMenu");


        RuntimeManager.PlayOneShot(UI_start);
        RuntimeManager.PlayOneShot(music_lvl1);

        if(currentSceneIndex == 0)
        {
            //musicEventInstance.setParameterByNameWithLabel("Menu_parameter", "ClosetMenu");
            musicEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    public void LoadStartScene()
    {

        SceneManager.LoadScene(0);

        // Reset game and score
        gameStatus = FindObjectOfType<GameSession>();
        gameStatus.ResetGame();
    }

/*    public int GetIndexLevel()
    {
        return SceneManager.GetActiveScene().buildIndex;

    }*/

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Start()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        print("current scene index start" + currentSceneIndex);

        if (currentSceneIndex == 0)
        {
            musicEventInstance = RuntimeManager.CreateInstance(FMODEvents.instance_bizarre.music_menu);
            musicEventInstance.start();
        }

    }

}
