using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHpUI : MonoBehaviour
{
    public Slider hpSlider;
    public Canvas hpCanvas;
    private MonsterStat stat;

    private bool isVisible = false;
    private Transform cam; 

    void Start()
    {
        stat = GetComponent<MonsterStat>();
        cam = Camera.main.transform; 
        hpCanvas.enabled = false; 
    }

    void Update()
    {
        if (isVisible && stat != null)
        {
            hpSlider.value = (float)stat.currentHp / stat.maxHp;

           
            hpCanvas.transform.LookAt(cam);
            hpCanvas.transform.Rotate(0, 180f, 0);
        }
    }

    public void ShowHpBar()
    {
        hpCanvas.enabled = true;
        isVisible = true;
    }
}
