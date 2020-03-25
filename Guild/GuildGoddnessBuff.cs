using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public class GuildGoddnessBuff : MonoBehaviour
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UILabel _TitleLabel;

    [SerializeField] private List<UILabel> _GuildGoddnessBuffLabelList = new List<UILabel>();

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private UILabel _BuffDurationLabel = null;

    private int _LabelCount = 0;

    private bool _IsInit = false;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    private void Update()
    {
        if (_IsInit == false)
            return;

        DateTime ServerTime = TimeManager.Instance.GetServerTime();
        DateTime GuildBuffEndTime = UserInfo.Instance.GuildBuffEndTime;

        if (GuildBuffEndTime.Ticks > ServerTime.Ticks)
        {
            gameObject.SetActive(true);

            TimeSpan ts = GuildBuffEndTime - ServerTime;
            if (ts.Hours > 0)
            {
                // 4915	{0}시간 {1}분 남음
                _BuffDurationLabel.text = string.Format(StringTableManager.GetData(4915), ts.Hours, ts.Minutes);
            }
            else if (ts.Minutes > 0)
            {
                // 4916	{0}분 남음
                _BuffDurationLabel.text = string.Format(StringTableManager.GetData(4916), ts.Minutes);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init(Vector3 position)
    {
        _IsInit = false;

        DateTime ServerTime = TimeManager.Instance.GetServerTime();
        DateTime GuildBuffEndTime = UserInfo.Instance.GuildBuffEndTime;

        if (GuildBuffEndTime.Ticks > ServerTime.Ticks)
        {
            _IsInit = true;

            gameObject.SetActive(true);
            SetData(position);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void SetData(Vector3 position)
    {
        transform.localPosition = position;

        _TitleLabel.text = string.Empty;

        for (int i = 0; i < _GuildGoddnessBuffLabelList.Count; ++i)
        {
            _GuildGoddnessBuffLabelList[i].text = string.Empty;
        }

        DATA_GUILD_TRIBUTE._enTributeEnum GuildBuffKind = UserInfo.Instance.GuildBuffKind;
        DATA_GUILD_TRIBUTE GuildTributeData = CDATA_GUILD_TRIBUTE.Get(GuildBuffKind);
        if (GuildTributeData == null)
            return;     // error

        int iGuildLevel = 0;
        int iCount = CDATA_GUILD_MAIN.GetCount();
        for (int i = 0; i < iCount; ++i)
        {
            DATA_GUILD_MAIN GuildMainData = CDATA_GUILD_MAIN.GetByIndex(i);
            if (GuildMainData == null)
                continue;

            if (GuildMainData.enTributeFree == GuildBuffKind)
            {
                iGuildLevel = GuildMainData.iGuildLv;
                break;
            }
            else if (GuildMainData.enTributeGood == GuildBuffKind)
            {
                iGuildLevel = GuildMainData.iGuildLv;
                break;
            }
            else if (GuildMainData.enTributeExtra == GuildBuffKind)
            {
                iGuildLevel = GuildMainData.iGuildLv;
                break;
            }
        }

        _TitleLabel.text = string.Format(StringTableManager.GetData(GuildTributeData.iBuffTitle), iGuildLevel);

        float Percent = 0.0f;
        if (GuildTributeData.fbuff_Gold > 0)
        {
            Percent = (GuildTributeData.fbuff_Gold * 100);
            _GuildGoddnessBuffLabelList[_LabelCount].text = string.Format(StringTableManager.GetData(6890), Percent.ToString("F2"));
            _LabelCount++;
        }

        if (GuildTributeData.fbuff_Pexp > 0)
        {
            Percent = (GuildTributeData.fbuff_Pexp * 100);
            _GuildGoddnessBuffLabelList[_LabelCount].text = string.Format(StringTableManager.GetData(6891), Percent.ToString("F2"));
            _LabelCount++;
        }

        if (GuildTributeData.fbuff_Cexp > 0)
        {
            Percent = (GuildTributeData.fbuff_Cexp * 100);
            _GuildGoddnessBuffLabelList[_LabelCount].text = string.Format(StringTableManager.GetData(6892), Percent.ToString("F2"));
            _LabelCount++;
        }

        _BuffDurationLabel = _GuildGoddnessBuffLabelList[_LabelCount];
        _BuffDurationLabel.color = new Color(107 / 255, 255 / 255, 218 / 255, 255 / 255);
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
}