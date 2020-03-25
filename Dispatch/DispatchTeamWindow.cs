using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using DGL_DATA_READER;

/// <summary>
/// 왼쪽 (1,2,3 친구) 4가지 목록에 들어가는 슬롯
/// </summary>
[System.Serializable]
public class DispatchTeamSlot
{
    [SerializeField] private GameObject m_CloseButton;
    public GameObject CloseButton { get { return m_CloseButton; } }

    [SerializeField] private GameObject m_SlotObj;
    public GameObject SlotObj { get { return m_SlotObj; } }

    [SerializeField] private UISprite m_SelectSprite;

    private CreatureIcon m_CreatureIcon;
    public CreatureIcon CreatureIcon { get { return m_CreatureIcon; } set { m_CreatureIcon = value; } }

    [SerializeField] private int m_iNum;
    public int iNum { get { return m_iNum; } }

    private bool m_bSelect = false;
    public bool bSelect { get { return m_bSelect; } }

    public void SetActiveSelect(bool bIsActive)
    {
        m_bSelect = bIsActive;

        m_SelectSprite.gameObject.SetActive(bIsActive);
    }
}

public class DispatchTeamWindow : UIWindow
{
    public enum enDispatchTeamSlotType
    {
        Creature,
        Friend,
    }

    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private GameObject m_CloseButton;

    [SerializeField] private UILabel m_TitleLabel;                              // 파견
    [SerializeField] private UILabel m_TitleContentLabel;                       // 파견팀편성

    // left top
    [SerializeField] private UILabel m_DispatchMissionTitleLabel;               // 파견임무 제목.
    [SerializeField] private UILabel m_DispatchMissionConditionLabel;           // 파견임무 조건.
    [SerializeField] private UILabel m_DispatchMissionTimeLabel;                // 파견임무 소요시간.

    // left middle
    [SerializeField] private List<DispatchTeamSlot> m_DispatchTeamSlotList = new List<DispatchTeamSlot>();
    [SerializeField] private UILabel m_DispatchTeamNoticeLabel;                 // 네번째 슬롯터치 친구크리쳐 참여 라벨.

    // left bottom
    [SerializeField] private UILabel m_DispatchMissionRewardTitleLabel;         // 보상 타이틀.
    [SerializeField] private UI2DSprite m_DispatchMissionRewardSprite;          // 보상 이미지.
    [SerializeField] private UILabel m_DispatchMissionRewardLabel;              // 보상 갯수.
    [SerializeField] private UILabel m_DispatchMissionSuccessTitleLabel;        // 임무 대성공 확률 타이틀
    [SerializeField] private UILabel m_DispatchMissionSuccessLabel;             // 퍼센트 숫자.
    [SerializeField] private CustomButtonUI m_DispatchMissionButton;            // 파견버튼.

    // right menu
    [SerializeField] private UILabel m_CreatureListTitleLabel;                  // 크리쳐 리스트상단 타이틀.
    [SerializeField] private UILabel m_FriendListTitleLabel;                    // 크리쳐 리스트상단 타이틀.

    [SerializeField] private GameObject m_FriendListCloseButton;
    [SerializeField] private UIScrollView m_FriendListScrollView;
    [SerializeField] private UIGrid m_FriendListGrid;

    [SerializeField] private GameObject m_CreatureTeamList;
    [SerializeField] private GameObject m_FriendList;

    [SerializeField] private DispatchInfiniteScrollView _DispatchInfiniteScrollView;

    [SerializeField] private UILabel _EmptyLabel;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private List<CreatureItemInfo> _CreatureList = new List<CreatureItemInfo>();
    private List<DispatchTeamItemFriend> _FriendList = new List<DispatchTeamItemFriend>();

    private CDispatch m_DispatchRecvData = null;
    private DATA_DISPATCH m_DispatchTableData = null;

    private DispatchTeamItemFriend m_SelectFriend = null;

    private int m_iPanelDepth = 0;
    private int m_iTeamCount = 0;

