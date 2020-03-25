using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public class GuildGoddnessConfirm : UIWindowPopup
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private GameObject _GoddnessFree;
    [SerializeField] private GameObject _GoddnessGood;
    [SerializeField] private GameObject _GoddnessExtra;

    [SerializeField] private UILabel _GuildTributeKindLabel;

    [SerializeField] private List<UILabel> _GuildGoddnessBuffLabelList = new List<UILabel>();

    [SerializeField] private UILabel _ContextLabel;                     // 여신이 우편으로 선물을 보냈다.

    [SerializeField] private GameObject _CloseAreaButton;
    [SerializeField] private UILabel _CloseAreaButtonLabel;             // 아무곳이나 터치하면 창이 닫힌다.

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private MyGuild _MyGuild = null;

    private float _TweenDuration = 0.0f;
    private float _Time = 0.0f;

    private bool _IsTweenEnd = false;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    protected override void Awake()
    {
        if (_CloseAreaButton != null) UIEventListener.Get(_CloseAreaButton).onClick = OnClose;

        TweenAlpha tweenAlpha = GetComponent<TweenAlpha>();
        if (tweenAlpha != null)
        {
            _TweenDuration = tweenAlpha.duration;
        }
    }

    protected override void OnDestroy()
    {
        if (_CloseAreaButton != null) UIEventListener.Get(_CloseAreaButton).onClick -= OnClose;
    }

    protected override void Update()
    {
        _Time += Time.deltaTime;
        if(_Time >= _TweenDuration)
        {
            _IsTweenEnd = true;
        }
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init(_stGuildAttendanceAck stAck, MyGuild myGuild)
    {
        _MyGuild = myGuild;

        for(int i = 0; i < _GuildGoddnessBuffLabelList.Count; ++i)
        {
            _GuildGoddnessBuffLabelList[i].text = string.Empty;
        }

        _GuildTributeKindLabel.text = string.Empty;

        // 6904    여신의 신전에서 우편함으로 봉헌 감사 선물을 전달했습니다.
        _ContextLabel.text = StringTableManager.GetData(6904);

        // 6311	아무 곳이나 터치하시면 이전 화면으로 돌아갑니다.
        _CloseAreaButtonLabel.text = StringTableManager.GetData(6311);

        DATA_GUILD_MAIN GuildMainData = CDATA_GUILD_MAIN.Get(stAck.kResultGuildLevel);
        if (GuildMainData == null)
            return;     // error

        DATA_GUILD_TRIBUTE._enTributeEnum TributeEnum = stAck.kGuildTributeKind;
        DATA_GUILD_TRIBUTE GuildTributeData = CDATA_GUILD_TRIBUTE.Get(TributeEnum);
        if (GuildTributeData == null)
            return;     // error

        // 여신 이미지 셋팅.
        {
            _GoddnessFree.SetActive(false);
            _GoddnessGood.SetActive(false);
            _GoddnessExtra.SetActive(false);

            if (GuildTributeData.enTributeEnum.ToString().IndexOf("free", System.StringComparison.OrdinalIgnoreCase) != -1)
            {
                _GoddnessFree.SetActive(true);
            }
            else if (GuildTributeData.enTributeEnum.ToString().IndexOf("good", System.StringComparison.OrdinalIgnoreCase) != -1)
            {
                _GoddnessGood.SetActive(true);
            }
            else if (GuildTributeData.enTributeEnum.ToString().IndexOf("extra", System.StringComparison.OrdinalIgnoreCase) != -1)
            {
                _GoddnessExtra.SetActive(true);
            }
        }

        _GuildTributeKindLabel.text = string.Format(StringTableManager.GetData(GuildTributeData.iBuffTitle), GuildMainData.iGuildLv);

        int iLabelCount = 0;
        float Percent = 0.0f;
        if (GuildTributeData.fbuff_Gold > 0)
        {
            Percent = (GuildTributeData.fbuff_Gold * 100);
            _GuildGoddnessBuffLabelList[iLabelCount].text = string.Format(StringTableManager.GetData(6890), Percent.ToString("F2"));
            iLabelCount++;
        }

        if (GuildTributeData.fbuff_Pexp > 0)
        {
            Percent = (GuildTributeData.fbuff_Pexp * 100);
            _GuildGoddnessBuffLabelList[iLabelCount].text = string.Format(StringTableManager.GetData(6891), Percent.ToString("F2"));
            iLabelCount++;
        }

        if (GuildTributeData.fbuff_Cexp > 0)
        {
            Percent = (GuildTributeData.fbuff_Cexp * 100);
            _GuildGoddnessBuffLabelList[iLabelCount].text = string.Format(StringTableManager.GetData(6892), Percent.ToString("F2"));
            iLabelCount++;
        }
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    private void OnClose(GameObject go)
    {
        if(_IsTweenEnd == true)
        {
            base.OnClickBack(go);

            _MyGuild.RecvGuildAttendance();
        }
    }
}