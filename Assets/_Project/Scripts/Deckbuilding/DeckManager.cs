using System;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField] private Deck _currentDeck;

    private void Awake() {
        _currentDeck.ResetDeckInstance();
    }

    public Article Draw() {
        return _currentDeck.Draw();
    }
}
