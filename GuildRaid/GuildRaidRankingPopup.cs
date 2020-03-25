using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildRaidRankingPopup : UIWindow
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private GameObject _CloseButton;

    [SerializeField] private UILabel _TitleLabel;

    [SerializeField] private UILabel _MyGuildTitleLabel;

    [SerializeField] private UILabel _TotalRankTitleLabel;

    [SerializeField] private UILabel _BottomNoticeLabel;

    [SerializeField] private Transform _myGuild;

    [SerializeField] private Transform _no1Ranking;

    [SerializeField] private UIScrollView _uiScrollVeiw;
    [SerializeField] private UIGrid _uiGrid;

    [SerializeField] private GuildRaidRankInfiniteScrollView _guildRaidRankInfiniteScrollView;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private GuildRaidRankingItem _myRankingItem;

    private GuildRaidRankingItem _no1RankingItem;

    private List<GuildRaidRankingItem> _rankingItemList = new List<GuildRaidRankingItem>();

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    protected override void Awake()
    {
        if (_CloseButton != null) UIEventListener.Get(_CloseButton).onClick = OnClickBack;
    }

    protected override void OnDestroy()
    {
        if (_CloseButton != null) UIEventListener.Get(_CloseButton).onClick -= OnClickBack;

        ClearRankingItem();
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
        //Bounds b = _uiScrollVeiw.bounds;
        //Vector3 constraint = _uiScrollVeiw.panel.CalculateConstrainOffset(b.min, b.max);

        //if (constraint.y < -0.1f)
        //{
        //    TestAdd();
        //}
    }

    public override void Clear()
    {
    }

    public override void Init()
    {
        _TitleLabel.text = StringTableManager.GetData(8671);        // 8671 전체 길드 랭킹
        _MyGuildTitleLabel.text = StringTableManager.GetData(8678);     // 8678    내 길드
        _TotalRankTitleLabel.text = StringTableManager.GetData(3407);       // 3407	전체 순위
        _BottomNoticeLabel.text = StringTableManager.GetData(8672);     // 8672 랭킹은 매 주 월요일 오전 4시에 갱신됩니다.

        _uiGrid.sorting = UIGrid.Sorting.Custom;
        _uiGrid.onCustomSort = UtilFunc.SortByNumber;

        _guildRaidRankInfiniteScrollView.Init(200.0f, 82.0f);

        //TestAdd();
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void SetRecvData(_stGuildRaidGetRankListAck stAck)
    {
        //ClearRankingItem();

        //_myRankingItem = UIResourceMgr.CreatePrefab<GuildRaidRankingItem>(BUNDLELIST.PREFABS_UI_GUILDRAID, _myGuild, "GuildRaidRankingItem");
        //_myRankingItem.gameObject.SetActive(true);
        //_myRankingItem.name = stAck.kMyRankList.kGuildName;
        //_myRankingItem.Init(stAck.kMyRankList);

        //Transform parent;
        //foreach (CGuildRaidRankInfo data in stAck.kRankList)
        //{
        //    if (data.kGuildRaidRank == 1)
        //        parent = _no1Ranking;
        //    else
        //        parent = _uiGrid.transform;

        //    GuildRaidRankingItem item = UIResourceMgr.CreatePrefab<GuildRaidRankingItem>(BUNDLELIST.PREFABS_UI_GUILDRAID, parent, "GuildRaidRankingItem");
        //    item.gameObject.SetActive(true);
        //    item.name = data.kGuildRaidRank.ToString();
        //    item.Init(data);

        //    _rankingItemList.Add(item);
        //}

        ClearRankingItem();

        _myRankingItem = UIResourceMgr.CreatePrefab<GuildRaidRankingItem>(BUNDLELIST.PREFABS_UI_GUILDRAID, _myGuild, "GuildRaidRankingItem");
        _myRankingItem.gameObject.SetActive(true);
        _myRankingItem.name = stAck.kMyRankList.kGuildName;
        _myRankingItem.Init(stAck.kMyRankList);

        List<CGuildRaidRankInfo> kRankList = new List<CGuildRaidRankInfo>();

        foreach (CGuildRaidRankInfo data in stAck.kRankList)
        {
            if (data.kGuildRaidRank == 1)
            {
                _no1RankingItem = UIResourceMgr.CreatePrefab<GuildRaidRankingItem>(BUNDLELIST.PREFABS_UI_GUILDRAID, _no1Ranking, "GuildRaidRankingItem");
                _no1RankingItem.gameObject.SetActive(true);
                _no1RankingItem.name = data.kGuildRaidRank.ToString();
                _no1RankingItem.Init(data);
                continue;
            }

            kRankList.Add(data);
        }

        kRankList.Sort((a, b) => a.kGuildRaidRank.CompareTo(b.kGuildRaidRank));

        _guildRaidRankInfiniteScrollView.SetData(kRankList);
    }

    private void ClearRankingItem()
    {
        if (_myRankingItem != null) DestroyImmediate(_myRankingItem.gameObject);

        if (_no1RankingItem != null) DestroyImmediate(_no1RankingItem.gameObject);

        for (int i = 0; i < _rankingItemList.Count; ++i)
        {
            DestroyImmediate(_rankingItemList[i].gameObject);
        }

        _rankingItemList.Clear();
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
}
