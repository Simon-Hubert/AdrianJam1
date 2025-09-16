using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectExecutor : MonoBehaviour
{
    private List<EffectManager> _effectManagers;

    public void RegisterEffectManager(EffectManager em) {
        _effectManagers.Add(em);
    }

    private void ResetManagers() {
        _effectManagers.Clear();
    }

    public void UnRegisterEffectManager(EffectManager em) {
        _effectManagers.Remove(em);
    }
    
    public void ExecuteEffects() {
        EffectHandle[] effects = _effectManagers.SelectMany(manager => manager.GetAllEffectHandles()).ToArray();
        effects = effects.OrderBy(x => x.priority).Reverse().ToArray();

        foreach (EffectHandle effect in effects) {
            effect.effect(effect.articleRef);
        }
    }
}
