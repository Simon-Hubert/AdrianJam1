using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ArticleAddEffectEffect : ArticleAreaEffectBase
{
    public override int Priority => 2;
    protected override ArticleEffectBase Copy() => throw new System.NotImplementedException();

    [SerializeField] private ArticleEffectBase _effectPrefab;
    
    public override ArticleExecuteEffect GetEffect() { 
        return article =>
        {
            throw new System.NotImplementedException();
        };
    }
}
