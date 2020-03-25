using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using DGL_DATA_READER;

public enum enNoticeAndDesc
{
    Notice,
    Desc,
}

public class MyGuild : UIWindow
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UIScrollView m_GuildMemberScrollView;
    [SerializeField] private UIGrid m_GuildMemberGrid;

    [SerializeField] private UILabel m_TodayAttendanceTitleLabel;           // 금일 출석 인원 표시 라벨.
    [SerializeField] private UILabel m_TodayAttendanceCountLabel;           // 금일 출석 인원 수.

    [SerializeField] private MenuButton _attendanceRewardButton;
    [SerializeField] private UILabel _attendanceRewardButtonLabel;

    [SerializeField] private UILabel m_GuildLevelLabel;                     // 길드레벨 라벨.
    [SerializeField] private UISprite _GuildLevelGageSprite;                // 길드레벨 게이지.
    [SerializeField] private UILabel _GuildLevelGageLabel;                  // 길드라벨 게이지 퍼센트 라벨.

    [SerializeField] private UILabel m_GuildNameLabel;
    [SerializeField] private UILabel m_GuildCaptainLabel;
    [SerializeField] private UILabel m_GuildJoinMethodLabel;

    [SerializeField] private UI2DSprite m_GuildMarkSprite;                  // 길드마크.

    [SerializeField] private MenuButton m_NoticeButton;                     // 공지사항버튼.
    [SerializeField] private MenuButton m_DescButton;                       // 소개글버튼.
    [SerializeField] private UILabel m_NoticeOrDescLabel;                   // 공지사항&소개글 내용 라벨.

    [SerializeField] private CustomButtonUI m_CommissionCheckButton;        // 길드장 위임 체크버튼.

    [SerializeField] private MenuButton _TributeButton;                     // 공물버튼.
    [SerializeField] private MenuButton _GuildRaidButton;                   // 길드 레이드 버튼.
    [SerializeField] private MenuButton c;      // 임시 길드전버튼.
    [SerializeField] private MenuButton d;      // 임시 토벌전버튼.

    [SerializeField] private GameObject m_GuildLeaveButton;                 // 길드탈퇴 버튼.
    [SerializeField] private UILabel m_GuildLeaveButtonLabel;               // 길드탈퇴 버튼라벨.

    [SerializeField] private GameObject m_GuildModifyButton;                // (길드장 전용) 길드관리 버튼.
    [SerializeField] private UILabel m_GuildModifyButtonLabel;              // (길드장 전용) 길드관리 버튼 라벨.
    [SerializeField] private UISprite m_GuildModifyNewSprite;               // (길드장 전용) 길드관리 버튼 New 이미지.

    [SerializeField] private GameObject m_GuildSubCaptainButtonObj;         // (부길드장 전용) 길드탈퇴, 관리버튼 부모 오브젝트(껐다켰다 하는 용도로 쓰임)
    
    [SerializeField] private GameObject m_GuildSubCaptainLeaveButton;       // (부길드장 전용) 길드탈퇴 버튼.
    [SerializeField] private UILabel m_GuildSubCaptainLeaveButtonLabel;     // (부길드장 전용) 길드탈퇴 버튼 라벨.

    [SerializeField] private GameObject m_GuildSubCaptainModifyButton;      // (부길드장 전용) 길드관리 버튼.
    [SerializeField] private UILabel m_GuildSubCaptainModifyButtonLabel;    // (부길드장 전용) 길드관리 버튼 라벨.
    [SerializeField] private UISprite m_GuildSubCaptainModifyNewSprite;     // (부길드장 전용) 길드관리 버튼 New 이미지.

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private MyGuildMain m_ParentWindow = null;

    private GuildGoddness _GuildGoddnessPopupWindow = null;

    private GuildRaidWindow _GuildRaidWindow = null;

    private CGuildDetail m_GuildDetailInfo = null;

    private CGuildMember m_MyMemberInfo = null;

    private List<GuildInformationItem> m_MemberList = new List<GuildInformationItem>();

    private bool m_bCheckState = true;
    private float m_fElapsedTime = 0.0f;
    private float m_fClickTime = 1.0f;

    private _enGuildJoinMethod m_JoinType = _enGuildJoinMethod.eGuildJoinMethod_Free;
    private enNoticeAndDesc m_NoticeAndDescType = enNoticeAndDesc.Notice;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    protected override void Awake()
    {
        //if (m_AttendanceRewardButton != null) UIEventListener.Get(m_AttendanceRewardButton).onClick = OnAttendanceReward;
        //if (m_AttendanceInfoButton != null) UIEventListener.Get(m_AttendanceInfoButton).onClick = OnAttendanceInfo;
        if (m_NoticeButton != null) UIEventListener.Get(m_NoticeButton.gameObject).onClick = OnNotice;
        if (m_DescButton != null) UIEventListener.Get(m_DescButton.gameObject).onClick = OnDesc;
        //if (m_GuildModifyButton != null) if(m_ParentWindow != null) UIEventListener.Get(m_GuildModifyButton).onClick = m_ParentWindow.OnGuildModify;
        if (m_GuildLeaveButton != null) UIEventListener.Get(m_GuildLeaveButton).onClick = OnGuildLeave;
        if (m_GuildSubCaptainLeaveButton != null) UIEventListener.Get(m_GuildSubCaptainLeaveButton).onClick = OnGuildLeave; 
        if (m_CommissionCheckButton != null) UIEventListener.Get(m_CommissionCheckButton.gameObject).onClick = OnCommissionCheck;
        if (_TributeButton != null) UIEventListener.Get(_TributeButton.gameObject).onClick = OnTribute;
#if UNITY_EDITOR && GUILD_RAID
        if (_GuildRaidButton != null) UIEventListener.Get(_GuildRaidButton.gameObject).onClick = OnGuildRaid;
#endif
        if (_attendanceRewardButton.gameObject != null) UIEventListener.Get(_attendanceRewardButton.gameObject).onClick = OnAttendanceReward;
    }

    protected override void OnDestroy()
    {
        //if (m_AttendanceRewardButton != null) UIEventListener.Get(m_AttendanceRewardButton).onClick -= OnAttendanceReward;
        //if (m_AttendanceInfoButton != null) UIEventListener.Get(m_AttendanceInfoButton).onClick -= OnAttendanceInfo;
        if (m_NoticeButton != null) UIEventListener.Get(m_NoticeButton.gameObject).onClick -= OnNotice;
        if (m_DescButton != null) UIEventListener.Get(m_DescButton.gameObject).onClick -= OnDesc;
        if (m_GuildModifyButton != null) if (m_ParentWindow != null) UIEventListener.Get(m_GuildModifyButton).onClick -= m_ParentWindow.OnGuildModify;
        if (m_GuildLeaveButton != null) UIEventListener.Get(m_GuildLeaveButton).onClick -= OnGuildLeave;
        if (m_GuildSubCaptainLeaveButton != null) UIEventListener.Get(m_GuildSubCaptainLeaveButton).onClick -= OnGuildLeave;
        if (m_GuildSubCaptainModifyButton != null) if (m_ParentWindow != null) UIEventListener.Get(m_GuildSubCaptainModifyButton).onClick -= m_ParentWindow.OnGuildModify;
        if (m_CommissionCheckButton != null) UIEventListener.Get(m_CommissionCheckButton.gameObject).onClick -= OnCommissionCheck;
        if (_TributeButton != null) UIEventListener.Get(_TributeButton.gameObject).onClick -= OnTribute;
#if UNITY_EDITOR && GUILD_RAID
        if (_GuildRaidButton != null) UIEventListener.Get(_GuildRaidButton.gameObject).onClick -= OnGuildRaid;
#endif
        if (_attendanceRewardButton.gameObject != null) UIEventListener.Get(_attendanceRewardButton.gameObject).onClick -= OnAttendanceReward;

        Clear();
    }

    protected override void Start()
    {
    }

    protected override void OnEnable()
    {
    }

    protected override void OnDisable()
    {
    }

    protected override void Update()
    {
        if (m_bCheckState == false)
        {
            m_fElapsedTime += Time.deltaTime;
            if (m_fElapsedTime >= m_fClickTime)
            {
                m_fElapsedTime = 0.0f;
                m_bCheckState = true;
            }
        }

        _TributeButton.SetNew(UserInfo.Instance.otherNew.IsGuildAttendance);
        _attendanceRewardButton.SetNew(UserInfo.Instance.rewardNew.isGuildAttendanceRewardNew);

        bool isGuildRaidRewardNew = UserInfo.Instance.rewardNew.isGuildRaidRewardNew;

#if UNITY_EDITOR && GUILD_RAID
        _GuildRaidButton.SetNew(isGuildRaidRewardNew);
        if (isGuildRaidRewardNew == false)
            _GuildRaidButton.SetEvent(UserInfo.Instance.otherNew.isGuildRaidTicketFull);
#endif

        if (UserInfo.Instance.otherNew.GuildAlram == _enGuildAlram.eGuildNewMark_NewMember)
        {
            m_GuildModifyNewSprite.gameObject.SetActive(true);
            m_GuildSubCaptainModifyNewSprite.gameObject.SetActive(true);
        }
        else
        {
            m_GuildModifyNewSprite.gameObject.SetActive(false);
            m_GuildSubCaptainModifyNewSprite.gameObject.SetActive(false);
        }
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

    public override void Init()
    {
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init(MyGuildMain parent)
    {
        m_ParentWindow = parent;

        if (m_ParentWindow != null)
        {
            if (m_GuildModifyButton != null) UIEventListener.Get(m_GuildModifyButton).onClick = m_ParentWindow.OnGuildModify;
            if (m_GuildSubCaptainModifyButton != null) UIEventListener.Get(m_GuildSubCaptainModifyButton).onClick = m_ParentWindow.OnGuildModify;
        }

        // 6752	봉헌
        //_TributeButton.SetNew(UserInfo.Instance.otherNew.IsGuildAttendance);
        _TributeButton.SetLabel(StringTableManager.GetData(6752));
        _TributeButton.state = ButtonState.On;

        m_TodayAttendanceTitleLabel.text = StringTableManager.GetData(6586); // 6586    오늘 출석 인원.

        m_NoticeButton.SetLabel(StringTableManager.GetData(6255));
        m_DescButton.SetLabel(StringTableManager.GetData(6249));

        // 6592    체크 하시면 길드장 위임을 받을 수 있는 상태가 됩니다.
        m_CommissionCheckButton.SetLabel(StringTableManager.GetData(6592));

        m_GuildLeaveButtonLabel.text = StringTableManager.GetData(6256);
        m_GuildModifyButtonLabel.text = StringTableManager.GetData(6276);

        m_GuildSubCaptainLeaveButtonLabel.text = StringTableManager.GetData(6256);
        m_GuildSubCaptainModifyButtonLabel.text = StringTableManager.GetData(6276);

#if UNITY_EDITOR && GUILD_RAID
        _GuildRaidButton.gameObject.SetActive(true);
        _GuildRaidButton.SetLabel(StringTableManager.GetData(6753));        // 6753	길드 레이드
#else
        _GuildRaidButton.gameObject.SetActive(false);
#endif
        c.gameObject.SetActive(false);
        d.gameObject.SetActive(false);
        
        _attendanceRewardButton.state = ButtonState.Off;
        _attendanceRewardButton.SetLabel(string.Format(StringTableManager.GetData(8679), 0));      // 8679    X{0}
        _attendanceRewardButtonLabel.text = StringTableManager.GetData(3467);   // 3467	모두 받기
    }

    public void GuildDetailInfoAck(CGuildDetail kGuildDetailInfo)
    {
        m_GuildDetailInfo = kGuildDetailInfo;

        SetMyGuildInfo(m_GuildDetailInfo);
        SetMyGuildMember(m_GuildDetailInfo.kGuildKey, m_GuildDetailInfo.vMembers);
    }

    public void SetAttendenceReward(ulong attendenceRewardCount)
    {
        if (attendenceRewardCount > 0)
            _attendanceRewardButton.state = ButtonState.On;
        else
            _attendanceRewardButton.state = ButtonState.Off;

        _attendanceRewardButton.SetLabel(string.Format(StringTableManager.GetData(8679), attendenceRewardCount));
    }

    private void SetMyGuildInfo(CGuildDetail guildDetail)
    {
        m_GuildLeaveButton.SetActive(false);
        m_GuildModifyButton.SetActive(false);
        m_GuildSubCaptainButtonObj.SetActive(false); // 부길드장이 사용하는 버튼.
        m_CommissionCheckButton.gameObject.SetActive(true);

        // 6587    {0} / {1} 명
        m_TodayAttendanceCountLabel.text = string.Format(StringTableManager.GetData(6587), guildDetail.kAttendanceCount, guildDetail.kCurrMemberCount);

        m_GuildMarkSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_GUILDEMBLEM, string.Format("GuildEmblem{0}", guildDetail.kGuildMark.ToString("D2")));        
        m_GuildNameLabel.text = guildDetail.kGuildName;

        DATA_GUILD_MAIN GuildMainData = CDATA_GUILD_MAIN.Get(guildDetail.kGuildLevel);
        if (GuildMainData != null)
        {
            // string num : 12 -> LV
            m_GuildLevelLabel.text = string.Format("{0} {1}", StringTableManager.GetData(12), GuildMainData.iGuildLv);

            float GuildExpPercentage = guildDetail.kGuildExp / (float)GuildMainData.iGexp;
            _GuildLevelGageSprite.fillAmount = GuildExpPercentage;
            _GuildLevelGageLabel.text = string.Format("{0}%", (GuildExpPercentage * 100).ToString("F2"));
        }

        bool isRequestMember = false;
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
            else if (memberInfo.kMemberState == _enGuildMemberState.eGuildMemberState_Request)
            {
                isRequestMember = true;
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

        if (guildDetail.kJoinMethod == _enGuildJoinMethod.eGuildJoinMethod_Free)
        {
            m_GuildJoinMethodLabel.text = StringTableManager.GetData(6233);
        }
        else if (guildDetail.kJoinMethod == _enGuildJoinMethod.eGuildJoinMethod_Approval)
        {
            m_GuildJoinMethodLabel.text = StringTableManager.GetData(6239);
        }

        SetJoinMethodButton(guildDetail.kJoinMethod);
        SetNoticeOrDescButton(enNoticeAndDesc.Notice);     // 공지사항이 디폴트.

        if (m_MyMemberInfo != null)
        {
            if (m_MyMemberInfo.kMemberState == _enGuildMemberState.eGuildMemberState_Captain)       // 길드장.
            {
                m_GuildModifyButton.SetActive(true);
                m_CommissionCheckButton.gameObject.SetActive(false);

                if(isRequestMember == true)
                {
                    UserInfo.Instance.otherNew.GuildAlram = _enGuildAlram.eGuildNewMark_NewMember;
                }
            }
            else if (m_MyMemberInfo.kMemberState == _enGuildMemberState.eGuildMemberState_SubCaptain || // 부길드장.
                m_MyMemberInfo.kMemberState == _enGuildMemberState.eGuildMemberState_AbleSubCaptain)   
            {
                m_GuildSubCaptainButtonObj.SetActive(true);
                m_GuildSubCaptainLeaveButton.SetActive(true);
                m_GuildSubCaptainModifyButton.SetActive(true);

                if (isRequestMember == true)
                {
                    UserInfo.Instance.otherNew.GuildAlram = _enGuildAlram.eGuildNewMark_NewMember;
                }
            }
            else                                                                                       // 길드원.
            {
                m_GuildLeaveButton.SetActive(true);

                UserInfo.Instance.otherNew.GuildAlram = _enGuildAlram.eGuildNewMark_None;
            }

            SetCommissionCheckButton(m_MyMemberInfo.kMemberState);
        }
    }

    public void SetMyGuildMember(ulong kGuildKey, _vGuildMembers vMembers)
    {
        Clear();

        int iCount = vMembers.Count;
        for (int i = 0; i < iCount; ++i)
        {
            CGuildMember memberInfo = vMembers[i];
            if (memberInfo == null)
                continue;

            if (memberInfo.kMemberState == _enGuildMemberState.eGuildMemberState_None || memberInfo.kMemberState == _enGuildMemberState.eGuildMemberState_Request)
                continue;

            GuildInformationItem memberItem = UIResourceMgr.CreatePrefab<GuildInformationItem>(BUNDLELIST.PREFABS_UI_GUILD, m_GuildMemberGrid.transform, "GuildInformationItem");
            memberItem.Init(enGuildInfo_WindowType.MyGuild, kGuildKey, memberInfo);
            m_MemberList.Add(memberItem);
        }

        SortMemberList();
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

        m_GuildMemberGrid.sorting = UIGrid.Sorting.Custom;
        m_GuildMemberGrid.onCustomSort = UtilFunc.SortByNumber;

        //m_GuildMemberGrid.sorting = UIGrid.Sorting.Alphabetic;
        m_GuildMemberGrid.Reposition();
        m_GuildMemberScrollView.ResetPosition();
    }

    /// <summary>
    /// 자유가입 인지 승인가입 라벨 셋팅.
    /// </summary>
    private void SetJoinMethodButton(_enGuildJoinMethod type)
    {
        m_JoinType = type;

        if (type == _enGuildJoinMethod.eGuildJoinMethod_Free)
        {
            m_GuildJoinMethodLabel.text = StringTableManager.GetData(6233);
        }
        else if (type == _enGuildJoinMethod.eGuildJoinMethod_Approval)
        {
            m_GuildJoinMethodLabel.text = StringTableManager.GetData(6239);
        }
    }

    private void SetNoticeOrDescButton(enNoticeAndDesc type)
    {
        m_NoticeAndDescType = type;

        if (type == enNoticeAndDesc.Notice)
        {
            m_NoticeOrDescLabel.text = m_GuildDetailInfo.kGuildNotice;

            m_NoticeButton.state = ButtonState.On;
            m_DescButton.state = ButtonState.Off;
        }
        else if (type == enNoticeAndDesc.Desc)
        {
            m_NoticeOrDescLabel.text = m_GuildDetailInfo.kGuildDesc;

            m_NoticeButton.state = ButtonState.Off;
            m_DescButton.state = ButtonState.On;
        }
    }

    private void SetCommissionCheckButton(_enGuildMemberState state)
    {
        if (state == _enGuildMemberState.eGuildMemberState_AbleMember || state == _enGuildMemberState.eGuildMemberState_AbleSubCaptain)
        {
            m_CommissionCheckButton.state = ButtonState.On;
        }
        else
        {
            m_CommissionCheckButton.state = ButtonState.Off;
        }
    }

    /// <summary>
    /// 길드 가입요청 수락& 거절 패킷 받음.
    /// </summary>
    public void GuildJoinRequestAnswer(_stGuildJoinRequestAnswerAck stAck, CGuildMember member, CGuildDetail GuildDetailInfo)
    {
        int iCount = m_MemberList.Count;
        for (int i = 0; i < iCount; ++i)
        {
            GuildInformationItem infoItem = m_MemberList[i];
            if (infoItem == null)
                continue;

            if (infoItem.MemberInfo.kCharNo == stAck.kDestCharNo)
            {
                DestroyImmediate(infoItem.gameObject);
                m_MemberList.Remove(infoItem);
                break;
            }
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

        // 6587    {0} / {1} 명
        m_TodayAttendanceCountLabel.text = string.Format(StringTableManager.GetData(6587), GuildDetailInfo.kAttendanceCount, GuildDetailInfo.kCurrMemberCount);

        SortMemberList();
    }

    /// <summary>
    /// 길드 추방 패킷 받음(길드장 전용).
    /// </summary>
    public void GuildOrderLeave(_stGuildOrderLeaveAck stAck)
    {
        string strCharName = string.Empty;

        int iCount = m_MemberList.Count;
        for (int i = 0; i < iCount; ++i)
        {
            GuildInformationItem member = m_MemberList[i];
            if (member == null)
                continue;

            if (member.MemberInfo.kCharNo == stAck.kDestCharNo)
            {
                strCharName = member.MemberInfo.kCharName;

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

        m_GuildMemberGrid.Reposition();
        m_GuildMemberScrollView.ResetPosition();
    }

    /// <summary>
    /// 길드 마크 변경 패킷 받음.
    /// </summary>
    /// <param name="kNewGuildMark"></param>
    public void GuildChangeMark(byte kNewGuildMark)
    {
        m_GuildMarkSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_GUILDEMBLEM, string.Format("GuildEmblem{0}", kNewGuildMark.ToString("D2")));
    }

    /// <summary>
    /// 내 길드 위임상태 변경 패킷 받음.
    /// </summary>
    /// <param name="stAck"></param>
    public void GuildMyMemberStateChange(_stGuildMemberStateChangeAck stAck)
    {
        m_MyMemberInfo.kMemberState = stAck.kNextState;
        SetCommissionCheckButton(stAck.kNextState);
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

        SetJoinMethodButton(m_GuildDetailInfo.kJoinMethod);
        SetNoticeOrDescButton(m_NoticeAndDescType);
    }

    public void RecvGuildAttendance()
    {
        //_TributeButton.SetNew(UserInfo.Instance.IsGuildAttendance);

        if (_GuildGoddnessPopupWindow != null)
        {
            _GuildGoddnessPopupWindow.CloseUI();
        }
    }

    private void OpenGuildRaidWindow(_stGuildRaidInfoAck stAck)
    {
        if (_GuildRaidWindow == null)
        {
            _GuildRaidWindow = UIResourceMgr.CreatePrefab<GuildRaidWindow>(BUNDLELIST.PREFABS_UI_GUILDRAID, transform, "GuildRaidWindow");
            _GuildRaidWindow.Init();

            UIControlManager.instance.AddWindow(enUIType.GUILDRAID, _GuildRaidWindow);
        }

        _GuildRaidWindow.OpenUI();
        _GuildRaidWindow.SetRecvData(stAck);
    }

    private void ReOpenGuildRaidWindow(_stGuildRaidInfoAck stAck)
    {
        OpenGuildRaidWindow(stAck);

        if (GameSceneManager.Instance.UIOpenType == SceneUIOpenType.GuildRaidReady)
        {
            _GuildRaidWindow.ReopenGuildRaidReady();
        }
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    /// <summary>
    /// 공물.
    /// </summary>
    /// <param name="go"></param>
    private void OnTribute(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        if (_GuildGoddnessPopupWindow == null)
        {
            _GuildGoddnessPopupWindow = UIResourceMgr.CreatePrefab<GuildGoddness>(BUNDLELIST.PREFABS_UI_GUILD, transform, "GuildGoddness");
        }

        _GuildGoddnessPopupWindow.Init(m_GuildDetailInfo);
        _GuildGoddnessPopupWindow.OpenUI();
    }

    private void OnGuildRaid(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        CNetManager.Instance.GuildRaidStub.OnGuildRaidInfo = OpenGuildRaidWindow;

        _stGuildRaidInfoReq stGuildRaidInfoReq = new _stGuildRaidInfoReq();
        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildRaidProxy.GuildRaidInfo, stGuildRaidInfoReq, typeof(_stGuildRaidInfoAck));
    }

    public void OnReopenGuildRaid(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        CNetManager.Instance.GuildRaidStub.OnGuildRaidInfo = ReOpenGuildRaidWindow;

        _stGuildRaidInfoReq stGuildRaidInfoReq = new _stGuildRaidInfoReq();
        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildRaidProxy.GuildRaidInfo, stGuildRaidInfoReq, typeof(_stGuildRaidInfoAck));
    }

    private void OnNotice(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        SetNoticeOrDescButton(enNoticeAndDesc.Notice);
    }

    private void OnDesc(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        SetNoticeOrDescButton(enNoticeAndDesc.Desc);
    }

    /// <summary>
    /// 길드 탈퇴 버튼.
    /// </summary>
    /// <param name="go"></param>
    private void OnGuildLeave(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        string str = string.Format(StringTableManager.GetData(6257), m_GuildDetailInfo.kGuildName);
        SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.YesNo, StringTableManager.GetData(4300), str, GuildLeaveReq);
    }

    /// <summary>
    /// 길드 탈퇴 패킷 보냄.
    /// </summary>
    /// <param name="state"></param>
    private void GuildLeaveReq(enSystemMessageFlag state)
    {
        if (state == enSystemMessageFlag.NO)
            return;

        _stGuildLeaveReq stGuildLeaveReq = new _stGuildLeaveReq();
        stGuildLeaveReq.kGuildKey = m_GuildDetailInfo.kGuildKey;

        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildLeave, stGuildLeaveReq, typeof(_stGuildLeaveAck));
    }

    /// <summary>
    /// 길드장 혹은 부길드장 위임을 받을수 있는 버튼.
    /// </summary>
    /// <param name="go"></param>
    private void OnCommissionCheck(GameObject go)
    {
        if (m_bCheckState == false)
            return;

        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        _stGuildMemberStateChangeReq stGuildMemberStateChangeReq = new _stGuildMemberStateChangeReq();

        if (m_CommissionCheckButton.state == ButtonState.Off)
        {
            if (m_MyMemberInfo.kMemberState == _enGuildMemberState.eGuildMemberState_Member)
            {
                stGuildMemberStateChangeReq.kNextState = _enGuildMemberState.eGuildMemberState_AbleMember;
            }
            else if(m_MyMemberInfo.kMemberState == _enGuildMemberState.eGuildMemberState_SubCaptain)
            {
                stGuildMemberStateChangeReq.kNextState = _enGuildMemberState.eGuildMemberState_AbleSubCaptain;
            }
        }
        else if (m_CommissionCheckButton.state == ButtonState.On)
        {
            if (m_MyMemberInfo.kMemberState == _enGuildMemberState.eGuildMemberState_AbleMember)
            {
                stGuildMemberStateChangeReq.kNextState = _enGuildMemberState.eGuildMemberState_Member;
            }
            else if (m_MyMemberInfo.kMemberState == _enGuildMemberState.eGuildMemberState_AbleSubCaptain)
            {
                stGuildMemberStateChangeReq.kNextState = _enGuildMemberState.eGuildMemberState_SubCaptain;
            }
        }

        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildMyMemberStateChange, stGuildMemberStateChangeReq, typeof(_stGuildMemberStateChangeAck));

        m_bCheckState = false;
    }

    private void OnAttendanceReward(GameObject go)
    {
        if (_attendanceRewardButton.state == ButtonState.Off)
            return;

        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        _stGuildAttendanceRecvKeyReq GuildAttendanceRecvKeyReq = new _stGuildAttendanceRecvKeyReq();
        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildAttendanceRecvKey, GuildAttendanceRecvKeyReq, typeof(_stGuildAttendanceRecvKeyAck));
    }

    private void TestButton(GameObject go)
    {
        SystemPopupWindow.Instance.OpenSystemServerPopup(enSystemPopupType.Ok, StringTableManager.GetData(4300), "TEST\n날짜가 변경되어 길드 정보를 갱신합니다.");

        _stGuildRecommendListReq req = new _stGuildRecommendListReq();
        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildRecommendList, req, typeof(_stGuildRecommendListAck));
    }
}