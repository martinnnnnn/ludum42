using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    private List<Vector2> EditorLeftWallList;
    private List<Vector2> EditorRightWallList;
    private List<Vector2> EditorTopWallList;
    private List<Vector2> EditorBottomWallList;

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

        foreach(Wall wall in GetComponents<Wall>())
        {
            Destroy(wall);
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

        wallPartPrefab.transform.position = boxes[sizeInt.x / 2][sizeInt.y / 2].transform.position;


        LeftWall = gameObject.AddComponent<Wall>();
        RightWall = gameObject.AddComponent<Wall>();
        TopWall = gameObject.AddComponent<Wall>();
        BottomWall = gameObject.AddComponent<Wall>();

        UpdateEditorParamsLists();

        for (int i = 0; i < sizeInt.y; ++i)
        {
            // adding wall parts to leftwall
            WallPart currentWallPart = Instantiate(wallPartPrefab, this.transform).GetComponent<WallPart>();
            currentWallPart.GetComponent<SpriteRenderer>().enabled = true;
            currentWallPart.direction = new Vector2Int(1, 0);
            int moveTo = FindInEditorParameters(EditorLeftWallList, i);
            if (moveTo != -1)
            {
                currentWallPart.box1 = boxes[moveTo][i];
                currentWallPart.box2 = boxes[moveTo-1][i];
            }
            else
            {
                currentWallPart.box1 = boxes[0][i];
                currentWallPart.box2 = null;
            }
            //
            LeftWall.Add(currentWallPart);
            currentWallPart.UpdateRealPositionSnap();

            // adding wall parts to rightwall
            currentWallPart = Instantiate(wallPartPrefab, this.transform).GetComponent<WallPart>();
            currentWallPart.GetComponent<SpriteRenderer>().enabled = true;
            currentWallPart.direction = new Vector2Int(-1, 0);

            moveTo = FindInEditorParameters(EditorRightWallList, i);
            if (moveTo != -1)
            {
                currentWallPart.box1 = boxes[boxes.Length - 1 - moveTo][i];
                currentWallPart.box2 = boxes[boxes.Length - moveTo][i];
            }
            else
            {
                currentWallPart.box1 = boxes[boxes.Length - 1][i];
                currentWallPart.box2 = null;
            }

            RightWall.Add(currentWallPart);
            currentWallPart.UpdateRealPositionSnap();
        }

        for (int i = 0; i < sizeInt.x; ++i)
        {
            // adding wall parts to bottomwall
            WallPart currentWallPart = Instantiate(wallPartPrefab, this.transform).GetComponent<WallPart>();
            currentWallPart.GetComponent<SpriteRenderer>().enabled = true;
            currentWallPart.direction = new Vector2Int(0, 1);
            currentWallPart.box1 = boxes[i][boxes[i].Length - 1];
            currentWallPart.box2 = null;
            BottomWall.Add(currentWallPart);
            currentWallPart.UpdateRealPositionSnap();

            // adding wall parts to topwall
            currentWallPart = Instantiate(wallPartPrefab, this.transform).GetComponent<WallPart>();
            currentWallPart.GetComponent<SpriteRenderer>().enabled = true;
            currentWallPart.direction = new Vector2Int(0, -1);
            currentWallPart.box1 = boxes[i][0];
            currentWallPart.box2 = null;
            TopWall.Add(currentWallPart);
            currentWallPart.UpdateRealPositionSnap();
        }
    }

    int FindInEditorParameters(List<Vector2> _wallParams, int _index)
    {

        Vector2 found = _wallParams.Find(p => (int)p.x == _index);
        if (found.x != 0 || found.y != 0)
        {
            return (int)found.y;
        }
        return -1;
    }

    void UpdateEditorParamsLists()
    {
        EditorLeftWallList = new List<Vector2>(EditorLeftWall);
        EditorRightWallList = new List<Vector2>(EditorRightWall);
        EditorTopWallList = new List<Vector2>(EditorTopWall);
        EditorBottomWallList = new List<Vector2>(EditorBottomWall);

        if (!base0)
        {
            for (int i = 0; i < EditorLeftWallList.Count; ++i)
            {
                EditorLeftWallList[i] = new Vector2(EditorLeftWallList[i].x - 1.0f, EditorLeftWallList[i].y - 1.0f);
            }
            for (int i = 0; i < EditorRightWallList.Count; ++i)
            {
                EditorRightWallList[i] = new Vector2(EditorRightWallList[i].x - 1.0f, EditorRightWallList[i].y - 1.0f);
            }
            for (int i = 0; i < EditorTopWallList.Count; ++i)
            {
                EditorTopWallList[i] = new Vector2(EditorTopWallList[i].x - 1.0f, EditorTopWallList[i].y - 1.0f);
            }
            for (int i = 0; i < EditorBottomWallList.Count; ++i)
            {
                EditorBottomWallList[i] = new Vector2(EditorBottomWallList[i].x - 1.0f, EditorBottomWallList[i].y - 1.0f);
            }
        }
    }

    //void AddWallPartsHorizontal(Wall _wall, Vector2Int _direction, )
    //{

    //}

}
