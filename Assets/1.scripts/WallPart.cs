using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPart : MonoBehaviour
{
    public Vector2Int direction;
    public Box box1;
    public Box box2;
    public Wall wall;


    public void UpdateRealPositionSnap()
    {
        transform.position = GetNewPosition();
    }

    public void UpdateRealPositionLerp()
    {
        StartCoroutine(LerpToPosition(GetNewPosition()));
    }

    IEnumerator LerpToPosition(Vector2 newPosition)
    {
        // TODO(martin) : do the lerp thing
        yield return null;
    }

    public Vector2 GetNewPosition()
    {
        Vector2 newPosition = new Vector2(0, 0);
        if ((box1 == null && box2 != null) || (box1 != null && box2 == null))
        {
            Box box = (box1 == null) ? box2 : box1;

            // if moving horizontally
            if (direction.x != 0)
            {
                // if new position on the far left of the grid
                if (transform.position.x > box.transform.position.x)
                {
                    newPosition = new Vector2(box.transform.position.x - box.size / 2, box.transform.position.y);
                }
                // if new position on the far right of the grid
                else
                {
                    newPosition = new Vector2(box.transform.position.x + box.size / 2, box.transform.position.y);
                }
            }

            // if going vertically and 
            else if (direction.y != 0)
            {
                // if new position on the bottom of the plane the grid
                if (transform.position.y > box.transform.position.y)
                {
                    newPosition = new Vector2(box.transform.position.x, box.transform.position.y - box.size / 2);
                }
                // if new position on the top of the plane the grid
                else
                {
                    newPosition = new Vector2(box.transform.position.x, box.transform.position.y + box.size / 2);
                }
            }
        }
        else
        {
            newPosition = Vector2.Lerp(
                box1.transform.position, box2.transform.position, 0.5f);
        }
        return newPosition;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1, 1F);
        if (box1 != null)
            Gizmos.DrawSphere(box1.transform.position, 0.5f);
        if (box2 != null)
            Gizmos.DrawSphere(box2.transform.position, 0.5f);
    }
}
