using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildRaidRankInfiniteScrollView : InfiniteListPopulator
{
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
    private List<CGuildRaidRankInfo> _guildRaidRankInfo = new List<CGuildRaidRankInfo>();

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================
    protected override void PopulateListItemWithIndex(InfiniteItemBehavior ItemBehavior, int dataIndex)
    {
        ItemBehavior.guildRaidRankElement.Init(_guildRaidRankInfo[ItemBehavior.ItemDataIndex]);
    }

    public override void RefreshItemVisable()
    {
        for (int i = 0; i < itemsPool.Count; ++i)
        {
            InfiniteItemBehavior ItemBehavior = itemsPool[i].GetComponent<InfiniteItemBehavior>();
            if (ItemBehavior == null)
                continue;

            ItemBehavior.guildRaidRankElement.Init(_guildRaidRankInfo[ItemBehavior.ItemDataIndex]);
        }
    }

    public override void itemClicked(GameObject go, int itemDataIndex)
    {
    }

    protected override void CreateItem(InfiniteItemBehavior ItemBehavior)
    {
        GuildRaidRankingItem guildRaidRankElement = UIResourceMgr.CreatePrefab<GuildRaidRankingItem>(BUNDLELIST.PREFABS_UI_GUILDRAID, ItemBehavior.transform, "GuildRaidRankingItem");
        ItemBehavior.AddItemElement(guildRaidRankElement);
    }

    private void OnDestroy()
    {
        _guildRaidRankInfo.Clear();
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init(float width, float height)
    {
        _uiPanel = GetComponent<UIPanel>();
        uiScrollView.panel = _uiPanel;

        _cellWidth = width;
        _cellHeight = height;

        if (_uiGrid != null)
        {
            _uiGrid.cellWidth = width;
            _uiGrid.cellHeight = height;
        }

        GameObject prefab = UIResourceMgr.CreateDefaultPrefab(BUNDLELIST.PREFABS_UI_COMMON, "InfiniteItemBehavior");
        if (prefab != null)
        {
            itemPrefab = prefab.transform;
        }
    }

    public void SetData(List<CGuildRaidRankInfo> guildRaidRankInfo)
    {
        _guildRaidRankInfo.Clear();
        _guildRaidRankInfo = guildRaidRankInfo;

        InitScroll(_guildRaidRankInfo.Count);
    }
}
