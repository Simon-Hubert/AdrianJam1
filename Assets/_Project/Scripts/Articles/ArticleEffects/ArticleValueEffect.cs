using UnityEngine;

public class ArticleValueEffect : ArticleEffectBase
{
    public override int Priority => 1;
    protected override ArticleEffectBase Copy() => throw new System.NotImplementedException();
    
    [SerializeField] private int _value;
    
    public override ArticleExecuteEffect GetEffect() {
        return (article, grid) =>
        {
            if (_definitive) {
                article.BaseValue += _value;
            }
            else {
                article.Value += _value;
            }
            
        };
    }
    
    public void Init(int value, bool definitive, bool def) {
        _value = value;
        _definitive = definitive;
        isOriginal = def;
    }
}
