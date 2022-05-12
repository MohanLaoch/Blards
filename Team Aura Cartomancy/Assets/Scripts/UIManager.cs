using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public string saveName;
    public string playerName;

    public TMP_Text playerNameText;
    public TMP_InputField playerNameInput;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("ThemeMusic");
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        playerNameText.text = playerName.ToString();
    }

    public void SetName()
    {
        saveName = playerNameInput.text;
        SceneManager.LoadScene(1);
        
    }
}
