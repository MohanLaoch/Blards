using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRotation : MonoBehaviour
{
    [SerializeField] private GameObject RotationUI;

    public void BlueSide()
    {
        Vector3 scale = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector3(scale.x * 1, scale.y * 1, scale.z);
        RotationUI.SetActive(false);
    }

    public void RedSide()
    {
        Vector3 scale = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector3(scale.x * -1, scale.y * -1, scale.z);
        RotationUI.SetActive(false);
    }
}
