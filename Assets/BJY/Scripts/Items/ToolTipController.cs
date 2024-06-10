using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTipController : MonoBehaviour, IPointerClickHandler, IPointerExitHandler
{
    public ToolTip tool_tip;

    private void Start()
    {
        tool_tip = GameObject.Find("Canvas").transform.GetChild(4).GetComponent<ToolTip>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        Item item = GetComponent<ItemEvent>().item;

        if (item != null)
        {
            tool_tip.gameObject.SetActive(true);
            tool_tip.SetToolTip(item.item_name, item.description, item.hp,
            item.power, item.speed, item.attack_speed, item.critical_rate);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tool_tip.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        tool_tip.gameObject.SetActive(false);
    }
}
