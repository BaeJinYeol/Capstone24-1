using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public float moveSpeed;
    public float alphaSpeed;
    public float destroyTime;
    public int   damage;

    TextMeshPro  text;
    Color        alpha;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
        alpha = text.color;
    }

    private void Start()
    {
        Invoke("DestroyObject", destroyTime);
        text.text = damage.ToString();
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
