using UnityEngine;

[RequireComponent(typeof(Grid))]
public class ArticleGrid : MonoBehaviour
{
    private Grid _grid;
    [SerializeField] private Vector2Int _gridHalfSize;

    private void Awake() {
        _grid = GetComponent<Grid>();
    }

    public Vector2 SnapToGrid(Vector2 pos) {
        Vector3Int gridPos= _grid.WorldToCell(pos);
        gridPos.x = Mathf.Clamp(gridPos.x, -_gridHalfSize.x, _gridHalfSize.x);
        gridPos.y = Mathf.Clamp(gridPos.y, -_gridHalfSize.y, _gridHalfSize.y);
        return _grid.GetCellCenterWorld(gridPos);
    }
}
