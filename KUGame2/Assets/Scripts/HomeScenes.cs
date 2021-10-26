using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeScenes : MonoBehaviour
{
    public void GoToPlaygroud()
    {
        //PlayerPrefs.SetInt("CoinCount", 0);
        PlayerPrefs.DeleteKey("CoinCount");
        SceneManager.LoadScene("Playground");
    }

    public void ContinuePlay()
    {
        if (PlayerPrefs.HasKey("PrevScene"))
        {
            string prevSceneName = PlayerPrefs.GetString("PrevScene");
            SceneManager.LoadScene(prevSceneName);
        }
        
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
