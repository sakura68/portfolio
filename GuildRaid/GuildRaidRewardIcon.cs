using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public class GuildRaidRewardIcon : MonoBehaviour
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private GameObject _wealthParent;
    [SerializeField] private UI2DSprite _wealthSprite;
    [SerializeField] private UILabel _wealthCountLabel;
    
    [SerializeField] private GameObject _creatureParent;
    [SerializeField] private UI2DSprite _creatureSprite;
    [SerializeField] private UISprite _creatureGradeSprite;
    [SerializeField] private UISprite _creatureClassSprite;

    [SerializeField] private GameObject _itemParent;
    [SerializeField] private UI2DSprite _itemSprite;
    [SerializeField] private UISprite _itemGradeSprite;

    [SerializeField] private WealthIcon _wealthIcon;

    [SerializeField] private CreatureIcon _creatureIcon;

    [SerializeField] private ItemBaseIcon _itemIcon;

    [SerializeField] private UILabel _IconNameLabel;

    [SerializeField] private GameObject _SparkEffect;

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
    public void InitItem(CItem item)
    {
        _wealthIcon.gameObject.SetActive(false);
        _creatureIcon.gameObject.SetActive(false);
        _itemIcon.gameObject.SetActive(false);

        GameObject itemGameObject = null;

        DATA_ITEM_NEW ItemTable = CDATA_ITEM_NEW.Get(item.m_ItemID);

        if (ItemTable.m_enItemType == DATA_ITEM_TYPE_NEW._enItemStatusType.ITEMTYPE_MONEY)
        {
            itemGameObject = _wealthIcon.gameObject;
            _wealthIcon.Init(ItemTable);
            _wealthCountLabel.text = item.m_ItemLot.ToString();
        }
        else
        {
            itemGameObject = _itemIcon.gameObject;
            _itemIcon.Init(ItemTable);
        }

        foreach (Transform tr in itemGameObject.GetComponentsInChildren<Transform>(true))
        {
            tr.gameObject.SetActive(true);
        }

        //_wealthParent.SetActive(false);
        //_creatureParent.SetActive(false);

        //foreach (Transform tr in _itemParent.GetComponentsInChildren<Transform>(true))
        //{
        //    tr.gameObject.SetActive(true);
        //}

        //DATA_ITEM_NEW ItemTable = CDATA_ITEM_NEW.Get(item.m_ItemID);
        //_itemSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_ITEMICON, ItemTable.m_szIconName);
        //_itemGradeSprite.spriteName = string.Format("{0}{1}", "inventory_slot0", ItemTable.m_iGrade);

        //_IconNameLabel.text = StringTableManager.GetData(ItemTable.iItemName);
    }

    public void InitCreature(CCreatureDetail creature)
    {
        _wealthIcon.gameObject.SetActive(false);
        _itemIcon.gameObject.SetActive(false);

        foreach (Transform tr in _creatureIcon.GetComponentsInChildren<Transform>(true))
        {
            tr.gameObject.SetActive(true);
        }

        int iCreatureTID = CDATA_CREATURE_NEWVER.Get(creature.kCreatureID).m_iCreatureTID;
        _creatureIcon.SetIcon(iCreatureTID, enCreatureIcon_Type.GuildRaidReward);

        //_wealthParent.SetActive(false);
        //_itemParent.SetActive(false);

        //foreach (Transform tr in _creatureParent.GetComponentsInChildren<Transform>(true))
        //{
        //    tr.gameObject.SetActive(true);
        //}

        //int iCreatureTID = CDATA_CREATURE_NEWVER.Get(creature.kCreatureID).m_iCreatureTID;
        //DATA_CREATURE_NEWVER CreatureTable = UtilFunc.GetCreatureDataByTID(iCreatureTID);

        //_creatureSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_CREATUREHEAD, CreatureTable.m_szIcon);
        //_creatureGradeSprite.spriteName = string.Format("{0}{1}", "comm_thumbnailStar0", CreatureTable.m_iGrade);
        //_creatureClassSprite.spriteName = string.Format("{0}{1}", "UI_", CreatureTable.m_enCreatureArmy.ToString());

        //_IconNameLabel.text = StringTableManager.GetData(CreatureTable.iCreatureName);
    }

    public void InitWealth(_stWealth wealth)
    {
        _creatureIcon.gameObject.SetActive(false);
        _itemIcon.gameObject.SetActive(false);

        foreach (Transform tr in _wealthIcon.GetComponentsInChildren<Transform>(true))
        {
            tr.gameObject.SetActive(true);
        }

        DATA_ITEM_NEW ItemTable;
        for (int i = 0; i < CDATA_ITEM_NEW.GetCount(); ++i)
        {
            ItemTable = CDATA_ITEM_NEW.GetByIndex(i);
            if (ItemTable.m_enItemSubType == wealth.kWealthType)
            {
                _wealthIcon.Init(ItemTable);
                break;
            }
        }

        //_creatureParent.SetActive(false);
        //_itemParent.SetActive(false);

        //foreach (Transform tr in _wealthParent.GetComponentsInChildren<Transform>(true))
        //{
        //    tr.gameObject.SetActive(true);
        //}

        //DATA_ITEM_NEW ItemTable;
        //for (int i = 0; i < CDATA_ITEM_NEW.GetCount(); ++i)
        //{
        //    ItemTable = CDATA_ITEM_NEW.GetByIndex(i);
        //    if(ItemTable.m_enItemSubType == wealth.kWealthType)
        //    {
        //        _wealthSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_ITEMICON, ItemTable.m_szIconName);
        //        _wealthCountLabel.text = wealth.kWealthCount.ToString();

        //        _IconNameLabel.text = StringTableManager.GetData(ItemTable.iItemName);
        //        break;
        //    }
        //}
    }

    public void Action()
    {
        gameObject.SetActive(true);
        _SparkEffect.SetActive(true);
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
}
