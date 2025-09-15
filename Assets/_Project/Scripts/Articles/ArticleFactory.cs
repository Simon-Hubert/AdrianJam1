using UnityEngine;

public class ArticleFactory
{
    private readonly ArticleGrid _grid;
    
    public ArticleFactory(ArticleGrid grid) {
        _grid = grid;
    }

    public void SpawnArticle(Article prefab, Vector2 basePos) {
        if (prefab == null) return;
        Article art = GameObject.Instantiate(prefab, basePos, Quaternion.identity);
        art.Init(basePos, _grid);
    }
}
