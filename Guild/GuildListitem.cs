using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public class GuildListitem : MonoBehaviour
{
    //===================================================================================
    //
    // Enum
    //
    //===================================================================================
    public enum enGuildListItem_Type
    {
        JoinRequest,
        Recommend,
    }

    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UILabel m_GuildNameLabel;
    [SerializeField] private UILabel m_CaptainNameLabel;
    [SerializeField] private UILabel m_GuildLevelLabel;


    [SerializeField] private UI2DSprite m_GuildMarkSprite;

    [SerializeField] private GameObject m_JoinRequestMemberObj;             // 가입한 길드 목록 멤버표시.
    [SerializeField] private UILabel m_JoinRequestMemberCountLabel;

    [SerializeField] private GameObject m_RecommendMemberObj;
    [SerializeField] private UILabel m_RecommendMemberCountLabel;

    [SerializeField] private UILabel m_JoinMethodLabel;

    [SerializeField] private GameObject m_GuildJoinCancleButton;             // 길드가입 취소버튼.
    [SerializeField] private GameObject m_GuildInfoButton;                   // 정보보기 버튼.
    [SerializeField] private GameObject m_GuildJoinApplicationButton;        // 길드가입 신청버튼.

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private GuildList m_Parent = null;

    private CGuild m_GuildInfo = null;

    private ulong m_kGuildKey = 0;
    public ulong kGuildKey { get { return m_kGuildKey; } }

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    private void Awake()
    {
        if (m_GuildJoinCancleButton != null) UIEventListener.Get(m_GuildJoinCancleButton).onClick = OnGuildJoinCancle;
        if (m_GuildInfoButton != null) UIEventListener.Get(m_GuildInfoButton).onClick = OnGuildInfo;
        if (m_GuildJoinApplicationButton != null) UIEventListener.Get(m_GuildJoinApplicationButton).onClick = OnGuildJoinApplication;
    }

    private void OnDestroy()
    {
        if(m_GuildJoinCancleButton != null) UIEventListener.Get(m_GuildJoinCancleButton).onClick -= OnGuildJoinCancle;
        if (m_GuildInfoButton != null) UIEventListener.Get(m_GuildInfoButton).onClick -= OnGuildInfo;
        if (m_GuildJoinApplicationButton != null) UIEventListener.Get(m_GuildJoinApplicationButton).onClick -= OnGuildJoinApplication;
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init(GuildList guildList)
    {
        m_Parent = guildList;

        m_GuildNameLabel.gameObject.SetActive(true);
        m_CaptainNameLabel.gameObject.SetActive(true);
        m_GuildLevelLabel.gameObject.SetActive(true);
        m_GuildMarkSprite.gameObject.SetActive(true);
    }

    private void SetMiddleObj_And_Button(enGuildListItem_Type type)
    {
        m_JoinRequestMemberObj.SetActive(false);
        m_RecommendMemberObj.SetActive(false);

        m_GuildJoinCancleButton.SetActive(false);
        m_GuildInfoButton.SetActive(false);
        m_GuildJoinApplicationButton.SetActive(false);

        if (type == enGuildListItem_Type.JoinRequest)
        {
            m_JoinRequestMemberObj.SetActive(true);

            m_GuildJoinCancleButton.SetActive(true);
            m_GuildInfoButton.SetActive(true);

            m_JoinRequestMemberCountLabel.text = string.Format("{0} / {1}", m_GuildInfo.kCurrMemberCount, m_GuildInfo.kMaxMemberCount);
        }
        else if (type == enGuildListItem_Type.Recommend)
        {
            m_RecommendMemberObj.SetActive(true);

            m_GuildInfoButton.SetActive(true);

            if (UserInfo.Instance.GuildKey == 0)
            {
                m_GuildJoinApplicationButton.SetActive(true);
            }

            m_RecommendMemberCountLabel.text = string.Format("{0} / {1}", m_GuildInfo.kCurrMemberCount, m_GuildInfo.kMaxMemberCount);
        }
    }

    public void SetGuildInfo(CGuild guild, enGuildListItem_Type type)
    {
        m_GuildInfo = guild;
        m_kGuildKey = m_GuildInfo.kGuildKey;

        m_GuildMarkSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_GUILDEMBLEM, string.Format("GuildEmblem{0}", m_GuildInfo.kGuildMark.ToString("D2")));

        m_GuildNameLabel.text = m_GuildInfo.kGuildName;
        m_CaptainNameLabel.text = guild.kGuildCaptainName;

        DATA_GUILD_MAIN GuildMainData = CDATA_GUILD_MAIN.Get(m_GuildInfo.kGuildLevel);
        if (GuildMainData != null)
        {
            // string num : 12 -> LV
            m_GuildLevelLabel.text = string.Format("{0} {1}", StringTableManager.GetData(12), GuildMainData.iGuildLv);
        }

        if (guild.kJoinMethod == _enGuildJoinMethod.eGuildJoinMethod_Free)
        {
            m_JoinMethodLabel.text = StringTableManager.GetData(6233);
        }
        else if (guild.kJoinMethod == _enGuildJoinMethod.eGuildJoinMethod_Approval)
        {
            m_JoinMethodLabel.text = StringTableManager.GetData(6239);
        }

        SetMiddleObj_And_Button(type);
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    /// <summary>
    /// 길드 정보 보기 버튼.
    /// </summary>
    /// <param name="go"></param>
    private void OnGuildInfo(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        m_Parent.SetSelectGuildInfo(m_GuildInfo);

        _stGuildDetailInfoReq stGuildDetailInfoReq = new _stGuildDetailInfoReq();
        stGuildDetailInfoReq.kGuildKey = m_kGuildKey;

        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildDetailInfo, stGuildDetailInfoReq, typeof(_stGuildDetailInfoAck));
    }

    /// <summary>
    /// 길드가입 신청버튼.
    /// </summary>
    /// <param name="go"></param>
    private void OnGuildJoinApplication(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);
        if (m_GuildInfo.kJoinMethod == _enGuildJoinMethod.eGuildJoinMethod_Free)
        {
            string str = string.Format(StringTableManager.GetData(6234), m_GuildInfo.kGuildName);
            SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.YesNo, StringTableManager.GetData(4300), str, GuildJoinRequest);
        }
        else if (m_GuildInfo.kJoinMethod == _enGuildJoinMethod.eGuildJoinMethod_Approval)
        {
            string str = string.Format(StringTableManager.GetData(6240), m_GuildInfo.kGuildName);
            SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.YesNo, StringTableManager.GetData(4300), str, GuildJoinRequest);
        }
    }

    /// <summary>
    /// 길드가입 신청 패킷보냄.
    /// </summary>
    private void GuildJoinRequest(enSystemMessageFlag state)
    {
        if (state == enSystemMessageFlag.NO)
            return;

        // 길드가입 형태가 승인 가입이면 가입조건을 체크한다.
        if (m_GuildInfo.kJoinMethod == _enGuildJoinMethod.eGuildJoinMethod_Approval && m_Parent.GuildJoinCheck(m_GuildInfo) == false)
                return;

        m_Parent.SetSelectGuildInfo(m_GuildInfo);

        _stGuildJoinRequestReq stGuildJoinRequestReq = new _stGuildJoinRequestReq();
        stGuildJoinRequestReq.kGuildKey = m_GuildInfo.kGuildKey;

        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildJoinRequest, stGuildJoinRequestReq, typeof(_stGuildJoinRequestAck));
    }

    /// <summary>
    /// 길드가입 요청 취소버튼.
    /// </summary>
    /// <param name="go"></param>
    private void OnGuildJoinCancle(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        string str = string.Format(StringTableManager.GetData(6247), m_GuildInfo.kGuildName);
        SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.YesNo, StringTableManager.GetData(4300), str, GuildJoinCancleRequest);

    }
    
    private void GuildJoinCancleRequest(enSystemMessageFlag state)
    {
        if (state == enSystemMessageFlag.NO)
            return;

        m_Parent.SetSelectGuildInfo(m_GuildInfo);

        _stGuildJoinRequestCancelReq stGuildJoinRequestCancelReq = new _stGuildJoinRequestCancelReq();
        stGuildJoinRequestCancelReq.kGuildKey = m_kGuildKey;

        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildJoinRequestCancel, stGuildJoinRequestCancelReq, typeof(_stGuildJoinRequestCancelAck));
    }
}
