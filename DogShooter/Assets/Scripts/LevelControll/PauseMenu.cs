using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] static bool GameIsPaused = false;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] AudioMixerGroup Mixer;

    public void ToggleMusic(bool enabled)
    {
        if (enabled)
        {
            Mixer.audioMixer.SetFloat("MusicVolume", 0);

        }
        else
        {
            Mixer.audioMixer.SetFloat("MusicVolume", -80);
        }
    }

    public void ToggleMaster(bool enabled)
    {
        if (enabled)
        {
            Mixer.audioMixer.SetFloat("MasterVolume", 0);

        }
        else
        {
            Mixer.audioMixer.SetFloat("MasterVolume", -80);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Resume();
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
