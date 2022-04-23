using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    private Vector3 dragOffset;
    private Camera cam;
    private BoxCollider2D col;
    [SerializeField] private GameObject RotationUI;
    [SerializeField] float speed = 10f;
    [SerializeField] float LockOnDistance = 1f;
    [HideInInspector] public GameObject[] PlayablePositions;

    public Vector3 HandPosition;

    private void Awake()
    {
        cam = Camera.main;
        PlayablePositions = GameObject.FindGameObjectsWithTag("PlayablePosition");
        col = gameObject.GetComponent<BoxCollider2D>();
    }

    private void OnMouseDown()
    {
        dragOffset = transform.position - GetMousePos();
    }

    private void OnMouseDrag()
    {
        transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + dragOffset, speed * Time.deltaTime);

        for (int i = 0; i < PlayablePositions.Length; i++)
        {
            SpriteRenderer SpriteRenderer = PlayablePositions[i].gameObject.GetComponent<SpriteRenderer>();
            SpriteRenderer.enabled = Vector3.Distance(PlayablePositions[i].transform.position, transform.position) <= LockOnDistance;
        }
    }

    private void OnMouseUp()
    {
        for(int i = 0; i < PlayablePositions.Length; i++)
        {
            if(Vector3.Distance(PlayablePositions[i].transform.position, transform.position) <= LockOnDistance)
            {
                transform.position = PlayablePositions[i].transform.position;
                col.enabled = false;
                RotationUI.SetActive(true);
                return;
            }
        }
        transform.position = HandPosition;
    }

    Vector3 GetMousePos()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }


}
