using System;
using System.Collections;
using UnityEngine;
 
public class CharaMovement2 : MonoBehaviour {
    [SerializeField] private float _rollSpeed = 5;
    private bool _isMoving;

    public int[] positionOnGrid = new int[2];

    [SerializeField] private GameManager _gameManager;

    private void Update() {
        if (_isMoving) return;

        if (Input.GetKey(KeyCode.LeftArrow) && checkMovementHorizontal(false))
        {
            Assemble(Vector3.left);
            positionOnGrid[0] -= 1; 
        }
        else if (Input.GetKey(KeyCode.RightArrow) && checkMovementHorizontal(true))
        {
            Assemble(Vector3.right);
            positionOnGrid[0] += 1; 
        }
        else if (Input.GetKey(KeyCode.UpArrow) && checkMovementVertical(false))
        {
            Assemble(Vector3.forward);
            positionOnGrid[1] -= 1; 
        }
        else if (Input.GetKey(KeyCode.DownArrow) && checkMovementVertical(true))
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
 
    private IEnumerator Roll(Vector3 anchor, Vector3 axis) {
        _isMoving = true;
        for (var i = 0; i < 90 / _rollSpeed; i++) {
            transform.RotateAround(anchor, axis, _rollSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        _isMoving = false;
    }

    private IEnumerator ChangeSideLeft()
    {
        var anchor = transform.position + (Vector3.down + Vector3.left) * 1.5f;
        var axis = Vector3.Cross(Vector3.up, Vector3.left);
        _isMoving = true;
        for (var i = 0; i < 180 / _rollSpeed; i++) {
            transform.RotateAround(anchor, axis, _rollSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        
        for (var i = 0; i < 90; i++)
        {
            transform.parent.Rotate(Vector3.forward,-1,Space.World);
            yield return new WaitForSeconds(0.005f);
        }
        for (var i = 0; i < 90; i++)
        {
            transform.parent.Rotate(Vector3.up,1,Space.World);
            yield return new WaitForSeconds(0.005f);
        }
        _isMoving = false;
    }
    
    private IEnumerator ChangeSideRight()
    {
        var anchor = transform.position + (Vector3.down + Vector3.back) * 1.5f;
        var axis = Vector3.Cross(Vector3.up, Vector3.back);
        _isMoving = true;
        for (var i = 0; i < 180 / _rollSpeed; i++) {
            transform.RotateAround(anchor, axis, _rollSpeed);
            yield return new WaitForSeconds(0.01f);
        }
        
        for (var i = 0; i < 90; i++)
        {
            transform.parent.Rotate(Vector3.right,1, Space.World);
            yield return new WaitForSeconds(0.005f);
        }
        for (var i = 0; i < 90; i++)
        {
            transform.parent.Rotate(Vector3.up,-1, Space.World);
            yield return new WaitForSeconds(0.005f);
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
