using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DethManager : MonoBehaviour
{
    [SerializeField] bool gameComplete = true;
    [SerializeField] bool gameOver = false;

    private GameObject player;
    private GameObject uiManager;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        uiManager = GameObject.FindGameObjectWithTag("UIManager");
        DontDestroyOnLoad(uiManager.gameObject);
    }

    void Update()
    {
        uiManager.SetActive(true);

        if (player == null && !gameOver && SceneManager.GetActiveScene().buildIndex != 3 
            && SceneManager.GetActiveScene().buildIndex != 0)
        {
            gameComplete = false;
            GameOver();
        }

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            uiManager.SetActive(false);

            if (gameComplete)
            {
                player.SetActive(false);

                for (int a = 0; a < transform.childCount; a++)
                {
                    transform.GetChild(a).gameObject.SetActive(true);
                }
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            player.SetActive(false);
            uiManager.SetActive(false);

            for (int a = 0; a < transform.childCount; a++)
            {
                transform.GetChild(a).gameObject.SetActive(false);
            }
        }
    }

    public void GameOver()
    {
        gameOver = true;

        if (PlayerPrefs.GetInt("Score")>PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));
        }

        StartCoroutine(LoadGameOver());
    }

    IEnumerator LoadGameOver()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(3);
    }
}
