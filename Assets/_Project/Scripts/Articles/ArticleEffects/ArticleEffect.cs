using System;
using UnityEngine;

public class ArticleEffect : ArticleAreaEffectBase
{
    [SerializeField] private TypeOfEffect _operation;
    [SerializeField] private float _value;
    
    public override int Priority => 0;
    protected override ArticleEffectBase Copy() => throw new NotImplementedException();

    public override ArticleExecuteEffect GetEffect() {
        return (article, grid) =>
        {
            foreach (Article art in GridUtils.GetArticlesAt(_targetArea)(grid, article)) {
                if ((art.CurrentTags & _targetTag) != _targetTag || art == article) {
                    continue;
                }
                switch (_operation) {
                    case TypeOfEffect.ADD:
                        AddValueTo(art);
                        break;
                    case TypeOfEffect.MULTIPLY:
                        MultiplyValue(art);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        };
    }

    private void AddValueTo(Article article) {
        if (_definitive) {
            article.BaseValue += Mathf.FloorToInt(_value);
        }
        else {
            article.Value += Mathf.FloorToInt(_value);
        }
    }
    
    private void MultiplyValue(Article article) {
        if (_definitive) {
            article.BaseValue = Mathf.FloorToInt(article.BaseValue * _value);
        }
        else {
            article.Value = Mathf.FloorToInt(article.Value * _value);
        }
    }

    public void Init(AreaOfEffect aoe, Tags targetTags, bool targetSelf, TypeOfEffect operation, float value,
        bool definitive, bool def) {
        _targetArea = aoe;
        _targetTag = targetTags;
        _targetSelf = targetSelf;
        _operation = operation;
        _value = value;
        _definitive = definitive;
        isOriginal = def;
    }
}
