using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public enum enGuildInfo_WindowType
{
    MyGuild,
    GuildModify,                    // 길드 관리.
    GuildCaptain_Delegation,        // 길드장 위임.
    GuildSubCaptain_Appointment,    // 부길드장 임명.
}


public class GuildInformationItem : MonoBehaviour
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UISprite m_BgMeSprite;

    [SerializeField] private UI2DSprite m_CreatureSprite;
    [SerializeField] private UISprite m_CaptainCrown;
    [SerializeField] private UISprite m_SubCaptainCrown;

    [SerializeField] private GameObject _GoddnessTributeFreeObj;
    [SerializeField] private UILabel _GoddnessTributeFreeLabel;

    [SerializeField] private GameObject _GoddnessTributeGoodObj;
    [SerializeField] private GameObject _GoddnessTributeExtraObj;

    [SerializeField] private UISprite m_VipRankSprite;
    [SerializeField] private UILabel m_VipRankLabel;

    [SerializeField] private UILabel m_LevelLabel;
    [SerializeField] private UILabel m_NameLabel;
    [SerializeField] private UILabel m_LastConnectLabel;

    [SerializeField] private UILabel _ContributionTitleLabel;       // "기여도" 표시 라벨.
    [SerializeField] private UILabel _ContributionPointLabel;       // 기여도 점수 라벨.

    [SerializeField] private MenuButton m_HomeButton;               // 방문버튼.
    [SerializeField] private GameObject m_RejectButton;             // 길드가입 거절버튼(길드장, 부길드장 전용).
    [SerializeField] private GameObject m_ApprovalButton;           // 길드가입 수락버튼(길드장, 부길드장 전용).
    [SerializeField] private GameObject m_DeleteButton;             // 길드원 추방 버튼(길드장, 부길드장 전용).

    [SerializeField] private GameObject m_DismissButton;            // 부길드장 해임 버튼(길드장 전용).
    [SerializeField] private UILabel m_DismissButtonLabel;

    //[SerializeField] private CustomButtonUI m_AttendanceButton;     // 출석버튼.

    [SerializeField] private GameObject m_DelegationButton;         // 길드장 위임버튼(길드장 전용).
    [SerializeField] private UILabel m_DelegationButtonLabel;

    [SerializeField] private GameObject m_AppointButton;            // 부길드장 임명버튼(길드장 전용).
    [SerializeField] private UILabel m_AppointButtonLabel;

    [SerializeField] private GameObject _MemberLogOffObj;

    [SerializeField] private MenuButton _attendanceReward;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private ulong m_kGuildKey = 0;

    private CGuildMember m_MemberInfo = null;
    public CGuildMember MemberInfo { get { return m_MemberInfo; } }

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    public void Awake()
    {
        //if (m_AttendanceButton != null) UIEventListener.Get(m_AttendanceButton.gameObject).onClick = OnAttendance;
        if (m_HomeButton != null) UIEventListener.Get(m_HomeButton.gameObject).onClick = OnHome;
        if (m_RejectButton != null) UIEventListener.Get(m_RejectButton).onClick = OnReject;
        if (m_ApprovalButton != null) UIEventListener.Get(m_ApprovalButton).onClick = OnApproval;
        if (m_DeleteButton != null) UIEventListener.Get(m_DeleteButton).onClick = OnDelete;
        if (m_DismissButton != null) UIEventListener.Get(m_DismissButton).onClick = OnDismiss;
        if (m_DelegationButton != null) UIEventListener.Get(m_DelegationButton).onClick = OnDelegation;
        if (m_AppointButton != null) UIEventListener.Get(m_AppointButton).onClick = OnAppoint;
        
    }

    public void OnDestroy()
    {
        //if (m_AttendanceButton != null) UIEventListener.Get(m_AttendanceButton.gameObject).onClick -= OnAttendance;
        if (m_HomeButton != null) UIEventListener.Get(m_HomeButton.gameObject).onClick -= OnHome;
        if (m_RejectButton != null) UIEventListener.Get(m_RejectButton).onClick -= OnReject;
        if (m_ApprovalButton != null) UIEventListener.Get(m_ApprovalButton).onClick -= OnApproval;
        if (m_DeleteButton != null) UIEventListener.Get(m_DeleteButton).onClick -= OnDelete;
        if (m_DismissButton != null) UIEventListener.Get(m_DismissButton).onClick -= OnDismiss;
        if (m_DelegationButton != null) UIEventListener.Get(m_DelegationButton).onClick -= OnDelegation;
        if (m_AppointButton != null) UIEventListener.Get(m_AppointButton).onClick -= OnAppoint;
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init(enGuildInfo_WindowType type, ulong kGuildKey, CGuildMember member)
    {
        m_DelegationButtonLabel.text = StringTableManager.GetData(6294);        // 6294	길드장 위임
        m_AppointButtonLabel.text = StringTableManager.GetData(6298);        // 6298	부길드장 임명

        m_BgMeSprite.gameObject.SetActive(false);
        m_CaptainCrown.gameObject.SetActive(false);
        m_SubCaptainCrown.gameObject.SetActive(false);

        m_kGuildKey = kGuildKey;
        m_MemberInfo = member;

        m_NameLabel.text = m_MemberInfo.kCharName;

        m_LevelLabel.text = string.Format("{0} {1}", StringTableManager.GetData(12), m_MemberInfo.kCharLevel);  // 12번 텍스트 LV

        DATA_VIP VipData = CDATA_VIP.Get(m_MemberInfo.kCharVIPLevel);
        if (VipData != null)
        {
            m_VipRankSprite.spriteName = VipData.szGradeImg;
        }
        m_VipRankLabel.text = string.Format(StringTableManager.GetData(4984), (int)m_MemberInfo.kCharVIPLevel);

        DATA_CREATURE_NEWVER CreatureData = CDATA_CREATURE_NEWVER.Get(m_MemberInfo.kLeaderCreatureID);
        if (CreatureData != null)
        {
            int iCreatureTID = CDATA_CREATURE_NEWVER.Get(m_MemberInfo.kLeaderCreatureID).m_iCreatureTID;
            DATA_CREATURE_NEWVER pCreatureData = UtilFunc.GetCreatureDataByTID(iCreatureTID);
            m_CreatureSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_CREATUREHEAD, pCreatureData.m_szIcon);
        }

        // 공물형태가 무엇인지?
        {
            // 6856	무료 공물
            _GoddnessTributeFreeLabel.text = StringTableManager.GetData(6856);
            _GoddnessTributeFreeObj.SetActive(false);
            _GoddnessTributeGoodObj.SetActive(false);
            _GoddnessTributeExtraObj.SetActive(false);

            _attendanceReward.state = ButtonState.Off;

            DateTime MemberAttendanceTime = m_MemberInfo.kGuildAttendanceTime.GetDateTime();
            if (MemberAttendanceTime.Date >= TimeManager.Instance.GetServerTime().Date && 
                (m_MemberInfo.kBuffKind != DATA_GUILD_TRIBUTE._enTributeEnum.None && m_MemberInfo.kBuffKind != DATA_GUILD_TRIBUTE._enTributeEnum._enTributeEnum_Max))
            {
                _attendanceReward.state = ButtonState.On;

                DATA_GUILD_TRIBUTE._enTributeEnum GuildBuffKind = m_MemberInfo.kBuffKind;
                DATA_GUILD_TRIBUTE GuildTributeData = CDATA_GUILD_TRIBUTE.Get(GuildBuffKind);
                if (GuildTributeData == null)
                    return;     // error

                _attendanceReward.SetOnLabel(string.Format(StringTableManager.GetData(8679), GuildTributeData.iKeyAmount));

                int iCount = CDATA_GUILD_MAIN.GetCount();
                for (int i = 0; i < iCount; ++i)
                {
                    DATA_GUILD_MAIN GuildMainData = CDATA_GUILD_MAIN.GetByIndex(i);
                    if (GuildMainData == null)
                        continue;

                    if (GuildMainData.enTributeFree == GuildBuffKind)
                    {
                        _GoddnessTributeFreeObj.SetActive(true);
                        break;
                    }
                    else if (GuildMainData.enTributeGood == GuildBuffKind)
                    {
                        _GoddnessTributeGoodObj.SetActive(true);
                        break;
                    }
                    else if (GuildMainData.enTributeExtra == GuildBuffKind)
                    {
                        _GoddnessTributeExtraObj.SetActive(true);
                        break;
                    }
                }
            }            
        }

        if (type == enGuildInfo_WindowType.MyGuild)
        {
            _ContributionTitleLabel.gameObject.SetActive(true);
            _ContributionPointLabel.gameObject.SetActive(true);
            _attendanceReward.gameObject.SetActive(true);

            // 6905    기여도
            _ContributionTitleLabel.text = StringTableManager.GetData(6905);
            _ContributionPointLabel.text = m_MemberInfo.kUserTributeExp.ToString();     // 기여도 점수.
        }
        else
        {
            _ContributionTitleLabel.gameObject.SetActive(false);
            _ContributionPointLabel.gameObject.SetActive(false);
            _attendanceReward.gameObject.SetActive(false);
        }

        if (m_MemberInfo.kAccessState == _enGuildMemberAccessState.eAccess_Yes)
        {
            _MemberLogOffObj.SetActive(false);

            // 3473	접속중
            m_LastConnectLabel.text = string.Format(StringTableManager.GetData(3473));
        }
        else
        {
            if (UserInfo.Instance.CharNo == m_MemberInfo.kCharNo)
            {
                _MemberLogOffObj.SetActive(false);

                // 3473	접속중
                m_LastConnectLabel.text = string.Format(StringTableManager.GetData(3473));
            }
            else
            {
                if (m_kGuildKey == UserInfo.Instance.GuildKey)
                {
                    // 내가 속한 길드일때만 길드원의 접속여부를 확인한다.
                    _MemberLogOffObj.SetActive(true);
                }
                else
                {
                    _MemberLogOffObj.SetActive(false);
                }

                System.TimeSpan timeresult = TimeManager.Instance.GetServerTime() - m_MemberInfo.kLastLogonTime.GetDateTime();
                if (timeresult.Days > 0)
                {
                    // 6589    {0} 일 전.
                    m_LastConnectLabel.text = string.Format(StringTableManager.GetData(6589), timeresult.Days);
                }
                else if (timeresult.Hours > 0)
                {
                    // 6590    {0} 시간 전.
                    m_LastConnectLabel.text = string.Format(StringTableManager.GetData(6590), timeresult.Hours);
                }
                else
                {
                    // 6591    {0} 시간 미만.
                    m_LastConnectLabel.text = string.Format(StringTableManager.GetData(6591), "1");
                }
            }
        }

        if (m_MemberInfo.kMemberState == _enGuildMemberState.eGuildMemberState_Captain)
        {
            m_CaptainCrown.gameObject.SetActive(true);
        }
        else if(m_MemberInfo.kMemberState == _enGuildMemberState.eGuildMemberState_SubCaptain || m_MemberInfo.kMemberState == _enGuildMemberState.eGuildMemberState_AbleSubCaptain)
        {
            m_SubCaptainCrown.gameObject.SetActive(true);
        }

        bool IsMine = false;        // 내 캐릭인지
        if(m_MemberInfo.kCharNo == UserInfo.Instance.CharNo)
        {
            m_BgMeSprite.gameObject.SetActive(true);
            IsMine = true;
        }

        SetButton(type, m_MemberInfo.kMemberState, IsMine);
    }

    private void SetButton(enGuildInfo_WindowType type, _enGuildMemberState state, bool IsMine)
    {
        m_DismissButtonLabel.text = StringTableManager.GetData(6301);

        //m_AttendanceButton.gameObject.SetActive(false);             // 출석버튼.
        

        m_RejectButton.SetActive(false);                            // 길드가입 거절버튼(길드장 전용).
        m_ApprovalButton.SetActive(false);                          // 길드가입 수락버튼(길드장 전용).
        m_DeleteButton.SetActive(false);                            // 길드원 추방버튼.
        m_DismissButton.SetActive(false);                           // 부길드장 해임 버튼(길드장 전용).
        m_DelegationButton.SetActive(false);                        // 길드장 위임버튼(길드장 전용).
        m_AppointButton.SetActive(false);                           // 부길드장 임명버튼(길드장 전용).

        if (IsMine == true)
        {
            m_HomeButton.gameObject.SetActive(false);                    // 방문버튼.
            //m_HomeButton.state = ButtonState.Off;
        }
        else
        {
            m_HomeButton.gameObject.SetActive(true);                    // 방문버튼.
            m_HomeButton.state = ButtonState.On;
        }

        if (type == enGuildInfo_WindowType.MyGuild)
        {
        }
        else if (type == enGuildInfo_WindowType.GuildModify)
        {
            if (UserInfo.Instance.CharGuildState == _enGuildMemberState.eGuildMemberState_Captain)
            {
                if (state == _enGuildMemberState.eGuildMemberState_SubCaptain || state == _enGuildMemberState.eGuildMemberState_AbleSubCaptain)
                {
                    m_DismissButton.SetActive(true);
                }
            }

            if (state == _enGuildMemberState.eGuildMemberState_Request)
            {
                m_ApprovalButton.SetActive(true);
                m_RejectButton.SetActive(true);
            }
            else if (state == _enGuildMemberState.eGuildMemberState_AbleMember || state == _enGuildMemberState.eGuildMemberState_Member)
            {
                m_DeleteButton.SetActive(true);
            }
        }
        else if (type == enGuildInfo_WindowType.GuildCaptain_Delegation)
        {
            m_HomeButton.gameObject.SetActive(false);
            m_DelegationButton.SetActive(true);
        }
        else if (type == enGuildInfo_WindowType.GuildSubCaptain_Appointment)
        {
            m_HomeButton.gameObject.SetActive(false);
            m_AppointButton.SetActive(true);
        }
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    ///// <summary>
    ///// 출석버튼.
    ///// </summary>
    ///// <param name="go"></param>
    //private void OnAttendance(GameObject go)
    //{
    //    if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
    //}

    /// <summary>
    /// 방문버튼.
    /// </summary>
    /// <param name="go"></param>
    private void OnHome(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        if (m_HomeButton.state == ButtonState.Off)
            return;

        _stGuildUserTeamInfoReq stGuildUserTeamInfoReq = new _stGuildUserTeamInfoReq();
        stGuildUserTeamInfoReq.kDestCharNo = m_MemberInfo.kCharNo;

        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildUserTeamInfo, stGuildUserTeamInfoReq, typeof(_stGuildUserTeamInfoAck));
    }

    /// <summary>
    /// 길드가입 거절 버튼.
    /// </summary>
    /// <param name="go"></param>
    private void OnReject(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        //{플레이어 이름}\n님의 가입 요청을 거절하겠습니까?(스트링 ID : 6278)
        string str = string.Format(StringTableManager.GetData(6278), m_MemberInfo.kCharName);
        SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.YesNo, StringTableManager.GetData(4300), str, GuildRejectReq);
    }

    /// <summary>
    /// 길드가입 거절 패킷 보냄.
    /// </summary>
    /// <param name="state"></param>
    private void GuildRejectReq(enSystemMessageFlag state)
    {
        if (state == enSystemMessageFlag.NO)
            return;

        _stGuildJoinRequestAnswerReq stGuildJoinRequestAnswerReq = new _stGuildJoinRequestAnswerReq();
        stGuildJoinRequestAnswerReq.kGuildKey = m_kGuildKey;
        stGuildJoinRequestAnswerReq.kDestCharNo = m_MemberInfo.kCharNo;
        stGuildJoinRequestAnswerReq.kAnswer = _enGuildJoinRequestAnswer.eGuildJoinRequestAnswer_NO;     // <<<<

        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildJoinRequestAnswer, stGuildJoinRequestAnswerReq, typeof(_stGuildJoinRequestAnswerAck));
    }

    /// <summary>
    /// 길드가입 수락 버튼.
    /// </summary>
    /// <param name="go"></param>
    private void OnApproval(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        // {플레이어 이름}\n님을 길드에 가입시키겠습니까? (스트링 ID : 6282)
        string str = string.Format(StringTableManager.GetData(6282), m_MemberInfo.kCharName);
        SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.YesNo, StringTableManager.GetData(4300), str, GuildApprovalReq);
    }

    /// <summary>
    /// 길드가입 수락 패킷 보냄.
    /// </summary>
    /// <param name="state"></param>
    private void GuildApprovalReq(enSystemMessageFlag state)
    {
        if (state == enSystemMessageFlag.NO)
            return;

        _stGuildJoinRequestAnswerReq stGuildJoinRequestAnswerReq = new _stGuildJoinRequestAnswerReq();
        stGuildJoinRequestAnswerReq.kGuildKey = m_kGuildKey;
        stGuildJoinRequestAnswerReq.kDestCharNo = m_MemberInfo.kCharNo;
        stGuildJoinRequestAnswerReq.kAnswer = _enGuildJoinRequestAnswer.eGuildJoinRequestAnswer_YES;    // <<<<

        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildJoinRequestAnswer, stGuildJoinRequestAnswerReq, typeof(_stGuildJoinRequestAnswerAck));
    }

    /// <summary>
    /// 길드원 추방 버튼.
    /// </summary>
    /// <param name="go"></param>
    private void OnDelete(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        //{플레이어 이름}\n님을 길드에서 추방하겠습니까?(스트링 ID : 6286)
        string str = string.Format(StringTableManager.GetData(6286), m_MemberInfo.kCharName);
        SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.YesNo, StringTableManager.GetData(4300), str, GuildOrderLeaveReq);
    }

    private void GuildOrderLeaveReq(enSystemMessageFlag state)
    {
        if (state == enSystemMessageFlag.NO)
            return;

        _stGuildOrderLeaveReq stGuildOrderLeaveReq = new _stGuildOrderLeaveReq();
        stGuildOrderLeaveReq.kGuildKey = m_kGuildKey;
        stGuildOrderLeaveReq.kDestCharNo = m_MemberInfo.kCharNo;

        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildOrderLeave, stGuildOrderLeaveReq, typeof(_stGuildOrderLeaveAck));
    }

    /// <summary>
    /// 부길드장 해임 버튼.
    /// </summary>
    /// <param name="go"></param>
    private void OnDismiss(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        //{플레이어 이름}\n님을 부길드장에서 해임시키겠습니까?(스트링 ID : 6302)
        string str = string.Format(StringTableManager.GetData(6302), m_MemberInfo.kCharName);
        SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.YesNo, StringTableManager.GetData(4300), str, GuildOrderMemberReq);
    }

    private void GuildOrderMemberReq(enSystemMessageFlag state)
    {
        if (state == enSystemMessageFlag.NO)
            return;

        _stGuildOrderSubCaptainReq stGuildOrderSubCaptainReq = new _stGuildOrderSubCaptainReq();
        stGuildOrderSubCaptainReq.kGuildKey = m_kGuildKey;
        stGuildOrderSubCaptainReq.kDestCharNo = m_MemberInfo.kCharNo;
        stGuildOrderSubCaptainReq.kDestChangeState = _enGuildMemberState.eGuildMemberState_AbleMember;

        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildOrderSubCaptain, stGuildOrderSubCaptainReq, typeof(_stGuildOrderSubCaptainAck));
    }

    /// <summary>
    /// 길드장 위임 버튼.
    /// </summary>
    /// <param name="go"></param>
    private void OnDelegation(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        //길드장을 위임 하면 골드가 소비 됩니다. \n {길드원 이름}\n님에게 길드장을 위임하시겠습니까?(스트링 ID : 6295)
        string str = string.Format(StringTableManager.GetData(6295), m_MemberInfo.kCharName);
        SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.YesNo, StringTableManager.GetData(4300), str, GuildOrderCaptainReq);
    }

    /// <summary>
    /// 길드장 위임 패킷보냄.
    /// </summary>
    /// <param name="state"></param>
    private void GuildOrderCaptainReq(enSystemMessageFlag state)
    {
        if (state == enSystemMessageFlag.NO)
            return;

        _stGuildOrderCaptainReq stGuildOrderCaptainReq = new _stGuildOrderCaptainReq();
        stGuildOrderCaptainReq.kGuildKey = m_kGuildKey;
        stGuildOrderCaptainReq.kDestCharNo = m_MemberInfo.kCharNo;

        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildOrderCaptain, stGuildOrderCaptainReq, typeof(_stGuildOrderCaptainAck));
    }

    /// <summary>
    /// 부길드장 임명 버튼.
    /// </summary>
    /// <param name="go"></param>
    private void OnAppoint(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        //{ 길드원 이름}\n님을 부길드장으로 임명하시겠습니까 ? (스트링 ID : 6299)
        string str = string.Format(StringTableManager.GetData(6299), m_MemberInfo.kCharName);
        SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.YesNo, StringTableManager.GetData(4300), str, GuildOrderSubCaptainReq);
    }

    private void GuildOrderSubCaptainReq(enSystemMessageFlag state)
    {
        if (state == enSystemMessageFlag.NO)
            return;

        _stGuildOrderSubCaptainReq stGuildOrderSubCaptainReq = new _stGuildOrderSubCaptainReq();
        stGuildOrderSubCaptainReq.kGuildKey = m_kGuildKey;
        stGuildOrderSubCaptainReq.kDestCharNo = m_MemberInfo.kCharNo;
        stGuildOrderSubCaptainReq.kDestChangeState = _enGuildMemberState.eGuildMemberState_SubCaptain;

        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildOrderSubCaptain, stGuildOrderSubCaptainReq, typeof(_stGuildOrderSubCaptainAck));
    }
}