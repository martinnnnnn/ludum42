using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public List<WallPart> wallparts = new List<WallPart>();
    public Vector2Int direction;
    public Grid grid;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnPlayerMovement(new Vector2Int(0, 1));
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            OnPlayerMovement(new Vector2Int(0, 1));
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            OnPlayerMovement(new Vector2Int(-1, 0));
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            OnPlayerMovement(new Vector2Int(1, 0));
        }
    }

    public void OnPlayerMovement(Vector2Int _direction)
    {
        if (_direction == direction)
        {
            foreach (WallPart wallpart in wallparts)
            {
                wallpart.UpdateBoxes(_direction);
                wallpart.UpdateRealPositionSnap();
            }
        }
    }

    public void Add(WallPart _part)
    {
        _part.wall = this;
        wallparts.Add(_part);
    }
}
