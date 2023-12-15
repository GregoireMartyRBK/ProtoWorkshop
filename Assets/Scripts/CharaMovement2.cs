using System;
using System.Collections;
using UnityEngine;
 
public class CharaMovement2 : MonoBehaviour {
    private bool _isMoving;

    public int[] positionOnGrid = new int[2];

    [SerializeField] private GameManager _gameManager;

    private void Update() {
        if (_isMoving) return;

        if ((Input.GetKey(KeyCode.LeftArrow) || CheckTouch("left")) && checkMovementHorizontal(false))
        {
            Assemble(Vector3.left);
            positionOnGrid[0] -= 1; 
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || CheckTouch("right")) && checkMovementHorizontal(true))
        {
            Assemble(Vector3.right);
            positionOnGrid[0] += 1; 
        }
        else if ((Input.GetKey(KeyCode.UpArrow) || CheckTouch("up")) && checkMovementVertical(false))
        {
            Assemble(Vector3.forward);
            positionOnGrid[1] -= 1; 
        }
        else if ((Input.GetKey(KeyCode.DownArrow)|| CheckTouch("down")) && checkMovementVertical(true))
        {
            Assemble(Vector3.back);
            positionOnGrid[1] += 1; 
        }

        GameManager.instance.playerPositionOnGrid = positionOnGrid;
 
        void Assemble(Vector3 dir) {
            var anchor = transform.position + (Vector3.down + dir) * 1.5f;
            var axis = Vector3.Cross(Vector3.up, dir);
            StartCoroutine(Roll(anchor, axis));
        }
    }

    bool CheckTouch(string side)
    {
        if (Input.touches.Length == 0)
        {
            return false;
        }
        switch (side)
        {
            case "left":
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        if (touch.position.x < 986 && touch.position.y < 445)
                        {
                            return true;
                        }
                    }
                }
                break;
            case "right":
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        if (touch.position.x > Screen.width/6 && touch.position.y > 445 && touch.position.y < 900)
                        {
                            return true;
                        }
                    }
                }
                break;
            case "up":
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        if (touch.position.x < 986 && touch.position.y > 445 && touch.position.y < 900)
                        {
                            return true;
                        }
                    }
                }
                break;
            case "down":
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        if (touch.position.x > 986 && touch.position.y < 445)
                        {
                            return true;
                        }
                    }
                }
                break;
        }
        return false;
    }
    
    private IEnumerator Roll(Vector3 anchor, Vector3 axis) {
        _isMoving = true;
        for (var i = 0; i < 15 ; i++) {
            transform.RotateAround(anchor, axis, 6);
            yield return new WaitForFixedUpdate();
        }
        _isMoving = false;
    }

    private IEnumerator ChangeSideLeft()
    {
        var anchor = transform.position + (Vector3.down + Vector3.left) * 1.5f;
        var axis = Vector3.Cross(Vector3.up, Vector3.left);
        _isMoving = true;
        for (var i = 0; i < 30; i++)
        {
            transform.RotateAround(anchor, axis, 6);
            yield return new WaitForFixedUpdate();
        }
        
        for (var i = 0; i < 30; i++)
        {
            transform.parent.Rotate(Vector3.forward,-3,Space.World);
            yield return new WaitForFixedUpdate();
        }
        for (var i = 0; i < 30; i++)
        {
            transform.parent.Rotate(Vector3.up,3,Space.World);
            yield return new WaitForFixedUpdate();
        }
        _isMoving = false;
    }
    
    private IEnumerator ChangeSideRight()
    {
        var anchor = transform.position + (Vector3.down + Vector3.back) * 1.5f;
        var axis = Vector3.Cross(Vector3.up, Vector3.back);
        _isMoving = true;
        for (var i = 0; i < 30; i++) {
            transform.RotateAround(anchor, axis,6);
            yield return new WaitForFixedUpdate();
        }
        
        for (var i = 0; i < 30; i++)
        {
            transform.parent.Rotate(Vector3.right,3, Space.World);
            yield return new WaitForFixedUpdate();
        }
        for (var i = 0; i < 30; i++)
        {
            transform.parent.Rotate(Vector3.up,-3, Space.World);
            yield return new WaitForFixedUpdate();
        }
        _isMoving = false;
    }

    private bool checkMovementHorizontal(bool forward)
    {
        if (forward)
        {
            if (positionOnGrid[0] > 2)
            {
                return false;
            }
            else
            {
                return !_gameManager.activeSide.verticalGrid[positionOnGrid[0]+1][positionOnGrid[1]];
            }
        }
        else
        {
            if (positionOnGrid[0] < 1)
            {
                
                if (!_gameManager.activeSide.verticalGrid[positionOnGrid[0]][positionOnGrid[1]] &&
                    !_gameManager.leftSide.verticalGrid[4][positionOnGrid[1]])
                {
                    _gameManager.activeSide.Rotate(true);
                    _gameManager.rightSide.Rotate(true);
                    _gameManager.rightSide.Rotate(true);
                    _gameManager.leftSide.Rotate(true);
                    Side_ stock = _gameManager.activeSide; 
                    _gameManager.activeSide = _gameManager.leftSide;
                    _gameManager.leftSide = _gameManager.rightSide;
                    _gameManager.rightSide = stock;
                    _gameManager.ChangeActive();
                    StartCoroutine(ChangeSideLeft());
                    positionOnGrid[0] = 3 - positionOnGrid[1];
                    positionOnGrid[1] = 3;
                    return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return !_gameManager.activeSide.verticalGrid[positionOnGrid[0]][positionOnGrid[1]];
            }
        }
    }
    
    private bool checkMovementVertical(bool forward)
    {
        if (forward)
        {
            if (positionOnGrid[1] > 2)
            {
                
                if (!_gameManager.activeSide.horizontalGrid[positionOnGrid[0]][positionOnGrid[1]+1] &&
                    !_gameManager.rightSide.horizontalGrid[positionOnGrid[0]][0])
                {
                    _gameManager.activeSide.Rotate(false);
                    _gameManager.leftSide.Rotate(true);
                    _gameManager.leftSide.Rotate(true);
                    _gameManager.rightSide.Rotate(false);
                    Side_ stock = _gameManager.activeSide;
                    _gameManager.activeSide = _gameManager.rightSide;
                    _gameManager.rightSide = _gameManager.leftSide;
                    _gameManager.leftSide = stock;
                    _gameManager.ChangeActive();
                    StartCoroutine(ChangeSideRight());
                    positionOnGrid[1] = 3 - positionOnGrid[0];
                    positionOnGrid[0] = 0;
                    return false;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return !_gameManager.activeSide.horizontalGrid[positionOnGrid[0]][positionOnGrid[1]+1];
            }
        }
        else
        {
            if (positionOnGrid[1] < 1)
            {
                return false;
            }
            else
            {
                return !_gameManager.activeSide.horizontalGrid[positionOnGrid[0]][positionOnGrid[1]];
            }
        }
    }
    
    
}
