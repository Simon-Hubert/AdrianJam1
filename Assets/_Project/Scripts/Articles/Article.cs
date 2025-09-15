using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

[Flags]
public enum Tags
{
    Gauche = 1 << 0,
    Droite = 1 << 1,
}

public class Article : MonoBehaviour
{
    [SerializeField] private ArticleShape _shape;
    [SerializeField] private ArticleGrid _grid;
    
    private (int i, int j) _gridPos;
    private bool _placed;
    private bool _dragging;

    private Vector2 _basePos;

    private Coroutine _draggingRoutine;
    private Camera _camera;

    public ArticleShape Shape => _shape;
    public (int i, int j) GridPos => _gridPos;

    public void Init(Vector2 basePos, ArticleGrid gridRef) {
        _basePos = basePos;
        _grid = gridRef;
    }

    private void Awake() {
        _camera = Camera.main;
        Physics2D.queriesHitTriggers = true;
    }

    private void OnMouseDown() {
        StartDragging();

        if (_placed) {
            _grid.RemoveArticle(this);
        }
    }

    private void OnMouseUp() {
        StopDragging();
        transform.position = _grid.PlaceArticle(this, transform.position) ? _grid.SnapToGrid(transform.position) : _basePos;
    }

    private void StartDragging() {
        _dragging = true;
        _draggingRoutine = StartCoroutine(DraggingCoroutine());
    }
    
    private void StopDragging() {
        if (_dragging) {
            _dragging = false;
            if(_draggingRoutine != null) StopCoroutine(_draggingRoutine);
        }
    }
    
    public void Place((int i, int j) gridPosition) {
        _gridPos = gridPosition;
        _placed = true;
        Debug.Log("Placed");
    }

    public void RemoveFromBoard() {
        _gridPos = (-1, -1);
        _placed = false;
    }

    IEnumerator DraggingCoroutine() {
        while (_dragging) {
            transform.position = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);
            //TODO Juice it up !
            _grid.ShowPreview(_shape, transform.position);
            yield return 0;
        }
    }
}
