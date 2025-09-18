using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ArticleGrid _grid;
    private EffectExecutor _effectExecutor = new EffectExecutor();
    
    [Button]
    public void EndTurn() {
        foreach (Article article in _grid.PlacedArticles) {
            article.Value = article.BaseValue;
            article.CurrentTags = article.BaseTags;
            EffectManager em = article.gameObject.GetComponent<EffectManager>();
            em.ClearAddedEffects();
            _effectExecutor.RegisterEffectManager(em);
        }
        
        _effectExecutor.ExecuteEffects(_grid);
        _effectExecutor.ResetManagers();

        int total = _grid.PlacedArticles.Sum(article => article.Value);
        
        foreach (Article article in _grid.PlacedArticles) {
            article.gameObject.GetComponent<EffectManager>().ClearAddedEffects();
        }
        
        Debug.Log(total);
    }
}
