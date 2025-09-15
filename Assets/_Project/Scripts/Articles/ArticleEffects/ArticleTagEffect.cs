using UnityEngine;

public class ArticleTagEffect : ArticleAreaEffectBase
{
    public override int Priority => 0;
    protected override ArticleEffectBase Copy() => throw new System.NotImplementedException();

    [SerializeField] private Tags _tagsToRemove;
    [SerializeField] private Tags _tagsToAdd;
    
    //TODO Implementation
}
