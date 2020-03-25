using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using DGL_DATA_READER;

public class DispatchInfiniteScrollView : InfiniteListPopulator
{
    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private List<CreatureItemInfo> _CreatureItemInfoList = new List<CreatureItemInfo>();

    private CreatureIcon.OnClickEvent OnClickEvent;

    private int _BaseCreatureCount = 4;

    //===================================================================================
    //
    // Default Method
    //
    //===================================================================================

    protected override void PopulateListItemWithIndex(InfiniteItemBehavior Item, int dataIndex)
    {
        SetCreatureItem(Item, dataIndex);
    }

    public override void RefreshItemVisable()
    {
        for (int i = 0; i < itemsPool.Count; ++i)
        {
            InfiniteItemBehavior ItemBehavior = itemsPool[i].GetComponent<InfiniteItemBehavior>();
            if (ItemBehavior == null)
                continue;

            SetCreatureItem(ItemBehavior, ItemBehavior.ItemDataIndex);
        }
    }

    public override void itemClicked(GameObject go, int itemDataIndex)
    {
        itemDataIndex = GetRealIndexForItem(itemDataIndex);
#if DEBUG_LOG
        Debug.Log("Clicked item " + itemDataIndex);
#endif

        CreatureIcon icon = go.GetComponent<CreatureIcon>();
        if (icon == null)
            return;

        if (OnClickEvent != null)
        {
            OnClickEvent(icon);
        }

        SetDispatchSelect(icon);

        //CreatureItemInfo info = _CreatureItemInfoList.Find((data) => data.m_creatureUnique == icon.CreatureKey);
        //if (info == null)
        //    return;

        //info.SetDispatchSelect(icon.IsDispatchSelect, icon.GetDispatchSelectNumberLabel());
    }

    protected override void CreateItem(InfiniteItemBehavior ItemBehavior)
    {
        for (int k = 0; k < _BaseCreatureCount; ++k)
        {
            float posx = k * 150.0f;
            CreatureIcon icon = UIResourceMgr.CreatePrefab<CreatureIcon>(BUNDLELIST.PREFABS_UI_COMMON, ItemBehavior.transform, "CreatureIcon");
            icon.name = k.ToString();
            icon.transform.localPosition = new Vector3(posx, 0.0f, 0.0f);

            ItemBehavior.AddItemElement(icon);
        }
    }

    private void OnDestroy()
    {
        _CreatureItemInfoList.Clear();
    }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init(CreatureIcon.OnClickEvent evt, float width, float height)
    {
        OnClickEvent = evt;

        _uiPanel = GetComponent<UIPanel>();

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

    public void SetData(List<CreatureItemInfo> CreatureItemInfoList)
    {
        _CreatureItemInfoList = CreatureItemInfoList;

        int aListCount = 0;
        if (_CreatureItemInfoList.Count % 4 > 0)
            aListCount = (_CreatureItemInfoList.Count / 4) + 1;
        else
            aListCount = _CreatureItemInfoList.Count / 4;

        InitScroll(aListCount);
    }

    private void SetCreatureItem(InfiniteItemBehavior itemBehaver, int dataIndex)
    {
        List<CreatureIcon> ItemElementList = itemBehaver.ItemElementList;
        int iCount = ItemElementList.Count;

        for (int i = 0; i < iCount; ++i)
        {
            int aIdx = ((dataIndex * iCount) + i);

            CreatureIcon ItemElement = ItemElementList[i];
            if (ItemElement == null)
                continue;

            if (_CreatureItemInfoList.Count > aIdx)
            {
                CreatureItemInfo ItemInfo = _CreatureItemInfoList[aIdx];
                if (ItemInfo == null)
                    continue;

                ItemElement.gameObject.SetActive(true);
                ItemElement.SetIcon(ItemInfo, enCreatureIcon_Type.Dispatch);

                ItemElement.SetActiveDispatchSelect(ItemInfo.IsDispatchSelect);
                ItemElement.SetDispatchSelectNumberLabel(ItemInfo.DispatchSelectNumber);
            }
            else
            {
                ItemElement.gameObject.SetActive(false);
            }
        }
    }

    public void SetDispatchSelect(CreatureIcon icon)
    {
        CreatureItemInfo info = _CreatureItemInfoList.Find((data) => data.CreatureKey == icon.CreatureKey);
        if (info == null)
            return;

        info.SetDispatchSelect(icon.IsDispatchSelect, icon.GetDispatchSelectNumberLabel());

        RefreshItemVisable();
    }

    public void ReSetDispatchTeam(ulong kCreatureKey)
    {
        for (int i = 0; i < itemsPool.Count; ++i)
        {
            InfiniteItemBehavior ItemBehavior = itemsPool[i].GetComponent<InfiniteItemBehavior>();
            if (ItemBehavior == null)
                continue;

            for(int k = 0; k < ItemBehavior.ItemElementList.Count; ++k)
            {
                CreatureIcon icon = ItemBehavior.ItemElementList[k];
                if (icon == null)
                    continue;

                if(icon.CreatureKey == kCreatureKey)
                {
                    if (OnClickEvent != null)
                        OnClickEvent(icon);

                    SetDispatchSelect(icon);

                    return;
                }
            }
        }
    }
}

