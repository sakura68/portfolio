using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public class DispatchWindow : UIWindow
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private GameObject m_CloseButton;                  // 종료버튼

    [SerializeField] private UILabel m_TitleLabel;                      // 파견
    [SerializeField] private UILabel m_TitleContentLabel;               // 임무카드
    
    [SerializeField] private UIScrollView m_DispatchCardScrollView;     // 파견카드 스크롤뷰
    [SerializeField] private UIGrid m_DispatchCardGrid;                 // 파견카드 그리드

    [SerializeField] private GameObject _HelpButton;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private List<DispatchCardItem> m_DispatchCardList = new List<DispatchCardItem>();

    private DispatchTeamWindow m_DispatchTeamWindow = null;
    private DispatchMissionWindow m_DispatchMissionWindow = null;

    private CDispatch m_DispatchRewardInfo = null;

    private readonly int m_iCardCount = 4;

    private CDispatch _DispatchRecvData = null;
    private DATA_DISPATCH _DispatchTableData = null;

    private SimpleHelpTip _SimpleHelpTip = null;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    public override void Init()
    {
        m_TitleLabel.text = StringTableManager.GetData(6722);                   // 6722	파견
        m_TitleContentLabel.text = StringTableManager.GetData(6723);            // 6723	임무 카드

        // 서버에 값이 있는 만큼만 생성하고 없으면 empty 슬롯으로 초기화.
        _stDispatchInfoAck stDispatchInfoAck = UserInfo.Instance.DispatchInfoAck;
        if (stDispatchInfoAck == null)
        {
#if DEBUG_LOG
            Debug.LogError("서버에서 파견정보 안줌!! - UserInfo.Instance.DispatchInfoAck is NULL");
#endif
            return;     // error;
        }

        _vDispatch vDispatch = stDispatchInfoAck.vDispatch;
        if (vDispatch == null)
        {
#if DEBUG_LOG
            Debug.LogError("서버에서 파견정보 안줌!! - stDispatchInfoAck.vDispatch is NULL");
#endif
            return;     // error;
        }

        DestroyDispatchCard();

        int iPanelDepth = m_DispatchCardScrollView.panel.depth;

        vDispatch.Sort((a,b) => a.kTeamIndex.CompareTo(b.kTeamIndex));
        for (int i = 0; i < vDispatch.Count; ++i)
        {
            CDispatch Dispatch = vDispatch[i];
            if (Dispatch == null)
            {
#if DEBUG_LOG
                Debug.LogError(string.Format("서버에서 파견정보 안줌!! - stDispatchInfoAck.vDispatch Number : {0} is NULL", i));
#endif
                continue;
            }

            DispatchCardItem cardItem = UIResourceMgr.CreatePrefab<DispatchCardItem>(BUNDLELIST.PREFABS_UI_DISPATCH, m_DispatchCardGrid.transform, "DispatchCardItem");
            cardItem.Init(Dispatch);

            if (Dispatch.kDispatchState == _enDispatchState.eDispatchState_None)     // 빈 카드 생성.
            {
                cardItem.SetCard(OnClickDispatchCard);
            }
            else                                                                            // 진행중, 성공, 대성공 카드 생성.
            {
                cardItem.SetCard(iPanelDepth);
            }

            m_DispatchCardList.Add(cardItem);
        }

        ResetPosition();

#if TUTORIAL
        SetTutorialButtonEvent();
#endif
    }

    public override void Clear()
    {
    }

    protected override void Awake()
    {
        UIEventListener.Get(m_CloseButton).onClick = OnClickBack;
        if (_HelpButton != null) UIEventListener.Get(_HelpButton).onPress = OnHelpTooltip;

    }

    protected override void OnEnable()
    {
#if GMTOOLSHOP
        UIControlManager.instance.SetChangeWealth(WEB_SHOP_CATEGORY._enWebUI_Category.Key);
#else
        UIControlManager.instance.SetChangeWealth(DATA_SHOP_NEW_CATEGORY._enCategory.Shop_Category_Assets);
#endif

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
        UIEventListener.Get(m_CloseButton).onClick -= OnClickBack;
        if (_HelpButton != null) UIEventListener.Get(_HelpButton).onPress -= OnHelpTooltip;

        DestroyDispatchCard();
    }

    //public override void OpenUI()
    //{
    //    base.OpenUI();
    //}

    //public override void CloseUI()
    //{
    //    base.CloseUI();
    //}

    public override void PurchaseShopRefresh()
    {
        if(m_DispatchTeamWindow != null)
        {
            m_DispatchTeamWindow.Init(_DispatchRecvData, _DispatchTableData);
        }
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    private void OnClickDispatchCard(DispatchCardItem card)
    {
        if (m_DispatchMissionWindow == null)
        {
            m_DispatchMissionWindow = UIResourceMgr.CreatePrefab<DispatchMissionWindow>(BUNDLELIST.PREFABS_UI_DISPATCH, transform, "DispatchMissionWindow");
            m_DispatchMissionWindow.Init(this);

            UIControlManager.instance.AddWindow(enUIType.DISPATCHMISSIONWINDOW, m_DispatchMissionWindow);
        }

        m_DispatchMissionWindow.SetData(card.DispatchRecvData);
        m_DispatchMissionWindow.OpenUI();
    }

    public void OpenDispatchTeamWindow(CDispatch DispatchRecvData, DATA_DISPATCH DispatchTableData)
    {
        if (m_DispatchTeamWindow == null)
        {
            m_DispatchTeamWindow = UIResourceMgr.CreatePrefab<DispatchTeamWindow>(BUNDLELIST.PREFABS_UI_DISPATCH, transform, "DispatchTeamWindow");

            UIControlManager.instance.AddWindow(enUIType.DISPATCHTEAMWINDOW, m_DispatchTeamWindow);
        }

        _DispatchRecvData = DispatchRecvData;
        _DispatchTableData = DispatchTableData;

        m_DispatchTeamWindow.Init(DispatchRecvData, DispatchTableData);
        m_DispatchTeamWindow.OpenUI();
    }

    private void ResetPosition()
    {
        m_DispatchCardGrid.Reposition();
        m_DispatchCardScrollView.ResetPosition();
    }

    private void DestroyDispatchCard()
    {
        for (int i = 0; i < m_DispatchCardList.Count; ++i)
        {
            DestroyImmediate(m_DispatchCardList[i].gameObject);
        }

        m_DispatchCardList.Clear();
    }

    //===================================================================================
    //
    // Packet
    //
    //===================================================================================
    /// <summary>
    /// Rmi_DispatchStart(_stDispatchStartAck stAck) 패킷 받음.
    /// </summary>
    /// <param name="stAck"></param>
    public void DispatchStart(_stDispatchStartAck stAck)
    {
        if(m_DispatchMissionWindow != null)
        {
            m_DispatchMissionWindow.OnClickBack(m_DispatchMissionWindow.gameObject);
            m_DispatchMissionWindow = null;
        }

        if (m_DispatchTeamWindow != null)
        {
            m_DispatchTeamWindow.OnClickBack(m_DispatchTeamWindow.gameObject);
            m_DispatchTeamWindow = null;
        }

        Init();
    }

    /// <summary>
    /// Rmi_DispatchCheck(_stDispatchCheckEndAck stAck) 패킷 받음.
    /// </summary>
    public void SetPreDispatchInfo(CDispatch kDispatch)
    {
        if (m_DispatchRewardInfo != null)
            m_DispatchRewardInfo = null;

        m_DispatchRewardInfo = new CDispatch();
        m_DispatchRewardInfo.vCreatureKey = new _vCreatureKey();

        m_DispatchRewardInfo.vCreatureKey.Clear();

        m_DispatchRewardInfo.kTeamIndex = kDispatch.kTeamIndex;
        m_DispatchRewardInfo.kMissionCategory = kDispatch.kMissionCategory;
        m_DispatchRewardInfo.kDispatchSubID = kDispatch.kDispatchSubID;
        m_DispatchRewardInfo.kDispatchState = kDispatch.kDispatchState;
        m_DispatchRewardInfo.vCreatureKey.AddRange(kDispatch.vCreatureKey);
        m_DispatchRewardInfo.kFriendCreatureID = kDispatch.kFriendCreatureID;
        m_DispatchRewardInfo.kStartTime = kDispatch.kStartTime;
        m_DispatchRewardInfo.kEndTime = kDispatch.kEndTime;

        //Init();
    }

    /// <summary>
    /// Rmi_DispatchReward(_stDispatchRewardAck stAck) 패킷 받음.
    /// </summary>
    /// <param name="stAck"></param>
    public void DispatchReward(_stDispatchRewardAck stAck , DATA_DISPATCH_CATEGORY._enCategory  dispatchMissionCategory , DATA_DISPATCH_ENUM._enDispatchEnum dispatchsubID )
    {
        if(SetDispatchCardEmpty(stAck.kDispatch.kTeamIndex) == false)
        {
#if DEBUG_LOG
            Debug.Log("Rmi_DispatchReward - 파견카드를 빈 카드로 만들려고 하는데 카드 인덱스를 찾지못했다!!!");
#endif
        }
        //m_DispatchRewardInfo = stAck.kDispatch;

        // 6733	임무 재파견
        // 6734	보상은 우편함에서 수령하실 수 있습니다.\n동일한 임무에 다시 파견하시겠습니까?
        //SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.YesNo, StringTableManager.GetData(6733), StringTableManager.GetData(6734), ReDispatchStart);

        DATA_DISPATCH m_DispatchTableData = CDATA_DISPATCH.Get(dispatchMissionCategory, dispatchsubID);
        if ( stAck.kRewardState == _enDispatchState.eDispatchState_BIGCOMPLETE ) // 대박사건
        {
            Dictionary<int, DATA_REWARD_NEW> RewardData = CDATA_REWARD_NEW.Get(m_DispatchTableData.RewardItemDoubleEnum);
            if (RewardData != null)
            {
                foreach (KeyValuePair<int, DATA_REWARD_NEW> data in RewardData)
                {
                    DATA_ITEM_NEW item = CDATA_ITEM_NEW.Get(data.Value.RewardValue);
                    if (item == null)
                        continue;
                    // 대박 터지면 대박 마크 찍어줌 
                    //-----------------------------------------------------------------------------------------------------------------------------
                    // 재화일 경우 팝업창
                    AbyssRewardConfirmPopup rewardPopup = UIResourceMgr.CreatePrefab<AbyssRewardConfirmPopup>(BUNDLELIST.PREFABS_UI_ABYSSRING, gameObject.transform, "AbyssRewardConfirmPopup", SetTransformType.OriginValue);
                    rewardPopup.SetUI(item
                        , data.Value.RewardCount
                        , StringTableManager.GetData(6734), ReDispatchStart, true);
                    rewardPopup.OpenUI();
                }
                    
                //-----------------------------------------------------------------------------------------------------------------------------
            }
        }
        else if(stAck.kRewardState == _enDispatchState.eDispatchState_COMPLETE )
        {
            // 대박 안터지면 그냥 마크 찍어줌
            Dictionary<int, DATA_REWARD_NEW> RewardData = CDATA_REWARD_NEW.Get(m_DispatchTableData.RewardItemEnum);
            if (RewardData != null)
            {
                foreach (KeyValuePair<int, DATA_REWARD_NEW> data in RewardData)
                {
                    DATA_ITEM_NEW item = CDATA_ITEM_NEW.Get(data.Value.RewardValue);
                    if (item == null)
                        continue;
                    // 대박 터지면 대박 마크 찍어줌 
                    //-----------------------------------------------------------------------------------------------------------------------------
                    // 재화일 경우 팝업창
                    AbyssRewardConfirmPopup rewardPopup = UIResourceMgr.CreatePrefab<AbyssRewardConfirmPopup>(BUNDLELIST.PREFABS_UI_ABYSSRING, gameObject.transform, "AbyssRewardConfirmPopup", SetTransformType.OriginValue);
                    rewardPopup.SetUI(item
                        , data.Value.RewardCount
                        , StringTableManager.GetData(6734), ReDispatchStart, false);
                    rewardPopup.OpenUI();
                }

                //-----------------------------------------------------------------------------------------------------------------------------
            }
        }

    }
    
    /// <summary>
    /// 파견보냈던 데이터 그대로 재파견.
    /// </summary>
    /// <param name="state"></param>
    private void ReDispatchStart()
    {
        // 파견보냈던 데이터의 크리쳐 정보를 지운다.
        _vDispatch DispatchInfo = UserInfo.Instance.DispatchInfoAck.vDispatch;
        for (int i = 0; i < DispatchInfo.Count; ++i)
        {
            CDispatch dispatch = DispatchInfo[i];
            if (dispatch == null)
                continue;

            if(m_DispatchRewardInfo.kTeamIndex == dispatch.kTeamIndex)
            {
                dispatch.vCreatureKey.Clear();
                break;
            }
        }

        for (int i = 0; i < m_DispatchCardList.Count; ++i)
        {
            DispatchCardItem cardItem = m_DispatchCardList[i];
            if (cardItem == null)
                continue;

            if(cardItem.DispatchRecvData.kTeamIndex == m_DispatchRewardInfo.kTeamIndex)
            {
                cardItem.Init(m_DispatchRewardInfo);        // 데이터 변경.
                OpenDispatchTeamWindow(m_DispatchRewardInfo, cardItem.DispatchTableData);
                m_DispatchTeamWindow.ReSetDispatchTeam();
                //OnClickDispatchCard(cardItem);
                break;
            }
        }

        //_stDispatchStartReq stDispatchStartReq = new _stDispatchStartReq();
        //stDispatchStartReq.kTeamIndex = m_DispatchRewardInfo.kTeamIndex;
        //stDispatchStartReq.kMissionCategory = m_DispatchRewardInfo.kMissionCategory;
        //stDispatchStartReq.kDispatchSubID = m_DispatchRewardInfo.kDispatchSubID;

        //stDispatchStartReq.vUseCreatureKey = new _vCreatureKey();
        //for (int i = 0; i < m_DispatchRewardInfo.vCreatureKey.Count - 1; ++i)
        //{
        //    stDispatchStartReq.vUseCreatureKey.Add(m_DispatchRewardInfo.vCreatureKey[i]);
        //}

        //// 다시 보내는 데이터에는 친구 x
        //stDispatchStartReq.kUseFriendCharKey = 0;

        //CNetManager.Instance.DispatchProxy.DispatchStart(stDispatchStartReq);

        //UIControlManager.instance.ShowLoading(true);
    }

    /// <summary>
    /// Rmi_DispatchCancel(_stDispatchCancelAck stAck) 패킷 받음.
    /// Rmi_DispatchFast(_stDispatchFastAck stAck) 패킷 받음.
    /// </summary>
    /// <param name="stAck"></param>
    public bool SetDispatchCardEmpty(byte kTeamIndex)
    {
        for (int i = 0; i < m_DispatchCardList.Count; ++i)
        {
            DispatchCardItem cardItem = m_DispatchCardList[i];
            if (cardItem == null)
                continue;

            if (kTeamIndex == cardItem.DispatchRecvData.kTeamIndex)
            {
                cardItem.SetCard(OnClickDispatchCard);
                return true;
            }
        }

        return false;
    }

    private void OnHelpTooltip(GameObject go, bool state)
    {
        if (state == true)
        {
            if (_SimpleHelpTip == null)
            {
                _SimpleHelpTip = UIResourceMgr.CreatePrefab<SimpleHelpTip>(BUNDLELIST.PREFABS_UI_MAINMENU, _HelpButton.transform, "SimpleHelpTip");
                _SimpleHelpTip.Init(20);
            }

            _SimpleHelpTip.OpenUI();
        }
        else
        {
            _SimpleHelpTip.CloseUI();
        }
    }

    private void SetTutorialButtonEvent()
    {
        if (Tutorial.IsCompletedTutorial)
            return;
        Tutorial tutoManager = GameSceneManager.Instance.TutorialManager;
        if (tutoManager.CurrentTutorialLevel == (DATA_TUTORIAL._enTutorialLevel)enTutorialEnumWrapper.DispatchLobbyTutorial)
        {
            tutoManager.RegisterButtonEvent(enTutorialButtonEvent.DispatchBtn_FirstCard, m_DispatchCardList[0].gameObject, OnClickFirstItem_Tutorial);
        }
    }
    private void OnClickFirstItem_Tutorial(GameObject go)
    {
        for (int i = 0; i < m_DispatchCardList.Count; ++i)
        {
            DispatchCardItem cardItem = m_DispatchCardList[i];
            if (cardItem == null)
                continue;

            if (0 == cardItem.DispatchRecvData.kTeamIndex)
            {
                cardItem.OnCliclTutorial(cardItem.gameObject);
            }
        }
    }
}


//===================================================================================
//
// Field
//
//===================================================================================

//===================================================================================
//
// Variable
//
//===================================================================================

//===================================================================================
//
// Default Method
//
//===================================================================================

//===================================================================================
//
// Method
//
//===================================================================================

//===================================================================================
//
// Event
//
//===================================================================================



/*
 
 
 
//===================================================================================
//
// Field
//
//===================================================================================

//===================================================================================
//
// Variable
//
//===================================================================================

//===================================================================================
//
// Default Method
//
//===================================================================================
protected override void Awake()
{
}

protected override void OnDestroy()
{
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
}

public override void Clear()
{
}

public override void Init()
{
}

//===================================================================================
//
// Method
//
//===================================================================================

//===================================================================================
//
// Event
//
//===================================================================================

 


*/
