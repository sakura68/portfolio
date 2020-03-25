using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildGoddnessText : MonoBehaviour
{
    public enum enGuildGoddnessText_Type
    {
        GuildExp = 1,
        GuildContribution,      // 기여도
        BuffGold,
        //BuffUserExpPercent,
        BuffCreatureExpPercent,
        //BuffDuration,
        BuffRewardKey,
    }

    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UILabel _Label;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private enGuildGoddnessText_Type _TextType;
    public enGuildGoddnessText_Type TextType { get { return _TextType; } }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void SetText(string text, enGuildGoddnessText_Type type)
    {
        _Label.text = text;
        _TextType = type;
    }
}