using System;
using UnityEngine;

public abstract class ArticleAreaEffectBase : ArticleEffectBase
{
    [Flags]
    protected enum AreaOfEffect
    {
        ALL = 1 << 0,
        ADJACENT = 1 << 1,
        LINE = 1 << 2,
        COLUMN = 1 << 3,
        LEFT = 1 << 4,
        RIGHT = 1 << 5,
        UP = 1 << 6,
        DOWN = 1 << 7
    }
    
    [SerializeField] protected AreaOfEffect _targetArea;
    [SerializeField] private Tags _targetTag;
    [SerializeField] private bool _targetSelf;
}
