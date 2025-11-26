using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public void OnPointerEnter(PointerEventData eventData) {
        ToolTip.instance?.Show(GetAllToolTipsInfo());
    }
    
    public void OnPointerExit(PointerEventData eventData) {
        ToolTip.instance?.Hide();
    }

    private ToolTipInfo[] GetAllToolTipsInfo() {
        ToolTipInfo[] toolTipInfos = Array.Empty<ToolTipInfo>();
        return GetComponents<IToolTipable>().Aggregate(toolTipInfos, (current, toolTipable) => current.Union(toolTipable.GetToolTipInfo()).ToArray());
    }
}
