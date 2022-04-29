using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public string saveName;
    public string playerName;

    public TMP_Text playerNameText;
    public TMP_InputField playerNameInput;

    public void Update()
    {
        playerName = PlayerPrefs.GetString("name", "none");
        playerNameText.text = playerName.ToString();
    }

    public void SetName()
    {
        saveName = playerNameInput.text;
        PlayerPrefs.SetString("name", saveName);
    }
}
