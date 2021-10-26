using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpController : MonoBehaviour
{

    public string sceneName;
    public AudioSource warpSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Invoke("LoadNextScene", 1f);
            PlayerPrefs.SetString("PrevScene", sceneName);

            var player = other.gameObject.GetComponent<PlayerControllerRogidBody>();

            PlayerPrefs.SetInt("CoinCount", player.coinCount);
            warpSound?.Play();
        }
        
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(sceneName);
    }


}
