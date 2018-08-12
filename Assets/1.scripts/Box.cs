using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Vector2Int _position;

    public List<Item> _items = new List<Item>();
    public float size;

    public WallPart wallLeft;
    public WallPart wallRight;
    public WallPart wallTop;
    public WallPart wallBottom;

    private void Awake()
    {
    }

    private void Update()
    {
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = new Color(1, 0, 0, 1F);
    //    Gizmos.DrawWireCube(transform.position, new Vector2(size, size));
    //}
}
