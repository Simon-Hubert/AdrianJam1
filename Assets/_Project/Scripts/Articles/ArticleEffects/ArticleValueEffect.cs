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
                article.Value += _value;
            }
            else {
                article.Value += _value;
            }
            
        };
    }
    public override ToolTipInfo[] GetToolTipInfo() {
        ToolTipInfo toolTip = new ToolTipInfo();
        toolTip.Text = $"L'article gagne {_value} électeurs.";
        return new[] { toolTip };
    }

    public void Init(int value, bool definitive, bool def) {
        _value = value;
        _definitive = definitive;
        isOriginal = def;
    }
}
