using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public class GuildTributeRewardItem : MonoBehaviour
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UILabel _ItemNameLabel;
    [SerializeField] private UI2DSprite _Item2DSprite;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init(DATA_REWARD_NEW reward)
    {
        DATA_ITEM_NEW item = CDATA_ITEM_NEW.Get(reward.RewardValue);
        if (item == null)
            return;     // error

        if (item.m_enItemType == DATA_ITEM_TYPE_NEW._enItemStatusType.ITEMTYPE_MONEY)
        {
            _ItemNameLabel.text = reward.RewardCount.ToString();
        }
        else
        {
            _ItemNameLabel.text = StringTableManager.GetData(item.iItemName);
        }

        _Item2DSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_ITEMICON, item.m_szIconName);
    }
}