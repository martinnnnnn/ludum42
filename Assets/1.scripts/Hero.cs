using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


// TODO : add boxcollider to hero
// TODO : mechanism to set boxes's wall automaticly
// TODO : modify half height to distance from center to edge at desired locations




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
    Box nextBox;
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

        Vector2 newPosition = transform.position;

        if (side == Side.Bottom || side == Side.Top)
        {
            // moves from between two boxes onto the socket of the next one (the one on the right)
            if (nextBox != null)
            {
                newPosition = new Vector2(
                    nextBox.transform.position.x,
                    nextBox.transform.position.y - nextBox.size / 2 + size / 2);
                currentBox = nextBox;
                nextBox = null;
            }
            else if (direction.x == 1)
            {
                // hero on the currentbox's socket, going up the right wall
                if (currentBox.wallRight != null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x + currentBox.size / 2 - size / 2,
                        currentBox.transform.position.y);
                }
                // hero on the currentbox's socket, going between this box and the one on the right
                else
                {
                    nextBox = grid.boxes[currentBox._position.x + 1][currentBox._position.y];
                    newPosition = new Vector2(
                        currentBox.transform.position.x + currentBox.size / 2,
                        currentBox.transform.position.y - currentBox.size + size / 2); // <---- change size / 2 to height of center to edge
                }
                
            }
            else if (direction.x == -1)
            {
                // hero on the currentbox's socket, going up the left wall
                if (currentBox.wallLeft != null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x - currentBox.size / 2 + size / 2,
                        currentBox.transform.position.y);
                }
                // hero on the currentbox's socket, going between this box and the one on the left
                else
                {
                    nextBox = grid.boxes[currentBox._position.x - 1][currentBox._position.y];
                    newPosition = new Vector2(
                        currentBox.transform.position.x - currentBox.size / 2,
                        currentBox.transform.position.y - currentBox.size + size / 2); // <---- change size / 2 to height of center to edge
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
                }
                // going up left wall
                else if (currentBox.wallLeft == null && currentBox.wallRight != null)
                {
                    newPosition = new Vector2(
                        currentBox.transform.position.x - currentBox.size / 2 + size / 2,
                        currentBox.transform.position.y);
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
        else if (side == Side.Left)
        {

        }
        else if (side == Side.Right)
        {

        }
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
