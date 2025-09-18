using System;
using UnityEngine;

public enum AreaOfEffect
{
    ALL = 0,
    ADJACENT = 1,
    LINE = 2,
    COLUMN =  3,
    LEFT = 4,
    RIGHT = 5,
    UP = 6,
    DOWN = 7
}

public abstract class ArticleAreaEffectBase : ArticleEffectBase
{
    [SerializeField] protected AreaOfEffect _targetArea;
    [SerializeField] protected Tags _targetTag;
    [SerializeField] protected bool _targetSelf;
}
