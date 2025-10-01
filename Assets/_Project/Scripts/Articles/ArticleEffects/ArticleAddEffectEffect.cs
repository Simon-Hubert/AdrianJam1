using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ArticleAddEffectEffect : ArticleAreaEffectBase
{
    public override int Priority => 3;
    protected override ArticleEffectBase Copy() => throw new System.NotImplementedException();

    [SerializeField] private ArticleEffectObject _effect;
    
    public override ArticleExecuteEffect GetEffect() {
        return (article, grid) =>
        {
            foreach (Article art in GridUtils.GetArticlesAt(_targetArea)(grid, article)) {
                if ((art.CurrentTags & _targetTag) == 0 || art == article) {
                    continue;
                }
                _effect.AddNewEffectTo(art, _definitive);
            }
        };
    }

    public void Init(AreaOfEffect aoe, Tags targetTags, bool targetSelf, ArticleEffectObject effect, bool definitive, bool def) {
        _targetArea = aoe;
        _targetTag = targetTags;
        _targetSelf = targetSelf;
        _effect = effect;
        _definitive = definitive;
        isOriginal = def;
    }
}
