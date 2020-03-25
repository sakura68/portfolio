using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public class GuildList : MonoBehaviour
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UILabel m_JoinRequestGuildCountLabel;
    
    [SerializeField] private UIInput m_GuildSearchInput;
    [SerializeField] private GameObject m_SearchButton;

    [SerializeField] private UILabel m_RecommendGuildLabel;
    [SerializeField] private GameObject m_RecommendGuildRefreshButton;

    [SerializeField] private UIScrollView m_JoinRequestScrollView;
    [SerializeField] private UIGrid m_JoinRequestGrid;

    [SerializeField] private UIScrollView m_RecommendScrollView;
    [SerializeField] private UIGrid m_RecommendGrid;

    [SerializeField] private UILabel _NoJoinRequestLabel;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private List<GuildListitem> m_JoinRequestGuildList = new List<GuildListitem>();                                    // 가입신청 길드 목록.
    private List<GuildListitem> m_RecommendGuildList = new List<GuildListitem>();                                      // 추천길드 목록.

    private GuildInformation m_GuildInformation = null;

    private CGuild m_SelectGuildInfo = null;
    public CGuild SelectGuildInfo { get { return m_SelectGuildInfo; } }

    private int m_iGuildSearchStringCount = 0;
    private int m_iGuildWaitingCount = 0;

    private bool m_bCheckRefresh = true;
    private float m_fElapsedTime = 3.0f;
    private float m_fCheckTime = 3.0f;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    private void Awake()
    {
        if (m_SearchButton != null) UIEventListener.Get(m_SearchButton).onClick = OnSearch;
        if (m_RecommendGuildRefreshButton != null) UIEventListener.Get(m_RecommendGuildRefreshButton).onClick = OnRefresh;
    }

    private void OnDestroy()
    {
        if (m_SearchButton != null) UIEventListener.Get(m_SearchButton).onClick -= OnSearch;
        if (m_RecommendGuildRefreshButton != null) UIEventListener.Get(m_RecommendGuildRefreshButton).onClick -= OnRefresh;

        Clear();
    }

    private void Update()
    {
        if (m_bCheckRefresh == false)
        {
            m_fElapsedTime -= Time.deltaTime;
            if (m_fElapsedTime <= 0.0f)
            {
                m_fElapsedTime = m_fCheckTime;
                m_bCheckRefresh = true;
            }
        }
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init()
    {
        m_iGuildSearchStringCount = (int)CDATA_FIXED_CONSTANTS.Get(DATA_FIXED_CONSTANTS._enConstant.Guild_SearchString_Count).Value;
        m_iGuildWaitingCount = (int)CDATA_FIXED_CONSTANTS.Get(DATA_FIXED_CONSTANTS._enConstant.Guild_Waiting_Count).Value;

        m_GuildSearchInput.defaultText = StringTableManager.GetData(6229);  // 길드를 검색하세요.
        m_GuildSearchInput.characterLimit = m_iGuildSearchStringCount;

        m_RecommendGuildLabel.text = StringTableManager.GetData(6228); // 추천길드.

        _NoJoinRequestLabel.gameObject.SetActive(true);
        // 6903    가입 신청 대기중인 길드가 없습니다.
        _NoJoinRequestLabel.text = StringTableManager.GetData(6903);

        m_JoinRequestGuildCountLabel.text = string.Format(StringTableManager.GetData(6244));
    }

    private void Clear()
    {
        JoinRequestListClear();
        RecommendListClear();
    }

    private void JoinRequestListClear()
    {
        int iCount = m_JoinRequestGuildList.Count;
        for (int i = 0; i < iCount; ++i)
        {
            GuildListitem item = m_JoinRequestGuildList[i];
            if (item == null)
                continue;

            DestroyImmediate(item.gameObject);
        }

        m_JoinRequestGuildList.Clear();
    }

    private void RecommendListClear()
    {
        int iCount = m_RecommendGuildList.Count;
        for (int i = 0; i < iCount; ++i)
        {
            GuildListitem item = m_RecommendGuildList[i];
            if (item == null)
                continue;

            DestroyImmediate(item.gameObject);
        }

        m_RecommendGuildList.Clear();
    }

    public void CreateJoinRequestGuildList(_stGuildRecommendListAck stAck)
    {
        // 길드 가입 신청 개수의 리스팅 순서는 초성 기준으로 "영문 ABC -> 한글 가나다" 순으로 정렬 된다.
        stAck.vJoinRequestGuildList.Sort((a, b) => a.kGuildName.CompareTo(b.kGuildName));

        JoinRequestListClear();

        int iJoinRequestCount = stAck.vJoinRequestGuildList.Count;
        m_JoinRequestGuildCountLabel.text = string.Format("{0}  {1} / {2}", StringTableManager.GetData(6244), iJoinRequestCount, m_iGuildWaitingCount);

        if (iJoinRequestCount > 0)
        {
            _NoJoinRequestLabel.gameObject.SetActive(false);

            // 내가 가입 신청한 길드.
            for (int i = 0; i < iJoinRequestCount; ++i)
            {
                CGuild JoinRequestGuild = stAck.vJoinRequestGuildList[i];
                if (JoinRequestGuild == null)
                    continue;

                GuildListitem JoinRequestItem = UIResourceMgr.CreatePrefab<GuildListitem>(BUNDLELIST.PREFABS_UI_GUILD, m_JoinRequestGrid.transform, "GuildListitem");
                JoinRequestItem.Init(this);
                JoinRequestItem.SetGuildInfo(JoinRequestGuild, GuildListitem.enGuildListItem_Type.JoinRequest);

                m_JoinRequestGuildList.Add(JoinRequestItem);
            }
        }
        else
        {
            _NoJoinRequestLabel.gameObject.SetActive(true);
        }

        ResetPosition();
    }

    public void CreateRecommendGuildList(_stGuildRecommendListAck stAck)
    {
        stAck.vRecommendGuildList.Sort((a, b) => a.kGuildName.CompareTo(b.kGuildName));

        RecommendListClear();

        // 추천 길드.
        ulong GuildKey = UserInfo.Instance.GuildKey;
        int iRecommendCount = stAck.vRecommendGuildList.Count;
        for (int i = 0; i < iRecommendCount; ++i)
        {
            CGuild RecommendGuild = stAck.vRecommendGuildList[i];
            if (RecommendGuild == null)
                continue;

            if (GuildKey == RecommendGuild.kGuildKey)
                continue;

            GuildListitem RecommendItem = UIResourceMgr.CreatePrefab<GuildListitem>(BUNDLELIST.PREFABS_UI_GUILD, m_RecommendGrid.transform, "GuildListitem");
            RecommendItem.Init(this);
            RecommendItem.SetGuildInfo(RecommendGuild, GuildListitem.enGuildListItem_Type.Recommend);

            m_RecommendGuildList.Add(RecommendItem);
        }

        ResetPosition();
    }

    private bool GuildNameCheck(string strName, out string error)
    {
        bool bCompleted = true;
        error = string.Empty;

        int iGuildNameLength = strName.Length;

        if (iGuildNameLength < 1) // 1글자 미만.
        {
            // 사용할 길드 이름을 입력해주세요
            error = StringTableManager.GetData(6262);
            bCompleted = false;
        }
        else if (iGuildNameLength > m_iGuildSearchStringCount)  // 15글자 넘어가면.
        {
            // 길드 이름이 최대 글자수를 초과 하었습니다.
            error = StringTableManager.GetData(6514);
            bCompleted = false;
        }

        if (UtilFunc.IsEmptyCharacters(strName) == true)        // 공백체크.
        {
            // 특수문자, 금칙어는 사용할 수 없습니다.
            error = StringTableManager.GetData(6231);
            bCompleted = false;
        }

        if (UtilFunc.IsSpecialAndFiterCharacters(strName) == true) // 특수기호와 금칙어 사용 여부.
        {
            // 특수문자, 금칙어는 사용할 수 없습니다.
            error = StringTableManager.GetData(6231);
            bCompleted = false;
        }

        return bCompleted;
    }

    /// <summary>
    /// 길드가입 체크.
    /// </summary>
    /// <returns></returns>
    public bool GuildJoinCheck(CGuild GuildInfo)
    {
        int iJoinRequestCount = m_JoinRequestGuildList.Count;
        if (iJoinRequestCount >= m_iGuildWaitingCount)
        {
            // 더 이상 길드 가입 신청을 할 수 없습니다.
            StartCoroutine(GuildJoinCheckPopup(6242));
            return false;
        }

        for (int i = 0; i < iJoinRequestCount; ++i)
        {
            GuildListitem item = m_JoinRequestGuildList[i];
            if (item == null)
                continue;

            if (item.kGuildKey == GuildInfo.kGuildKey)
            {
                // 이미 길드 가입을 신청 하였습니다.
                StartCoroutine(GuildJoinCheckPopup(6253));
                return false;
            }
        }

        // 이미 가입 신청한 길드면 추천길드 목록에서 제거.
        int iRecommendCount = m_RecommendGuildList.Count;
        for (int i = 0; i < iRecommendCount; ++i)
        {
            GuildListitem item = m_RecommendGuildList[i];
            if (item == null)
                continue;

            if(item.kGuildKey == GuildInfo.kGuildKey)
            {
                DestroyImmediate(item.gameObject);
                m_RecommendGuildList.Remove(item);

                ResetPosition();
                break;

            }
        }

        return true;
    }

    public IEnumerator GuildJoinCheckPopup(int iIndex)
    {
        yield return new WaitForSeconds(0.1f);

        SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(4300), StringTableManager.GetData(iIndex));
    }

    /// <summary>
    /// 길드가입 신청 패킷 받음.
    /// </summary>
    public void GuildJoinRequest(_stGuildJoinRequestAck stAck)
    {
        // 추천길드 목록에서 가입신청 한 길드를 지움.
        int iCount = m_RecommendGuildList.Count;
        for (int i = 0; i < iCount; ++i)
        {
            GuildListitem item = m_RecommendGuildList[i];
            if (item == null)
                continue;

            if (item.kGuildKey == stAck.kGuildKey)
            {
                DestroyImmediate(item.gameObject);
                m_RecommendGuildList.Remove(item);
                break;
            }
        }

        GuildListitem JoinRequestItem = UIResourceMgr.CreatePrefab<GuildListitem>(BUNDLELIST.PREFABS_UI_GUILD, m_JoinRequestGrid.transform, "GuildListitem");
        JoinRequestItem.Init(this);
        JoinRequestItem.SetGuildInfo(m_SelectGuildInfo, GuildListitem.enGuildListItem_Type.JoinRequest);
        m_JoinRequestGuildList.Add(JoinRequestItem);

        m_JoinRequestGuildCountLabel.text = string.Format("{0}  {1} / {2}", StringTableManager.GetData(6244), m_JoinRequestGuildList.Count, m_iGuildWaitingCount);

        ResetPosition();
    }

    /// <summary>
    /// 길드가입 요청 취소패킷 받음.
    /// </summary>
    /// <param name="stAck"></param>
    public void GuildJoinRequestCancel(_stGuildJoinRequestCancelAck stAck)
    {
        // 가입 신청 대기중인 길드 목록에서 지우고
        int iJoinRequestCount = m_JoinRequestGuildList.Count;
        for (int i = 0; i < iJoinRequestCount; ++i)
        {
            GuildListitem item = m_JoinRequestGuildList[i];
            if (item == null)
                continue;

            if (item.kGuildKey == stAck.kGuildKey)
            {
                DestroyImmediate(item.gameObject);
                m_JoinRequestGuildList.Remove(item);
                break;
            }
        }

        bool bInclude = false;
        int iRecommendCount = m_RecommendGuildList.Count;
        for (int i = 0; i < iRecommendCount; ++i)
        {
            GuildListitem item = m_RecommendGuildList[i];
            if (item == null)
                continue;

            if (item.kGuildKey == stAck.kGuildKey)
            {
                // 추천길드 목록에 포함되어 있다.
                bInclude = true;
                break;
            }
        }

        if (bInclude == false)
        {
            // 추천길드 목록에 없으면 추가.
            GuildListitem RecommendItem = UIResourceMgr.CreatePrefab<GuildListitem>(BUNDLELIST.PREFABS_UI_GUILD, m_RecommendGrid.transform, "GuildListitem");
            RecommendItem.Init(this);
            RecommendItem.SetGuildInfo(m_SelectGuildInfo, GuildListitem.enGuildListItem_Type.Recommend);
            m_RecommendGuildList.Add(RecommendItem);
        }

        m_JoinRequestGuildCountLabel.text = string.Format("{0}  {1} / {2}", StringTableManager.GetData(6244), m_JoinRequestGuildList.Count, m_iGuildWaitingCount);

        ResetPosition();
    }

    public void SetSelectGuildInfo(CGuild GuildInfo)
    {
        m_SelectGuildInfo = GuildInfo;
    }

    /// <summary>
    /// 가입하지 않은 길드 정보 패킷 받음.
    /// </summary>
    /// <param name="stAck"></param>
    public void GuildDetailInfo(_stGuildDetailInfoAck stAck)
    {
        if (m_GuildInformation == null)
        {
            m_GuildInformation = UIResourceMgr.CreatePrefab<GuildInformation>(BUNDLELIST.PREFABS_UI_GUILD, transform, "GuildInformation");
            UIControlManager.instance.AddWindow(enUIType.GUILDINFORMATION, m_GuildInformation);
            m_GuildInformation.Init(this);
        }

        m_GuildInformation.OpenUI();
        m_GuildInformation.SetGuildInfomation(stAck);
    }

    /// <summary>
    /// 가입하지 않은 길드 정보를 요청할때
    /// 길드목록을 갱신하지 않은 상태에서 길드가 해체되었을때.
    /// 가입신청 대기중인 목록과 추천목록에서 길드정보를 삭제한다.
    /// </summary>
    /// <param name="stAck"></param>
    public void GuildDetailInfoDelete(ulong kGuildKey)
    {
        // 가입 신청 대기중인 길드 목록에서 지우고
        int iJoinRequestCount = m_JoinRequestGuildList.Count;
        for (int i = 0; i < iJoinRequestCount; ++i)
        {
            GuildListitem item = m_JoinRequestGuildList[i];
            if (item == null)
                continue;

            if (item.kGuildKey == kGuildKey)
            {
                DestroyImmediate(item.gameObject);
                m_JoinRequestGuildList.Remove(item);
                break;
            }
        }

        int iRecommendCount = m_RecommendGuildList.Count;
        for (int i = 0; i < iRecommendCount; ++i)
        {
            GuildListitem item = m_RecommendGuildList[i];
            if (item == null)
                continue;

            if (item.kGuildKey == kGuildKey)
            {
                DestroyImmediate(item.gameObject);
                m_RecommendGuildList.Remove(item);
                break;
            }
        }

        m_JoinRequestGuildCountLabel.text = string.Format("{0}  {1} / {2}", StringTableManager.GetData(6244), m_JoinRequestGuildList.Count, m_iGuildWaitingCount);

        ResetPosition();
    }

    /// <summary>
    /// 길드 정보창을 열고 자유가입을 눌렀을때 팝업이 살아있어서 닫는걸 한번 호출한다.
    /// </summary>
    public void CloseGuildInfomation()
    {
        UIControlManager.instance.RemoveWindow(enUIType.GUILDINFORMATION, m_GuildInformation);
        //m_GuildInformation.CloseUI();
    }

    private void ResetPosition()
    {
        m_JoinRequestGrid.Reposition();
        m_JoinRequestScrollView.ResetPosition();

        m_RecommendGrid.Reposition();
        m_RecommendScrollView.ResetPosition();
    }

    /// <summary>
    /// 길드 검색 패킷 받음.
    /// </summary>
    public void GuildSearch(_stGuildSearchAck stAck)
    {
        RecommendListClear();

        // 추천길드 목록에 검색 길드 정보 추가.
        GuildListitem RecommendItem = UIResourceMgr.CreatePrefab<GuildListitem>(BUNDLELIST.PREFABS_UI_GUILD, m_RecommendGrid.transform, "GuildListitem");
        RecommendItem.Init(this);
        RecommendItem.SetGuildInfo(stAck.kGuildInfo, GuildListitem.enGuildListItem_Type.Recommend);
        m_RecommendGuildList.Add(RecommendItem);

        ResetPosition();
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    /// <summary>
    /// 길드 검색 요청.
    /// </summary>
    /// <param name="go"></param>
    private void OnSearch(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);


        string strError = string.Empty;
        if (GuildNameCheck(m_GuildSearchInput.value, out strError) == false)
        {
            SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(3954), strError);
            return;
        }

        _stGuildSearchReq stGuildSearchReq = new _stGuildSearchReq();
        stGuildSearchReq.kSearchName = m_GuildSearchInput.value;

        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildSearch, stGuildSearchReq, typeof(_stGuildSearchAck));
    }

    private void OnRefresh(GameObject go)
    {

        if (m_bCheckRefresh == false)
        {
            string str = string.Empty;

            if (m_fElapsedTime >= 1.0f)
            {
                // 6582    {0}초 후 사용 가능합니다.
                str = string.Format(StringTableManager.GetData(6582), (int)m_fElapsedTime);
            }
            else if(m_fElapsedTime < 1.0f)
            {
                // 6582    {0}초 후 사용 가능합니다.
                str = string.Format(StringTableManager.GetData(6582), m_fElapsedTime.ToString("F1"));
            }

            SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(4300), str);
            return;
        }

        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        _stGuildRecommendListReq req = new _stGuildRecommendListReq();
        CNetManager.Instance.SendPacket(CNetManager.Instance.GuildProxy.GuildRecommendList, req, typeof(_stGuildRecommendListAck));

        m_bCheckRefresh = false;
    }
}