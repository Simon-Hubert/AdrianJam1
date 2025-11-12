using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ToolTip tt;
    
    private void Awake() {
        tt = FindFirstObjectByType<ToolTip>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        tt?.Show(GetAllToolTipsInfo());
    }
    
    public void OnPointerExit(PointerEventData eventData) {
        tt?.Hide();

    }

    private ToolTipInfo[] GetAllToolTipsInfo() {
        ToolTipInfo[] toolTipInfos = Array.Empty<ToolTipInfo>();
        return GetComponents<IToolTipable>().Aggregate(toolTipInfos, (current, toolTipable) => current.Union(toolTipable.GetToolTipInfo()).ToArray());
    }
}
