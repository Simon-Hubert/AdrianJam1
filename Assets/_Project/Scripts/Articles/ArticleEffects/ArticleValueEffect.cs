using UnityEngine;

public class ArticleValueEffect : ArticleEffectBase
{
    public override int Priority => 0;
    protected override ArticleEffectBase Copy() => throw new System.NotImplementedException();

    [SerializeField] private int _value;
    //TODO Implementation
}
