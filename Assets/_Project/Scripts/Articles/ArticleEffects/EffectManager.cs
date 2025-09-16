using System;
using UnityEngine;

public delegate void ArticleExecuteEffect(Article article);

public struct EffectHandle
{
    public Article articleRef;
    public int priority;
    public ArticleExecuteEffect effect;
}

public class EffectManager : MonoBehaviour
{
    private Article _owner;

    private void Awake() {
        _owner = GetComponent<Article>();
    }


    public EffectHandle[] GetAllEffectHandles() {
        ArticleEffectBase[] effects = GetComponents<ArticleEffectBase>();
        EffectHandle[] effectHandles = new EffectHandle[effects.Length];

        for (int i = 0; i < effects.Length; i++) {
            effectHandles[i].effect = effects[i].GetEffect();
            effectHandles[i].articleRef = _owner;
            effectHandles[i].priority = effects[i].Priority;
        }
        
        return effectHandles;
    }
}
