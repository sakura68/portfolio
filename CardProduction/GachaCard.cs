using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public class GachaCard : MonoBehaviour
{
    public enum enCardType
    {
        None,
        Creature,
        Item,
    }

    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private GameObject _CreatureCardParent;

    [SerializeField] private GameObject _ItemCardParent;
    [SerializeField] private ItemBaseIcon _ItemIcon;

    [SerializeField] private GameObject _PvpItemCardParent;
    [SerializeField] private ItemBaseIcon _PvpItemIcon;

    [SerializeField] private GameObject _RaidItemCardParent;
    [SerializeField] private ItemBaseIcon _RaidItemIcon;
    
    //===================================================================================
    //
    // Variable
    //
    //===================================================================================
    private CreatureIcon _CreatureIcon = null;

    private enCardType _enCardType = enCardType.None;

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
    public void Init(CCreatureDetail creatureData)
    {
        _enCardType = enCardType.Creature;

        _CreatureCardParent.SetActive(true);
        _ItemCardParent.SetActive(false);

        _CreatureIcon = UIResourceMgr.CreatePrefab<CreatureIcon>(BUNDLELIST.PREFABS_UI_COMMON, _CreatureCardParent.transform, "CreatureIcon");
        _CreatureIcon.SetIcon(creatureData.kCreatureKey, enCreatureIcon_Type.Shop);
        _CreatureIcon.RemoveBoxCollider();
        _CreatureIcon.RemoveDragScrollView();

        // default layer is 0
        Transform[] tran = _CreatureIcon.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in tran)
            t.gameObject.layer = 0;

        _CreatureIcon.gameObject.SetActive(true);
    }
#if GMTOOLSHOP
    public void Init(WEB_SHOP_UI_PRICETYPE._enWebListName kPayType, GachaCardItem.enCardGrade type, CItem item, DATA_ITEM_NEW ItemTableData)
    {
        _enCardType = enCardType.Item;

        _CreatureCardParent.SetActive(false);

        _ItemCardParent.SetActive(false);
        _PvpItemCardParent.SetActive(false);
        _RaidItemCardParent.SetActive(false);

        GameObject parent = null;
        if (kPayType == WEB_SHOP_UI_PRICETYPE._enWebListName.Medal)
        {
            parent = _PvpItemCardParent;
            _PvpItemIcon.Init(ItemTableData, item);
        }
        else if (kPayType == WEB_SHOP_UI_PRICETYPE._enWebListName.Shard)
        {
            parent = _RaidItemCardParent;
            _RaidItemIcon.Init(ItemTableData, item);
        }
        else
        {
            parent = _ItemCardParent;
            _ItemIcon.Init(ItemTableData, item);
        }

        parent.SetActive(true);
        Transform[] childrens = parent.GetComponentsInChildren<Transform>(true);
        if (childrens != null)
        {
            for (int i = 0; i < childrens.Length; ++i)
            {
                childrens[i].gameObject.SetActive(true);
            }
        }
    }

#else
    public void Init(DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType kPayType, GachaCardItem.enCardGrade type, CItem item, DATA_ITEM_NEW ItemTableData)
    {
        _enCardType = enCardType.Item;

        _CreatureCardParent.SetActive(false);

        _ItemCardParent.SetActive(false);
        _PvpItemCardParent.SetActive(false);
        _RaidItemCardParent.SetActive(false);

        GameObject parent = null;
        if (kPayType == DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType.ITEMTYPE_MI_MEDAL)
        {
            parent = _PvpItemCardParent;
            _PvpItemIcon.Init(ItemTableData, item);
        }
        else if (kPayType == DATA_ITEM_SUB_TYPE_NEW._enItemStatusSubType.ITEMTYPE_MI_SHARD)
        {
            parent = _RaidItemCardParent;
            _RaidItemIcon.Init(ItemTableData, item);
        }
        else
        {
            parent = _ItemCardParent;
            _ItemIcon.Init(ItemTableData, item);
        }

        parent.SetActive(true);
        Transform[] childrens = parent.GetComponentsInChildren<Transform>(true);
        if (childrens != null)
        {
            for (int i = 0; i < childrens.Length; ++i)
            {
                childrens[i].gameObject.SetActive(true);
                childrens[i].gameObject.layer = 0;
            }
        }
    }
#endif

}
