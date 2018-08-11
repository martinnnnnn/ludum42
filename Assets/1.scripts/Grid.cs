using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public GameObject boxPrefab;
    public GameObject wallPartPrefab;
    Box[][] boxes;
    public Vector2 size;
    [HideInInspector]
    public Vector2Int sizeInt;

    public bool base0 = true;
    public Vector2[] EditorLeftWall;
    public Vector2[] EditorRightWall;
    public Vector2[] EditorTopWall;
    public Vector2[] EditorBottomWall;

    [HideInInspector] public Wall LeftWall;
    [HideInInspector] public Wall RightWall;
    [HideInInspector] public Wall TopWall;
    [HideInInspector] public Wall BottomWall;


    private void Start()
    {
        InitGrid();
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            InitGrid();
        }
    }


    public Box GetBoxAt(int _x, int _y)
    {
        return boxes[_x][_y];
    }


    public void OnPlayerActivateNewBox()
    {
        for (int i = 0; i < boxes.Length; ++i)
        {
            
        }
    }

    public void InitGrid()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        sizeInt = new Vector2Int(size);
        boxes = new Box[sizeInt.x][];

        for (int i = 0; i < sizeInt.x; ++i)
        {
            for (int j = 0; j < sizeInt.y; ++j)
            {
                boxes[i] = new Box[sizeInt.y];
                GameObject obj = Instantiate(boxPrefab, this.transform) as GameObject;
                Box currentBox = obj.GetComponent<Box>();
                currentBox.size = wallPartPrefab.GetComponent<BoxCollider2D>().bounds.extents.x * 2 + wallPartPrefab.GetComponent<BoxCollider2D>().bounds.extents.y * 2;
                currentBox._position.x = i;
                currentBox._position.y = j;
                currentBox.transform.position = new Vector2(
                    currentBox._position.x * currentBox.size,
                    currentBox._position.y * currentBox.size);
            }
        }

        foreach (Transform child in transform)
        {
            Box box = child.GetComponent<Box>();
            if (box)
            {
                boxes[box._position.x][box._position.y] = box;
            }
        }

        LeftWall = gameObject.AddComponent<Wall>();
        RightWall = gameObject.AddComponent<Wall>();
        TopWall = gameObject.AddComponent<Wall>();
        BottomWall = gameObject.AddComponent<Wall>();

        for (int i = 0; i < sizeInt.y; ++i)
        {
            // adding wall parts to leftwall
            WallPart currentWallPart = Instantiate(wallPartPrefab, this.transform).GetComponent<WallPart>();
            currentWallPart.direction = new Vector2Int(1, 0);
            currentWallPart.box1 = boxes[0][i];
            currentWallPart.box2 = null;
            LeftWall.Add(currentWallPart);
            currentWallPart.UpdateRealPositionSnap();

            // adding wall parts to rightwall
            currentWallPart = Instantiate(wallPartPrefab, this.transform).GetComponent<WallPart>();
            currentWallPart.direction = new Vector2Int(-1, 0);
            currentWallPart.box1 = boxes[boxes.Length - 1][i];
            currentWallPart.box2 = null;
            RightWall.Add(currentWallPart);
            currentWallPart.UpdateRealPositionSnap();
        }

        for (int i = 0; i < sizeInt.x; ++i)
        {
            // adding wall parts to bottomwall
            WallPart currentWallPart = Instantiate(wallPartPrefab, this.transform).GetComponent<WallPart>();
            currentWallPart.direction = new Vector2Int(0, 1);
            currentWallPart.box1 = boxes[i][boxes[i].Length - 1];
            Debug.Log(boxes[i][boxes[i].Length - 1]);
            currentWallPart.box2 = null;
            BottomWall.Add(currentWallPart);
            currentWallPart.UpdateRealPositionSnap();

            // adding wall parts to topwall
            currentWallPart = Instantiate(wallPartPrefab, this.transform).GetComponent<WallPart>();
            currentWallPart.direction = new Vector2Int(0, -1);
            currentWallPart.box1 = boxes[i][0];
            Debug.Log(boxes[i][0]);
            currentWallPart.box2 = null;
            TopWall.Add(currentWallPart);
            currentWallPart.UpdateRealPositionSnap();
        }

        //foreach (Vector2 pos in EditorLeftWall)
        //{
        //    Vector2Int posInt;
        //    if (base0)
        //    {
        //        posInt = new Vector2Int(pos);
        //    }
        //    else
        //    {
        //        posInt = new Vector2Int(pos.x - 1.0f, pos.y - 1.0f);
        //    }

        //    WallPart currentWallPart = Instantiate(wallPartPrefab, this.transform).GetComponent<WallPart>();
        //    currentWallPart.m_direction = new Vector2Int(1, 0);
        //    currentWallPart.box1 = boxes[posInt.x][posInt.y];
        //    currentWallPart.box2 = ;
        //    //currentWallPart.transform.position = ...
        //    LeftWall.wallparts.Add(currentWallPart);
        //}
    }

}
