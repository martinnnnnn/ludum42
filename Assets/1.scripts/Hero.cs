using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hero : MonoBehaviour
{
    public enum Side
    {
        Bottom,
        Top,
        Left,
        Right
    }

    public Side side;
    //public float speed = 10;
    //private Rigidbody2D rigidBody;

    //public Walkable walkable1;
    //public Walkable walkable2;

    private void Start()
    {
        //rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2Int direction = GetInputs();

        if (side == Side.Bottom || side == Side.Top)
        {
            if (direction.x == 1)
            {
                // nexbox 
                //if ()
            }
            else if (direction.x == -1)
            {

            }
            else if (direction.y == 1)
            {
                if (side == Side.Bottom)
                {

                }
                else if (side == Side.Top)
                {

                }
            }
            else if (direction.y == -1)
            {
                if (side == Side.Bottom)
                {

                }
                else if (side == Side.Top)
                {

                }
            }
        }
    }

    //void OnCollisionEnter2D(Collision2D coll)
    //{
    //    Walkable walkable = coll.gameObject.GetComponent<Walkable>();
    //    if (walkable != null)
    //    {
    //        if (walkable1 == null && walkable2 != walkable)
    //        {
    //            walkable1 = walkable;
    //        }
    //        else if (walkable2 == null && walkable1 != walkable)
    //        {
    //            walkable2 = walkable;
    //        }
    //    }
    //}

    //void OnCollisionExit2D(Collision2D coll)
    //{
    //    Walkable walkable = coll.gameObject.GetComponent<Walkable>();
    //    if (walkable != null)
    //    {
    //        if (walkable1 == walkable)
    //        {
    //            walkable1 = null;
    //        }
    //        else if (walkable2 == walkable)
    //        {
    //            walkable2 = null;
    //        }
    //    }

    //}

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
