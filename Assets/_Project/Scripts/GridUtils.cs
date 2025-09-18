using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public static class GridUtils
{
    public delegate Article[] GridArticleGetter(ArticleGrid grid, Article article);

    private static GridArticleGetter[] getters = {
        GetAllArticles,
        GetAdjacentArticles,
        GetLineArticles,
        GetColumnArticles,
        GetLeftArticles,
        GetRightArticles,
        GetUpArticles,
        GetDownArticles
    };

    
    public static GridArticleGetter GetArticlesAt(AreaOfEffect effect) {
        return getters[(int)effect];
    }

    private static Article[] GetAllArticles(ArticleGrid grid, Article article) {
        return grid.PlacedArticles.Except(new [] {article}).ToArray();
    }

    private static Article[] GetAdjacentArticles(ArticleGrid grid, Article article) {
        HashSet<Article> arts = new HashSet<Article>();
        for (int i = 0; i < article.Shape.Rows; i++) {
            for (int j = 0; j < article.Shape.Columns; j++) {
                if (!article.Shape[i, j]) {
                    continue;
                }
                arts.UnionWith(GetAdjacentArticles(grid, new Vector2Int(article.GridPos.i + i, article.GridPos.j +j)));
            }
        }
        return arts.Except(new [] {article}).ToArray();
    }

    private static IEnumerable<Article> GetAdjacentArticles(ArticleGrid grid, Vector2Int pos) {
        HashSet<Article> arts = new HashSet<Article>();

        bool left = pos.x == 0;
        bool right = pos.x >= grid.GridSize.x - 1;
        bool top = pos.y == 0;
        bool bottom =  pos.y >= grid.GridSize.y - 1;
        
        if(!right && grid[pos.x+1, pos.y  ].Occupied) arts.Add(grid[pos.x+1, pos.y  ].Article);
        if(!right && !bottom && grid[pos.x+1, pos.y+1].Occupied) arts.Add(grid[pos.x+1, pos.y+1].Article);
        if(!right && !top && grid[pos.x+1, pos.y-1].Occupied) arts.Add(grid[pos.x+1, pos.y-1].Article);
        if(!left && grid[pos.x-1, pos.y  ].Occupied) arts.Add(grid[pos.x-1, pos.y  ].Article);
        if(!left && !bottom && grid[pos.x-1, pos.y+1].Occupied) arts.Add(grid[pos.x-1, pos.y+1].Article);
        if(!left && !top && grid[pos.x-1, pos.y-1].Occupied) arts.Add(grid[pos.x-1, pos.y-1].Article);
        if(!bottom && grid[pos.x  , pos.y+1].Occupied) arts.Add(grid[pos.x  , pos.y+1].Article);
        if(!top && grid[pos.x  , pos.y-1].Occupied) arts.Add(grid[pos.x  , pos.y-1].Article);
        
        return arts;
    }
    
    private static Article[] GetLineArticles(ArticleGrid grid, Article article) {
        HashSet<Article> arts = new HashSet<Article>();
        for (int i = 0; i < article.Shape.Rows; i++) {
            for (int j = 0; j < article.Shape.Columns; j++) {
                if (!article.Shape[i, j]) {
                    continue;
                }
                arts.UnionWith(GetLineArticles(grid, new Vector2Int(article.GridPos.i + i, article.GridPos.j +j)));
            }
        }
        return arts.Except(new [] {article}).ToArray();
    }
    
    private static IEnumerable<Article> GetLineArticles(ArticleGrid grid, Vector2Int pos) {
        HashSet<Article> arts = new HashSet<Article>();
        for (int i = 0; i < grid.GridSize.x; i++) {
            if (grid[i,pos.y].Occupied) {
                arts.Add(grid[i,pos.y].Article);
            }
        }
        return arts;
    }
    
    private static Article[] GetColumnArticles(ArticleGrid grid, Article article) {
        HashSet<Article> arts = new HashSet<Article>();
        for (int i = 0; i < article.Shape.Rows; i++) {
            for (int j = 0; j < article.Shape.Columns; j++) {
                if (!article.Shape[i, j]) {
                    continue;
                }
                arts.UnionWith(GetColumnArticles(grid, new Vector2Int(article.GridPos.i + i, article.GridPos.j +j)));
            }
        }
        return arts.Except(new [] {article}).ToArray();
    }
    
    private static IEnumerable<Article> GetColumnArticles(ArticleGrid grid, Vector2Int pos) {
        HashSet<Article> arts = new HashSet<Article>();
        for (int j = 0; j < grid.GridSize.x; j++) {
            if (grid[pos.x,j].Occupied) {
                arts.Add(grid[pos.x,j].Article);
            }
        }
        return arts;
    }

    private static Article[] GetLeftArticles(ArticleGrid grid, Article article) {
        HashSet<Article> arts = new HashSet<Article>();
        for (int i = 0; i < article.Shape.Rows; i++) {
            for (int j = 0; j < article.Shape.Columns; j++) {
                if (!article.Shape[i, j]) {
                    continue;
                }
                arts.UnionWith(GetLeftArticles(grid, new Vector2Int(article.GridPos.i + i, article.GridPos.j +j)));
            }
        }
        return arts.Except(new [] {article}).ToArray();
    }
    
    private static IEnumerable<Article> GetLeftArticles(ArticleGrid grid, Vector2Int pos) {
        
        if (pos.x > grid.GridSize.x - 1) {
            return new Article[]{};
        }
        
        HashSet<Article> arts = new HashSet<Article>();
        if(grid[pos.x+1, pos.y  ].Occupied) arts.Add(grid[pos.x+1, pos.y  ].Article);
        return arts;
    }
    
    private static Article[] GetRightArticles(ArticleGrid grid, Article article) {
        HashSet<Article> arts = new HashSet<Article>();
        for (int i = 0; i < article.Shape.Rows; i++) {
            for (int j = 0; j < article.Shape.Columns; j++) {
                if (!article.Shape[i, j]) {
                    continue;
                }
                arts.UnionWith(GetRightArticles(grid, new Vector2Int(article.GridPos.i + i, article.GridPos.j +j)));
            }
        }
        return arts.Except(new [] {article}).ToArray();
    }
    
    private static IEnumerable<Article> GetRightArticles(ArticleGrid grid, Vector2Int pos) {
        
        if (pos.x == 0) {
            return new Article[]{};
        }
        
        HashSet<Article> arts = new HashSet<Article>();
        if(grid[pos.x-1, pos.y  ].Occupied) arts.Add(grid[pos.x-1, pos.y  ].Article);
        return arts;
    }
    
    private static Article[] GetUpArticles(ArticleGrid grid, Article article) {
        HashSet<Article> arts = new HashSet<Article>();
        for (int i = 0; i < article.Shape.Rows; i++) {
            for (int j = 0; j < article.Shape.Columns; j++) {
                if (!article.Shape[i, j]) {
                    continue;
                }
                arts.UnionWith(GetUpArticles(grid, new Vector2Int(article.GridPos.i + i, article.GridPos.j +j)));
            }
        }
        return arts.Except(new [] {article}).ToArray();
    }
    
    private static IEnumerable<Article> GetUpArticles(ArticleGrid grid, Vector2Int pos) {
        if (pos.y == 0) {
            return new Article[]{};
        }
        HashSet<Article> arts = new HashSet<Article>();
        if(grid[pos.x, pos.y-1].Occupied) arts.Add(grid[pos.x-1, pos.y  ].Article);
        return arts;
    }
    
    private static Article[] GetDownArticles(ArticleGrid grid, Article article) {
        HashSet<Article> arts = new HashSet<Article>();
        for (int i = 0; i < article.Shape.Rows; i++) {
            for (int j = 0; j < article.Shape.Columns; j++) {
                if (!article.Shape[i, j]) {
                    continue;
                }
                arts.UnionWith(GetDownArticles(grid, new Vector2Int(article.GridPos.i + i, article.GridPos.j +j)));
            }
        }
        return arts.Except(new [] {article}).ToArray();
    }
    
    private static IEnumerable<Article> GetDownArticles(ArticleGrid grid, Vector2Int pos) {
        if (pos.y > grid.GridSize.y - 1) {
            return new Article[]{};
        }
        HashSet<Article> arts = new HashSet<Article>();
        if(grid[pos.x, pos.y-1].Occupied) arts.Add(grid[pos.x-1, pos.y  ].Article);
        return arts;
    }
}
