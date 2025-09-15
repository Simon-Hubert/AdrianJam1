using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ArticleAddEffectEffect : ArticleAreaEffectBase
{
    public override int Priority => 2;
    protected override ArticleEffectBase Copy() => throw new System.NotImplementedException();

    [SerializeField] private ArticleEffectBase _effectPrefab;
    
    //TODO Implementation
}
