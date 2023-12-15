using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] private int[] positionOnGrid = new int[2];
    [SerializeField] private Side_ side;
    [SerializeField] private string nextScene;

    private void Awake()
    {
        side = GetComponentInParent<Side_>();
    }

    void Update()
    {
        if (side.active && GameManager.instance.playerPositionOnGrid[0] == positionOnGrid[0] && GameManager.instance.playerPositionOnGrid[1] == positionOnGrid[1])
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