    private enDispatchTeamSlotType _enDispatchTeamSlotType = enDispatchTeamSlotType.Creature;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    protected override void Awake()
    {
        if (m_CloseButton != null) UIEventListener.Get(m_CloseButton).onClick = OnClickBack;
        if (m_DispatchMissionButton != null) UIEventListener.Get(m_DispatchMissionButton.gameObject).onClick = OnDispatchMissionStart;
        if (m_FriendListCloseButton != null) UIEventListener.Get(m_FriendListCloseButton).onClick = OnDispatchFriendListClose;

        m_TitleLabel.text = StringTableManager.GetData(6722);                           // 6722	파견
        m_TitleContentLabel.text = StringTableManager.GetData(6743);                    // 6743	파견 팀 편성
        m_DispatchTeamNoticeLabel.text = StringTableManager.GetData(6744);              // 6744	네번째 슬롯을 터치하여 친구의 크리처를 참여시킬 수 있습니다.
        m_CreatureListTitleLabel.text = StringTableManager.GetData(6745);               // 6745	내 크리처
        m_FriendListTitleLabel.text = StringTableManager.GetData(6747);                 // 6747	친구 지원

        m_DispatchMissionRewardTitleLabel.text = StringTableManager.GetData(86);        // 86 보상
        m_DispatchMissionSuccessTitleLabel.text = StringTableManager.GetData(6746);     // 6746 임무 대성공 확률
        m_DispatchMissionButton.SetLabel(StringTableManager.GetData(6722));             // 6722 파견
    }

    protected override void OnDestroy()
    {
        if (m_CloseButton != null) UIEventListener.Get(m_CloseButton).onClick -= OnClickBack;
        if (m_DispatchMissionButton != null) UIEventListener.Get(m_DispatchMissionButton.gameObject).onClick -= OnDispatchMissionStart;
    }

    protected override void Start()
    {
    }

    public override void Init()
    {
    }

    public override void Clear()
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

    public override void OpenUI()
    {
        base.OpenUI();

        // 열었을때 기본적으로 크리쳐 리스트를 보여준다.
        SetSlotType(enDispatchTeamSlotType.Creature);
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init(CDispatch DispatchRecvData, DATA_DISPATCH DispatchTableData)
    {
        m_iTeamCount = 0;
        m_iPanelDepth = GetComponent<UIPanel>().depth;

        m_DispatchRecvData = DispatchRecvData;
        m_DispatchTableData = DispatchTableData;

        InitDispatchTeamCreature();

        SetDispatchMissionInfo();
        CreateFriendCreatureIcon();

        MakeUseCreatureData();
        _DispatchInfiniteScrollView.Init(SetDispatchCreature, 620.0f, 152.0f);
        _DispatchInfiniteScrollView.SetData(_CreatureList);
    }


    private void MakeUseCreatureData()
    {
        InventoryInfo pInven = UserInfo.Instance.InventoryInfo;
        if (pInven == null)
            return;

        Dictionary<ulong, ItemCreature> pDicValue = pInven.MapInvenCreature;
        if (pDicValue == null)
            return;

        _CreatureList.Clear();

        // 등급순 -> 레벨순으로 정렬.
        foreach (KeyValuePair<ulong, ItemCreature> dicvalue in pDicValue.OrderByDescending((data) => data.Value.Grade).ThenByDescending((data) => data.Value.Level))
        {
            ulong kCreatureKey = dicvalue.Key;

            if (UtilFunc.IsDispatching(kCreatureKey) == true)                                    // 파견중인 크리쳐는 제낌.
                continue;

            ItemCreature pMy = UserInfo.Instance.InventoryInfo.GetMyCreature(kCreatureKey);
            DATA_CREATURE_NEWVER pCreatureData = UtilFunc.GetCreatureDataByTID(pMy.Index);

            if (pCreatureData.m_iGrade < (int)m_DispatchTableData.RequireGrade)      // 필요등급 검사
                continue;

            if (pMy.Level < m_DispatchTableData.RequireLevel)                  // 필요레벨 검사
                continue;

            if (UserInfo.Instance.CreatureTeam.HasTempCreatureAll(kCreatureKey))        // 팀에 속해있는지 검사
                continue;

            CreatureItemInfo info = new CreatureItemInfo();
            info.Init(kCreatureKey, pMy, pCreatureData);
            info.SetDispatchSelect(false, string.Empty);

            _CreatureList.Add(info);
        }

        // 전에 보냈던 크리쳐가 있으면 다시 정렬.
        for (int i = 0; i < m_DispatchRecvData.vCreatureKey.Count; i++)
        {
            foreach (CreatureItemInfo info in _CreatureList)
            {
                if (m_DispatchRecvData.vCreatureKey[i] == info.CreatureKey)
                {
                    CreatureItemInfo oldInfo = info;
                    _CreatureList.Remove(info);
                    _CreatureList.Insert(i, oldInfo);       // 전에 보냈던 순서대로 Insert
                    break;
                }
            }
        }
    }

    private void SetDispatchMissionInfo()
    {
        SetTeamCount(0);

        m_DispatchMissionTitleLabel.text = StringTableManager.GetData(int.Parse(m_DispatchTableData.String_Name));

        // 6847    Lv {0} 이상 {1}성 이상
        m_DispatchMissionConditionLabel.text = string.Format(StringTableManager.GetData(6847), m_DispatchTableData.RequireLevel, (int)m_DispatchTableData.RequireGrade);

        int ihour = m_DispatchTableData.RequireTime / 60;
        int iMinutes = m_DispatchTableData.RequireTime - (60 * ihour);
        if (ihour > 0)
        {
            // 6740	소요 시간 : {0}시간 {1}분
            m_DispatchMissionTimeLabel.text = string.Format(StringTableManager.GetData(6740), ihour, iMinutes);
        }
        else
        {
            // 6741	소요 시간 : {0}분
            m_DispatchMissionTimeLabel.text = string.Format(StringTableManager.GetData(6741), iMinutes);
        }

        Dictionary<int, DATA_REWARD_NEW> RewardData = CDATA_REWARD_NEW.Get(m_DispatchTableData.RewardItemEnum);
        if (RewardData == null)
            return;     // error.

        foreach (KeyValuePair<int, DATA_REWARD_NEW> data in RewardData)
        {
            DATA_ITEM_NEW item = CDATA_ITEM_NEW.Get(data.Value.RewardValue);
            if (item == null)
                continue;

            m_DispatchMissionRewardSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_ITEMICON, item.m_szIconName);
            m_DispatchMissionRewardLabel.text = data.Value.RewardCount.ToString();
        }
        //m_DispatchMissionRewardSprite
        //m_DispatchMissionRewardLabel.text = m_DispatchTableData.QuickCost.ToString();
    }

