using UnityEngine;

[System.Serializable]
public class BrickGridData
{
    public int X => x;
    [SerializeField] private int x;

    public int Y => y;
    [SerializeField] private int y;

    public int BrickIndex => brickIndex;
    [SerializeField] private int brickIndex;

    public BrickGridData(int x, int y, int brickIndex)
    {
        this.x = x;
        this.y = y;
        this.brickIndex = brickIndex;
    }
}
