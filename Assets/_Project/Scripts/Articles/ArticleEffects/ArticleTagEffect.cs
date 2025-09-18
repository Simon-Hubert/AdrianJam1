using System;
using UnityEngine;

public class ArticleTagEffect : ArticleAreaEffectBase
{
    public override int Priority => 2;
    protected override ArticleEffectBase Copy() => throw new NotImplementedException();

    [SerializeField] private Tags _tagsToRemove;
    [SerializeField] private Tags _tagsToAdd;
    
    public override ArticleExecuteEffect GetEffect() {
        return (article, grid) =>
        {
            foreach (Article art in GridUtils.GetArticlesAt(_targetArea)(grid, article)) {
                if ((art.CurrentTags & _targetTag) != _targetTag || art == article) {
                    continue;
                }

                if (_definitive) {
                    art.BaseTags |= _tagsToAdd;
                    art.BaseTags &= ~_tagsToRemove;
                    art.CurrentTags |= _tagsToAdd;
                    art.CurrentTags &= ~_tagsToRemove;
                }
                else {
                    art.CurrentTags |= _tagsToAdd;
                    art.CurrentTags &= ~_tagsToRemove;
                }
                
            }
        };
    }

    public void Init(AreaOfEffect aoe, Tags targetTags, bool targetSelf, Tags tagsToAdd, Tags tagsToRemove, bool definitive, bool def) {
        _targetArea = aoe;
        _targetTag = targetTags;
        _targetSelf = targetSelf;
        _tagsToAdd = tagsToAdd;
        _tagsToRemove = tagsToRemove;
        _definitive = definitive;
        isOriginal = def;
    }
}
