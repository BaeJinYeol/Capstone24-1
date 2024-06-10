using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    public TextMeshProUGUI name_text;
    public TextMeshProUGUI desc_text;
    public TextMeshProUGUI hp_text;
    public TextMeshProUGUI hp_value_text;
    public TextMeshProUGUI power_text;
    public TextMeshProUGUI power_value_text;
    public TextMeshProUGUI speed_text;
    public TextMeshProUGUI speed_value_text;
    public TextMeshProUGUI aspeed_text;
    public TextMeshProUGUI aspeed_value_text;
    public TextMeshProUGUI critical_text;
    public TextMeshProUGUI critical_value_text;

    public void SetToolTip(string name, string desc, int hp, int power, int speed, int aspeed, int critical)
    {
        name_text.text = name;
        desc_text.text = desc;

        if (hp == 0)
        {
            hp_text.gameObject.SetActive(false);
            hp_value_text.gameObject.SetActive(false);
        }
        else
        {
            hp_text.gameObject.SetActive(true);
            hp_value_text.gameObject.SetActive(true);
            hp_value_text.text = hp.ToString();
        }

        if (power == 0)
        {
            power_text.gameObject.SetActive(false);
            power_value_text.gameObject.SetActive(false);
        }
        else
        {
            power_text.gameObject.SetActive(true);
            power_value_text.gameObject.SetActive(true);
            power_value_text.text = power.ToString();
        }

        if (speed == 0)
        {
            speed_text.gameObject.SetActive(false);
            speed_value_text.gameObject.SetActive(false);
        }
        else
        {
            speed_text.gameObject.SetActive(true);
            speed_value_text.gameObject.SetActive(true);
            speed_value_text.text = speed.ToString();
        }

        if (aspeed == 0)
        {
            aspeed_text.gameObject.SetActive(false);
            aspeed_value_text.gameObject.SetActive(false);
        }
        else
        {
            aspeed_text.gameObject.SetActive(true);
            aspeed_value_text.gameObject.SetActive(true);
            aspeed_value_text.text = aspeed.ToString();
        }

        if (critical == 0)
        {
            critical_text.gameObject.SetActive(false);
            critical_value_text.gameObject.SetActive(false);
        }
        else
        {
            critical_text.gameObject.SetActive(true);
            critical_value_text.gameObject.SetActive(true);
            critical_value_text.text = critical.ToString();
        }
    }

    float half_width;
    RectTransform rect;

    private void Start()
    {
        half_width = GetComponentInParent<CanvasScaler>().referenceResolution.x * 0.5f;
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector3 world_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        world_position.z = 0f;

        transform.position = world_position;
        if (rect.anchoredPosition.x + rect.sizeDelta.x > half_width)
            rect.pivot = new Vector2(1, 1);
        else
            rect.pivot = new Vector2(0, 1);
    }
}
