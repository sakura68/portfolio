using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureIconSmall : CreatureIcon
{
    //===================================================================================
    //
    // Field
    //
    //===================================================================================
    [SerializeField] private UILabel _PercentLabel;

    [SerializeField] private GameObject _ResultMarkParent;                  // 항상 켜놓는다.
    [SerializeField] private UISprite _ResultSuccessSprite;                 // 성공 이미지.
    [SerializeField] private UISprite _ResultFailSprite;                    // 실패 이미지.
    [SerializeField] private UISprite _ResultReturnSprite;                  // 반환됨 이미지.
    
    [SerializeField] private TweenController _SuccessTweenController;
    [SerializeField] private TweenController _FailTweenController;
    [SerializeField] private TweenController _ReturnTweenController;

    //===================================================================================
    //
    // Variable
    //
    //===================================================================================

    private GameObject _ReinforceSuccessEffect;            // 강화 성공했을때 이펙트.
    private GameObject _ReinforceMaterialEffect;           // 강화시 재료로 넣었을때 이펙트.

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
    public bool SetIconReinforceMaterial(ulong kCreatureUniqueIdx, enCreatureIcon_Type type, UIEventListener.VoidDelegate ClickEvt)
    {
        if (type != enCreatureIcon_Type.CreatureReinforceMaterial)
        {
#if DEBUG_LOG
            Debug.LogError("=== No Material === ");
#endif
            return false;
        }

        if(_ResultMarkParent != null)
        {
            _ResultMarkParent.SetActive(true);
        }

        if (_ResultSuccessSprite != null) _ResultSuccessSprite.gameObject.SetActive(false);
        if (_ResultFailSprite != null) _ResultFailSprite.gameObject.SetActive(false);
        if (_ResultReturnSprite != null) _ResultReturnSprite.gameObject.SetActive(false);

        // 강화시 재료로 넣었을때 이펙트.
        if (_ReinforceMaterialEffect == null)
            _ReinforceMaterialEffect = UIResourceMgr.CreatePrefab(BUNDLELIST.PREFABS_EFFECT_UI, transform, "Fx_UI_Cha_reinforce_s_Equip", SetTransformType.Default);
        _ReinforceMaterialEffect.SetActive(true);   // 재료로 넣었을때 바로 나와야 해서 active true

        if (_ReinforceSuccessEffect != null)
            _ReinforceSuccessEffect.SetActive(false);
        else
        {        
            _ReinforceSuccessEffect = UIResourceMgr.CreatePrefab(BUNDLELIST.PREFABS_EFFECT_UI, transform, "Fx_UI_Cha_reinforce_s", SetTransformType.Default);
            _ReinforceSuccessEffect.SetActive(false);            
        }

        UIEventListener.Get(gameObject).onClick = ClickEvt;

        return base.SetIcon(kCreatureUniqueIdx, type);
    }

    public bool SetIconReinforceResultMaterial(ulong kCreatureUniqueIdx, enCreatureIcon_Type type, bool bUsed, bool bSuccess)
    {
        if (type != enCreatureIcon_Type.CreatureReinforceResultMaterial)
        {
#if DEBUG_LOG
            Debug.LogError("=== No Material === ");
#endif
            return false;
        }

        // 결과 부모는 항상 On
        if (_ResultMarkParent != null) { _ResultMarkParent.SetActive(true); }

        // 강화 확률 사용안함.
        if (_PercentLabel != null) { _PercentLabel.gameObject.SetActive(false); }


        // 강화 결과에서 안쓰임 Off.
        if (_ReinforceMaterialEffect != null)
            _ReinforceMaterialEffect.SetActive(false);
        else
        {
            _ReinforceMaterialEffect = UIResourceMgr.CreatePrefab(BUNDLELIST.PREFABS_EFFECT_UI, transform, "Fx_UI_Cha_reinforce_s_Equip", SetTransformType.Default);
            _ReinforceMaterialEffect.SetActive(false);
        }

        // 일단 꺼두고 성공에서만 킨다.
        if (_ReinforceSuccessEffect != null)
            _ReinforceSuccessEffect.SetActive(false);
        else
        {
            _ReinforceSuccessEffect = UIResourceMgr.CreatePrefab(BUNDLELIST.PREFABS_EFFECT_UI, transform, "Fx_UI_Cha_reinforce_s", SetTransformType.Default);
            _ReinforceSuccessEffect.SetActive(false);
        }


        if (_ResultSuccessSprite != null) _ResultSuccessSprite.gameObject.SetActive(false);
        if (_ResultFailSprite != null) _ResultFailSprite.gameObject.SetActive(false);
        if (_ResultReturnSprite != null) _ResultReturnSprite.gameObject.SetActive(false);

        // 크리쳐를 강화에 사용했으면.
        if (bUsed == true)
        {
            // 강화 성공.
            if (bSuccess == true)
            {
                if (_ResultSuccessSprite != null) _ResultSuccessSprite.gameObject.SetActive(true);
                if (_ReinforceSuccessEffect != null) _ReinforceSuccessEffect.SetActive(true);

            }
            else
            {
                if (_ResultFailSprite != null) _ResultFailSprite.gameObject.SetActive(true);
            }
        }
        else
        {
            if (_ResultReturnSprite != null) _ResultReturnSprite.gameObject.SetActive(true);
        }

        return base.SetIcon(kCreatureUniqueIdx, type);
    }

    public void SetReinforcePercentLabel(int value)
    {
        if (_PercentLabel != null)
        {
            _PercentLabel.gameObject.SetActive(true);

            //100 : 8BFF2D
            //70 : FFF463
            //40 : FFAC17
            //10 : FF652D

            if (value >= 100)
                _PercentLabel.color = new Color(139f / 255f, 1f, 45f / 255f, 1f);
            else if(value >= 70)
                _PercentLabel.color = new Color(1f, 244f / 255f, 99f / 255f, 1f);
            else if(value >= 40)
                _PercentLabel.color = new Color(1f, 172f / 255f, 23f / 255f, 1f);
            else
                _PercentLabel.color = new Color(1f, 101f / 255f, 45f / 255f, 1f);

            _PercentLabel.text = string.Format("{0}%", value);
        }
    }

    //===================================================================================
    //
    // Event
    //
    //===================================================================================
}
