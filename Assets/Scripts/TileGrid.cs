using UnityEngine;

public class TileGrid : MonoBehaviour
{

    public TileCol[] cols { get; private set; }
    public TileCell[] cells { get; private set; }

    public int size => cells.Length;
    public int width => cols.Length;
    public int height => size / width;

    private void Awake()
    {
        cols = GetComponentsInChildren<TileCol>();
        cells = GetComponentsInChildren<TileCell>();
    }

    private void Start()
    {
        for (int x = 0; x < cols.Length; x++)
        {
            for (int y = 0; y < cols[x].cells.Length; y++)
            {
                cols[x].cells[y].coordinates = new Vector2Int(x, y);
            }
        }
    }

    public TileCell GetRandomEmptyCell()
    {
        int index = Random.Range(0, cells.Length);
        int startIndex = index;

        while (cells[index].occupied)
        {
            index++;

            if (index == cells.Length)
                index = 0;

            if (index == startIndex)
                return null;
        }

        return cells[index];
    }

    public TileCell GetCell(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
            return cols[x].cells[y];
        return null;
    }

    public TileCell GetAdjacentCell(TileCell cell, Vector2Int direction)
    {
        Vector2Int coordinates = cell.coordinates;
        coordinates.x += direction.x;
        coordinates.y -= direction.y;

        return GetCell(coordinates.x, coordinates.y);
    }
}
