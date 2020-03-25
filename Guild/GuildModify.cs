using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using DGL_DATA_READER;
using System;

public class GuildModify : UIWindow
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UILabel m_WindowTitleLabel;

    [SerializeField] private GameObject m_CloseButton;

    //[SerializeField] private UILabel m_GuildMemberCountLabel;
    [SerializeField] private MenuButton m_GuildMemberButton;

    [SerializeField] private UIScrollView m_GuildMemberScrollView;
    [SerializeField] private UIGrid m_GuildMemberGrid;
    
    [SerializeField] private UI2DSprite m_GuildEmblemSprite;

    [SerializeField] private UILabel m_GuildLevelLabel;
    [SerializeField] private UILabel m_GuildNameLabel;
    [SerializeField] private UILabel m_GuildCaptainLabel;
    [SerializeField] private UILabel m_GuildJoinMethodLabel;

    [SerializeField] private GameObject m_EmblemChangeButton;
    [SerializeField] private UILabel m_EmblemChangeLabel;

    [SerializeField] private CustomButtonUI m_FreeJoinButton;
    [SerializeField] private CustomButtonUI m_ApprovalJoinButton;

    [SerializeField] private MenuButton m_NoticeButton;
    [SerializeField] private MenuButton m_DescButton;
    [SerializeField] private UIInput m_NoticeOrDescInput;
    [SerializeField] private UILabel m_NoticeOrDescLabel;
    [SerializeField] private GameObject m_NoticeOrDescModifyButton;     // 공지사항 소개글 수정.

    [SerializeField] private GameObject m_GuildOrderCaptainButton;      // 길드장 위임.
    [SerializeField] private UILabel m_GuildOrderCaptainButtonLabel;

    [SerializeField] private GameObject m_GuildOrderSubCaptainButton;   // 부길드장 임명.
    [SerializeField] private UILabel m_GuildOrderSubCaptainButtonLabel;

    [SerializeField] private GameObject m_GuildDeleteButton;            // 길드해체.
    [SerializeField] private UILabel m_GuildDeleteButtonLabel;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private MyGuildMain m_ParentWindow = null;
    private GuildEmblem m_GuildEmblemPopupWindow = null;
    private GuildEmblemChange m_GuildEmblemChangePopupWindow = null;

    private CGuildDetail m_GuildDetailInfo = null;

    private CGuildMember m_MyMemberInfo = null;

    private List<GuildInformationItem> m_MemberList = new List<GuildInformationItem>();

    private int m_iGuildWaitingCount = 0;
    private int m_iGuildMarkChangeCountDia = 0;
    private int m_iGuildNotifyCount = 0;            // 길드 공지글 글자 수.
    private int m_iGuildIntroStringCount = 0;       // 길드 소개글 글자 수.

    private _enGuildJoinMethod m_JoinType = _enGuildJoinMethod.eGuildJoinMethod_Free;
    private enNoticeAndDesc m_NoticeAndDescType = enNoticeAndDesc.Notice;
    private _enGuildJoinMethod _originGuildJoinMethod = _enGuildJoinMethod.eGuildJoinMethod_Approval;

    private bool m_bNoticeAndDescModifiy = false;
    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    protected override void Awake()
    {
        if (m_CloseButton != null) UIEventListener.Get(m_CloseButton).onClick = OnClickBack;
        if (m_EmblemChangeButton != null) UIEventListener.Get(m_EmblemChangeButton).onClick = OnEmblemChange;
        if (m_FreeJoinButton != null) UIEventListener.Get(m_FreeJoinButton.gameObject).onClick = OnFreeJoin;
        if (m_ApprovalJoinButton != null) UIEventListener.Get(m_ApprovalJoinButton.gameObject).onClick = OnApprovalJoin;
        if (m_NoticeButton != null) UIEventListener.Get(m_NoticeButton.gameObject).onClick = OnNotice;
        if (m_DescButton != null) UIEventListener.Get(m_DescButton.gameObject).onClick = OnDesc;
        if (m_NoticeOrDescModifyButton != null) UIEventListener.Get(m_NoticeOrDescModifyButton).onClick = OnNoticeOrDescModify;
        //if (m_GuildOrderCaptainButton != null) UIEventListener.Get(m_GuildOrderCaptainButton).onClick = OnGuildDelegation;
        //if (m_GuildOrderSubCaptainButton != null) UIEventListener.Get(m_GuildOrderSubCaptainButton).onClick = OnGuildOrderSubCaptain;
        if (m_GuildDeleteButton != null) UIEventListener.Get(m_GuildDeleteButton).onClick = OnGuildDelete;
    }

    protected override void OnDestroy()
    {
        if (m_CloseButton != null) UIEventListener.Get(m_CloseButton).onClick -= OnClickBack;
        if (m_EmblemChangeButton != null) UIEventListener.Get(m_EmblemChangeButton).onClick -= OnEmblemChange;
        if (m_FreeJoinButton != null) UIEventListener.Get(m_FreeJoinButton.gameObject).onClick -= OnFreeJoin;
        if (m_ApprovalJoinButton != null) UIEventListener.Get(m_ApprovalJoinButton.gameObject).onClick -= OnApprovalJoin;
        if (m_NoticeButton != null) UIEventListener.Get(m_NoticeButton.gameObject).onClick -= OnNotice;
        if (m_DescButton != null) UIEventListener.Get(m_DescButton.gameObject).onClick -= OnDesc;
        if (m_NoticeOrDescModifyButton != null) UIEventListener.Get(m_NoticeOrDescModifyButton).onClick -= OnNoticeOrDescModify;
        if (m_GuildOrderCaptainButton != null) UIEventListener.Get(m_GuildOrderCaptainButton).onClick -= m_ParentWindow.OnGuildDelegation;
        if (m_GuildOrderSubCaptainButton != null) UIEventListener.Get(m_GuildOrderSubCaptainButton).onClick -= m_ParentWindow.OnGuildOrderSubCaptain;
        if (m_GuildDeleteButton != null) UIEventListener.Get(m_GuildDeleteButton).onClick -= OnGuildDelete;
    }

    protected override void Start()
    {
    }

    public override void Init()
    {
    }

    protected override void OnEnable()
    {
    }

    protected override void OnDisable()
    {
    }

    public override void Clear()
    {
        int iCount = m_MemberList.Count;
        for (int i = 0; i < iCount; ++i)
        {
            GuildInformationItem infoItem = m_MemberList[i];
            if (infoItem == null)
                continue;

            DestroyImmediate(infoItem.gameObject);
        }

        m_MemberList.Clear();
    }

    protected override void Update()
    {
        m_GuildMemberButton.SetEvent(UserInfo.Instance.otherNew.GuildAlram == _enGuildAlram.eGuildNewMark_NewMember);
    }

    public override bool OnClickBack()
    {
        SetNoticeOrDescButton(m_NoticeAndDescType);
        m_bNoticeAndDescModifiy = false;
        m_NoticeOrDescInput.isSelected = m_bNoticeAndDescModifiy;
        m_NoticeOrDescInput.gameObject.SetActive(m_bNoticeAndDescModifiy);

        if (m_GuildDetailInfo.kJoinMethod == _enGuildJoinMethod.eGuildJoinMethod_Free)
        {
            UserInfo.Instance.otherNew.GuildAlram = _enGuildAlram.eGuildNewMark_None;
        }

        return true;
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init(MyGuildMain parent, CGuildDetail kGuildDetailInfo)
    {
        m_iGuildWaitingCount = (int)CDATA_FIXED_CONSTANTS.Get(DATA_FIXED_CONSTANTS._enConstant.Guild_Waiting_Count).Value;
        m_iGuildMarkChangeCountDia = (int)CDATA_FIXED_CONSTANTS.Get(DATA_FIXED_CONSTANTS._enConstant.Guild_MarkChange_Count_Dia).Value;
        m_iGuildNotifyCount = (int)CDATA_FIXED_CONSTANTS.Get(DATA_FIXED_CONSTANTS._enConstant.Guild_Notify_Count).Value;                // 길드 공지글 글자 수.
        m_iGuildIntroStringCount = (int)CDATA_FIXED_CONSTANTS.Get(DATA_FIXED_CONSTANTS._enConstant.Guild_IntroString_Count).Value;      // 길드 소개글 글자 수.

        m_ParentWindow = parent;
        if (m_GuildOrderCaptainButton != null) UIEventListener.Get(m_GuildOrderCaptainButton).onClick = m_ParentWindow.OnGuildDelegation;
        if (m_GuildOrderSubCaptainButton != null) UIEventListener.Get(m_GuildOrderSubCaptainButton).onClick = m_ParentWindow.OnGuildOrderSubCaptain;

        m_GuildDetailInfo = kGuildDetailInfo;
        _originGuildJoinMethod = m_GuildDetailInfo.kJoinMethod;

        m_WindowTitleLabel.text = StringTableManager.GetData(6276);
        m_EmblemChangeLabel.text = StringTableManager.GetData(6595);    // 6595    길드 마크 변경.

        m_NoticeButton.SetLabel(StringTableManager.GetData(6255));
        m_DescButton.SetLabel(StringTableManager.GetData(6249));

        m_GuildOrderCaptainButtonLabel.text = StringTableManager.GetData(6294);
        m_GuildOrderSubCaptainButtonLabel.text = StringTableManager.GetData(6298);
        m_GuildDeleteButtonLabel.text = StringTableManager.GetData(6304);

        m_FreeJoinButton.SetLabel(StringTableManager.GetData(6233));
        m_ApprovalJoinButton.SetLabel(StringTableManager.GetData(6239));

        byte kCurrMemberCount = m_GuildDetailInfo.kCurrMemberCount;
        byte kMaxMemberCount = m_GuildDetailInfo.kMaxMemberCount;

        // 6254 길드원 ({0} / {1})
        //m_GuildMemberCountLabel.text = string.Format(StringTableManager.GetData(6254), kCurrMemberCount, kMaxMemberCount);

        SetMyGuildInfo(m_GuildDetailInfo);
        SetMyGuildMember(m_GuildDetailInfo.kGuildKey, m_GuildDetailInfo.vMembers);

        // 길드 공지사항이나 소개글을 쓸수 있는 인풋을 막는다.
        m_NoticeOrDescInput.gameObject.SetActive(m_bNoticeAndDescModifiy);

        if (m_GuildMemberButton != null)
        {
            string szLabeltext = string.Format(StringTableManager.GetData(6254), kCurrMemberCount, kMaxMemberCount);
            m_GuildMemberButton.SetLabel(szLabeltext);

            m_GuildMemberButton.state = ButtonState.On;            
        }
    }

    private void SetMyGuildInfo(CGuildDetail guildDetail)
    {
        m_GuildDeleteButton.SetActive(false);
        m_GuildOrderCaptainButton.SetActive(false); // 길드장 위임버튼.
        m_GuildOrderSubCaptainButton.SetActive(false); // 부길드장 임명버튼.

        m_GuildEmblemSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_GUILDEMBLEM, string.Format("GuildEmblem{0}", guildDetail.kGuildMark.ToString("D2")));        
        m_GuildNameLabel.text = guildDetail.kGuildName;

        DATA_GUILD_MAIN GuildMainData = CDATA_GUILD_MAIN.Get(guildDetail.kGuildLevel);
        if (GuildMainData != null)
        {
            // string num : 12 -> LV
            m_GuildLevelLabel.text = string.Format("{0} {1}", StringTableManager.GetData(12), GuildMainData.iGuildLv);
        }

        CGuildMember captainInfo = null;
        int iCount = guildDetail.vMembers.Count;
        for (int i = 0; i < iCount; ++i)
        {
            CGuildMember memberInfo = guildDetail.vMembers[i];
            if (memberInfo == null)
                continue;

            if (memberInfo.kMemberState == _enGuildMemberState.eGuildMemberState_Captain)
            {
                captainInfo = memberInfo;
            }

            if (memberInfo.kCharNo == UserInfo.Instance.CharNo)
            {
                m_MyMemberInfo = memberInfo;
                UserInfo.Instance.GuildKey = guildDetail.kGuildKey;
                UserInfo.Instance.GuildName = guildDetail.kGuildName;
                UserInfo.Instance.CharGuildState = m_MyMemberInfo.kMemberState;
            }
        }

        if (captainInfo != null)
        {
            m_GuildCaptainLabel.text = captainInfo.kCharName;
        }

        SetJoinMethodButton(guildDetail.kJoinMethod);
        SetNoticeOrDescButton(enNoticeAndDesc.Notice);     // 공지사항이 디폴트.

        if (m_MyMemberInfo != null)
        {
            if (m_MyMemberInfo.kMemberState == _enGuildMemberState.eGuildMemberState_Captain)
            {
                m_GuildDeleteButton.SetActive(true);
                m_GuildOrderCaptainButton.SetActive(true);
                m_GuildOrderSubCaptainButton.SetActive(true);
            }
        }
    }

    public void SetMyGuildMember(ulong kGuildKey, _vGuildMembers vMembers)
    {
        Clear();

        int iRequestCount = 0;

        int iCount = vMembers.Count;
        for (int i = 0; i < iCount; ++i)
        {
            CGuildMember memberInfo = vMembers[i];
            if (memberInfo == null)
                continue;

            if (memberInfo.kMemberState == _enGuildMemberState.eGuildMemberState_None)
                continue;

            if (memberInfo.kMemberState == _enGuildMemberState.eGuildMemberState_Request)
                iRequestCount++;

            // 길드요청이 테이블값 이상이면 데이터가 존재해도 UI를 추가하지 않는다.
            if (iRequestCount > m_iGuildWaitingCount && memberInfo.kMemberState == _enGuildMemberState.eGuildMemberState_Request)     
                continue;

            GuildInformationItem memberItem = UIResourceMgr.CreatePrefab<GuildInformationItem>(BUNDLELIST.PREFABS_UI_GUILD, m_GuildMemberGrid.transform, "GuildInformationItem");
            memberItem.Init(enGuildInfo_WindowType.GuildModify, kGuildKey, memberInfo);

            m_MemberList.Add(memberItem);
        }

        SortMemberList();

        // 길드요청이 테이블값 이상이면 팝업띄움.
        if (iRequestCount > m_iGuildWaitingCount)
        {
            // 4300 알림. 6277 길드 가입 신청 대기 인원이 10명 이상입니다. \n 더 이상 길드 가입 신청을 받을 수 없습니다. 
            SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(4300), StringTableManager.GetData(6277));
        }
    }

    /// <summary>
    /// 생성한 길드멤버 리스트를 정렬한다.
    /// </summary>
    private void SortMemberList()
    {
        // 정렬변경 -> 1. 멤버상태(길드장->부길드장->길드원으로 정렬) -> 2. 오늘 로그인했나 -> 3. 기여도
        m_MemberList = m_MemberList.OrderBy((data) => data.MemberInfo.kMemberState)
            .ThenByDescending((data) => UtilFunc.TodayLogon(data.MemberInfo.kLastLogonTime.GetDateTime()))
            .ThenByDescending((data) => data.MemberInfo.kUserTributeExp).ToList();


        int iCount = m_MemberList.Count;
        for (int i = 0; i < iCount; ++i)
        {
            GuildInformationItem member = m_MemberList[i];
            if (member == null)
                continue;

            member.name = i.ToString();
        }

        ResetPosition();
    }

    private void ResetPosition()
    {
        m_GuildMemberGrid.sorting = UIGrid.Sorting.Custom;
        m_GuildMemberGrid.onCustomSort = UtilFunc.SortByNumber;

        //m_GuildMemberGrid.sorting = UIGrid.Sorting.Alphabetic;
        m_GuildMemberGrid.Reposition();
        m_GuildMemberScrollView.ResetPosition();
    }

    /// <summary>
    /// 길드 관리창에서 길드마크 변경 팝업 띄움.
    /// </summary>
    /// <param name="SelectEmblem"></param>
    public void SetEmblem(EmblemElement SelectEmblem)
    {
        if(m_GuildEmblemChangePopupWindow == null)
        {
            m_GuildEmblemChangePopupWindow = UIResourceMgr.CreatePrefab<GuildEmblemChange>(BUNDLELIST.PREFABS_UI_GUILD, transform, "GuildEmblemChange");
            m_GuildEmblemChangePopupWindow.Init(this);
        }

        m_GuildEmblemChangePopupWindow.SetEmbelemInfo(m_GuildDetailInfo.kGuildKey, m_GuildEmblemSprite.sprite2D, SelectEmblem.GetEmblemSprite(), byte.Parse(SelectEmblem.name));
        m_GuildEmblemChangePopupWindow.OpenUI();
    }

    public void GuildEmblemPopupClose()
    {
        if (m_GuildEmblemPopupWindow != null)
        {
            m_GuildEmblemPopupWindow.CloseUI();
        }
    }

    /// <summary>
    /// 길드 마크 변경 패킷 받음.
    /// </summary>
    /// <param name="kNewGuildMark"></param>
    public void GuildChangeMark(byte kNewGuildMark)
    {
        m_GuildEmblemSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_GUILDEMBLEM, string.Format("GuildEmblem{0}", kNewGuildMark.ToString("D2")));
        m_GuildEmblemPopupWindow.CloseUI();
        m_GuildEmblemChangePopupWindow.CloseUI();
    }

    /// <summary>
    /// 길드 가입요청 수락& 거절 패킷 받음.
    /// </summary>
    public void GuildJoinRequestAnswer(_stGuildJoinRequestAnswerAck stAck, CGuildMember member, CGuildDetail GuildDetailInfo)
    {
        bool isJoinRequest = false;
        for (int i = 0; i < m_MemberList.Count;)
        {
            GuildInformationItem infoItem = m_MemberList[i];
            if (infoItem.MemberInfo.kCharNo == stAck.kDestCharNo)
            {
                DestroyImmediate(infoItem.gameObject);
                m_MemberList.Remove(infoItem);
            }
            else
            {
                ++i;

                if(infoItem.MemberInfo.kMemberState == _enGuildMemberState.eGuildMemberState_Request)
                {
                    isJoinRequest = true;
                }
            }
        }

        m_GuildMemberButton.SetEvent(isJoinRequest);
        if (isJoinRequest == false)
        {
            UserInfo.Instance.otherNew.GuildAlram = _enGuildAlram.eGuildNewMark_None;
        }

        if (stAck.kAnswer == _enGuildJoinRequestAnswer.eGuildJoinRequestAnswer_YES)
        {
            // 수락이면 길드원으로 다시 생성.
            GuildInformationItem memberItem = UIResourceMgr.CreatePrefab<GuildInformationItem>(BUNDLELIST.PREFABS_UI_GUILD, m_GuildMemberGrid.transform, "GuildInformationItem");
            memberItem.Init(enGuildInfo_WindowType.GuildModify, stAck.kGuildKey, member);
            m_MemberList.Add(memberItem);

        }
        else if (stAck.kAnswer == _enGuildJoinRequestAnswer.eGuildJoinRequestAnswer_NO)
        {
            // 거절이면 삭제된 데이터 대입.
            m_GuildDetailInfo = GuildDetailInfo;
        }

        string szLabeltext = string.Format(StringTableManager.GetData(6254), GuildDetailInfo.kCurrMemberCount, GuildDetailInfo.kMaxMemberCount);
        m_GuildMemberButton.SetLabel(szLabeltext);

        SortMemberList();
    }

    /// <summary>
    /// 길드 추방 패킷 받음(길드장 전용).
    /// </summary>
    public void GuildOrderLeave(_stGuildOrderLeaveAck stAck)
    {
        int iCount = m_MemberList.Count;
        for (int i = 0; i < iCount; ++i)
        {
            GuildInformationItem member = m_MemberList[i];
            if (member == null)
                continue;

            if (member.MemberInfo.kCharNo == stAck.kDestCharNo)
            {
                DestroyImmediate(member.gameObject);
                m_MemberList.Remove(member);
                break;
            }
        }

        _vGuildMembers vGuildMembers = m_GuildDetailInfo.vMembers;
        for (int i = 0; i < vGuildMembers.Count; ++i)
        {
            CGuildMember member = vGuildMembers[i];
            if (member == null)
                continue;

            if (member.kCharNo == stAck.kDestCharNo)
            {
                vGuildMembers.Remove(member);
                break;
            }
        }

        string szLabeltext = string.Format(StringTableManager.GetData(6254), m_GuildDetailInfo.vMembers.Count, m_GuildDetailInfo.kMaxMemberCount);
        m_GuildMemberButton.SetLabel(szLabeltext);

        m_GuildMemberGrid.Reposition();
        m_GuildMemberScrollView.ResetPosition();
    }

    /// <summary>
    /// 자유가입 인지 승인가입 인지 셋팅.
    /// </summary>
    /// <param name="type"></param>
    private void SetJoinMethodButton(_enGuildJoinMethod type)
    {
        m_JoinType = type;

        if (type == _enGuildJoinMethod.eGuildJoinMethod_Free)
        {
            m_GuildJoinMethodLabel.text = StringTableManager.GetData(6233);
            m_FreeJoinButton.state = ButtonState.On;
            m_ApprovalJoinButton.state = ButtonState.Off;
        }
        else if (type == _enGuildJoinMethod.eGuildJoinMethod_Approval)
        {
            m_GuildJoinMethodLabel.text = StringTableManager.GetData(6239);
            m_FreeJoinButton.state = ButtonState.Off;
            m_ApprovalJoinButton.state = ButtonState.On;
        }
    }

    /// <summary>
    /// 공지사항, 소개글 버튼 셋팅.
    /// </summary>
    /// <param name="type"></param>
    private void SetNoticeOrDescButton(enNoticeAndDesc type)
    {
        m_NoticeAndDescType = type;

        if (type == enNoticeAndDesc.Notice)
        {
            m_NoticeOrDescLabel.text = m_GuildDetailInfo.kGuildNotice;
            m_NoticeOrDescInput.characterLimit = m_iGuildNotifyCount;

            m_NoticeButton.state = ButtonState.On;
            m_DescButton.state = ButtonState.Off;
        }
        else if (type == enNoticeAndDesc.Desc)
        {
            m_NoticeOrDescLabel.text = m_GuildDetailInfo.kGuildDesc;
            m_NoticeOrDescInput.characterLimit = m_iGuildIntroStringCount;

            m_NoticeButton.state = ButtonState.Off;
            m_DescButton.state = ButtonState.On;
        }
    }

    /// <summary>
    /// 길드 관리 자유가입&승인가입 변경 패킷 받음.
    /// 길드 관리 공지사항&소개글 변경 패킷 받음.
    /// </summary>
    /// <param name="stAck"></param>
    public void GuildChangeDescAndJoinMethod(_stGuildChangeDescAck stAck)
    {
        m_GuildDetailInfo.kGuildNotice = stAck.kGuildNotice;
        m_GuildDetailInfo.kGuildDesc = stAck.kGuildDesc;
        m_GuildDetailInfo.kJoinMethod = stAck.kJoinMethod;

        // 가입형태가 변경되었으면 new 를 꺼준다.
        if (_originGuildJoinMethod != stAck.kJoinMethod)
            UserInfo.Instance.otherNew.GuildAlram = _enGuildAlram.eGuildNewMark_None;
        _originGuildJoinMethod = stAck.kJoinMethod;

        // 승인가입에서 자유가입으로 변경이 되면.
        if (stAck.kJoinMethod == _enGuildJoinMethod.eGuildJoinMethod_Free)
        {
            for (int i = 0; i < m_MemberList.Count;)
            {
                GuildInformationItem infoItem = m_MemberList[i];
                if (infoItem == null)
                {
                    ++i;
                    continue;
                }

                // 길드가 자유가입 형태로 변경됐으니 승인가입 형태일때 지원했던 유저들을 삭제.
                if(infoItem.MemberInfo.kMemberState == _enGuildMemberState.eGuildMemberState_Request)
                {
                    DestroyImmediate(infoItem.gameObject);
                    m_MemberList.Remove(infoItem);
                }
                else
                {
                    ++i;
                }
            }
        }

        SortMemberList();

        SetJoinMethodButton(m_GuildDetailInfo.kJoinMethod);
        SetNoticeOrDescButton(m_NoticeAndDescType);
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    /// <summary>
    /// 길드마크 변경 버튼.
    /// </summary>
    /// <param name="go"></param>
    private void OnEmblemChange(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        if (m_GuildEmblemPopupWindow == null)
        {
            m_GuildEmblemPopupWindow = UIResourceMgr.CreatePrefab<GuildEmblem>(BUNDLELIST.PREFABS_UI_GUILD, transform, "GuildEmblem");
            m_GuildEmblemPopupWindow.Init(this);
        }

        m_GuildEmblemPopupWindow.OpenUI();
    }

    /// <summary>
    /// 자유가입 변경 체크버튼.
    /// </summary>
    /// <param name="go"></param>
    private void OnFreeJoin(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        if (m_JoinType == _enGuildJoinMethod.eGuildJoinMethod_Free)
            return;

        // 6593    길드 가입 형태를 \n자유 가입으로 변경하시겠습니까?
        SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.YesNo, StringTableManager.GetData(4300), StringTableManager.GetData(6593), FreeJoinReq);
    }

    private void FreeJoinReq(enSystemMessageFlag state)
    {
        if (state == enSystemMessageFlag.NO)
            return;

        _stGuildChangeDescReq stGuildChangeDescReq = new _stGuildChangeDescReq();
        stGuildChangeDescReq.kGuildKey = m_GuildDetailInfo.kGuildKey;
        stGuildChangeDescReq.kGuildNotice = m_GuildDetailInfo.kGuildNotice;
        stGuildChangeDescReq.kGuildDesc = m_GuildDetailInfo.kGuildDesc;
        stGuildChangeDescReq.kJoinMethod = _enGuildJoinMethod.eGuildJoinMethod_Free;

        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildChangeDesc, stGuildChangeDescReq, typeof(_stGuildChangeDescAck));
    }

    /// <summary>
    /// 승인가입 변경 체크버튼.
    /// </summary>
    /// <param name="go"></param>
    private void OnApprovalJoin(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        if (m_JoinType == _enGuildJoinMethod.eGuildJoinMethod_Approval)
            return;

        // 6594    길드 가입 형태를 \n승인 가입으로 변경하시겠습니까?
        SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.YesNo, StringTableManager.GetData(4300), StringTableManager.GetData(6594), ApprovalJoinReq);
    }

    private void ApprovalJoinReq(enSystemMessageFlag state)
    {
        if (state == enSystemMessageFlag.NO)
            return;

        _stGuildChangeDescReq stGuildChangeDescReq = new _stGuildChangeDescReq();
        stGuildChangeDescReq.kGuildKey = m_GuildDetailInfo.kGuildKey;
        stGuildChangeDescReq.kGuildNotice = m_GuildDetailInfo.kGuildNotice;
        stGuildChangeDescReq.kGuildDesc = m_GuildDetailInfo.kGuildDesc;
        stGuildChangeDescReq.kJoinMethod = _enGuildJoinMethod.eGuildJoinMethod_Approval;

        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildChangeDesc, stGuildChangeDescReq, typeof(_stGuildChangeDescAck));
    }

    /// <summary>
    /// 공지사항 탭 버튼.
    /// </summary>
    /// <param name="go"></param>
    private void OnNotice(GameObject go)
    {
        if (m_bNoticeAndDescModifiy == true)
            return;

        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        SetNoticeOrDescButton(enNoticeAndDesc.Notice);
    }

    /// <summary>
    /// 소개글 탭 버튼.
    /// </summary>
    /// <param name="go"></param>
    private void OnDesc(GameObject go)
    {
        if (m_bNoticeAndDescModifiy == true)
            return;

        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        SetNoticeOrDescButton(enNoticeAndDesc.Desc);
    }

    /// <summary>
    /// 공지사항 & 소개글 수정 버튼.
    /// </summary>
    /// <param name="go"></param>
    private void OnNoticeOrDescModify(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        m_NoticeOrDescInput.gameObject.SetActive(m_bNoticeAndDescModifiy = !m_bNoticeAndDescModifiy);
        m_NoticeOrDescInput.isSelected = m_bNoticeAndDescModifiy;

        if (m_bNoticeAndDescModifiy == true)
        {
            m_NoticeOrDescInput.value = string.Empty;
        }
        else 
        {
            _stGuildChangeDescReq stGuildChangeDescReq = new _stGuildChangeDescReq();
            stGuildChangeDescReq.kGuildKey = m_GuildDetailInfo.kGuildKey;

            if(m_NoticeAndDescType == enNoticeAndDesc.Notice)
            {
                stGuildChangeDescReq.kGuildNotice = m_NoticeOrDescInput.value;
                stGuildChangeDescReq.kGuildDesc = m_GuildDetailInfo.kGuildDesc;
            }
            else if (m_NoticeAndDescType == enNoticeAndDesc.Desc)
            {
                stGuildChangeDescReq.kGuildNotice = m_GuildDetailInfo.kGuildNotice;
                stGuildChangeDescReq.kGuildDesc = m_NoticeOrDescInput.value;
            }

            stGuildChangeDescReq.kJoinMethod = m_JoinType;

            CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildChangeDesc, stGuildChangeDescReq, typeof(_stGuildChangeDescAck));
        }
    }

    /// <summary>
    /// 길드 해체 버튼.
    /// </summary>
    /// <param name="go"></param>
    private void OnGuildDelete(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.YesNo, StringTableManager.GetData(4300), StringTableManager.GetData(6305), GuildDelete);
    }

    private void GuildDelete(enSystemMessageFlag state)
    {
        if (state == enSystemMessageFlag.NO)
            return;

        int iGuldMemberCount = 0;
        int iCount = m_GuildDetailInfo.vMembers.Count;
        for (int i = 0; i < iCount; ++i)
        {
            CGuildMember member = m_GuildDetailInfo.vMembers[i];
            if (member == null)
                continue;

            if (member.kMemberState == _enGuildMemberState.eGuildMemberState_Member || member.kMemberState == _enGuildMemberState.eGuildMemberState_AbleMember ||
                member.kMemberState == _enGuildMemberState.eGuildMemberState_SubCaptain || member.kMemberState == _enGuildMemberState.eGuildMemberState_AbleSubCaptain)
            {
                iGuldMemberCount++;
            }
        }

        if(iGuldMemberCount > 0)
        {
            StartCoroutine(IeGuildDeletePopup());
            return;
        }

        _stGuildDeleteReq stGuildDeleteReq = new _stGuildDeleteReq();
        stGuildDeleteReq.kGuildKey = m_GuildDetailInfo.kGuildKey;

        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildDelete, stGuildDeleteReq, typeof(_stGuildDeleteAck));
    }

    private IEnumerator IeGuildDeletePopup()
    {
        yield return new WaitForSeconds(0.1f);

        //6307 길드원이 남아 있어 길드를 해체할 수 없습니다. 
        SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(4300), StringTableManager.GetData(6307));
    }
}