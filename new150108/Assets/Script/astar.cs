using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;


public class astar : MonoBehaviour
{
    // singleton
    private static astar inst;
    public static astar Inst
    {
        get { return inst; }
        private set { }
    }

    //int maxSearchRounds = 0;
    private Node[,] NodeMap = null;

    public float HighestPoint = 100F;
    public float LowestPoint = -50F;
    public bool CheckFullTileSize = false;

    // this position is left down position of x,y coordinate map
    public Vector2 MapMinPos;
    // this position is right up position of x,y coordinate map
    public Vector2 MapMaxPos;

    public bool DrawMapInEditor = true;
    public float MaxFalldownHeight;
    public float ClimbLimit;

    // tile size
    float tilesize = 1.0f;

    // disallow object tag list
    public List<string> DisallowTags;
    // Ignore object tag list
    public List<string> IgnoreTags;

    // find a*
    //private Node[] OpenList;
    //private Node[] CloseList;
    private Node StartNode;
    private Node EndNode;
    private Node CurrentNode;
    //private List<NodeSearch> sortedOpenList = new List<NodeSearch>();

    void Awake()
    {
        inst = this;
    }

    // Use this for initialization
    void Start()
    {
        Inst.CreateMap();
        //CreateMap();
    }

    // Update is called once per frame
    void Update()
    {
        DrawMapLines();
    }


    // create map node
    private void CreateMap()
    {
        int StartX = (int)MapMinPos.x;
        int StartZ = (int)MapMinPos.y;
        int EndX = (int)MapMaxPos.x;
        int EndZ = (int)MapMaxPos.y;

        // Set node interval
        int WidthInterval = (int)((EndX - StartX) / tilesize);
        int HeightInterval = (int)((EndZ - StartZ) / tilesize);

        NodeMap = new Node[WidthInterval, HeightInterval];
        int size = WidthInterval * HeightInterval;
        SetListSize(size);

        // fill up node
        for (int i = 0; i < HeightInterval; i++)
        {
            for (int j = 0; j < WidthInterval; j++)
            {
                float x = StartX + (j * tilesize) + (tilesize / 2);
                float y = StartZ + (i * tilesize) + (tilesize / 2);
                int Id = (i * WidthInterval) + j;

                float dist = Mathf.Abs(HighestPoint) + Mathf.Abs(LowestPoint);
                RaycastHit[] rayHit;

                if (CheckFullTileSize)
                {
                    rayHit = Physics.SphereCastAll(new Vector3(x, HighestPoint, y), tilesize / 2, Vector3.down, dist);
                }
                else
                {
                    rayHit = Physics.SphereCastAll(new Vector3(x, HighestPoint, y), tilesize / 16, Vector3.down, dist);
                }

                bool free = true;
                float maxY = -Mathf.Infinity;

                foreach (RaycastHit h in rayHit)
                {
                    if (DisallowTags.Contains(h.transform.tag))
                    {
                        if (h.point.y > maxY)
                        {
                            // It is a disallowed walking tile, we can't make node
                            NodeMap[j, i] = new Node(j, i, 0, Id, x, y, false); // this node non walkable
                            free = false;
                            // renewal maxY
                            maxY = h.point.y;
                        }
                    }
                    else if (IgnoreTags.Contains(h.transform.tag))
                    {
                        // do nothing
                    }
                    else
                    {
                        if (h.point.y > maxY)
                        {
                            // It is a disallowed walking tile, we can't make node
                            NodeMap[j, i] = new Node(j, i, 0, Id, x, y, true); // this node walkable
                            free = false;
                            // renewal maxY
                            maxY = h.point.y;
                        }
                    }
                }

                // We hit nothing set tile to false
                if (free == true)
                {
                    NodeMap[j, i] = new Node(j, i, 0, Id, x, y, false); // Non walkable tile
                }
            }
        }
    }

    void DrawMapLines()
    {
        if (DrawMapInEditor == true && NodeMap != null)
        {
            for (int i = 0; i < NodeMap.GetLength(1); i++)
            {
                for (int j = 0; j < NodeMap.GetLength(0); j++)
                {
                    if (!NodeMap[j, i].walkable)
                        continue;

                    for (int y = i - 1; y < i + 2; y++)
                    {
                        for (int x = j - 1; x < j + 2; x++)
                        {
                            if (y < 0 || x < 0 || y >= NodeMap.GetLength(1) || x >= NodeMap.GetLength(0))
                                continue;

                            if (!NodeMap[x, y].walkable)
                                continue;

                            if (NodeMap[j, i].yCoord > NodeMap[x, y].yCoord && Mathf.Abs(NodeMap[j, i].yCoord - NodeMap[x, y].yCoord) > MaxFalldownHeight)
                                continue;

                            if (NodeMap[j, i].yCoord < NodeMap[x, y].yCoord && Mathf.Abs(NodeMap[x, y].yCoord - NodeMap[j, i].yCoord) > ClimbLimit)
                                continue;

                            Vector3 start = new Vector3(NodeMap[j, i].xCoord, NodeMap[j, i].yCoord + 0.1f, NodeMap[j, i].zCoord);
                            Vector3 end = new Vector3(NodeMap[x, y].xCoord, NodeMap[x, y].yCoord + 0.1f, NodeMap[x, y].zCoord);

                            UnityEngine.Debug.DrawLine(start, end, Color.green);
                        }
                    }
                }
            }
        }
    }

    private void SetListSize(int size)
    {
        //OpenList = new Node[size];
        //CloseList = new Node[size];
    }
}
