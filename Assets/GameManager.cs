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
}
