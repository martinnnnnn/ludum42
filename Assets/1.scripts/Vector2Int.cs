using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Vector2Int
{
    public Vector2Int(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public Vector2Int(float _x, float _y)
    {
        x = (int)_x;
        y = (int)_y;
    }

    public Vector2Int(Vector2 _vec)
    {
        x = (int)_vec.x;
        y = (int)_vec.y;
    }

    public static bool operator ==(Vector2Int _vector1, Vector2Int _vector2)
    {
        return _vector1.x == _vector2.x && _vector1.y == _vector2.y;
    }
    public static bool operator !=(Vector2Int _vector1, Vector2Int _vector2)
    {
        return !(_vector1 == _vector2);
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public int x;
    public int y;
}