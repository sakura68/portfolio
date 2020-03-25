using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public class DispatchCardItem : MonoBehaviour
{
    public enum enDispatchCardType
    {
        Empty,
        Dispatching,
    }

    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private GameObject m_SlotEmpty;                                    // 임무없을때 빈 임무카드.
    [SerializeField] private UILabel m_SlotEmptyLabel;                                  // 터치하여 임무시작 라벨.

    // top
    [SerializeField] private GameObject m_SlotDispatch;                                 // 임무를 받았을때 나오는 임무카드.
    [SerializeField] private UILabel m_DispatchDescLabel;                               // 임무 이름 라벨.
    [SerializeField] private UILabel m_DispatchContentLabel;                            // 임무 조건 라벨(몇레벨, 몇성등).
    [SerializeField] private GameObject m_CloseButton;                                  // 임무삭제 버튼.

    // middle
    [SerializeField] private List<Transform> m_SlotList = new List<Transform>();        // 아이콘을 붙일 슬롯
    [SerializeField] private GameObject _RewardObj;                                     // 보상표시.
    [SerializeField] private UILabel _RewardLabel;                                      // [보상] 라벨표시.
    [SerializeField] private UI2DSprite _RewardIcon;
    [SerializeField] private UILabel _RewardCountLabel;

    [SerializeField] private GameObject m_SuccessObj;                                   // 임무성공 이미지 오브젝트.
    [SerializeField] private UILabel m_SuccessLabel;

    [SerializeField] private GameObject _BigSuccessObj;                                   // 임무성공 이미지 오브젝트.
    [SerializeField] private UILabel _BigSuccessLabel;

    // bottom
    [SerializeField] private GameObject m_ProgressButton;                               // 진행중일때 표시하는 버튼.
    [SerializeField] private UISprite m_ProgressGageSprite;                             // 진행중일때 남은기간 표시 게이지.
    [SerializeField] private UILabel m_ProgressGageLabel;                               // 진행중일때 남은기간 라벨.
    [SerializeField] private UILabel m_ImmediatelyCompleteCountLabel;                   // 즉시완료시 필요한 다이아 갯수.
    [SerializeField] private UILabel m_ImmediatelyCompleteLabel;                        // 즉시완료 라벨.

    [SerializeField] private GameObject m_RewardButton;                                 // 보상수령 버튼.
    [SerializeField] private UILabel m_RewardButtonLabel;                               // 보상수령 버튼 라벨.

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private CDispatch m_DispatchRecvData = null;
    public CDispatch DispatchRecvData { get { return m_DispatchRecvData; } }

    private DATA_DISPATCH m_DispatchTableData = null;
    public DATA_DISPATCH DispatchTableData { get { return m_DispatchTableData; } }

    private List<CreatureIcon> m_CreatureIconList = new List<CreatureIcon>();

    public delegate void OnClickCard(DispatchCardItem card);
    public event OnClickCard OnClickCardEvent;

    private DateTime m_DispatchStartTime;
    private DateTime m_DispatchEndTime;

    private enDispatchCardType m_enDispatchCardType = enDispatchCardType.Empty;

    private int m_iPanelDepth = 0;
    private int m_QuickCost = 0;            // 즉시완료시 소모 다이아.

    private bool m_bDispatchSuccess = false;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    private void Awake()
    {
        if (m_CloseButton != null) UIEventListener.Get(m_CloseButton).onClick = OnMissionCancle;
        if (m_RewardButton != null) UIEventListener.Get(m_RewardButton).onClick = OnReward;
        if (m_ProgressButton != null) UIEventListener.Get(m_ProgressButton).onClick = OnFastComplete;
    }

    private void OnDestroy()
    {
        DestroyCreatureIcon();

        if (m_CloseButton != null) UIEventListener.Get(m_CloseButton).onClick -= OnMissionCancle;
        if (m_RewardButton != null) UIEventListener.Get(m_RewardButton).onClick -= OnReward;
        if (m_ProgressButton != null) UIEventListener.Get(m_ProgressButton).onClick -= OnFastComplete;
    }

    private void Update()
    {
        if (m_enDispatchCardType == enDispatchCardType.Empty)
            return;

        if (m_bDispatchSuccess == true)
            return;

        DateTime kCurrentTime = TimeManager.Instance.GetServerTime();
        TimeSpan timeresult = m_DispatchEndTime - kCurrentTime;

        m_bDispatchSuccess = false;
        if (kCurrentTime.Ticks >= m_DispatchEndTime.Ticks)        // 파견임무 완료.
        {
            m_bDispatchSuccess = true;
        }

        SetDispatchButton(m_bDispatchSuccess, timeresult);
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init(CDispatch DispatchData)
    {
        m_bDispatchSuccess = false;

        m_DispatchRecvData = DispatchData;
        m_DispatchStartTime = m_DispatchRecvData.kStartTime.GetDateTime();
        m_DispatchEndTime = m_DispatchRecvData.kEndTime.GetDateTime();

        m_DispatchTableData = CDATA_DISPATCH.Get(m_DispatchRecvData.kMissionCategory, m_DispatchRecvData.kDispatchSubID);

        // 6728	임무\n완료
        m_SuccessLabel.text = StringTableManager.GetData(6728);
        _BigSuccessLabel.text = StringTableManager.GetData(6728);

        // 보상정보 표시 (새로들어감)
        {
            _RewardObj.SetActive(false);                            // 보상정보를 일단 끄고.

            if (m_DispatchRecvData.kMissionCategory == DATA_DISPATCH_CATEGORY._enCategory.Dispatch_World_None ||
                m_DispatchRecvData.kMissionCategory == DATA_DISPATCH_CATEGORY._enCategory._enCategory_Max)
                return;

                if (m_DispatchRecvData.kDispatchSubID == DATA_DISPATCH_ENUM._enDispatchEnum.Dispatch_None ||
                m_DispatchRecvData.kDispatchSubID == DATA_DISPATCH_ENUM._enDispatchEnum._enDispatchEnum_Max)
                return;

            Dictionary<int, DATA_REWARD_NEW> RewardData = CDATA_REWARD_NEW.Get(m_DispatchTableData.RewardItemEnum);
            if (RewardData == null)
                return;     // error.

            foreach (KeyValuePair<int, DATA_REWARD_NEW> data in RewardData)
            {
                DATA_ITEM_NEW item = CDATA_ITEM_NEW.Get(data.Value.RewardValue);
                if (item == null)
                    continue;

                _RewardIcon.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_ITEMICON, item.m_szIconName);
                _RewardCountLabel.text = data.Value.RewardCount.ToString();
            }

            _RewardObj.SetActive(true);                             // 보상정보 있으면 킨다.
            _RewardLabel.text = StringTableManager.GetData(86);     // 86    보상
        }
    }

    /// <summary>
    /// 빈 카드 생성할때.
    /// </summary>
    /// <param name="del"></param>
    public void SetCard(OnClickCard evt)
    {
        m_enDispatchCardType = enDispatchCardType.Empty;

        DestroyCreatureIcon();

        if (m_SlotEmpty != null)
        {
            m_SlotEmpty.SetActive(true);
            m_SlotEmptyLabel.text = StringTableManager.GetData(6724);       // 터치하여\n임무 시작.

            if (OnClickCardEvent != null)
                OnClickCardEvent = null;

            OnClickCardEvent = evt;
            UIEventListener.Get(m_SlotEmpty).onClick = OnClickEmptyCard;
        }

        if (m_SlotDispatch != null) { m_SlotDispatch.SetActive(false); }
        if (m_SuccessObj != null) { m_SuccessObj.SetActive(false); }
        if (_BigSuccessObj != null) { _BigSuccessObj.SetActive(false); }
    }

    /// <summary>
    /// 진행중, 성공, 대성공 카드 생성할때.
    /// </summary>
    public void SetCard(int iPanelDepth)
    {
        m_enDispatchCardType = enDispatchCardType.Dispatching;

        m_iPanelDepth = iPanelDepth;

        if (m_SlotDispatch != null) { m_SlotDispatch.SetActive(true); }
        if (m_SlotEmpty != null) { m_SlotEmpty.SetActive(false); }
        if (m_SuccessObj != null) { m_SuccessObj.SetActive(false); }
        if (_BigSuccessObj != null) { _BigSuccessObj.SetActive(false); }

        m_ImmediatelyCompleteLabel.text = StringTableManager.GetData(6725);     // 6725	즉시 완료

        SetDispatchInfo();
    }

    private void SetDispatchInfo()
    {
        //m_DispatchTableData = CDATA_DISPATCH.Get(m_DispatchRecvData.kMissionCategory, m_DispatchRecvData.kDispatchSubID);
        if (m_DispatchTableData == null)
            return;         // error.

        // top
        {
            // 임무이름.
            m_DispatchDescLabel.text = StringTableManager.GetData(int.Parse(m_DispatchTableData.String_Name));

            // 6847    Lv {0} 이상 {1}성 이상
            m_DispatchContentLabel.text = string.Format(StringTableManager.GetData(6847), m_DispatchTableData.RequireLevel, (int)m_DispatchTableData.RequireGrade);
        }

        // middle
        {
            DestroyCreatureIcon();

            int iCount = m_DispatchRecvData.vCreatureKey.Count;
            for (int i = 0; i < iCount; ++i)
            {
                ulong kCreatureKey = m_DispatchRecvData.vCreatureKey[i];
                if (kCreatureKey == 0)
                    continue;

                CreatureIcon icon = UIResourceMgr.CreatePrefab<CreatureIcon>(BUNDLELIST.PREFABS_UI_COMMON, m_SlotList[i].transform, "CreatureIcon");
                icon.SetIcon(kCreatureKey, enCreatureIcon_Type.DispatchDisplay);
                icon.AddPanel(m_iPanelDepth);
                icon.RemoveBoxCollider();

                m_CreatureIconList.Add(icon);
            }

            //if(m_DispatchRecvData.kFriendCreatureID != DATA_CREATURE_NEWVER._enIndex.CREATURE_NONE)
            if (m_DispatchRecvData.kFriendCreatureID != 0)
            {
                CreatureIcon icon = UIResourceMgr.CreatePrefab<CreatureIcon>(BUNDLELIST.PREFABS_UI_COMMON, m_SlotList[m_SlotList.Count - 1].transform, "CreatureIcon");
                icon.SetIcon(m_DispatchRecvData.kFriendCreatureID, enCreatureIcon_Type.DispatchDisplayFriend);                
                icon.AddPanel(m_iPanelDepth);
                icon.RemoveBoxCollider();

                m_CreatureIconList.Add(icon);
            }
        }

        // bottom
        {
            //SetDispatchButton(m_DispatchTableData);
        }
    }

    private void SetDispatchButton(bool bDispatchSuccess, TimeSpan TimeResult)
    {
        m_SuccessObj.SetActive(false);
        _BigSuccessObj.SetActive(false);

        if (bDispatchSuccess == false /*&& m_DispatchRecvData.kDispatchState == _enDispatchState.eDispatchState_INPROGRESSING*/)
        {
            m_RewardButton.SetActive(false);

            m_CloseButton.SetActive(true);
            m_ProgressButton.SetActive(true);

            TimeSpan tsTotalGap = m_DispatchEndTime - m_DispatchStartTime;
            //TimeSpan tsCurrGap = TimeResult;

            double fPercent = TimeResult.TotalMilliseconds / tsTotalGap.TotalMilliseconds;
            if (fPercent > 1f)
                fPercent = 1f;
            else if (fPercent < 0f)
                fPercent = 0f;

            m_QuickCost = (int)(m_DispatchTableData.QuickCost * fPercent);
            m_ImmediatelyCompleteCountLabel.text = m_QuickCost.ToString();
            
            m_ProgressGageSprite.fillAmount = 1 - (float)(TimeResult.TotalSeconds / (m_DispatchTableData.RequireTime * 60));

            if (TimeResult.Hours > 0)
            {
                // 4915	{0}시간 {1}분 남음
                m_ProgressGageLabel.text = string.Format(StringTableManager.GetData(4915), TimeResult.Hours, TimeResult.Minutes);
            }
            else if (TimeResult.Minutes > 0)
            {
                // 4916	{0}분 남음
                m_ProgressGageLabel.text = string.Format(StringTableManager.GetData(4916), TimeResult.Minutes);
            }
            else if (TimeResult.Seconds > 0)
            {
                // 4959	1분 미만
                m_ProgressGageLabel.text = StringTableManager.GetData(4959);
            }
        }
        else if (bDispatchSuccess == true /*&& m_DispatchRecvData.kDispatchState == _enDispatchState.eDispatchState_COMPLETE*/)
        {
            m_CloseButton.SetActive(false);
            m_ProgressButton.SetActive(false);

            m_RewardButton.SetActive(true);
            
            UIPanel SuccessPanel = null;
            if (m_DispatchRecvData.kDispatchState == _enDispatchState.eDispatchState_BIGCOMPLETE)
            {
                _BigSuccessObj.SetActive(true);
                SuccessPanel = _BigSuccessObj.GetComponent<UIPanel>();

                m_RewardButtonLabel.text = StringTableManager.GetData(6727); // 6727	보상 수령 X2.
            }
            else
            {
                m_SuccessObj.SetActive(true);
                SuccessPanel = m_SuccessObj.GetComponent<UIPanel>();

                m_RewardButtonLabel.text = StringTableManager.GetData(6726); // 6726	보상 수령.
            }

            // 크리쳐 아이콘 뎁스 =  m_iPanelDepth + 1
            // 크리쳐 아이콘 뎁스보다 높아야 하기때문에 뎁스를 높인다.
            if (m_CreatureIconList.Count > 1)
            {
                UIPanel IconPanel = m_CreatureIconList[0].GetComponent<UIPanel>();
                UtilFunc.SetPanelDepth(SuccessPanel, IconPanel.depth + 1);
            }
        }
    }

    private void DestroyCreatureIcon()
    {
        for(int i = 0; i < m_CreatureIconList.Count; ++i)
        {
            DestroyImmediate(m_CreatureIconList[i].gameObject);
        }

        m_CreatureIconList.Clear();
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    /// <summary>
    /// 빈 슬롯 클릭.
    /// </summary>
    /// <param name="go"></param>
    private void OnClickEmptyCard(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        if(OnClickCardEvent != null)
        {
            OnClickCardEvent(this);
        }
    }

    /// <summary>
    /// 파견 취소 패킷 보냄.
    /// </summary>
    /// <param name="go"></param>
    private void OnMissionCancle(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        // 6730	임무 취소
        // 6731	이 파견 임무를 취소하겠습니까?
        SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.YesNo, StringTableManager.GetData(6730), StringTableManager.GetData(6731), MissionCancle);
    }

    private void MissionCancle(enSystemMessageFlag state)
    {
        if (state != enSystemMessageFlag.YES)
            return;

        _stDispatchCancelReq stDispatchCancelReq = new _stDispatchCancelReq();
        stDispatchCancelReq.kTeamIndex = m_DispatchRecvData.kTeamIndex;
        stDispatchCancelReq.kMissionCategory = m_DispatchRecvData.kMissionCategory;
        stDispatchCancelReq.kDispatchSubID = m_DispatchRecvData.kDispatchSubID;

        CNetManager.Instance.SendPacket(CNetManager.Instance.DispatchProxy.DispatchCancel, stDispatchCancelReq, typeof(_stDispatchCancelAck));
    }

    /// <summary>
    /// 보상 수령 패킷 보냄.
    /// </summary>
    /// <param name="go"></param>
    private void OnReward(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        if (m_DispatchRecvData.kDispatchState == _enDispatchState.eDispatchState_INPROGRESSING)
        {
            CNetManager.Instance.DispatchStub.OnDispatchRewardReq = OnDispatchRewardReq;

            _stDispatchCheckEndReq stDispatchCheckEndReq = new _stDispatchCheckEndReq();
            stDispatchCheckEndReq.kDispatchTeamIndex = m_DispatchRecvData.kTeamIndex;

            CNetManager.Instance.SendPacket(CNetManager.Instance.DispatchProxy.DispatchCheck, stDispatchCheckEndReq, typeof(_stDispatchCheckEndAck));

        }
        else if (m_DispatchRecvData.kDispatchState == _enDispatchState.eDispatchState_COMPLETE || m_DispatchRecvData.kDispatchState == _enDispatchState.eDispatchState_BIGCOMPLETE)
        {
            _stDispatchRewardReq stDispatchRewardReq = new _stDispatchRewardReq();
            stDispatchRewardReq.kTeamIndex = m_DispatchRecvData.kTeamIndex;

            CNetManager.Instance.SendPacket(CNetManager.Instance.DispatchProxy.DispatchReward, stDispatchRewardReq, typeof(_stDispatchRewardAck));
        }
    }

    private void OnDispatchRewardReq()
    {
        _stDispatchRewardReq stDispatchRewardReq = new _stDispatchRewardReq();
        stDispatchRewardReq.kTeamIndex = m_DispatchRecvData.kTeamIndex;

        CNetManager.Instance.SendPacket(CNetManager.Instance.DispatchProxy.DispatchReward, stDispatchRewardReq, typeof(_stDispatchRewardAck));
    }

    private void OnFastComplete(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        if(UserInfo.Instance.iDiaCount < (ulong)m_QuickCost)
        {
            // 6513	다이아가 부족합니다.
            SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(4300), StringTableManager.GetData(6513));
            return;
        }

        // 6725	즉시 완료
        // 6732	다이아몬드 x {0}\n\n소모하여 임무를 즉시 완료하시겠습니까?
        string str = string.Format(StringTableManager.GetData(6732), m_QuickCost);
        SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.YesNo, StringTableManager.GetData(6725), str, FastComplete);
    }

    private void FastComplete(enSystemMessageFlag state)
    {
        if (state != enSystemMessageFlag.YES)
            return;

        CNetManager.Instance.DispatchStub.OnDispatchRewardReq = OnDispatchRewardReq;

        _stDispatchFastReq stDispatchFastReq = new _stDispatchFastReq();
        stDispatchFastReq.kTeamIndex = m_DispatchRecvData.kTeamIndex;

        CNetManager.Instance.SendPacket(CNetManager.Instance.DispatchProxy.DispatchFast, stDispatchFastReq, typeof(_stDispatchFastAck));
    }

    public void OnCliclTutorial( GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        if (OnClickCardEvent != null)
        {
            OnClickCardEvent(this);
        }
    }
}