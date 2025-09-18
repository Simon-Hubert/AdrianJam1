using System;
using UnityEngine;

public class ArticleTagEffect : ArticleAreaEffectBase
{
    public override int Priority => 0;
    protected override ArticleEffectBase Copy() => throw new NotImplementedException();

    [SerializeField] private Tags _tagsToRemove;
    [SerializeField] private Tags _tagsToAdd;
    
    public override ArticleExecuteEffect GetEffect() {
        return (article, grid) =>
        {
            throw new NotImplementedException();
        };
    }
}
