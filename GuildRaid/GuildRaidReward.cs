using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public class GuildRaidReward : UIWindow
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private GameObject _AllObject;

    [SerializeField] private UILabel _TitleLabel;

    [SerializeField] private TweenScale _FrameTweenScale;

    [SerializeField] private List<Transform> _RewardIconPosList = new List<Transform>();

    [SerializeField] private GameObject _ClosePanel;
    [SerializeField] private UILabel _ClosePanelLabel;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private List<GuildRaidRewardIcon> _RewardIconList = new List<GuildRaidRewardIcon>();

    private bool _IsEndAction = false;

    private GachaCardCamera _GachaCardCamera = null;
    private GachaBox _GachaBox = null;
    private Camera _3DCamera = null;
    private Transform _3DGUITransform = null;
    private Vector3 _3DCameraOriginPos = Vector3.zero;                                                  // 연출 이전 카메라 포지션.
    private Quaternion _3DCameraOriginRotion = Quaternion.identity;                                     // 연출 이전 카메라 로테이션.
    private Vector3 _3DCameraGachaBoxPos = Vector3.zero;                                                // 3D 박스 나올때 연출 포지션
    private Quaternion _3DCameraGachaBoxRotion = Quaternion.Euler(new Vector3(0.0f, 180.0f, 0.0f));     // 3D 박스 나올때 연출 로테이션.

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    protected override void Awake()
    {
        if (_ClosePanel != null) UIEventListener.Get(_ClosePanel).onClick = OnClickBack;
    }

    protected override void OnDestroy()
    {
        if (_ClosePanel != null) UIEventListener.Get(_ClosePanel).onClick -= OnClickBack;

        DestroyIcon();
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

    public override bool OnClickBack()
    {
        if (_IsEndAction == false)
            return false;

        UIControlManager.instance.ActiveWindow();

        UtilTransform.AttachTransForm(_3DGUITransform, _3DCamera.transform, SetTransformType.IgnoreValue);
        _3DCamera.transform.localPosition = _3DCameraOriginPos;
        _3DCamera.transform.localRotation = _3DCameraOriginRotion;

        UIControlManager.instance.Set3DProduction(true);

        if (_GachaBox != null) DestroyImmediate(_GachaBox.gameObject);
        if (_GachaCardCamera != null) DestroyImmediate(_GachaCardCamera.gameObject);

        return true;
    }

    public override void Init()
    {
        // 8746    길드레이드 상자 보상
        _TitleLabel.text = StringTableManager.GetData(8746);
        _ClosePanelLabel.text = StringTableManager.GetData(6311);
        _ClosePanelLabel.gameObject.SetActive(false);

        _IsEndAction = false;

        _AllObject.SetActive(false);

        UIControlManager.instance.InActiveWindow(new enUIType[] { WindowType });     // 켜져 있는 UI들을 다 끄고
        UIControlManager.instance.Set3DProduction(false);
        UIControlManager.instance.ShowLoading(false);
    }

    public void InitCard(_stOTShopBuyAck recvData)
    {
        DestroyIcon();

        int RewardIconPos = 0;

        foreach (CItem item in recvData.vAddItems)
        {
            GuildRaidRewardIcon guildRaidRewardIcon = UIResourceMgr.CreatePrefab<GuildRaidRewardIcon>(BUNDLELIST.PREFABS_UI_GUILDRAID, _RewardIconPosList[RewardIconPos], "GuildRaidRewardIcon");
            guildRaidRewardIcon.gameObject.SetActive(false);
            guildRaidRewardIcon.InitItem(item);
            _RewardIconList.Add(guildRaidRewardIcon);

            RewardIconPos++;
        }

        foreach(CCreatureDetail creature in recvData.vAddCreatures)
        {
            GuildRaidRewardIcon guildRaidRewardIcon = UIResourceMgr.CreatePrefab<GuildRaidRewardIcon>(BUNDLELIST.PREFABS_UI_GUILDRAID, _RewardIconPosList[RewardIconPos], "GuildRaidRewardIcon");
            guildRaidRewardIcon.gameObject.SetActive(false);
            guildRaidRewardIcon.InitCreature(creature);
            _RewardIconList.Add(guildRaidRewardIcon);

            RewardIconPos++;
        }

        //foreach (_stShopWealth wealth in recvData.vCurrWealth)
        //{
        //    if (wealth.kWealthType != DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType.ITEMTYPE_MI_GUILDTOKEN && UserInfo.Instance.GetWealth(wealth.kWealthType) != wealth.kWealthCount)
        //    {
        //        GuildRaidRewardIcon guildRaidRewardIcon = UIResourceMgr.CreatePrefab<GuildRaidRewardIcon>(BUNDLELIST.PREFABS_UI_GUILDRAID, _RewardIconPosList[RewardIconPos], "GuildRaidRewardIcon");
        //        guildRaidRewardIcon.gameObject.SetActive(false);
        //        guildRaidRewardIcon.InitWealth(wealth);
        //        _RewardIconList.Add(guildRaidRewardIcon);

        //        RewardIconPos++;
        //    }
        //}

        // 연출 카메라 셋팅.
        {
            if (_GachaCardCamera == null)
            {
                _GachaCardCamera = UIResourceMgr.CreatePrefab<GachaCardCamera>(BUNDLELIST.PREFABS_ETC_GACHACARD, UIControlManager.instance.GUI3DObj.transform, "GachaCardCamera");
            }

            _GachaCardCamera.Init();
        }

        // 연출 뽑기상자 셋팅.
        {
            if (_GachaBox == null)
            {
                _GachaBox = UIResourceMgr.CreatePrefab<GachaBox>(BUNDLELIST.PREFABS_ETC_GUILDBOX, UIControlManager.instance.MainMenuBaseObj.transform, "Guildbox", SetTransformType.IgnoreValue);
            }

            _GachaBox.Init();
        }

        // 카메라 원본값 저장.
        {
            _3DCamera = UIControlManager.instance.Camera3D;
            _3DCameraOriginPos = _3DCamera.transform.localPosition;
            _3DCameraOriginRotion = _3DCamera.transform.localRotation;
            _3DGUITransform = UIControlManager.instance.GUI3DObj.transform;
        }

        // 3D 연출을 하기 위해 카메라 변경.
        {
            _3DCamera.transform.parent = _GachaCardCamera.transform;
            _3DCamera.transform.localPosition = _3DCameraGachaBoxPos;
            _3DCamera.transform.localRotation = _3DCameraGachaBoxRotion;

            //_GachaBox.transform.localPosition = Vector3.zero;
        }

        _GachaBox.gameObject.SetActive(false);
        StartCoroutine(GachaBoxAction(0.5f));
    }

    private void DestroyIcon()
    {
        foreach (GuildRaidRewardIcon icon in _RewardIconList)
        {
            DestroyImmediate(icon.gameObject);
        }

        _RewardIconList.Clear();
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    private IEnumerator GachaBoxAction(float startDelayTime)
    {
        yield return new WaitForSeconds(startDelayTime);        // 오브젝트 로드 될때까지 기다림.

        _GachaCardCamera.Anim.Play();

        _GachaBox.gameObject.SetActive(true);
        _GachaBox.Anim.Play();
        //_GachaBox.OpenEffect.SetActive(true);

        //yield return new WaitForSeconds(Endtime);       // 연출이 끝날때까지 기다림.
        yield return new WaitForSeconds(_GachaBox.Anim.clip.length);       // 연출이 끝날때까지 기다림.

        _GachaBox.gameObject.SetActive(false);
        _AllObject.SetActive(true);

        yield return new WaitForSeconds(_FrameTweenScale.duration);      // 중간 백그라운드 오브젝트의 트윈 기다림.
        
        for(int i = 0; i < _RewardIconList.Count; ++i)
        {
            GuildRaidRewardIcon icon = _RewardIconList[i];
            if (icon == null)
                continue;

            icon.Action();

            yield return new WaitForSeconds(0.3f);
        }

        _IsEndAction = true;
        _ClosePanelLabel.gameObject.SetActive(true);
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
}
