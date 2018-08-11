using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public List<WallPart> wallparts = new List<WallPart>();
    public Vector2Int direction;

    public void OnPlayerMovement(Vector2Int _direction)
    {
        //if (_direction == direction)
        //{
        //    foreach(wallparts)
        //}
    }

    public void Add(WallPart _part)
    {
        _part.wall = this;
        wallparts.Add(_part);
    }
}
