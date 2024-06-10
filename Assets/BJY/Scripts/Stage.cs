using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public int stage_id;
    public TextMeshProUGUI stage_name;
    [SerializeField] private TextMeshProUGUI stage_level;
    [SerializeField] private TextMeshProUGUI stage_info;
    [SerializeField] private Image stage_image;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void SetStage(int id,string name, string level, string info, Sprite image)
    {
        stage_id = id;
        stage_name.text = name;
        stage_level.text = level;
        stage_info.text = info;
        stage_image.sprite = image;
    }
}
