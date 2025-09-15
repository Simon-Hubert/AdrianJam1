using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Deck")]
public class Deck : ScriptableObject
{
    [SerializeField] private List<Article> _baseDeck;
    private List<Article> _deck;

    public void AddToBaseDeck(Article prefab) {
        _baseDeck.Add(prefab);
    }

    public void RemoveFromBaseDeck(Article prefab) {
        _baseDeck.Remove(prefab);
    }
    
    public void AddToDeckInstance(Article prefab) {
        _deck.Add(prefab);
    }

    public void RemoveFromDeckInstance(Article prefab) {
        _deck.Remove(prefab);
    }

    public void ResetDeckInstance() {
        _deck = new List<Article>(_baseDeck);
        Shuffle();
    }

    public void Shuffle() {
        _deck = _deck.OrderBy(x => Random.value).ToList();
    }
}
