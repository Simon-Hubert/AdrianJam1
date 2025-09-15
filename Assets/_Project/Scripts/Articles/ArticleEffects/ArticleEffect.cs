using System;
using UnityEngine;

public class ArticleEffect : ArticleAreaEffectBase
{
    [SerializeField] private TypeOfEffect _operation;
    [SerializeField] private float _value;
    
    public override int Priority => 1;
    protected override ArticleEffectBase Copy() => throw new NotImplementedException();

    //TODO Implementation
}
