using System;
using UnityEngine;

public struct ToolTipInfo : IEquatable<ToolTipInfo>
{
    public string Title;
    public string Text;
    public Tags Tags;
    public int OrderInToolTip;
    
    public bool Equals(ToolTipInfo other) {
        return Text == other.Text;
    }
    public override bool Equals(object obj) {
        return obj is ToolTipInfo other && Equals(other);
    }
    public override int GetHashCode() {
        return (Text != null ? Text.GetHashCode() : 0);
    }
}

public interface IToolTipable
{
    public ToolTipInfo[] GetToolTipInfo();
}
