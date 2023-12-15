using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int[] positionOnGrid = new int[2];
    [SerializeField] private Side_ side;

    private void Awake()
    {
        side = GetComponentInParent<Side_>();
    }

    /*void Update()
    {
        if (side.active && GameManager.instance.playerPositionOnGrid[0] == positionOnGrid[0] && GameManager.instance.playerPositionOnGrid[1] == positionOnGrid[1])
        {
            GameManager.instance.GatherCollectible();
            Destroy(gameObject);
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.GatherCollectible();
        Destroy(gameObject);
    }
}
