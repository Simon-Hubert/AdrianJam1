using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Article : MonoBehaviour
{
    [SerializeField] private ArticleShape _shape;
    [SerializeField] private ArticleGrid _grid;
    
    private (int i, int j) _gridPos;
    private bool _placed;
    private bool _dragging;

    private Coroutine _draggingRoutine;
    private Camera _camera;

    public ArticleShape Shape => _shape;
    public (int i, int j) GridPos => _gridPos;

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
        _grid.PlaceArticle(this, transform.position);
    }

    private void StartDragging() {
        _dragging = true;
        _draggingRoutine = StartCoroutine(DraggingCoroutine());
    }
    
    private void StopDragging() {
        if (_dragging) {
            _dragging = false;
            StopCoroutine(_draggingRoutine);
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
            yield return 0;
        }
    }
}
