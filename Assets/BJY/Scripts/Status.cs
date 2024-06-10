using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Status : MonoBehaviour
{
    public TextMeshProUGUI hp_value;
    public TextMeshProUGUI power_value;
    public TextMeshProUGUI speed_value;
    public TextMeshProUGUI attack_speed_value;
    public TextMeshProUGUI critical_value;

    public void UpdateStat()
    {
        PlayerInfoManager plyer_info = PlayerInfoManager.Instance;
        hp_value.text = plyer_info.player_hp.ToString();
        power_value.text = plyer_info.player_power.ToString();
        speed_value.text = plyer_info.player_speed.ToString();
        attack_speed_value.text = plyer_info.player_attack_speed.ToString();
        critical_value.text = plyer_info.player_critical_rate.ToString();
    }
}
