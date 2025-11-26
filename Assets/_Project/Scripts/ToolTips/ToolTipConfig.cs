using System;
using UnityEngine;

public class ToolTipConfig : ScriptableObject
{
    public static string GetTargetingString(AreaOfEffect aoe, Tags tags) {
        return aoe switch
        {
            AreaOfEffect.ALL => "Tous les articles " + GetTagString(tags),
            AreaOfEffect.ADJACENT => "Tous les articles " + GetTagString(tags) + " adjacents ",
            AreaOfEffect.LINE => "Tous les articles " + GetTagString(tags) + " de la ligne ",
            AreaOfEffect.COLUMN => "Tous les articles " + GetTagString(tags) + " de la colonne ",
            AreaOfEffect.LEFT => "Les articles " + GetTagString(tags) + " adjacents à gauche ",
            AreaOfEffect.RIGHT => "Les articles " + GetTagString(tags) + " adjacents à droite ",
            AreaOfEffect.UP => "Les articles " + GetTagString(tags) + " adjacents au dessus ",
            AreaOfEffect.DOWN => "Les articles " + GetTagString(tags) + " adjacents en dessous ",
            _ => throw new ArgumentOutOfRangeException(nameof(aoe), aoe, null)
        };
    }

    public static string GetTagString(Tags tags) {
        string s = "";
        foreach (Enum tag in Enum.GetValues(typeof(Tags))) {
            if (tags.HasFlag(tag)) {
                if(tag.Equals(Tags.Aucun)) continue;
                s += GetTagStyle((Tags)tag);
                s += ", ";
            }
        }

        if (s != "") {
            s = s.Trim(',', ' ');
        }

        s += " ";

        return s;
    }

    public static string GetTagStyle(Tags tag) {
        return $"<style=\"{tag.ToString()}\">{tag.ToString()}</style>";
    }
}
