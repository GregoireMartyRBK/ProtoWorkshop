using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side_ : MonoBehaviour
{
    public bool active;
    
    [SerializeField] private SO_Grid initialGrid;

    public bool[][] verticalGrid;
    public bool[][] horizontalGrid;

    public bool[][] vertiTransiGrid;
    public bool[][] horiTransiGrid;

    private void Start()
    {
        verticalGrid = new bool[5][];
        verticalGrid[0] = initialGrid.verticalBlock1;
        verticalGrid[1] = initialGrid.verticalBlock2;
        verticalGrid[2] = initialGrid.verticalBlock3;
        verticalGrid[3] = initialGrid.verticalBlock4;
        verticalGrid[4] = initialGrid.verticalBlock5;
        
        horizontalGrid = new bool[4][];
        horizontalGrid[0] = initialGrid.horizontalBlock1;
        horizontalGrid[1] = initialGrid.horizontalBlock2;
        horizontalGrid[2] = initialGrid.horizontalBlock3;
        horizontalGrid[3] = initialGrid.horizontalBlock4;
    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Rotating());
            Rotate(true);
        }
    }

    public void Rotate(bool clockwise)
    {
        if (clockwise)
        {
            horiTransiGrid = new bool[4][];
            for (int i = 0; i < 4; i++)
            {
                horiTransiGrid[i] = new bool[5];
                for (int j = 0; j < 5; j++)
                {
                    horiTransiGrid[i][j] = verticalGrid[j][3-i];
                }
            }
            vertiTransiGrid = new bool[5][];
            for (int i = 0; i < 5; i++)
            {
                vertiTransiGrid[i] = new bool[4];
                for (int j = 0; j < 4; j++)
                {
                    vertiTransiGrid[i][j] = horizontalGrid[j][4-i];
                }
            }
        }
        else
        {
            horiTransiGrid = new bool[4][];
            for (int i = 0; i < 4; i++)
            {
                horiTransiGrid[i] = new bool[5];
                for (int j = 0; j < 5; j++)
                {
                    horiTransiGrid[i][j] = verticalGrid[4-j][i];
                }
            }

            vertiTransiGrid = new bool[5][];
            for (int i = 0; i < 5; i++)
            {
                vertiTransiGrid[i] = new bool[4];
                for (int j = 0; j < 4; j++)
                {
                    vertiTransiGrid[i][j] = horizontalGrid[3-j][i];
                }
            }
        }
        verticalGrid = vertiTransiGrid;
        horizontalGrid = horiTransiGrid;
    }

    public IEnumerator Rotating()
    {
        for (int i = 0; i < 90; i++)
        {
            gameObject.transform.Rotate(Vector3.up,1);
            yield return new WaitForSeconds(0.005f);
        }
    }
}
