using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayGroundSceneManager : MonoBehaviour
{
    public int coinCount = 0;
    public Text uiTextCoin;
    public Text uiTextBullet;

    public void GoToHome()
    {
        SceneManager.LoadScene("Home");
    }

    public void SetTextCoin(int amont)
    {
        uiTextCoin.text = amont.ToString();
    }

    public void SetTextBullet(int amont)
    {
        uiTextBullet.text = amont.ToString();
    }

}
