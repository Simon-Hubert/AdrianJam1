using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class ArticleEffectBase : MonoBehaviour
{
    public enum TypeOfEffect
    {
        ADD = 0,
        MULTIPLY = 1,
    }

    [SerializeField] protected bool _definitive;
    
    public abstract int Priority {
        get;
    }
    
    protected bool isOriginal = true;
    protected abstract ArticleEffectBase Copy();
    public abstract ArticleExecuteEffect GetEffect();
    
    public bool IsOriginal => isOriginal;
}
