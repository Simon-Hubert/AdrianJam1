using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArticlePlacer : MonoBehaviour
{
    [SerializeField] private ArticleGrid _grid;
    [SerializeField] private InputActionReference _placeInput;
    private Article _articlePrefab;
    private Article _toPlace;
    private Camera _camera;

    private void OnEnable() {
        _placeInput.action.started += PlaceArticle;
    }

    private void OnDisable() {
        _placeInput.action.started -= PlaceArticle;
    }

    private void Awake() {
        _camera = Camera.main;
    }

    private void Update() {
        if (_articlePrefab) {
            if (!_toPlace.gameObject.activeSelf) _toPlace.gameObject.SetActive(true);
            _toPlace.transform.position = _grid.SnapToGrid(_camera.ScreenToWorldPoint(Input.mousePosition));
        }
    }
    
    private void PlaceArticle(InputAction.CallbackContext obj) {
        _articlePrefab = null;
        _toPlace = null;
    }

    public void StartPlacing(Article prefab) {
        _articlePrefab = prefab;
        PrepareArticle();
    }

    private void PrepareArticle() {
        if(_toPlace) Destroy(_toPlace.gameObject);
        _toPlace = Instantiate(_articlePrefab);
        _toPlace.gameObject.SetActive(false);
    }
}
