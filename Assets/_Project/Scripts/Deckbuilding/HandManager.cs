using System;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private ArticleGrid _grid;

    private Deck _deck;
    private ArticleFactory _factory;

    private void Awake() {
        _factory = new ArticleFactory(_grid);
    }

    public void Draw() {

    }
}
