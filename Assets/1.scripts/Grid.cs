using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Grid : MonoBehaviour
{
    public GameObject boxPrefab;
    public GameObject wallPartPrefab;
    [HideInInspector]
    public Box[][] boxes;
    public Vector2 size;
    [HideInInspector]
    public Vector2Int roundSize;

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

    public bool level;

    private void Start()
    {
        roundSize = new Vector2Int(size);

        if (level)
        {
            LeftWall.direction = new Vector2Int(1, 0);
            RightWall.direction = new Vector2Int(-1, 0);
            TopWall.direction = new Vector2Int(0, -1);
            BottomWall.direction = new Vector2Int(0, 1);

            List<Box> littleBoxes = new List<Box>(GetComponentsInChildren<Box>());
            var byx = littleBoxes.GroupBy(box => box.transform.position.x);

            boxes = new Box[roundSize.x][];
            int i = 0;
            foreach (var group in byx)
            {
                boxes[i] = new Box[roundSize.y];
                int j = 0;
                foreach (var groupedItem in group)
                {
                    boxes[i][j] = groupedItem;
                    boxes[i][j]._position.x = i;
                    boxes[i][j]._position.y = j;
                    j++;
                }
                i++;
            }
        }

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

        roundSize = new Vector2Int(size);
        boxes = new Box[roundSize.x][];

        for (int i = 0; i < roundSize.x; ++i)
        {
            boxes[i] = new Box[roundSize.y];
            for (int j = 0; j < roundSize.y; ++j)
            {
                GameObject obj = Instantiate(boxPrefab, this.transform) as GameObject;
                Box currentBox = obj.GetComponent<Box>();
                currentBox.size = wallPartPrefab.GetComponent<BoxCollider2D>().bounds.extents.x * 2 + wallPartPrefab.GetComponent<BoxCollider2D>().bounds.extents.y * 2;
                currentBox._position.x = i;
                currentBox._position.y = j;

                //GameObject shameObject = Instantiate(, shame.transform);

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

        wallPartPrefab.transform.position = boxes[roundSize.x / 2][roundSize.y / 2].transform.position;


        InitWall(ref LeftWall, new Vector2Int(1, 0));
        InitWall(ref RightWall, new Vector2Int(-1, 0));
        InitWall(ref TopWall, new Vector2Int(0, -1));
        InitWall(ref BottomWall, new Vector2Int(0, 1));

        UpdateEditorParamsLists();

        //---------------------------------------------
        // ADDING WALL PARTS TO WALLS
        //---------------------------------------------
        for (int i = 0; i < roundSize.y; ++i)
        {
            // adding wall parts to leftwall
            WallPart currentWallPart = Instantiate(wallPartPrefab, this.transform).GetComponent<WallPart>();
            currentWallPart.GetComponent<SpriteRenderer>().enabled = true;
            currentWallPart.transform.Rotate(new Vector3(0, 0, 270));
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
            LeftWall.Add(currentWallPart);
            currentWallPart.UpdateRealPositionSnap();

            // adding wall parts to rightwall
            currentWallPart = Instantiate(wallPartPrefab, this.transform).GetComponent<WallPart>();
            currentWallPart.GetComponent<SpriteRenderer>().enabled = true;
            currentWallPart.transform.Rotate(new Vector3(0, 0, 90));
            moveTo = FindInEditorParameters(EditorRightWallList, i);
            if (moveTo != -1)
            {
                currentWallPart.box2 = boxes[boxes.Length - moveTo][i];
                currentWallPart.box1 = boxes[boxes.Length - moveTo - 1][i];
            }
            else
            {
                currentWallPart.box1 = boxes[boxes.Length - 1][i];
                currentWallPart.box2 = null;
            }
            RightWall.Add(currentWallPart);
            currentWallPart.UpdateRealPositionSnap();
        }

        for (int i = 0; i < roundSize.x; ++i)
        {
            // adding wall parts to bottomwall
            WallPart currentWallPart = Instantiate(wallPartPrefab, this.transform).GetComponent<WallPart>();
            currentWallPart.GetComponent<SpriteRenderer>().enabled = true;
            currentWallPart.transform.Rotate(new Vector3(0, 0, 180));
            int moveTo = FindInEditorParameters(EditorTopWallList, i);
            if (moveTo != -1)
            {
                currentWallPart.box2 = boxes[i][boxes[i].Length - moveTo];
                currentWallPart.box1 = boxes[i][boxes[i].Length - moveTo - 1];
            }
            else
            {
                currentWallPart.box1 = boxes[i][boxes[i].Length - 1];
                currentWallPart.box2 = null;
            }
            TopWall.Add(currentWallPart);
            currentWallPart.UpdateRealPositionSnap();

            // adding wall parts to bottomwall
            currentWallPart = Instantiate(wallPartPrefab, this.transform).GetComponent<WallPart>();
            currentWallPart.GetComponent<SpriteRenderer>().enabled = true;
                        moveTo = FindInEditorParameters(EditorBottomWallList, i);
            if (moveTo != -1)
            {
                currentWallPart.box1 = boxes[i][moveTo];
                currentWallPart.box2 = boxes[i][moveTo - 1];
            }
            else
            {
                currentWallPart.box1 = boxes[i][0];
                currentWallPart.box2 = null;
            }

            BottomWall.Add(currentWallPart);
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

    private void InitWall(ref Wall _wall, Vector2Int _direction)
    {
        _wall = gameObject.AddComponent<Wall>();
        _wall.direction = _direction;
        _wall.grid = this;
    }
}
