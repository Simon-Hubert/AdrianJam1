using System;
using Unity.Collections;
using UnityEngine;

public abstract class ArticleEffectBase : MonoBehaviour
{
   
    protected enum TypeOfEffect
    {
        ADD = 0,
        MULTIPLY = 1,
    }
    
    public abstract int Priority {
        get;
    }
    
    protected abstract ArticleEffectBase Copy();
}
