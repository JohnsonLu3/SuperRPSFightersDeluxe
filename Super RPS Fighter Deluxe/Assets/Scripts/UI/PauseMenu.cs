using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool isPaused = false;
    [SerializeField]
    public GameObject pauseMenuUI;

    bool pauseDown = false;

    void Update() {

        float pause = Input.GetAxisRaw("Pause");

        if (pause == 1 && !pauseDown)
        {
            pauseDown = true;

            if (isPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
        else {
            if (pause == 0 && pauseDown)
                pauseDown = false;
        }
    }


    public void resumeGame() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void pauseGame() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void quit() {
        Application.Quit();
    }

    public void loadMenu() {
        resumeGame();
        SceneManager.LoadScene("Menu");
    }
}
