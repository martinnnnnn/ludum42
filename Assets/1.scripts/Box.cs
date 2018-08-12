using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public Vector2Int _position;

    public List<Item> _items = new List<Item>();
    public float size;
    //public GameObject wall;
    private void Awake()
    {
        //size = 
    }

    private void Update()
    {
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 1F);
        Gizmos.DrawWireCube(transform.position, new Vector2(size, size));
        //Gizmos.color = new Color(0, 1, 0, 0.5F);
        //Gizmos.DrawCube(transform.position, new Vector2(size, size));
    }
}
