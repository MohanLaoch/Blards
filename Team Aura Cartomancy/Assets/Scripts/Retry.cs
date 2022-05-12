using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour
{
    public void TryAgain()
    {
        
        SceneManager.LoadScene(2);
    }

    public void Bye()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        FindObjectOfType<AudioManager>().Stop("GameMusic");
        FindObjectOfType<AudioManager>().Play("ThemeMusic");
        SceneManager.LoadScene(0);
    }
}
