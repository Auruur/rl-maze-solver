using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private MazeCell _mazeCellPrefab;

    [SerializeField]
    private int _mazeWidth;

    [SerializeField]
    private int _mazeDepth;

    [SerializeField]
    private int _seed;

    [SerializeField]
    private bool _useSeed;

    private MazeCell[,] _mazeGrid;

   void Start()
    {
        if (_useSeed)
        {
            Random.InitState(_seed);
        }
        else
        {
            int randomSeed = Random.Range(1, 1000000);
            Random.InitState(randomSeed);

            Debug.Log(randomSeed);
        }

        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];

        int ID = 0;

        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                MazeCell cell = Instantiate(_mazeCellPrefab);
                cell.transform.parent = _mazeCellPrefab.transform.parent;
                cell.transform.localPosition = new Vector3(x+0.5f, 0, z+0.5f);
                
                cell.gameObject.name = "Cell_" + new Vector3(x+0.5f, 0, z+0.5f).ToString();

                //spawn del bonus
                int bonus = Random.Range(1, 100);
                if(bonus < 90){
                    cell.ClearBonus();
                }

                _mazeGrid[x, z] = cell;
                ID++;
            }
        }

        GenerateMaze(null, _mazeGrid[0, 0]);

        CreateRandomExit();
    }

    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)currentCell.transform.localPosition.x;
        int z = (int)currentCell.transform.localPosition.z;

        if (x + 1 < _mazeWidth)
        {
            var cellToRight = _mazeGrid[x + 1, z];
            
            if (cellToRight.IsVisited == false)
            {
                yield return cellToRight;
            }
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = _mazeGrid[x - 1, z];

            if (cellToLeft.IsVisited == false)
            {
                yield return cellToLeft;
            }
        }

        if (z + 1 < _mazeDepth)
        {
            var cellToFront = _mazeGrid[x, z + 1];

            if (cellToFront.IsVisited == false)
            {
                yield return cellToFront;
            }
        }

        if (z - 1 >= 0)
        {
            var cellToBack = _mazeGrid[x, z - 1];

            if (cellToBack.IsVisited == false)
            {
                yield return cellToBack;
            }
        }
    }

    
private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
{
    if (previousCell == null)
    {
        return;
    }

    if (previousCell.transform.localPosition.x < currentCell.transform.localPosition.x)
    {
        previousCell.ClearRightWall();
        currentCell.ClearLeftWall();
        return;
    }

    if (previousCell.transform.localPosition.x > currentCell.transform.localPosition.x)
    {
        previousCell.ClearLeftWall();
        currentCell.ClearRightWall();
        return;
    }

    if (previousCell.transform.localPosition.z < currentCell.transform.localPosition.z)
    {
        previousCell.ClearFrontWall();
        currentCell.ClearBackWall();
        return;
    }

    if (previousCell.transform.localPosition.z > currentCell.transform.localPosition.z)
    {
        previousCell.ClearBackWall();
        currentCell.ClearFrontWall();
        return;
    }
}

    private void CreateRandomExit()
    {
        // Scegli un lato a caso
        int side = Random.Range(0, 4);
        int x, z;

        switch (side)
        {
            // Lato superiore
            case 0:
                x = Random.Range(0, _mazeWidth);
                _mazeGrid[x, _mazeDepth - 1].ClearFrontWall();
                break;

            // Lato destro
            case 1:
                z = Random.Range(0, _mazeDepth);
                _mazeGrid[_mazeWidth - 1, z].ClearRightWall();
                break;

            // Lato inferiore
            case 2:
                x = Random.Range(0, _mazeWidth);
                _mazeGrid[x, 0].ClearBackWall();
                break;

            // Lato sinistro
            case 3:
                z = Random.Range(0, _mazeDepth);
                _mazeGrid[0, z].ClearLeftWall();
                break;
        }
    }

}
