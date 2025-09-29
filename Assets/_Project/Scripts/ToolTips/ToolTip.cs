using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    [SerializeField] private GameObject _titleTipsPrefab;
    [SerializeField] private GameObject _tipPrefab;
    [SerializeField] private GameObject _tagTipsPrefab;
    
    private List<GameObject> _toolTips = new List<GameObject>();

    public void Show(ToolTipInfo[] infos) {
        foreach (ToolTipInfo info in infos) {
            AddToolTip(info);
        }
        gameObject.SetActive(true);
    }

    public void Hide() {
        foreach (GameObject toolTip in _toolTips) {
            Destroy(toolTip);
        }
        _toolTips.Clear();
        gameObject.SetActive(false);
    }
    
    private void AddToolTip(ToolTipInfo info) {
        if (!string.IsNullOrEmpty(info.Title)) {
            TMP_Text text = Instantiate(_titleTipsPrefab, transform).GetComponent<TMP_Text>();
            text.SetText(info.Title);
            _toolTips.Add(text.gameObject);
        }
        if (!string.IsNullOrEmpty(info.Text)) {
            TMP_Text text = Instantiate(_tipPrefab, transform).GetComponent<TMP_Text>();
            text.SetText(info.Text);
            _toolTips.Add(text.gameObject);
        }
        
        if (!info.Tags.Equals(Tags.Aucun)) {
            TMP_Text text = Instantiate(_tagTipsPrefab, transform).GetComponent<TMP_Text>();
            text.SetText(ToolTipConfig.GetTagString(info.Tags));
            _toolTips.Add(text.gameObject);
        }
    }
}
