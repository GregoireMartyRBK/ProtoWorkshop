using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SO_Grid", order = 1)]
public class SO_Grid : ScriptableObject
{
    public bool[] verticalBlock1 = new bool[4];
    public bool[] verticalBlock2 = new bool[4]; 
    public bool[] verticalBlock3 = new bool[4]; 
    public bool[] verticalBlock4 = new bool[4]; 
    public bool[] verticalBlock5 = new bool[4];

    public bool[] horizontalBlock1 = new bool[5];
    public bool[] horizontalBlock2 = new bool[5];
    public bool[] horizontalBlock3 = new bool[5];
    public bool[] horizontalBlock4 = new bool[5];
    
    
    
}
