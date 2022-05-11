using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPlayerName : MonoBehaviour
{
    private UIManager UIMan;
    public TMP_Text display;

    private void Start()
    {
        UIMan = FindObjectOfType<UIManager>();
    }

    public void Update()
    {
        if(UIMan != null)
        {
            display.text = UIMan.saveName;
        }
    }
}
