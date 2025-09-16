using System;
using UnityEngine;

public class ArticleEffect : ArticleAreaEffectBase
{
    [SerializeField] private TypeOfEffect _operation;
    [SerializeField] private int _value;
    
    public override int Priority => 0;
    protected override ArticleEffectBase Copy() => throw new NotImplementedException();

    public override ArticleExecuteEffect GetEffect() {
        return article =>
        {
            article.Value += _value;
        };
    }
    
}
