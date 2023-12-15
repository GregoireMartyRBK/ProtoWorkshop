using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public Side_ activeSide;
    public Side_ leftSide;
    public Side_ rightSide;

    public Image[] stars;

    [Header("Player Info")]
    public int[] playerPositionOnGrid = new int[2];
    public int gatheredCollectibles;

     public bool changeMode;
    [SerializeField] private Vector3 rightTargetPos;
    [SerializeField] private Vector3 leftTargetPos;
    
    [SerializeField] private Vector3 rightInitPos;
    [SerializeField] private Vector3 leftInitPos;
    

    private void Awake()
    {
        instance = this;
        ChangeActive();
    }

    public void GatherCollectible()
    {
        Debug.Log("gathered");
        stars[gatheredCollectibles].enabled = true;
        gatheredCollectibles++;
    }

    public void ChangeActive()
    {
        activeSide.active = true;
        leftSide.active = false;
        rightSide.active = false;
    }

    public void ChangeModeStart()
    {
        changeMode = !changeMode;
        if (changeMode)
        {
            StopCoroutine(ReturnModePosition());
            StartCoroutine(ChangeModePosition());
        }
        else
        {
            StopCoroutine(ChangeModePosition());
            StartCoroutine(ReturnModePosition());
        }
        
    }

    IEnumerator ChangeModePosition()
    {
        for (int i = 0; i < 20; i++)
        {
            rightSide.transform.position = Vector3.Lerp(rightSide.transform.position, rightTargetPos,0.2f);
            leftSide.transform.position = Vector3.Lerp(leftSide.transform.position, leftTargetPos,0.2f);
            yield return new WaitForFixedUpdate();
        }
    }
    
    IEnumerator ReturnModePosition()
    {
        for (int i = 0; i < 20; i++)
        {
            rightSide.transform.position = Vector3.Lerp(rightSide.transform.position, rightInitPos,0.2f);
            leftSide.transform.position = Vector3.Lerp(leftSide.transform.position, leftInitPos,0.2f);
            yield return new WaitForFixedUpdate();
        }
    }

    private void Update()
    {
        if (changeMode)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (touch.position.x > Screen.width/2 && touch.position.y > 950)
                    {
                        rightSide.Rotate(true);
                        rightSide.StartCoroutine("Rotating");
                    }
                    else if (touch.position.x < Screen.width/2 && touch.position.y > 950)
                    {
                        leftSide.Rotate(true);
                        leftSide.StartCoroutine("Rotating");
                    }
                }
            }
        }
    }
}
