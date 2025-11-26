using System;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private Deck _currentDeck;

    public static DeckManager instance;

    private void Awake() {
        if (instance != null) {
            Debug.Log("DeckManagerAlreadyExists");
            Destroy(this);
        }
        instance = this;
        _currentDeck.ResetDeckInstance();
    }

    public Article Draw() {
        return _currentDeck.Draw();
    }

    public void AddArticle(Article prefab) {
        _currentDeck.AddToDeckInstance(prefab);
    }
    
    public void RemoveArticle(Article prefab) {
        _currentDeck.RemoveFromDeckInstance(prefab);
    }
}