    private void SetDispatchCreature(CreatureIcon CreatureListIcon)
    {
        int iDispatchTeamSlotCount = m_DispatchTeamSlotList.Count - 1;
        if (CreatureListIcon.IsDispatchSelect == false)                    // 크리쳐를 추가할때
        {
            bool bAddCreature = false;
            for (int i = 0; i < iDispatchTeamSlotCount; ++i)
            {
                DispatchTeamSlot slot = m_DispatchTeamSlotList[i];
                if (slot == null)
                    continue;

                if (slot.bSelect == true)
                {
                    if (slot.CreatureIcon != null)
                    {
                        DestroyDispatchTeamCreature(slot);
                    }

                    SetTeamCount(1);

                    CreatureIcon creatureIcon = UIResourceMgr.CreatePrefab<CreatureIcon>(BUNDLELIST.PREFABS_UI_COMMON, slot.SlotObj.transform, "CreatureIcon");
                    creatureIcon.SetIcon(CreatureListIcon.CreatureKey, enCreatureIcon_Type.DispatchDisplay);
                    creatureIcon.AddPanel(m_iPanelDepth);
                    creatureIcon.RemoveBoxCollider();

                    slot.CreatureIcon = creatureIcon;
                    slot.SetActiveSelect(false);

                    CreatureListIcon.SetDispatchSelectNumberLabel(slot.iNum.ToString());
                    CreatureListIcon.SetActiveDispatchSelect(true);

                    bAddCreature = true;
                    break;
                }
            }

            if (bAddCreature == true)
            {
                for (int i = 0; i < iDispatchTeamSlotCount; ++i)                // 다음 선택될 칸을 지정한다.
                {
                    DispatchTeamSlot slot = m_DispatchTeamSlotList[i];
                    if (slot == null)
                        continue;

                    if (slot.CreatureIcon == null)
                    {
                        slot.SetActiveSelect(true);
                        break;
                    }
                }
            }
        }
        else if (CreatureListIcon.IsDispatchSelect == true)                // 추가된 크리쳐를 뺄때
        {
            for (int i = 0; i < iDispatchTeamSlotCount; ++i)
            {
                DispatchTeamSlot slot = m_DispatchTeamSlotList[i];
                if (slot == null)
                    continue;

                slot.SetActiveSelect(false);

                if (slot.CreatureIcon != null)
                {
                    if (slot.CreatureIcon.CreatureKey == CreatureListIcon.CreatureKey)
                    {
                        CreatureListIcon.SetActiveDispatchSelect(false);
                        DestroyDispatchTeamCreature(slot);
                        slot.SetActiveSelect(true);
                    }
                }
            }
        }
    }

