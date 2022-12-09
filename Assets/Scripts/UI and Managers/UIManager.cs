using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour {

    public static UIManager uIManager;

    [SerializeField] Sprite[] leftLoadingSprite;
    [SerializeField] Sprite[] rightLoadingSprite;
    [SerializeField] Image leftSprite;
    [SerializeField] Image rightSprite;

    [SerializeField] GameObject[] UIGame;
    [SerializeField] GameObject[] UIUnload;

    [SerializeField] TextMeshProUGUI leftMoney;
    [SerializeField] TextMeshProUGUI rightMoney;

    [SerializeField] GameObject gameDerCosas;
    [SerializeField] GameObject descDerCosas;

    [SerializeField] GameObject[] buttons;
    [SerializeField] GameObject[] sticks;

    GameplayDataHolder gameplayDataHolder;

    public enum Side {
        Left,
        Right
    }

    private void Start() 
    {
        uIManager = this;
        gameplayDataHolder = GameplayDataHolder.Instance;

        for (int i = 0; i < UIGame.Length; i++)
            UIGame[i].SetActive(true);
        for (int i = 0; i < UIUnload.Length; i++)
            UIUnload[i].SetActive(false);

        if (gameplayDataHolder.GetPlayerAmount() == GameplayDataHolder.PlayerAmount.MultiPlayer) {
            rightSprite.gameObject.SetActive(true);
            rightMoney.gameObject.SetActive(true);
            rightMoney.gameObject.SetActive(true);
            sticks[0].SetActive(true);
            sticks[1].SetActive(true);
        }

        else {
            rightSprite.gameObject.SetActive(false);
            rightMoney.gameObject.SetActive(false);
            rightMoney.gameObject.SetActive(false);
            sticks[0].SetActive(true);
            sticks[1].SetActive(false);
        }

        for (int i = 0; i < buttons.Length; i++)
            if (buttons[i] != null)
                buttons[i].SetActive(false);

#if UNITY_ANDROID || UNITY_IOS

        if(gameplayDataHolder.GetPlayerAmount() == GameplayDataHolder.PlayerAmount.MultiPlayer) {
            buttons[0].SetActive(true);
            buttons[3].SetActive(true);
        }
        else {
            buttons[0].SetActive(true);
            buttons[3].SetActive(false);
        }

#endif

        SubscribeToEvents(true);
    }

    private void OnDestroy() 
    {
        uIManager = null;
        SubscribeToEvents(false);
    }


    void SubscribeToEvents(bool subscribe)
    {
        if (subscribe)
        {
            Player.MoneySwapped += UpdateMoney;
            Player.OnExitedUnload += SalidaDescarga;
            Player.OnEnteredUnload += EntradaDescarga;
            Player.OnBagGrabbed += CambiarSprite;
        }
        else
        {
            Player.MoneySwapped -= UpdateMoney;
            Player.OnExitedUnload -= SalidaDescarga;
            Player.OnEnteredUnload -= EntradaDescarga;
            Player.OnBagGrabbed -= CambiarSprite;
        }
    }

    void UpdateMoney(int l, float p) {

        if (l == (int)Side.Left) {
            leftMoney.text = "$: " + (p / 1000f).ToString("F2");
            rightMoney.text = "$: " + (p / 1000f).ToString("F2");
        }
        else if (l == (int)Side.Right) {
            rightMoney.text = "$: " + (p / 1000f).ToString("F2");
            rightMoney.text = "$: " + (p / 1000f).ToString("F2");
        }
    }

    void EntradaDescarga(int l) {
        UIGame[l].SetActive(false);
        UIUnload[l].SetActive(true);
    }

    void SalidaDescarga(int l) {
        UIUnload[l].SetActive(false);
        UIGame[l].SetActive(true);
    }

    void CambiarSprite(int l, int sprite) {
        if (l == (int)Side.Left)
            leftSprite.sprite = leftLoadingSprite[sprite];
        else if (l == (int)Side.Right)
            rightSprite.sprite = rightLoadingSprite[sprite];
    }
}
