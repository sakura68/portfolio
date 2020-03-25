using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DGL_DATA_READER;

public class DispatchMissionItemLeft : MonoBehaviour
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UILabel m_TitleLabel;
    [SerializeField] private UI2DSprite m_RegionSprite;
    [SerializeField] private UISprite m_SelectSprite;

    //===================================================================================
    //
    // Variable
    //
    //==================================================================================
    private DATA_DISPATCH m_DispatchTableData = null;
    public DATA_DISPATCH DispatchTableData { get { return m_DispatchTableData; } }

    //===================================================================================
    //
    // Method
    //
    //===================================================================================
    public void Init(DATA_DISPATCH DispatchTableData)
    {
        m_DispatchTableData = DispatchTableData;

        DATA_DISPATCH_CATEGORY DispatchCategoryData = CDATA_DISPATCH_CATEGORY.Get(m_DispatchTableData.DispatchCategory);
        if(DispatchCategoryData != null)
        {
            m_TitleLabel.text = StringTableManager.GetData(DispatchCategoryData.String_Name);
            m_RegionSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_DISPATCH, DispatchCategoryData.Image_Name);
        }

        SetActiveSelect(false);
    }

    public void SetActiveSelect(bool bIsActive)
    {
        m_SelectSprite.gameObject.SetActive(bIsActive);
    }
}