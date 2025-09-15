using System;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private ArticleGrid _grid;
    [SerializeField] private DeckManager _deck;
    
    private ArticleFactory _factory;

    private void Awake() {
        _factory = new ArticleFactory(_grid);
    }

    private void Start() {
        Draw();
    }

    public void Draw() {
        foreach (Transform point in _spawnPoints) {
            _factory.SpawnArticle(_deck.Draw(), point.position);
        }
    }
}
