using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

[Flags]
public enum Tags
{
    Aucun = 0,
    Gauche = 1 << 0,
    Droite = 1 << 1,
    Centre = 1 << 2,
    Immigration = 1 << 3,
    Social = 1 << 4,
    Gouvernement = 1 << 5,
    Agronomie = 1 << 6,

}

public class Article : MonoBehaviour, IToolTipable
{
    [SerializeField] private ArticleShape _shape;
    [SerializeField] private ArticleGrid _grid;
    [SerializeField] private Tags _baseTags;
    [SerializeField] private int _baseValue;

    [SerializeField] private string _name;
    [SerializeField] private string _description;

    private (int i, int j) _gridPos;
    private bool _placed;
    private bool _dragging;

    private Vector2 _basePos;

    private Coroutine _draggingRoutine;
    private Camera _camera;

    [ShowNonSerializedField] private int _value;
    [ShowNonSerializedField] private Tags _tags;


    public ArticleShape Shape => _shape;
    public (int i, int j) GridPos => _gridPos;

    public int Value {
        get => _value;
        set => _value = value;
    }

    public int BaseValue {
        get => _baseValue;
        set => _baseValue = value;
    }

    public Tags BaseTags {
        get => _baseTags;
        set => _baseTags = value;
    }
    public Tags CurrentTags {
        get => _tags;
        set => _tags = value;
    }
    

    public void Init(Vector2 basePos, ArticleGrid gridRef) {
        _basePos = basePos;
        _grid = gridRef;
        _tags = Tags.Aucun;
        _tags |= _baseTags;
        _value = _baseValue;
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
    
    public ToolTipInfo[] GetToolTipInfo() {
        ToolTipInfo tt = new ToolTipInfo
        {
            Title = _name,
            Text = _description,
            Tags = _tags,
            OrderInToolTip = -1
        };
        
        List<ToolTipInfo> infos = new List<ToolTipInfo>() { tt };

        
        if (_value != 0) {
            ToolTipInfo value = new ToolTipInfo();
            value.Text = _value < 0 ? "-" : "+";
            value.Text += $"{_value} électeurs";
            infos.Add(value);
        }

        return infos.ToArray();
    }
}
