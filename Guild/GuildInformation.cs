using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using DGL_DATA_READER;

public class GuildInformation : UIWindow
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UILabel m_TitleLabel;

    [SerializeField] private UILabel m_GuildMemberCountLabel;

    [SerializeField] private GameObject m_CloseButton;

    [SerializeField] private UIScrollView m_GuildMemberScrollView;
    [SerializeField] private UIGrid m_GuildMemberGrid;
    
    [SerializeField] private UI2DSprite m_GuildMarkSprite;

    [SerializeField] private UILabel m_GuildLevelLabel;
    [SerializeField] private UILabel m_GuildNameLabel;
    [SerializeField] private UILabel m_GuildCaptainLabel;
    [SerializeField] private UILabel m_GuildJoinMethodLabel;

    [SerializeField] private UILabel m_GuildDescLabel;

    [SerializeField] private UILabel m_GuildBenefitTitleLabel;
    [SerializeField] private UILabel m_GuildBenefitLabel;

    [SerializeField] private GameObject m_GuildJoinRequestButton;
    [SerializeField] private UILabel m_GuildJoinRequestButtonLabel;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private GuildList m_Parent = null;

    private CGuildDetail m_GuildDetailInfo = null;

    private List<GuildInformationItem> m_MemberList = new List<GuildInformationItem>();

    private int m_iGuildWaitingCount = 0;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    protected override void Awake()
    {
    }

    protected override void Start()
    {
    }

    public override void Init()
    {
    }

    protected override void Update()
    {
    }

    protected override void OnEnable()
    {
        if (m_CloseButton != null) UIEventListener.Get(m_CloseButton).onClick = OnClickBack;
        if (m_GuildJoinRequestButton != null) UIEventListener.Get(m_GuildJoinRequestButton).onClick = GuildJoinRequestReq;
    }

    protected override void OnDisable()
    {
    }

    protected override void OnDestroy()
    {
        if (m_CloseButton != null) UIEventListener.Get(m_CloseButton).onClick -= OnClickBack;
        if (m_GuildJoinRequestButton != null) UIEventListener.Get(m_GuildJoinRequestButton).onClick -= GuildJoinRequestReq;

        Clear();
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

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init(GuildList guildList)
    {
        m_Parent = guildList;

        m_iGuildWaitingCount = (int)CDATA_FIXED_CONSTANTS.Get(DATA_FIXED_CONSTANTS._enConstant.Guild_Waiting_Count).Value;

        m_TitleLabel.text = StringTableManager.GetData(6248);
        m_GuildBenefitTitleLabel.text = StringTableManager.GetData(6250);
        m_GuildJoinRequestButtonLabel.text = StringTableManager.GetData(6252);
        m_GuildBenefitLabel.text = StringTableManager.GetData(8527);

        if (UserInfo.Instance.GuildKey != 0)
        {
            // 가입한 길드가 있다.
            m_GuildJoinRequestButton.SetActive(false);
        }
        else
        {
            // 가입한 길드가 없다.
            m_GuildJoinRequestButton.SetActive(true);
        }
    }

    /// <summary>
    /// 가입하지 않은 길드 정보 패킷 받음.
    /// </summary>
    /// <param name="stAck"></param>
    public void SetGuildInfomation(_stGuildDetailInfoAck stAck)
    {
        Clear();

        m_GuildDetailInfo = stAck.kGuildDetailInfo;

        // 6254 길드원 ({0} / {1})
        m_GuildMemberCountLabel.text = string.Format(StringTableManager.GetData(6254), m_GuildDetailInfo.kCurrMemberCount, m_GuildDetailInfo.kMaxMemberCount);

        SetOtherGuildInfo(m_GuildDetailInfo);
        SetOtherGuildMember(m_GuildDetailInfo.kGuildKey, m_GuildDetailInfo.vMembers);
    }

    private void SetOtherGuildInfo(CGuildDetail kGuildDetailInfo)
    {
        m_GuildMarkSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_GUILDEMBLEM, string.Format("GuildEmblem{0}", m_GuildDetailInfo.kGuildMark.ToString("D2")));
        m_GuildNameLabel.text = m_GuildDetailInfo.kGuildName;

        DATA_GUILD_MAIN GuildMainData = CDATA_GUILD_MAIN.Get(m_GuildDetailInfo.kGuildLevel);
        if (GuildMainData != null)
        {
            // string num : 12 -> LV
            m_GuildLevelLabel.text = string.Format("{0} {1}", StringTableManager.GetData(12), GuildMainData.iGuildLv);
        }

        CGuildMember captainInfo = null;
        int iCount = m_GuildDetailInfo.vMembers.Count;
        for (int i = 0; i < iCount; ++i)
        {
            CGuildMember memberInfo = m_GuildDetailInfo.vMembers[i];
            if (memberInfo == null)
                continue;

            if (memberInfo.kMemberState == _enGuildMemberState.eGuildMemberState_Captain)
            {
                captainInfo = memberInfo;
            }
        }

        if (captainInfo != null)
        {
            m_GuildCaptainLabel.text = captainInfo.kCharName;
        }

        if (m_GuildDetailInfo.kJoinMethod == _enGuildJoinMethod.eGuildJoinMethod_Free)
        {
            m_GuildJoinMethodLabel.text = StringTableManager.GetData(6233);
        }
        else if (m_GuildDetailInfo.kJoinMethod == _enGuildJoinMethod.eGuildJoinMethod_Approval)
        {
            m_GuildJoinMethodLabel.text = StringTableManager.GetData(6239);
        }

        m_GuildDescLabel.text = m_GuildDetailInfo.kGuildDesc;
    }

    private void SetOtherGuildMember(ulong kGuildKey, _vGuildMembers vMembers)
    {
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

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    /// <summary>
    /// 길드가입 신청버튼.
    /// </summary>
    /// <param name="go"></param>
    private void GuildJoinRequestReq(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        if (m_GuildDetailInfo.kJoinMethod == _enGuildJoinMethod.eGuildJoinMethod_Free)
        {
            string str = string.Format(StringTableManager.GetData(6234), m_GuildDetailInfo.kGuildName);
            SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.YesNo, StringTableManager.GetData(4300), str, GuildJoinRequestReq);
        }
        else if (m_GuildDetailInfo.kJoinMethod == _enGuildJoinMethod.eGuildJoinMethod_Approval)
        {
            string str = string.Format(StringTableManager.GetData(6240), m_GuildDetailInfo.kGuildName);
            SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.YesNo, StringTableManager.GetData(4300), str, GuildJoinRequestReq);
        }
    }

    /// <summary>
    /// 길드가입 신청 패킷보냄.
    /// </summary>
    private void GuildJoinRequestReq(enSystemMessageFlag state)
    {
        if (state == enSystemMessageFlag.NO)
            return;

        if (m_GuildDetailInfo.kJoinMethod == _enGuildJoinMethod.eGuildJoinMethod_Approval && m_Parent.GuildJoinCheck(m_GuildDetailInfo) == false)
            return;

        _stGuildJoinRequestReq stGuildJoinRequestReq = new _stGuildJoinRequestReq();
        stGuildJoinRequestReq.kGuildKey = m_GuildDetailInfo.kGuildKey;

        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildJoinRequest, stGuildJoinRequestReq, typeof(_stGuildJoinRequestAck));
    }
}