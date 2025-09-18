using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public struct GridCell
{
    public bool Occupied;
    public Article Article;
    
    
}

[RequireComponent(typeof(Grid))]
public class ArticleGrid : MonoBehaviour
{
    private Grid _grid;
    [SerializeField] private Vector2Int _gridHalfSize;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TileBase _right;
    [SerializeField] private TileBase _wrong;

    private GridCell[,] _board;
    private List<Article> _placedArticle = new List<Article>();

    public Vector2Int GridHalfSize => _gridHalfSize;
    public Vector2Int GridSize => new Vector2Int(_board.GetLength(0), _board.GetLength(1));
    public Article[] PlacedArticles => _placedArticle.ToArray();

    private void Awake() {
        _grid = GetComponent<Grid>();
        _board = new GridCell[_gridHalfSize.x * 2, _gridHalfSize.y * 2];
    }
    
    public Vector2 SnapToGrid(Vector2 pos) {
        Vector3Int gridPos= _grid.WorldToCell(pos);
        gridPos.x = Mathf.Clamp(gridPos.x, -_gridHalfSize.x, _gridHalfSize.x-1);
        gridPos.y = Mathf.Clamp(gridPos.y, -_gridHalfSize.y, _gridHalfSize.y-1);
        return _grid.GetCellCenterWorld(gridPos);
    }

    public bool IsPlacementValid(ArticleShape shape, Vector2 pos) {
        Vector2Int posIndex = WorldToIndexPos(pos);

        if (posIndex.x < 0 || posIndex.y < 0) return false;
        if (posIndex.x + shape.Rows > _board.GetLength(0)) return false;
        if (posIndex.y + shape.Columns > _board.GetLength(1)) return false;

        for (int i = 0; i < shape.Rows; i++) {
            for (int j = 0; j < shape.Columns; j++) {
                if (_board[posIndex.x + i, posIndex.y + j].Occupied && shape[i, j]) return false;
            }
        }
        return true;
    }

    public bool PlaceArticle(Article article, Vector2 pos) {
        _tilemap.ClearAllTiles();
        if (!IsPlacementValid(article.Shape, pos)) return false;
        _placedArticle.Add(article);
        Vector2Int posIndex = WorldToIndexPos(pos);
        for (int i = 0; i < article.Shape.Rows; i++) {
            for (int j = 0; j < article.Shape.Columns; j++) {
                _board[posIndex.x + i, posIndex.y + j] = new GridCell()
                {
                    Occupied = article.Shape[i, j] || _board[posIndex.x + i, posIndex.y + j].Occupied,
                    Article = article.Shape[i, j] ? article : null
                };
            }
        }
        article.Place((posIndex.x, posIndex.y));

        return true;
    }

    public void RemoveArticle(Article article) {
        _placedArticle.Remove(article);
        for (int i = 0; i < article.Shape.Rows; i++) {
            for (int j = 0; j < article.Shape.Columns; j++) {
                if (article.Shape[i, j]) {
                    _board[article.GridPos.i + i, article.GridPos.j + j] = new GridCell()
                    {
                        Occupied = false,
                        Article = null
                    };
                }
            }
        }
        article.RemoveFromBoard();
    }

    private Vector2Int WorldToIndexPos(Vector2 pos) {
        Vector3Int gridPos= _grid.WorldToCell(pos);
        Vector2Int indexPos = (Vector2Int)gridPos + new Vector2Int(_gridHalfSize.x, -_gridHalfSize.y+1);
        indexPos.y = -indexPos.y;
        return indexPos;
    }
    
    public GridCell this[int i, int j] {
        get => _board[i,j];
    }

    public void ShowPreview(ArticleShape shape, Vector3 position) {
        _tilemap.ClearAllTiles();
        TileBase tile = IsPlacementValid(shape, position) ? _right : _wrong;
        
        for (int i = 0; i < shape.Rows; i++) {
            for (int j = 0; j < shape.Columns; j++) {
                if (shape[i, j]) {
                    _tilemap.SetTile(_grid.WorldToCell(position) + new Vector3Int(i,-j,0), tile);
                }
            }
        }
    }
    

}
