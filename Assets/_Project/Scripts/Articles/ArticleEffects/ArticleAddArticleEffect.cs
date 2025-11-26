using UnityEngine;

public class ArticleAddArticleEffect : ArticleEffectBase
{
    [SerializeField] private Article _articlePrefabToAdd;
    [SerializeField] private bool _remove;

    public override int Priority => 0;
    
    protected override ArticleEffectBase Copy() => throw new System.NotImplementedException();

    public override ArticleExecuteEffect GetEffect() {
        return (article, grid) =>
        {
            if (!DeckManager.instance) {
                return;
            }

            if (_remove) {
                DeckManager.instance.RemoveArticle(_articlePrefabToAdd);
            }
            else {
                DeckManager.instance.AddArticle(_articlePrefabToAdd);
            }
        };
    }

    public override ToolTipInfo[] GetToolTipInfo() {
        ToolTipInfo info = new ToolTipInfo();

        info.Text = _remove ? $"Enleve l'article <b>{_articlePrefabToAdd.Name}</b> du Deck" : $"Ajoute l'article <b>{_articlePrefabToAdd.Name}</b> dans le Deck";
        
        return new[] { info };
    }
}
