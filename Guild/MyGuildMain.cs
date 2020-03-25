using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public enum enMyGuildMain_ButtonType
{
    None = -1,
    MyGuild,
    GuildSearch,
}

public class MyGuildMain : UIWindow
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UILabel _TitleLabel;

    [SerializeField] private MenuButton m_GuildMemberButton;

    [SerializeField] private MenuButton _GuildSearchButton;

    [SerializeField] private GameObject m_CloseButton;

    [SerializeField] private GameObject _HelpButton;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private _stGuildRecommendListAck _GuildRecommendList = null;

    private CGuildDetail m_GuildDetailInfo = null;

    private MyGuild _MyGuild = null;

    private GuildList _GuildList = null;                                  // 길드 검색 리스트.
    public GuildList guildList { get { return _GuildList; } }

    private GuildModify _guildModifyWindow = null;

    private GuildDelegation m_GuildDelegationPopupWindow = null;

    private GuildGoddnessConfirm _GuildGoddnessConfirmPopupWindow = null;

    private SimpleHelpTip _SimpleHelpTip = null;

    private int m_iGuildDelegateCountGold = 0;

    private enGuildInfo_WindowType m_enGuildInfoWindowType = enGuildInfo_WindowType.MyGuild;

    private enMyGuildMain_ButtonType _enMyGuildMainButtonType = enMyGuildMain_ButtonType.None;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    public override void Init()
    {
        if (_TitleLabel != null) _TitleLabel.text = StringTableManager.GetData(3494);

        _GuildSearchButton.SetLabel(StringTableManager.GetData(6750));   // 6750	길드 검색

        if (_MyGuild == null)
        {
            _MyGuild = UIResourceMgr.CreatePrefab<MyGuild>(BUNDLELIST.PREFABS_UI_GUILD, transform, "MyGuild");
            _MyGuild.Init(this);
        }

        if (_GuildList == null)
        {
            _GuildList = UIResourceMgr.CreatePrefab<GuildList>(BUNDLELIST.PREFABS_UI_GUILD, transform, "GuildList");
            _GuildList.Init();
        }

        if (m_GuildDelegationPopupWindow == null)
        {
            m_GuildDelegationPopupWindow = UIResourceMgr.CreatePrefab<GuildDelegation>(BUNDLELIST.PREFABS_UI_GUILD, transform, "GuildDelegation");
            m_GuildDelegationPopupWindow.Init();
            m_GuildDelegationPopupWindow.CloseUI();
        }

        m_iGuildDelegateCountGold = (int)CDATA_FIXED_CONSTANTS.Get(DATA_FIXED_CONSTANTS._enConstant.Guild_Delegate_Count_Gold).Value;

        SetMenu(enMyGuildMain_ButtonType.MyGuild);
    }

    public override void Clear()
    {
    }

    protected override void Awake()
    {
        if (m_CloseButton != null) UIEventListener.Get(m_CloseButton).onClick = OnClickBack;

        if (m_GuildMemberButton != null) UIEventListener.Get(m_GuildMemberButton.gameObject).onClick = OnClickMyGuildMenu;
        if (_GuildSearchButton != null) UIEventListener.Get(_GuildSearchButton.gameObject).onClick = OnClickGuildSearchMenu;

        if (_HelpButton != null) UIEventListener.Get(_HelpButton).onPress = OnHelpTooltip;
    }

    protected override void OnEnable()
    {
//#if GMTOOLSHOP
//        UIControlManager.instance.SetChangeWealth(WEB_SHOP_CATEGORY._enWebUI_Category._enWebUI_Category_Max);
//#else
//        UIControlManager.instance.SetChangeWealth(DATA_SHOP_NEW_CATEGORY._enCategory._enCategory_Max);
//#endif

    }

    protected override void Start()
    {
    }

    protected override void OnDisable()
    {
    }

    protected override void Update()
    {
    }

    protected override void OnDestroy()
    {
        if (m_CloseButton != null) UIEventListener.Get(m_CloseButton).onClick -= OnClickBack;

        if (m_GuildMemberButton != null) UIEventListener.Get(m_GuildMemberButton.gameObject).onClick -= OnClickMyGuildMenu;
        if (_GuildSearchButton != null) UIEventListener.Get(_GuildSearchButton.gameObject).onClick -= OnClickGuildSearchMenu;

        if (_HelpButton != null) UIEventListener.Get(_HelpButton).onPress -= OnHelpTooltip;
    }

    public override void OpenUI()
    {
        base.OpenUI();

#if GMTOOLSHOP
        UIControlManager.instance.SetChangeWealth(WEB_SHOP_CATEGORY._enWebUI_Category._enWebUI_Category_Max);
#else
        UIControlManager.instance.SetChangeWealth(DATA_SHOP_NEW_CATEGORY._enCategory._enCategory_Max);
#endif
    }

    public override void CloseUI()
    {
        base.CloseUI();
    }

    public override bool OnClickBack()
    {
        base.OnClickBack();

        // 길드알람이 가입이 되었는지 알람왔을때 (길드원만 체크)
        if(UserInfo.Instance.otherNew.GuildAlram == _enGuildAlram.eGuildNewMark_JoinOk)
        {
            // 길드원인지 체크.
            if(UserInfo.Instance.CharGuildState == _enGuildMemberState.eGuildMemberState_Member ||
                UserInfo.Instance.CharGuildState == _enGuildMemberState.eGuildMemberState_AbleMember)
            {
                UserInfo.Instance.otherNew.GuildAlram = _enGuildAlram.eGuildNewMark_None;
            }
        }
        else if (UserInfo.Instance.otherNew.GuildAlram == _enGuildAlram.eGuildNewMark_DelegateCaptain)
        {
            UserInfo.Instance.otherNew.GuildAlram = _enGuildAlram.eGuildNewMark_None;
        }

        return _MyGuild.OnClickBack();

        //if (_MyGuild.OnClickBack())
        //    return false;

        //return true;
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void GuildDetailInfoAck(CGuildDetail kGuildDetailInfo)
    {
        m_GuildDetailInfo = kGuildDetailInfo;

        byte kCurrMemberCount = m_GuildDetailInfo.kCurrMemberCount;
        byte kMaxMemberCount = m_GuildDetailInfo.kMaxMemberCount;

        string szLabeltext = string.Format(StringTableManager.GetData(6254), kCurrMemberCount, kMaxMemberCount);
        m_GuildMemberButton.SetLabel(szLabeltext);

        _MyGuild.GuildDetailInfoAck(m_GuildDetailInfo);
    }

    public void SetGuildSearch(_stGuildRecommendListAck stAck)
    {
        _GuildRecommendList = stAck;

        _GuildList.CreateJoinRequestGuildList(stAck);
        _GuildList.CreateRecommendGuildList(stAck);
    }

    public void SetAttendenceReward(ulong attendenceRewardCount)
    {
        _MyGuild.SetAttendenceReward(attendenceRewardCount);
    }

    private void SetMenu(enMyGuildMain_ButtonType type)
    {
        if (_enMyGuildMainButtonType == type)
            return;

        _enMyGuildMainButtonType = type;

        if (type == enMyGuildMain_ButtonType.MyGuild)
        {
            m_GuildMemberButton.state = ButtonState.On;
            _GuildSearchButton.state = ButtonState.Off;

            _MyGuild.OpenUI();
            _GuildList.gameObject.SetActive(false);
        }
        else if(type == enMyGuildMain_ButtonType.GuildSearch)
        {
            m_GuildMemberButton.state = ButtonState.Off;
            _GuildSearchButton.state = ButtonState.On;

            _MyGuild.CloseUI();
            _GuildList.gameObject.SetActive(true);
        }
        else
        {
#if DEBUG_LOG
            Debug.Log("error - None Type");
#endif
        }
    }

    /// <summary>
    /// 길드 가입요청 수락& 거절 패킷 받음.
    /// </summary>
    public void GuildJoinRequestAnswer(_stGuildJoinRequestAnswerAck stAck)
    {
        // 가입요청이 수락되면 내 길드에 길드원으로 넣고
        // 길드관리에도 길드원으로 변경.
        // 길드원수 갱신.
        
        string str = string.Empty;
        CGuildMember DestMember = null;

        for (int i = 0; i < m_GuildDetailInfo.vMembers.Count;)
        {
            CGuildMember member = m_GuildDetailInfo.vMembers[i];
            if (member == null)
                continue;

            if (member.kCharNo == stAck.kDestCharNo)
            {
                if (stAck.kAnswer == _enGuildJoinRequestAnswer.eGuildJoinRequestAnswer_YES)
                {
                    // {플레이어 이름}\n님이 길드에 가입되었습니다.(스트링 ID : 6283)
                    str = string.Format(StringTableManager.GetData(6283), member.kCharName);

                    if(member.kMemberState != _enGuildMemberState.eGuildMemberState_Member)
                    {
                        member.kMemberState = _enGuildMemberState.eGuildMemberState_Member;
                        DestMember = member;
                        m_GuildDetailInfo.kCurrMemberCount++;
                    }
                }
                else if (stAck.kAnswer == _enGuildJoinRequestAnswer.eGuildJoinRequestAnswer_NO)
                {
                    //  {플레이어 이름}\n님의 길드 가입을 거절하였습니다.(스트링 ID : 6279)
                    str = string.Format(StringTableManager.GetData(6279), member.kCharName);
                    m_GuildDetailInfo.vMembers.Remove(member);
                    //m_GuildDetailInfo.kCurrMemberCount--;
                    continue;
                }
                //break;
            }

            ++i;
        }

        // 6254 길드원 ({0} / {1})
        string szLabeltext = string.Format(StringTableManager.GetData(6254), m_GuildDetailInfo.kCurrMemberCount, m_GuildDetailInfo.kMaxMemberCount);
        m_GuildMemberButton.SetLabel(szLabeltext);

        _MyGuild.GuildJoinRequestAnswer(stAck, DestMember, m_GuildDetailInfo);
        _guildModifyWindow.GuildJoinRequestAnswer(stAck, DestMember, m_GuildDetailInfo);

        SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(4300), str);
    }

    /// <summary>
    /// 길드 추방 패킷 받음(길드장 전용).
    /// </summary>
    public void GuildOrderLeave(_stGuildOrderLeaveAck stAck)
    {
        int iCount = m_GuildDetailInfo.vMembers.Count;
        for (int i = 0; i < iCount; ++i)
        {
            CGuildMember member = m_GuildDetailInfo.vMembers[i];
            if (member == null)
                continue;

            if(member.kCharNo == stAck.kDestCharNo)
            {
                // {플레이어 이름}\n님을 길드에서 추방하였습니다.(스트링 ID : 6287)
                string str = string.Format(StringTableManager.GetData(6287), member.kCharName);
                SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(4300), str);

                m_GuildDetailInfo.vMembers.Remove(member);
                break;
            }
        }

        string szLabeltext = string.Format(StringTableManager.GetData(6254), m_GuildDetailInfo.vMembers.Count, m_GuildDetailInfo.kMaxMemberCount);
        m_GuildMemberButton.SetLabel(szLabeltext);

        _MyGuild.GuildOrderLeave(stAck);
        _guildModifyWindow.GuildOrderLeave(stAck);
    }

    /// <summary>
    /// 길드 마크 변경 패킷 받음.
    /// </summary>
    /// <param name="stAck"></param>
    /// <returns></returns>
    public void GuildChangeMark(_stGuildChangeMarkAck stAck)
    {
        _MyGuild.GuildChangeMark(stAck.kNewGuildMark);
        _guildModifyWindow.GuildChangeMark(stAck.kNewGuildMark);
    }

    /// <summary>
    /// 길드장 위임 패킷 받음.
    /// </summary>
    /// <param name="stAck"></param>
    public void GuildOrderCaptain(_stGuildOrderCaptainAck stAck)
    {
        // 길드관리 창이 꺼지면 관리창에서 바뀐 정보를 다시 받는다.
        //_guildModifyWindow.CloseUI();
        UIControlManager.instance.RemoveWindow(enUIType.GUILDMODIFY);

        m_GuildDelegationPopupWindow.CloseUI();

        _vGuildMembers vGuildMembers = m_GuildDetailInfo.vMembers;
        for (int i = 0; i < vGuildMembers.Count; ++i)
        {
            CGuildMember member = vGuildMembers[i];
            if (member == null)
                continue;

            // 길드원 정보에서 내 정보 변경.
            if (member.kCharNo == UserInfo.Instance.CharNo)
            {
                member.kMemberState = stAck.kUserMemberState;
            }

            // 위임한 길드원.
            if (member.kCharNo == stAck.kDestCharNo)
            {
                member.kMemberState = _enGuildMemberState.eGuildMemberState_Captain;
            }
        }

        _MyGuild.GuildDetailInfoAck(m_GuildDetailInfo);
    }

    /// <summary>
    /// 길드 탈퇴 패킷 받음.
    /// </summary>
    /// <param name="stAck"></param>
    public void GuildLeaveAck(_stGuildLeaveAck stAck)
    {
        string str = string.Format(StringTableManager.GetData(6258), m_GuildDetailInfo.kGuildName);
        SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(4300), str);
    }

    /// <summary>
    /// 길드 해체 패킷 받음.
    /// </summary>
    /// <param name="stAck"></param>
    public void GuildDeleteAck(_stGuildDeleteAck stAck)
    {
        UIControlManager.instance.RemoveWindow(enUIType.GUILDMODIFY);
        UIControlManager.instance.RemoveWindow(enUIType.MYGUILDMAIN);

        UIControlManager.instance.ShowWindow<_stGuildDeleteAck>(enUIType.MAINMENU, stAck);
    }

    /// <summary>
    /// 부길드장 임명 패킷 받음.
    /// </summary>
    /// <param name="stAck"></param>
    public void GuildOrderSubCaptain(_stGuildOrderSubCaptainAck stAck)
    {
        CGuildMember member = null;
        int iCount = m_GuildDetailInfo.vMembers.Count;
        for(int i = 0; i < iCount; ++i)
        {
            member = m_GuildDetailInfo.vMembers[i];
            if (member == null)
                continue;

            if(member.kCharNo == stAck.kDestCharNo)
            {
                member.kMemberState = stAck.kDestChangeState;
                break;
            }
        }

        if(member.kMemberState == _enGuildMemberState.eGuildMemberState_SubCaptain)
        {
            // {길드원 이름}\n님을 부길드장으로 임명하였습니다. (스트링 ID : 6300)
            string str = string.Format(StringTableManager.GetData(6300), member.kCharName);
            SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(4300), str);
        }
        else if(member.kMemberState == _enGuildMemberState.eGuildMemberState_AbleMember)
        {
            // {플레이어 이름}\n님이 부길드장에서 해임되었습니다.(스트링 ID : 6303)
            string str = string.Format(StringTableManager.GetData(6303), member.kCharName);
            SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(4300), str);
        }

        _MyGuild.SetMyGuildMember(m_GuildDetailInfo.kGuildKey, m_GuildDetailInfo.vMembers);
        _guildModifyWindow.SetMyGuildMember(m_GuildDetailInfo.kGuildKey, m_GuildDetailInfo.vMembers);
        m_GuildDelegationPopupWindow.SetDelegationMember(enGuildInfo_WindowType.GuildSubCaptain_Appointment, m_GuildDetailInfo);
    }

    /// <summary>
    /// 내 길드 위임상태 변경 패킷 받음.
    /// </summary>
    /// <param name="stAck"></param>
    public void GuildMyMemberStateChange(_stGuildMemberStateChangeAck stAck)
    {
        _MyGuild.GuildMyMemberStateChange(stAck);
    }

    /// <summary>
    /// 길드 관리 자유가입&승인가입 변경 패킷 받음.
    /// 길드 관리 공지사항&소개글 변경 패킷 받음.
    /// </summary>
    /// <param name="stAck"></param>
    public void GuildChangeDescAndJoinMethod(_stGuildChangeDescAck stAck)
    {
        _MyGuild.GuildChangeDescAndJoinMethod(stAck);
        _guildModifyWindow.GuildChangeDescAndJoinMethod(stAck);
    }

    /// <summary>
    /// 무료, 고급, 최고급 공물버튼 눌렀을때 받는 패킷.
    /// </summary>
    /// <param name="stAck"></param>
    public void RecvGuildAttendance(_stGuildAttendanceAck stAck)
    {
        if (_GuildGoddnessConfirmPopupWindow == null)
        {
            _GuildGoddnessConfirmPopupWindow = UIResourceMgr.CreatePrefab<GuildGoddnessConfirm>(BUNDLELIST.PREFABS_UI_GUILD, transform, "GuildGoddnessConfirm");
            _GuildGoddnessConfirmPopupWindow.Init(stAck, _MyGuild);
        }

        _GuildGoddnessConfirmPopupWindow.OpenUI();

        int iCount = m_GuildDetailInfo.vMembers.Count;
        for (int i = 0; i < iCount; ++i)
        {
            CGuildMember member = m_GuildDetailInfo.vMembers[i];
            if (member == null)
                continue;

            if(member.kCharNo == UserInfo.Instance.CharNo)
            {
                member.kBuffKind = stAck.kGuildTributeKind;
                member.kUserTributeExp = stAck.kResultUserTirbuteExp;
                break;
            }
        }

        m_GuildDetailInfo.kAttendanceCount = stAck.kResultAttendanceCount;
        m_GuildDetailInfo.kGuildExp = stAck.kResultGuildTributeExp;
        m_GuildDetailInfo.kGuildLevel = stAck.kResultGuildLevel;

        GuildDetailInfoAck(m_GuildDetailInfo);

        SetAttendenceReward(stAck.kRecvGuildAttendanceKey);
    }

    public void ReopenGuildRaid()
    {
        _MyGuild.OnReopenGuildRaid(null);
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    /// <summary>
    /// 길드관리 버튼.
    /// </summary>
    /// <param name="go"></param>
    public void OnGuildModify(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        if (_guildModifyWindow == null)
        {
            _guildModifyWindow = UIResourceMgr.CreatePrefab<GuildModify>(BUNDLELIST.PREFABS_UI_GUILD, transform, "GuildModify");
            _guildModifyWindow.Init(this, m_GuildDetailInfo);
            UIControlManager.instance.AddWindow(enUIType.GUILDMODIFY, _guildModifyWindow);
        }

        _guildModifyWindow.OpenUI();
    }

    /// <summary>
    /// 길드장 위임 버튼.
    /// </summary>
    /// <param name="go"></param>
    public void OnGuildDelegation(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        if(UserInfo.Instance.Gold < (ulong)m_iGuildDelegateCountGold)
        {
            SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(4300), StringTableManager.GetData(6296));
            return;
        }

        m_enGuildInfoWindowType = enGuildInfo_WindowType.GuildCaptain_Delegation;
        string str = string.Format(StringTableManager.GetData(6291), m_iGuildDelegateCountGold);
        SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.YesNo, StringTableManager.GetData(4300), str, OpenGuildDelegationPopup);
    }

    public void OnGuildOrderSubCaptain(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        m_enGuildInfoWindowType = enGuildInfo_WindowType.GuildSubCaptain_Appointment;
        OpenGuildDelegationPopup(enSystemMessageFlag.YES);
    }

    /// <summary>
    /// 길드장 위임창 열기.
    /// </summary>
    /// <param name="state"></param>
    private void OpenGuildDelegationPopup(enSystemMessageFlag state)
    {
        if (state == enSystemMessageFlag.NO)
            return;

        m_GuildDelegationPopupWindow.SetDelegationMember(m_enGuildInfoWindowType, m_GuildDetailInfo);
        m_GuildDelegationPopupWindow.OpenUI();
    }

    private void OnClickMyGuildMenu(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        SetMenu(enMyGuildMain_ButtonType.MyGuild);
    }

    private void OnClickGuildSearchMenu(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        SetMenu(enMyGuildMain_ButtonType.GuildSearch);
    }

    private void OnHelpTooltip(GameObject go, bool state)
    {
        if (state == true)
        {
            if (_SimpleHelpTip == null)
            {
                _SimpleHelpTip = UIResourceMgr.CreatePrefab<SimpleHelpTip>(BUNDLELIST.PREFABS_UI_MAINMENU, _HelpButton.transform, "SimpleHelpTip");
                _SimpleHelpTip.Init(16);
            }

            _SimpleHelpTip.OpenUI();
        }
        else
        {
            _SimpleHelpTip.CloseUI();
        }
    }
}
