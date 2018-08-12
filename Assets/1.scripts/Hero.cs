using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hero : MonoBehaviour
{
    //BoxCollider2D collider;

    //public Box currentBox;
    public GameObject StaticWallBig;
    public GameObject StaticWallSmall;
    float heightDiff = 0;
    float horizontalDistance;
    float horizontalDistanceBonus;

    private void Start()
    {
        //collider = GetComponent<BoxCollider2D>();

        //float side = collider.bounds.extents.x;
        horizontalDistance = StaticWallBig.GetComponent<BoxCollider2D>().bounds.extents.x * 2.0f / 3.0f;
        horizontalDistanceBonus = StaticWallSmall.GetComponent<BoxCollider2D>().bounds.extents.x;
        heightDiff = (horizontalDistance * Mathf.Sqrt(2.0f) / 2.0f) - horizontalDistance / 2.0f;

        // height difference between when it's on an edge and when it's on a side
        //side = collider.GetComponent<BoxCollider2D>().bounds.extents.x;
        //heightDiff = (side * Mathf.Sqrt(2.0f)) - side / 2.0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Vector2 newPositionMidWay = new Vector2(transform.position.x + horizontalDistance / 2, transform.position.y + heightDiff);
            Vector2 newPosition = new Vector2(transform.position.x + horizontalDistance, transform.position.y);
            float newRotation = transform.rotation.eulerAngles.z - 180;

            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(transform.DOMove(newPositionMidWay, 0.5f))
                .Append(transform.DOMove(newPosition, 0.5f))
                .Insert(0, transform.DORotate(new Vector3(0, 0, newRotation), 1.0f));
        }
    }
}
