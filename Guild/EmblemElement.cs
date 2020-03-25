using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmblemElement : MonoBehaviour
{
    public delegate void DelegateSelectEmblem(EmblemElement emblem);
    public event DelegateSelectEmblem onSelectEvent;

    [SerializeField] private UI2DSprite m_EmblemSprite;
    [SerializeField] private UISprite m_SelectSprite;

    public void Init(string emblemName)
    {
        m_EmblemSprite.sprite2D = UIResourceMgr.CreateSprite(BUNDLELIST.TEXTURE_ICON_GUILDEMBLEM, emblemName);

        UIEventListener.Get(gameObject).onClick = OnClickSprite;
        ActiveSelect(false);
    }

    public void OnClickSprite(GameObject go)
    {
        if (go != null) SoundManager.Instance.PlayFX(enSoundFXUI.BUTTON_MEDIUM);

        if (onSelectEvent != null)
        {
            onSelectEvent(this);
        }

        ActiveSelect(true);
    }

    public void ActiveSelect(bool bIsActive)
    {
        m_SelectSprite.gameObject.SetActive(bIsActive);
    }

    public Sprite GetEmblemSprite()
    {
        return m_EmblemSprite.sprite2D;
    }
}