using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangePlate: MonoBehaviour
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
            if (!GameManager.instance.changeMode)
            {
                GameManager.instance.ChangeModeStart();
            }
        }
        else
        {
            if (GameManager.instance.changeMode)
            {
                GameManager.instance.ChangeModeStart();
            }
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.ChangeModeStart();
    }

    private void OnTriggerExit(Collider other)
    {
        GameManager.instance.ChangeModeStart();
    }
}
