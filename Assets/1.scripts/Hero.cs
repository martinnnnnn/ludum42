using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


// TODO : mechanism to set boxes's wall automaticly
// TODO : modify half height to distance from center to edge at desired locations
// TODO : take wall into account to compute new positions
// TODO : wallparts update boxes's walls when moving
// TODO : check all the fucking lines again just to be sure

public class Hero : MonoBehaviour
{
    public enum Side
    {
        Bottom,
        Top,
        Left,
        Right
    }

    public Side side = Side.Bottom;

    //public float speed = 10;
    public Box currentBox;
    public Box nextBox;
    float size;
    Grid grid;

    private void Start()
    {
        size = GetComponent<BoxCollider2D>().bounds.extents.x * 2;
        Destroy(GetComponent<BoxCollider2D>());
        grid = FindObjectOfType<Grid>();
    }

    private void Update()
    {
        Vector2Int direction = GetInputs();
        if (direction.x != 0) Debug.Log(direction.x);
        Vector2 newPosition = transform.position;

        if (side == Side.Bottom)
        {
            if (direction.x == 1)
            {
                if (nextBox != null)
                {
                    // moves from between two boxes onto the socket of the next one (the one on the right)
                    if (nextBox.transform.position.x > currentBox.transform.position.x)
                    {
                        newPosition = new Vector2(
                            nextBox.transform.position.x,
                            nextBox.transform.position.y - nextBox.size / 2 + size / 2);
                        currentBox = nextBox;
                    }
                    // moves back from between boxes
                    else
                    {
                        newPosition = new Vector2(
                            currentBox.transform.position.x,
                            currentBox.transform.position.y - currentBox.size / 2 + size / 2);
                    }
                    nextBox = null;
                }
                // hero on the currentbox's socket, going up the right wall
                else if (currentBox.wallRight != null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x + currentBox.size / 2 - size / 2,
                        currentBox.transform.position.y);
                    side = Side.Right;
                }
                // hero on the currentbox's socket, going between this box and the one on the right
                else
                {
                    nextBox = grid.boxes[currentBox._position.x + 1][currentBox._position.y];
                    newPosition = new Vector2(
                        currentBox.transform.position.x + currentBox.size / 2,
                        currentBox.transform.position.y - currentBox.size / 2 + size / 2); // <---- change size / 2 to height of center to edge
                }
            }
            else if (direction.x == -1)
            {
                if (nextBox != null)
                {
                    // moves from between two boxes onto the socket of the next one (the one on the right)
                    if (nextBox.transform.position.x < currentBox.transform.position.x)
                    {
                        newPosition = new Vector2(
                            nextBox.transform.position.x,
                            nextBox.transform.position.y - nextBox.size / 2 + size / 2);
                        currentBox = nextBox;
                    }
                    // moves back from between boxes
                    else
                    {
                        newPosition = new Vector2(
                            currentBox.transform.position.x,
                            currentBox.transform.position.y - currentBox.size / 2 + size / 2);
                    }
                    nextBox = null;
                }
                // hero on the currentbox's socket, going up the left wall
                else if (currentBox.wallLeft != null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x - currentBox.size / 2 + size / 2,
                        currentBox.transform.position.y);
                    side = Side.Left;
                }
                // hero on the currentbox's socket, going between this box and the one on the left
                else
                {
                    nextBox = grid.boxes[currentBox._position.x - 1][currentBox._position.y];
                    newPosition = new Vector2(
                        currentBox.transform.position.x - currentBox.size / 2,
                        currentBox.transform.position.y - currentBox.size / 2 + size / 2); // <---- change size / 2 to height of center to edge
                }
            }
            else if (direction.y == 1)
            {
                // going up right wall
                if (currentBox.wallRight != null && currentBox.wallRight == null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x + currentBox.size / 2 - size / 2,
                        currentBox.transform.position.y);
                    side = Side.Right;
                }
                // going up left wall
                else if (currentBox.wallLeft == null && currentBox.wallRight != null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x - currentBox.size / 2 + size / 2,
                        currentBox.transform.position.y);
                    side = Side.Left;
                }
            }
            else if (direction.y == -1)
            {
                // TODO : going down left or right from bottom when there's no wall
            }
        }
        // --------------------------------------------------------------------------------------------------------
        else if (side == Side.Top)
        {
            if (direction.x == 1)
            {
                if (nextBox != null)
                {
                    // moves from between two boxes onto the socket of the next one (the one on the right)
                    if (nextBox.transform.position.x > currentBox.transform.position.x)
                    {
                        newPosition = new Vector2(
                            nextBox.transform.position.x,
                            nextBox.transform.position.y + nextBox.size / 2 - size / 2);
                        currentBox = nextBox;
                    }
                    // moves back from between boxes
                    else
                    {
                        newPosition = new Vector2(
                            currentBox.transform.position.x,
                            currentBox.transform.position.y + currentBox.size / 2 - size / 2);
                    }
                    nextBox = null;
                }
                // hero on the currentbox's socket, going up the right wall
                else if (currentBox.wallRight != null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x + currentBox.size / 2 - size / 2,
                        currentBox.transform.position.y);
                    side = Side.Right;
                }
                // hero on the currentbox's socket, going between this box and the one on the right
                else
                {
                    nextBox = grid.boxes[currentBox._position.x + 1][currentBox._position.y];
                    newPosition = new Vector2(
                        currentBox.transform.position.x + currentBox.size / 2,
                        currentBox.transform.position.y + currentBox.size / 2 - size / 2); // <---- change size / 2 to height of center to edge
                    
                }

            }
            else if (direction.x == -1)
            {
                if (nextBox != null)
                {
                    // moves from between two boxes onto the socket of the next one (the one on the right)
                    if (nextBox.transform.position.x < currentBox.transform.position.x)
                    {
                        newPosition = new Vector2(
                            nextBox.transform.position.x,
                            nextBox.transform.position.y + nextBox.size / 2 - size / 2);
                        currentBox = nextBox;
                    }
                    // moves back from between boxes
                    else
                    {
                        newPosition = new Vector2(
                            currentBox.transform.position.x,
                            currentBox.transform.position.y + currentBox.size / 2 - size / 2);
                    }
                    nextBox = null;
                }
                // hero on the currentbox's socket, going up the left wall
                else if (currentBox.wallLeft != null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x - currentBox.size / 2 + size / 2,
                        currentBox.transform.position.y);
                    side = Side.Left;
                }
                // hero on the currentbox's socket, going between this box and the one on the left
                else
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x - currentBox.size / 2,
                        currentBox.transform.position.y + currentBox.size / 2 - size / 2); // <---- change size / 2 to height of center to edge
                }
            }
            else if (direction.y == -1)
            {
                // going up right wall
                if (currentBox.wallRight != null && currentBox.wallRight == null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x + currentBox.size / 2 - size / 2,
                        currentBox.transform.position.y);
                    side = Side.Right;
                }
                // going up left wall
                else if (currentBox.wallLeft == null && currentBox.wallRight != null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x - currentBox.size / 2 + size / 2,
                        currentBox.transform.position.y);
                    side = Side.Left;
                }
            }
            else if (direction.y == 1)
            {
                // TODO : going up left or right from top when there's no wall
            }
        }
        // --------------------------------------------------------------------------------------------------------
        if (side == Side.Left)
        {
            if (direction.y == 1)
            {
                if (nextBox != null)
                {
                    // moves from between two boxes onto the socket of the next one (the one on the right)
                    if (nextBox.transform.position.x > currentBox.transform.position.x)
                    {
                        newPosition = new Vector2(
                            nextBox.transform.position.x - nextBox.size / 2 + size / 2,
                            nextBox.transform.position.y);
                        currentBox = nextBox;
                    }
                    // moves back from between boxes
                    else
                    {
                        newPosition = new Vector2(
                            currentBox.transform.position.x - currentBox.size / 2 + size / 2,
                            currentBox.transform.position.y);
                    }
                    nextBox = null;
                }
                // hero on the currentbox's socket, going up the top wall
                else if (currentBox.wallTop != null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x,
                        currentBox.transform.position.y + currentBox.size / 2 - size / 2);
                    side = Side.Top;
                }
                // hero on the currentbox's socket, going between this box and the one on the top
                else
                {
                    nextBox = grid.boxes[currentBox._position.x][currentBox._position.y + 1];
                    newPosition = new Vector2(
                        currentBox.transform.position.x - currentBox.size / 2 + size / 2,
                        currentBox.transform.position.y + currentBox.size / 2); // <---- change size / 2 to height of center to edge
                }
            }
            else if (direction.y == -1)
            {
                if (nextBox != null)
                {
                    // moves from between two boxes onto the socket of the next one (the one on the right)
                    if (nextBox.transform.position.x < currentBox.transform.position.x)
                    {
                        newPosition = new Vector2(
                            nextBox.transform.position.x - nextBox.size / 2 + size / 2,
                            nextBox.transform.position.y);
                        currentBox = nextBox;
                    }
                    // moves back from between boxes
                    else
                    {
                        newPosition = new Vector2(
                            nextBox.transform.position.x - nextBox.size / 2 + size / 2,
                            nextBox.transform.position.y);
                    }
                    nextBox = null;
                }
                // hero on the currentbox's socket, going down the bottom wall
                else if (currentBox.wallBottom != null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x,
                        currentBox.transform.position.y - currentBox.size / 2 + size / 2);
                    side = Side.Bottom;
                }
                // hero on the currentbox's socket, going between this box and the one on the bottom
                else
                {
                    nextBox = grid.boxes[currentBox._position.x][currentBox._position.y - 1];
                    newPosition = new Vector2(
                        currentBox.transform.position.x - currentBox.size / 2 + size / 2, // <---- change size / 2 to height of center to edge
                        currentBox.transform.position.y - currentBox.size / 2); 
                }
            }
            else if (direction.x == 1)
            {
                // going up top wall
                if (currentBox.wallTop != null && currentBox.wallRight == null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x,
                        currentBox.transform.position.y + currentBox.size / 2 - size / 2);
                    side = Side.Top;
                }
                // going down bottom wall
                else if (currentBox.wallBottom == null && currentBox.wallRight != null)
                {
                    newPosition = new Vector2(
                       currentBox.transform.position.x,
                       currentBox.transform.position.y - currentBox.size / 2 + size / 2);
                    side = Side.Bottom;
                }
            }
            else if (direction.y == -1)
            {
                // TODO : going down left or right from bottom when there's no wall
            }
        }
        // --------------------------------------------------------------------------------------------------------
        else if (side == Side.Right)
        {
            if (direction.y == 1)
            {
                if (nextBox != null)
                {
                    // moves from between two boxes onto the socket of the next one (the one on the right)
                    if (nextBox.transform.position.x > currentBox.transform.position.x)
                    {
                        newPosition = new Vector2(
                            nextBox.transform.position.x + nextBox.size / 2 - size / 2,
                            nextBox.transform.position.y);
                        currentBox = nextBox;
                    }
                    // moves back from between boxes
                    else
                    {
                        newPosition = new Vector2(
                            currentBox.transform.position.x + currentBox.size / 2 - size / 2,
                            currentBox.transform.position.y);
                    }
                    nextBox = null;
                }
                // hero on the currentbox's socket, going up the top wall
                else if (currentBox.wallTop != null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x + currentBox.size / 2 - size / 2,
                        currentBox.transform.position.y);
                    side = Side.Top;
                }
                // hero on the currentbox's socket, going between this box and the one on the top
                else
                {
                    nextBox = grid.boxes[currentBox._position.x][currentBox._position.y + 1];
                    newPosition = new Vector2(
                        currentBox.transform.position.x + currentBox.size / 2 - size / 2,
                        currentBox.transform.position.y + currentBox.size / 2); // <---- change size / 2 to height of center to edge
                }
            }
            else if (direction.y == -1)
            {
                if (nextBox != null)
                {
                    // moves from between two boxes onto the socket of the next one (the one on the top)
                    if (nextBox.transform.position.y < currentBox.transform.position.y)
                    {
                        newPosition = new Vector2(
                            nextBox.transform.position.x + nextBox.size / 2 - size / 2,
                            nextBox.transform.position.y);
                        currentBox = nextBox;
                    }
                    // moves back from between boxes
                    else
                    {
                        newPosition = new Vector2(
                            currentBox.transform.position.x + currentBox.size / 2 - size / 2,
                            currentBox.transform.position.y);
                    }
                    nextBox = null;
                }
                // hero on the currentbox's socket, going down the bottom wall
                else if (currentBox.wallBottom != null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x,
                        currentBox.transform.position.y - currentBox.size / 2 + size / 2);
                    side = Side.Bottom;
                }
                // hero on the currentbox's socket, going between this box and the one on the bottom
                else
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x - currentBox.size / 2 + size / 2, // <---- change size / 2 to height of center to edge
                        currentBox.transform.position.y + currentBox.size / 2);
                }
            }
            else if (direction.x == -1)
            {
                // going up top wall
                if (currentBox.wallTop != null && currentBox.wallRight == null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x,
                        currentBox.transform.position.y + currentBox.size / 2 - size / 2);
                    side = Side.Top;
                }
                // going down bottom wall
                else if (currentBox.wallBottom == null && currentBox.wallRight != null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x,
                        currentBox.transform.position.y - currentBox.size / 2 + size / 2);
                    side = Side.Bottom;
                }
            }
            else if (direction.y == 1)
            {
                // TODO : going up left or right from top when there's no wall
            }
        }














        if (side == Side.Bottom || side == Side.Top)
        {
            if (direction.x == 1)
            {
                if (nextBox != null)
                {
                    // moves from between two boxes onto the socket of the next one (the one on the right)
                    if (nextBox.transform.position.x > currentBox.transform.position.x)
                    {
                        if (side == Side.Bottom)
                        {
                            newPosition = new Vector2(
                                nextBox.transform.position.x,
                                nextBox.transform.position.y - nextBox.size / 2 + size / 2);
                        }
                        else if (side == Side.Top)
                        {
                            newPosition = new Vector2(
                                nextBox.transform.position.x,
                                nextBox.transform.position.y + nextBox.size / 2 - size / 2);
                            currentBox = nextBox;
                        }
                        currentBox = nextBox;
                        nextBox = null;
                    }
                    // moves back from between boxes
                    else
                    {
                        if (side == Side.Bottom)
                        {
                            newPosition = new Vector2(
                                currentBox.transform.position.x,
                                currentBox.transform.position.y - currentBox.size / 2 + size / 2);
                        }
                        else if (side == Side.Top)
                        {
                            newPosition = new Vector2(
                               currentBox.transform.position.x,
                               currentBox.transform.position.y + currentBox.size / 2 - size / 2);
                        }
                        nextBox = null;
                    }
                }
                // hero on the currentbox's socket, going up the right wall
                else if (currentBox.wallRight != null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x + currentBox.size / 2 - size / 2,
                        currentBox.transform.position.y);
                    side = Side.Right;
                }
                // hero on the currentbox's socket, going between this box and the one on the right
                else
                {
                    nextBox = grid.boxes[currentBox._position.x + 1][currentBox._position.y];
                    if (side == Side.Bottom)
                    {
                        newPosition = new Vector2(
                            currentBox.transform.position.x + currentBox.size / 2,
                            currentBox.transform.position.y - currentBox.size / 2 + size / 2); // <---- change size / 2 to height of center to edge
                    }
                    else if (side == Side.Top)
                    {
                        newPosition = new Vector2(
                            currentBox.transform.position.x + currentBox.size / 2,
                            currentBox.transform.position.y + currentBox.size / 2 - size / 2); // <---- change size / 2 to height of center to edge
                    }
                }
                
            }
            else if (direction.x == -1)
            {
                if (nextBox != null)
                {
                    // moves from between two boxes onto the socket of the next one (the one on the right)
                    if (nextBox.transform.position.x < currentBox.transform.position.x)
                    {
                        newPosition = new Vector2(
                            nextBox.transform.position.x,
                            nextBox.transform.position.y - nextBox.size / 2 + size / 2);
                        currentBox = nextBox;
                        nextBox = null;
                    }
                    // moves back from between boxes
                    else
                    {
                        newPosition = new Vector2(
                            currentBox.transform.position.x,
                            currentBox.transform.position.y - currentBox.size / 2 + size / 2);
                        nextBox = null;
                    }
                }
                // hero on the currentbox's socket, going up the left wall
                else if (currentBox.wallLeft != null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x - currentBox.size / 2 + size / 2,
                        currentBox.transform.position.y);
                    side = Side.Left;
                }
                // hero on the currentbox's socket, going between this box and the one on the left
                else
                {
                    nextBox = grid.boxes[currentBox._position.x - 1][currentBox._position.y];
                    if (side == Side.Bottom)
                    {
                        newPosition = new Vector2(
                            currentBox.transform.position.x - currentBox.size / 2,
                            currentBox.transform.position.y - currentBox.size / 2 + size / 2); // <---- change size / 2 to height of center to edge
                    }
                    else if (side == Side.Top)
                    {
                        newPosition = new Vector2(
                            currentBox.transform.position.x - currentBox.size / 2,
                            currentBox.transform.position.y + currentBox.size / 2 - size / 2); // <---- change size / 2 to height of center to edge
                    }

                    
                }
            }
            else if ((direction.y == 1 && side == Side.Bottom) || (direction.y == -1 && side == Side.Top))
            {
                // going up right wall
                if (currentBox.wallLeft != null && currentBox.wallRight == null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x + currentBox.size / 2 - size / 2,
                        currentBox.transform.position.y);
                    side = Side.Right;
                }
                // going up left wall
                else if (currentBox.wallLeft == null && currentBox.wallRight != null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x - currentBox.size / 2 + size / 2,
                        currentBox.transform.position.y);
                    side = Side.Left;
                }
            }
            else if (direction.y == 1 && side == Side.Top)
            {
                // TODO : going up left or right from top when there's no wall
            }
            else if (direction.y == -1 && side == Side.Bottom)
            {
                // TODO : going down left or right from bottom when there's no wall
            }
        }
        if (side == Side.Left || side == Side.Right)
        {
            if (direction.y == 1)
            {
                if (nextBox != null)
                {
                    // moves from between two boxes onto the socket of the next one (the one on the top)
                    if (nextBox.transform.position.y > currentBox.transform.position.y)
                    {
                        if (side == Side.Left)
                        {
                            newPosition = new Vector2(
                                nextBox.transform.position.x - nextBox.size / 2 + size / 2,
                                nextBox.transform.position.y);
                        }
                        else if (side == Side.Right)
                        {
                            newPosition = new Vector2(
                                nextBox.transform.position.x + nextBox.size / 2 - size / 2,
                                nextBox.transform.position.y);
                        }
                        currentBox = nextBox;
                        nextBox = null;
                    }
                    // moves back from between boxes
                    else
                    {
                        if (side == Side.Left)
                        {
                            newPosition = new Vector2(
                                currentBox.transform.position.x,
                                currentBox.transform.position.y - currentBox.size / 2 + size / 2);
                        }
                        else if (side == Side.Right)
                        {
                            newPosition = new Vector2(
                                currentBox.transform.position.x + nextBox.size / 2 - size / 2,
                                currentBox.transform.position.y);
                        }
                        nextBox = null;
                    }
                }
                // hero on the currentbox's socket, going up the Top wall
                else if (currentBox.wallTop != null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x,
                        currentBox.transform.position.y + currentBox.size / 2 - size / 2);
                    side = Side.Top;
                }
                // hero on the currentbox's socket, going between this box and the one on the right
                else
                {
                    nextBox = grid.boxes[currentBox._position.x + 1][currentBox._position.y];
                    newPosition = new Vector2(
                        currentBox.transform.position.x + currentBox.size / 2,
                        currentBox.transform.position.y - currentBox.size / 2 + size / 2); // <---- change size / 2 to height of center to edge
                }

            }
            else if (direction.x == -1)
            {
                if (nextBox != null)
                {
                    // moves from between two boxes onto the socket of the next one (the one on the right)
                    if (nextBox.transform.position.x < currentBox.transform.position.x)
                    {
                        newPosition = new Vector2(
                            nextBox.transform.position.x,
                            nextBox.transform.position.y - nextBox.size / 2 + size / 2);
                        currentBox = nextBox;
                        nextBox = null;
                    }
                    // moves back from between boxes
                    else
                    {
                        newPosition = new Vector2(
                            currentBox.transform.position.x,
                            currentBox.transform.position.y - currentBox.size / 2 + size / 2);
                        nextBox = null;
                    }
                }
                // hero on the currentbox's socket, going up the left wall
                else if (currentBox.wallLeft != null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x - currentBox.size / 2 + size / 2,
                        currentBox.transform.position.y);
                    side = Side.Left;
                }
                // hero on the currentbox's socket, going between this box and the one on the left
                else
                {
                    nextBox = grid.boxes[currentBox._position.x - 1][currentBox._position.y];
                    newPosition = new Vector2(
                        currentBox.transform.position.x - currentBox.size / 2,
                        currentBox.transform.position.y - currentBox.size / 2 + size / 2); // <---- change size / 2 to height of center to edge
                }
            }
            else if ((direction.y == 1 && side == Side.Bottom) || (direction.y == -1 && side == Side.Top))
            {
                // going up right wall
                if (currentBox.wallLeft != null && currentBox.wallRight == null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x + currentBox.size / 2 - size / 2,
                        currentBox.transform.position.y);
                    side = Side.Right;
                }
                // going up left wall
                else if (currentBox.wallLeft == null && currentBox.wallRight != null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x - currentBox.size / 2 + size / 2,
                        currentBox.transform.position.y);
                    side = Side.Left;
                }
            }
            else if (direction.y == 1 && side == Side.Top)
            {
                // TODO : going up left or right from top when there's no wall
            }
            else if (direction.y == -1 && side == Side.Bottom)
            {
                // TODO : going down left or right from bottom when there's no wall
            }
        }
        if (direction.x != 0 || direction.y != 0)
            transform.position = newPosition;
    }


    Vector2Int GetInputs()
    {
        Vector2Int inputs = new Vector2Int(0,0);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            inputs.y = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            inputs.x = -1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            inputs.y = -1;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            inputs.x = 1;
        }
        return inputs;
    }
}