    private void InitDispatchTeamCreature()
    {
        for (int i = 0; i < m_DispatchTeamSlotList.Count; ++i)
        {
            DispatchTeamSlot slot = m_DispatchTeamSlotList[i];
            if (slot == null)
                continue;

            if (slot.CreatureIcon != null)
            {
                DestroyImmediate(slot.CreatureIcon.gameObject);
            }

            UIEventListener.Get(slot.CloseButton).onClick -= OnDispatchTeamSlotClose;
            UIEventListener.Get(slot.SlotObj).onClick -= OnDispatchSlotClick;
            UIEventListener.Get(slot.CloseButton).onClick = OnDispatchTeamSlotClose;
            UIEventListener.Get(slot.SlotObj).onClick = OnDispatchSlotClick;

            // 첫번째 슬롯
            if (slot.iNum == 1)
            {
                slot.SetActiveSelect(true);
            }
            else
            {
                slot.SetActiveSelect(false);
            }
        }
    }

    /// <summary>
    /// 왼쪽 편성에 속해있는 크리쳐 1개를 삭제.
    /// </summary>
    private void DestroyDispatchTeamCreature(DispatchTeamSlot slot)
    {
        if (slot.CreatureIcon == null)
            return;

        SetTeamCount(-1);

        _DispatchInfiniteScrollView.SetDispatchSelect(slot.CreatureIcon);
        DestroyImmediate(slot.CreatureIcon.gameObject);
    }

    private void CreateFriendCreatureIcon()
    {
        _vFriend vFriends = UserInfo.Instance.FriendInfo.vFriends;
        if (vFriends == null)
            return;

        DestroyFriendCreatureIcon();

        for (int i = 0; i < vFriends.Count; ++i)
        {
            CFriend friendInfo = vFriends[i];
            if (friendInfo == null)
                continue;

            DATA_CREATURE_NEWVER pCreatureData = CDATA_CREATURE_NEWVER.Get(friendInfo.kCreatureID);
            if (pCreatureData.m_iGrade < (int)m_DispatchTableData.RequireGrade)      // 필요등급 검사
                continue;

            DispatchTeamItemFriend friendItem = UIResourceMgr.CreatePrefab<DispatchTeamItemFriend>(BUNDLELIST.PREFABS_UI_DISPATCH, m_FriendListGrid.transform, "DispatchTeamItemFriend");
            friendItem.Init(friendInfo, pCreatureData);
            UIEventListener.Get(friendItem.gameObject).onClick = OnClickFriendItem;

            _FriendList.Add(friendItem);
        }

        FriendListResetPosition();
    }

    private void SetTeamCount(int iCount)
    {
        m_iTeamCount += iCount;
        SetDispatchMissionSuccessLabel(m_iTeamCount);
        SetDispatchMissionButton(m_iTeamCount);
    }

    private void SetDispatchMissionSuccessLabel(int iTeamCount)
    {
        if (iTeamCount > m_DispatchTableData.RequireNumber)
        {
            m_DispatchMissionSuccessLabel.text = string.Format("{0}%", m_DispatchTableData.SuccessRate + (m_DispatchTableData.SuccessRate_Add * (iTeamCount - m_DispatchTableData.RequireNumber)));
        }
        else
        {
            m_DispatchMissionSuccessLabel.text = string.Format("{0}%", m_DispatchTableData.SuccessRate);
        }
    }

    private void SetDispatchMissionButton(int iTeamCount)
    {
        if (iTeamCount >= m_DispatchTableData.RequireNumber)
        {
            m_DispatchMissionButton.state = ButtonState.On;
        }
        else
        {
            m_DispatchMissionButton.state = ButtonState.Off;
        }
    }

