using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    private Text gemCountText;
    [SerializeField]
    private GameObject lifeUnit1; // left most
    [SerializeField]
    private GameObject lifeUnit2;
    [SerializeField]
    private GameObject lifeUnit3;
    [SerializeField]
    private GameObject lifeUnit4; // right most

    #region Singleton Patter
    //*****************
    // Singleton pattern
    // https://gamedev.stackexchange.com/a/116010/123894
    private static HUDManager _instance;
    public static HUDManager Instance { get { return _instance; } }

    private void Awake()
    {
        // Singleton Enforcement Code
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        gemCountText = GetComponentInChildren<Text>();
    }

    public void UpdateGemCount(int gemCount)
    {
        gemCountText.text = gemCount.ToString();
    }

    public void UpdateLifeBar(int playerHealth)
    {
        if (playerHealth == 4)
        {
            lifeUnit1.SetActive(true);
            lifeUnit2.SetActive(true);
            lifeUnit3.SetActive(true);
            lifeUnit4.SetActive(true);
        }
        else if (playerHealth == 3)
        {
            lifeUnit1.SetActive(true);
            lifeUnit2.SetActive(true);
            lifeUnit3.SetActive(true);
            lifeUnit4.SetActive(false);
        }
        else if (playerHealth == 2)
        {
            lifeUnit1.SetActive(true);
            lifeUnit2.SetActive(true);
            lifeUnit3.SetActive(false);
            lifeUnit4.SetActive(false);
        }
        else if (playerHealth == 1)
        {
            lifeUnit1.SetActive(true);
            lifeUnit2.SetActive(false);
            lifeUnit3.SetActive(false);
            lifeUnit4.SetActive(false);
        }
        else if (playerHealth <= 0)
        {
            lifeUnit1.SetActive(false);
            lifeUnit2.SetActive(false);
            lifeUnit3.SetActive(false);
            lifeUnit4.SetActive(false);
        }
    }


}
