using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectExecutor
{
    private List<EffectManager> _effectManagers = new List<EffectManager>();

    public void RegisterEffectManager(EffectManager em) {
        _effectManagers.Add(em);
    }

    public void ResetManagers() {
        _effectManagers.Clear();
    }

    public void UnRegisterEffectManager(EffectManager em) {
        _effectManagers.Remove(em);
    }
    
    public void ExecuteEffects(ArticleGrid grid) {
        EffectHandle[] effects = _effectManagers.SelectMany(manager => manager.GetAllEffectHandles()).ToArray();
        effects = effects.OrderBy(x => x.priority).Reverse().ToArray();

        foreach (EffectHandle effect in effects) {
            effect.effect(effect.articleRef, grid);
        }
    }
}
