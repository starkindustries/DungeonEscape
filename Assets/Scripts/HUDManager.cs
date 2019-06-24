using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    private Player player;
    private Text gemCountText;
    [SerializeField]
    private GameObject lifeUnit1; // left most
    [SerializeField]
    private GameObject lifeUnit2;
    [SerializeField]
    private GameObject lifeUnit3;
    [SerializeField]
    private GameObject lifeUnit4; // right most

    // Start is called before the first frame update
    void Start()
    {
        gemCountText = GetComponentInChildren<Text>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();        
    }

    // Update is called once per frame
    void Update()
    {
        gemCountText.text = player.gemCount.ToString();
        UpdateLifeBar(player.Health);
    }

    void UpdateLifeBar(int playerHealth)
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
        else if (playerHealth == 0)
        {
            lifeUnit1.SetActive(false);
            lifeUnit2.SetActive(false);
            lifeUnit3.SetActive(false);
            lifeUnit4.SetActive(false);
        }
    }


}
