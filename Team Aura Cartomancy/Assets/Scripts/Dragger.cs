using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    private TurnManager TurnMan;

    private Vector3 dragOffset;
    private Camera cam;
    private BoxCollider2D col;
    [SerializeField] private GameObject RotationUI;
    [SerializeField] float speed = 10f;
    [SerializeField] float LockOnDistance = 1f;
    [SerializeField] float HPLockOnDistance = 1.5f;
    [HideInInspector] public GameObject[] PlayablePositions;
    [HideInInspector] public GameObject[] EnemyPositions;

    [HideInInspector] public Player_Hand_Script PHS;

    public Vector3 HandPosition;

    private CardValues CV;
    public bool EnemyCardIsInPlay;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        TurnMan = FindObjectOfType<TurnManager>();
        CV = gameObject.GetComponent<CardValues>();
        if(CV.IsEnemyCard)
        {
            PlayablePositions = GameObject.FindGameObjectsWithTag("EnemyPlayablePosition");
            EnemyPositions = GameObject.FindGameObjectsWithTag("PlayablePosition");
        }
        else
        {
            PlayablePositions = GameObject.FindGameObjectsWithTag("PlayablePosition");
            EnemyPositions = GameObject.FindGameObjectsWithTag("EnemyPlayablePosition");
        }
    }
    private void Update()
    {
        EnemyCardInPlay();
    }

    private void OnMouseDown()
    {
        if (CV.CardIsActive == true && TurnMan.IsPlayerTurn == true && CV.CardAttackedForTurn != true && CV.IsEnemyCard != true && TurnMan.PlayerDrawnForTurn == true)
        {
            CV.selected = true; 
            for (int i = 0; i < PlayablePositions.Length; i++)
            {
                SpriteRenderer SpriteRenderer = PlayablePositions[i].gameObject.GetComponent<SpriteRenderer>();
                SpriteRenderer.enabled = Vector3.Distance(PlayablePositions[i].transform.position, transform.position) <= LockOnDistance;
            }
        }
        else
        {
            dragOffset = transform.position - GetMousePos();
        }
    }

    private void OnMouseDrag()
    {
        //Play the Card Drag
        if (CV.CardIsActive != true && TurnMan.IsPlayerTurn == true && TurnMan.PlayerCardPlayedForTurn == false && CV.IsEnemyCard != true && TurnMan.PlayerDrawnForTurn == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, GetMousePos() + dragOffset, speed * Time.deltaTime);

            for (int i = 0; i < PlayablePositions.Length; i++)
            {
                if (PlayablePositions[i].GetComponent<PlayablePosition>().CardInPlay == false)
                {
                    SpriteRenderer SpriteRenderer = PlayablePositions[i].gameObject.GetComponent<SpriteRenderer>();
                    SpriteRenderer.enabled = Vector3.Distance(PlayablePositions[i].transform.position, transform.position) <= LockOnDistance;
                }
            }
        }
        //Attack With Card Drag
        else if(CV.CardIsActive == true && CV.CardAttackedForTurn != true && TurnMan.IsFirstTurn == false)
        {
            if (EnemyCardIsInPlay != true)
            {
                Player_HP EnemyHP = GameObject.Find("Enemy HP").GetComponent<Player_HP>();
                EnemyHP.OrbHighlightedBorder.enabled = Vector3.Distance(EnemyHP.gameObject.transform.position, GetMousePos()) <= HPLockOnDistance;
            }
            else
            {
                for (int i = 0; i < EnemyPositions.Length; i++)
                {
                    if (EnemyPositions[i].GetComponent<PlayablePosition>().CardInPlay == true)
                    {
                        SpriteRenderer SpriteRenderer = EnemyPositions[i].gameObject.GetComponent<SpriteRenderer>();
                        SpriteRenderer.enabled = Vector3.Distance(EnemyPositions[i].transform.position, GetMousePos()) <= LockOnDistance;
                    }
                }
            }
        }
    }

    private void OnMouseUp()
    {
        if (CV.CardIsActive == true && CV.selected == true && CV.CardAttackedForTurn != true && TurnMan.IsFirstTurn == false)
        {
            if (EnemyCardIsInPlay == true)
            {
                for (int i = 0; i < EnemyPositions.Length; i++)
                {
                    PlayablePosition Position = EnemyPositions[i].GetComponent<PlayablePosition>();
                    if (Vector3.Distance(Position.gameObject.transform.position, GetMousePos()) <= LockOnDistance)
                    {
                        if (Position.CardInPlay == true)
                        {
                            //Start an Attack on the enemy Card
                            CV.CardAttackedForTurn = true;
                            CardValues EnemyCard = Position.Card;

                            int currentAttack;

                            if (CV.OnBlueSide == true)
                            {
                                currentAttack = CV.BlueAttack;
                            }
                            else
                            {
                                currentAttack = CV.RedAttack;
                            }
                            Position.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                            EnemyCard.TakeDamage(currentAttack, CV);
                        }
                    }
                }
            }
            else
            {
                Player_HP EnemyHP = GameObject.Find("Enemy HP").GetComponent<Player_HP>();
                if (Vector3.Distance(EnemyHP.gameObject.transform.position, GetMousePos()) <= HPLockOnDistance)
                {
                    CV.CardAttackedForTurn = true;
                    int currentAttack;

                    if (CV.OnBlueSide == true)
                    {
                        currentAttack = CV.BlueAttack;
                    }
                    else
                    {
                        currentAttack = CV.RedAttack;
                    }
                    EnemyHP.TakeDamage(currentAttack, CV);
                }
            }
        }
        else if(CV.CardIsActive != true)
        {
            for (int i = 0; i < PlayablePositions.Length; i++)
            {
                PlayablePosition Position = PlayablePositions[i].GetComponent<PlayablePosition>();
                if (Vector3.Distance(Position.gameObject.transform.position, transform.position) <= LockOnDistance)
                {
                    if (Position.CardInPlay == false)
                    {
                        Position.CardInPlay = true;
                        Position.Card = gameObject.GetComponent<CardValues>();
                        CV.position = Position;
                        transform.position = Position.gameObject.transform.position;
                        RotationUI.SetActive(true);

                        for (int j = 0; j < PHS.CardsInHand.Count; j++)
                        {
                            if (PHS.CardsInHand[j] == this.gameObject)
                            {
                                PHS.CardsInHand.RemoveAt(j);
                                PHS.ResetHand();
                                break;
                            }
                        }

                        CV.CardIsActive = true;
                        TurnMan.PlayerCardPlayedForTurn = true;
                        return;
                    }
                }
            }
            transform.position = HandPosition;
        }
    }

    Vector3 GetMousePos()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    public void EnemyCardInPlay()
    {
        bool IsInPlay = false;
        for(int i = 0; i < EnemyPositions.Length; i++)
        {
            if (EnemyPositions[i].GetComponent<PlayablePosition>().CardInPlay == true)
            {
                IsInPlay = true;
            }
        }

        if (IsInPlay != true)
        {
            EnemyCardIsInPlay = false;
        }
        else
        {
            EnemyCardIsInPlay = true;
        }
    }


}