    private void SetSlotType(enDispatchTeamSlotType type)
    {
        _EmptyLabel.gameObject.SetActive(false);

        _enDispatchTeamSlotType = type;

        if (type == enDispatchTeamSlotType.Creature)
        {
            m_CreatureTeamList.SetActive(true);
            m_FriendList.SetActive(false);

            if(_CreatureList.Count < 1)
            {
                // 7081    등급 및 레벨 조건을 만족하는 크리처가 없습니다.
                _EmptyLabel.text = StringTableManager.GetData(7081);
                _EmptyLabel.gameObject.SetActive(true);
            }
        }
        else
        {
            m_CreatureTeamList.SetActive(false);
            m_FriendList.SetActive(true);

            if(_FriendList.Count < 1)
            {
                // 8309    조건에 맞는 친구가 없습니다.
                _EmptyLabel.text = StringTableManager.GetData(8309);
                _EmptyLabel.gameObject.SetActive(true);
            }
        }
    }

    public void ReSetDispatchTeam()
    {
        for (int i = 0; i < m_DispatchRecvData.vCreatureKey.Count; ++i)
        {
            ulong kCreatureKey = m_DispatchRecvData.vCreatureKey[i];
            if (kCreatureKey == 0)
                continue;

            _DispatchInfiniteScrollView.ReSetDispatchTeam(kCreatureKey);
        }
    }

    private void DestroyFriendCreatureIcon()
    {
        for (int i = 0; i < _FriendList.Count; ++i)
        {
            DestroyImmediate(_FriendList[i].gameObject);
        }

        _FriendList.Clear();
    }

    private void FriendListResetPosition()
    {
        m_FriendListGrid.Reposition();
        m_FriendListScrollView.ResetPosition();
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
    private void OnDispatchTeamSlotClose(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        int iDispatchTeamSlotCount = m_DispatchTeamSlotList.Count;
        for (int i = 0; i < iDispatchTeamSlotCount; ++i)
        {
            DispatchTeamSlot slot = m_DispatchTeamSlotList[i];
            if (slot == null)
                continue;

            if (slot.CloseButton == go)
            {
                DestroyDispatchTeamCreature(slot);
                slot.SetActiveSelect(true);

                if (i < iDispatchTeamSlotCount - 1)
                {
                    SetSlotType(enDispatchTeamSlotType.Creature);
                }
                else
                {
                    SetSlotType(enDispatchTeamSlotType.Friend);

                    for (int k = 0; k < _FriendList.Count; ++k)        // 친구슬롯이면 선택된것들 다 해제.
                    {
                        DispatchTeamItemFriend friendItem = _FriendList[k];
                        if (friendItem == null)
                            continue;

                        friendItem.SetActiveSelect(false);
                    }
                }
            }
            else
            {
                slot.SetActiveSelect(false);
            }
        }
    }

    private void OnDispatchSlotClick(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        int iDispatchTeamSlotCount = m_DispatchTeamSlotList.Count;
        for (int i = 0; i < iDispatchTeamSlotCount; ++i)
        {
            DispatchTeamSlot slot = m_DispatchTeamSlotList[i];
            if (slot == null)
                continue;

            if(slot.SlotObj == go)
            {
                if(i < iDispatchTeamSlotCount - 1)
                {
                    SetSlotType(enDispatchTeamSlotType.Creature);
                }
                else
                {
                    SetSlotType(enDispatchTeamSlotType.Friend);
                }

                slot.SetActiveSelect(true);
            }
            else
            {
                slot.SetActiveSelect(false);
            }
        }
    }

    private void OnDispatchFriendListClose(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        SetSlotType(enDispatchTeamSlotType.Creature);

        int iDispatchTeamSlotCount = m_DispatchTeamSlotList.Count;
        for (int i = 0; i < iDispatchTeamSlotCount; ++i)
        {
            DispatchTeamSlot slot = m_DispatchTeamSlotList[i];
            if (slot == null)
                continue;

            slot.SetActiveSelect(false);
        }

        for (int i = 0; i < iDispatchTeamSlotCount; ++i)                // 다음 선택될 칸을 지정한다.
        {
            DispatchTeamSlot slot = m_DispatchTeamSlotList[i];
            if (slot == null)
                continue;

            if (slot.CreatureIcon == null)
            {
                slot.SetActiveSelect(true);
                break;
            }
        }
    }

    private void OnClickFriendItem(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        for (int i = 0; i < _FriendList.Count; ++i)
        {
            DispatchTeamItemFriend friendItem = _FriendList[i];
            if (friendItem == null)
                continue;

            friendItem.SetActiveSelect(false);

            if (friendItem.gameObject == go)
            {
                if (friendItem.bDispatching == true)
                {
                    // 6747	친구 지원       // 6748	친구의 크리처는 1일 1회만 사용할 수 있습니다.
                    SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(6747), StringTableManager.GetData(6748));

                    if (m_SelectFriend != null)
                    {
                        m_SelectFriend.SetActiveSelect(true);
                    }

                    break;
                }
                else
                {
                    friendItem.SetActiveSelect(true);
                    m_SelectFriend = friendItem;

                    DispatchTeamSlot slot = m_DispatchTeamSlotList[m_DispatchTeamSlotList.Count - 1];       // 친구슬롯.
                    if(slot != null)
                    {
                        if (slot.CreatureIcon != null)
                        {
                            DestroyDispatchTeamCreature(slot);
                        }

                        SetTeamCount(1);

                        CreatureIcon creatureIcon = UIResourceMgr.CreatePrefab<CreatureIcon>(BUNDLELIST.PREFABS_UI_COMMON, slot.SlotObj.transform, "CreatureIcon");
                        creatureIcon.SetIcon(friendItem.FriendInfo.kCreatureID, enCreatureIcon_Type.DispatchDisplayFriend);
                        creatureIcon.CreatureKey = friendItem.FriendInfo.kFriendCharNo;
                        creatureIcon.AddPanel(m_iPanelDepth);
                        creatureIcon.RemoveBoxCollider();
                        
                        slot.CreatureIcon = creatureIcon;
                        slot.SetActiveSelect(false);
                    }
                }
            }
        }
    }

