using UnityEngine;
     public class Node {
    public bool isWalkable;
    public Vector3Int cellPosition;
    public int gridX, gridY;
   
    public bool utilizzabile; 
    public int gCost;
    public int hCost;
    public Node parent;

    public int fCost {
        get {
            return gCost + hCost;
        }
    }

    public Node(bool _isWalkable, Vector3Int _cellPosition, int _gridX, int _gridY) {
        isWalkable = _isWalkable;
        cellPosition = _cellPosition;
        gridX = _gridX;
        gridY = _gridY;
        utilizzabile =true;
    }
}