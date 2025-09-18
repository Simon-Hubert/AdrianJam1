using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "ArticleEffectObject", menuName = "ArticleEffectObject")]
public class ArticleEffectObject : ScriptableObject
{
    private enum EffectType
    {
        RawValue,
        Effect,
        TagEffect, 
        AddEffect
    }

    [SerializeField] private EffectType _type;

    [ShowIf("AreaType")] public AreaOfEffect targetArea;
    [ShowIf("AreaType")] public Tags targetTag;
    [ShowIf("AreaType")] public bool targetSelf;
    [ShowIf("_type", EffectType.TagEffect)] public Tags tagsToRemove;
    [ShowIf("_type", EffectType.TagEffect)] public Tags tagsToAdd;
    [ShowIf("_type", EffectType.Effect)] public ArticleEffectBase.TypeOfEffect operation;
    [ShowIf("_type", EffectType.Effect)] public float floatValue;
    [ShowIf("_type", EffectType.AddEffect)]public ArticleEffectObject effect;
    [ShowIf("_type", EffectType.RawValue)] public int value;
    public bool definitive;

    public bool AreaType() {
        return _type is EffectType.Effect or EffectType.TagEffect or EffectType.AddEffect;
    }

    public void AddNewEffectTo(Article article, bool def) {
        switch (_type) {
            case EffectType.RawValue:
                article.gameObject.AddComponent<ArticleValueEffect>().Init(value, definitive, def);
                break;
            case EffectType.Effect:
                article.gameObject.AddComponent<ArticleEffect>().Init(targetArea, targetTag, targetSelf, operation, floatValue, definitive, def);
                break;
            case EffectType.TagEffect:
                article.gameObject.AddComponent<ArticleTagEffect>().Init(targetArea, targetTag, targetSelf, tagsToAdd, tagsToRemove, definitive, def);
                break;
            case EffectType.AddEffect:
                article.gameObject.AddComponent<ArticleAddEffectEffect>().Init(targetArea, targetTag, targetSelf, effect, definitive, def);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