    private void OnDispatchMissionStart(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        if (m_DispatchMissionButton.state == ButtonState.Off)
            return;

        // 6722	파견
        // 6749	파견 임무를 실행합니다.
        SystemPopupWindow.Instance.OpenSystemPopUp(enSystemPopupType.YesNo, StringTableManager.GetData(6722), StringTableManager.GetData(6749), DispatchMissionStart);
    }

    private void DispatchMissionStart(enSystemMessageFlag state)
    {
        if (state != enSystemMessageFlag.YES)
            return;

        bool bUseCreature = false;

        _stDispatchStartReq stDispatchStartReq = new _stDispatchStartReq();
        stDispatchStartReq.kTeamIndex = m_DispatchRecvData.kTeamIndex;
        stDispatchStartReq.kMissionCategory = m_DispatchTableData.DispatchCategory;
        stDispatchStartReq.kDispatchSubID = m_DispatchTableData.DispatchEnum;

        stDispatchStartReq.vUseCreatureKey = new _vCreatureKey();
        for (int i = 0; i < m_DispatchTeamSlotList.Count - 1; ++i)
        {
            DispatchTeamSlot TeamSlot = m_DispatchTeamSlotList[i];
            if (TeamSlot == null)
                continue;

            ulong kCreatureKey = 0;
            if (TeamSlot.CreatureIcon != null)
            {
                kCreatureKey = TeamSlot.CreatureIcon.CreatureKey;
                bUseCreature = true;
            }

            stDispatchStartReq.vUseCreatureKey.Add(kCreatureKey);
        }

        if (bUseCreature == false)        // 보낼 크리쳐가 없을때.
        {
            StartCoroutine(DispatchMissionStartFail());
            //SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(4300), "크리쳐를 선택해주세요.");
            return;
        }

        DispatchTeamSlot slot = m_DispatchTeamSlotList[m_DispatchTeamSlotList.Count - 1];       // 친구슬롯.
        if (slot.CreatureIcon != null)
        {
            stDispatchStartReq.kUseFriendCharKey = slot.CreatureIcon.CreatureKey;
        }
        else
        {
            stDispatchStartReq.kUseFriendCharKey = 0;
        }

        CNetManager.Instance.SendPacket(CNetManager.Instance.DispatchProxy.DispatchStart, stDispatchStartReq, typeof(_stDispatchStartAck));
    }

    private IEnumerator DispatchMissionStartFail()
    {
        yield return new WaitForSeconds(0.1f);

        // 6846    팀에 반드시 자신의 크리처가 포함되어야 합니다.
        SystemPopupWindow.Instance.SetSystemPopup(enSystemPopupType.Ok, StringTableManager.GetData(4300), StringTableManager.GetData(6846));
    }
}