using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 0.05f;
    public float xSpeed = 1;
    public float zSpeed = 1;

    public Material selectedMat;
    public Material defaultMat;
    private Rigidbody rb;
    
    private bool draggingChar;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            IdentifyFirstClick();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            GetComponent<MeshRenderer>().material = defaultMat;
            draggingChar = false;
            rb.velocity = Vector3.zero;
        }

        if (draggingChar)
        {
            DragCharacter();
        }
    }

    void IdentifyFirstClick()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if(hit.collider.CompareTag("Player"))
            {
                draggingChar = true;
                GetComponent<MeshRenderer>().material = selectedMat;
            }
        }
    }

    void DragCharacter()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.point.x - transform.position.x > 0)
            {
                
                xSpeed = speed;
            }
            else
            {
                xSpeed = -speed;
            }
            
            if (hit.point.z - transform.position.z > 0)
            {
                zSpeed = speed;
            }
            else
            {
                zSpeed = -speed;
            }
            
            rb.velocity = new Vector3(xSpeed, 0, zSpeed);
        }
    }
}
