using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ItemEvent : MonoBehaviour, 
    IPointerEnterHandler, 
    IPointerExitHandler,
    IPointerClickHandler
{
    // 아이템 마우스 오버 이벤트 변수
    private float hoverScaleFactor = 1.3f; // 마우스 호버 시 이미지의 크기를 확대하는 비율
    private Vector3 originalScale; // 원래 크기를 저장하는 변수
    private GameObject[] child = new GameObject[3];

    private InventoryManager inventory_manager;

    private Transform item_box;
    private Transform dress_box;
    private Transform root_transform;
    public SPUM_SpriteList playerSprite;

    private bool isStart = false;

    public Item item;

    private void Start()
    {
        isStart = true;

        inventory_manager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();

        if (transform.childCount > 0)
        {
            Transform childTransform = transform.GetChild(0);
            child[0] = childTransform.gameObject;
            originalScale = child[0].transform.localScale;
        }
        if (transform.childCount > 1)
        {
            Transform childTransform = transform.GetChild(1);
            child[1] = childTransform.gameObject;
            originalScale = child[1].transform.localScale;
        }
        if (transform.childCount > 2)
        {
            Transform childTransform = transform.GetChild(2);
            child[2] = childTransform.gameObject;
            originalScale = child[2].transform.localScale;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 마우스가 이미지에 들어오면 이미지 크기를 확대합니다.
        for (int i = 0; i < transform.childCount; i++)
        {
            child[i].transform.localScale = originalScale * hoverScaleFactor;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        // 마우스가 이미지에서 나가면 이미지 크기를 원래 크기로 되돌립니다.
        for (int i = 0; i < transform.childCount; i++)
        {
            child[i].transform.localScale = originalScale;
        }
    }

    public void OnMouseDoubleClick()
    {
        inventory_manager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        if (isStart == true)
        {
            GameObject.Find("MainSceneManager").GetComponent<MainSceneManager>().PlaySfxSound(false);
        }

        if (transform.parent.CompareTag("DressBox"))
        {
            dress_box = inventory_manager.dress_box;
            Transform box = dress_box.Find($"{item.type} Box");
            if (box.transform.childCount != 0)
            {
                Destroy(box.transform.GetChild(0).gameObject);
            }
            PlayerInfoManager.Instance.PutOnDress(item);
            DuplicateObject(box.transform);
            SetSprite(item.type, true);
        }
        else if (transform.parent.CompareTag("InvenBox"))
        {
            item_box = inventory_manager.item_box;
            Transform box = item_box.Find($"{item.type} Box");
            if (box.transform.childCount != 0)
            {
                inventory_manager.GenerateItem(box.transform.GetChild(0).gameObject.GetComponent<ItemEvent>().item);
                Destroy(box.transform.GetChild(0).gameObject);
            }

            PlayerInfoManager.Instance.PutOnItem(item);
            DuplicateObject(box.transform);
            SetSprite(item.type, true);
            Destroy(this.gameObject);
        }
        else if (transform.parent.CompareTag("DressPanel"))
        {
            PlayerInfoManager.Instance.TakeOffDress(item);
            SetSprite(item.type, false);
            Destroy(this.gameObject);
        }
        else if (transform.parent.CompareTag("InvenPanel"))
        {

            PlayerInfoManager.Instance.TakeOffItem(item);
            SetSprite(item.type, false);
            inventory_manager.GenerateItem(item);
            
            Destroy(this.gameObject);
        }
    }
    public void DuplicateObject(Transform parentTransform)
    {
        // 원본 오브젝트를 복제하여 새로운 오브젝트 생성
        GameObject newObject = Instantiate(this.gameObject, parentTransform);
        RectTransform rect = newObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = Vector2.zero;
    }

    float clickTime = 0f;
    public void OnPointerClick(PointerEventData eventData)
    {
        if ((Time.time - clickTime) < 0.3f)
        {
            OnMouseDoubleClick();
            clickTime = -1;
        }
        else
        {
            clickTime = Time.time;
        }
    }

    public void SetSprite(ItemType type, bool flag)
    {
        root_transform = inventory_manager.player_root;
        playerSprite = root_transform.GetComponent<SPUM_SpriteList>();
        switch (type)
        {
            case ItemType.Armor:
                Sprite[] armor_sprites;
                armor_sprites = Resources.LoadAll<Sprite>(item.icon_path);
                if (flag)
                {
                    if (armor_sprites.Length == 3)
                    {
                        playerSprite._armorList[0].sprite = armor_sprites[0];
                        playerSprite._armorList[1].sprite = armor_sprites[1];
                        playerSprite._armorList[2].sprite = armor_sprites[2];
                    }
                    else
                    {
                        playerSprite._armorList[0].sprite = item.icon;
                        playerSprite._armorList[1].sprite = null;
                        playerSprite._armorList[2].sprite = null;
                    }
                }
                else
                {
                    playerSprite._armorList[0].sprite = null;
                    playerSprite._armorList[1].sprite = null;
                    playerSprite._armorList[2].sprite = null;
                }
                break;
            case ItemType.Back:
                if (flag)
                {
                    playerSprite._backList[0].sprite = item.icon;
                }
                else
                {
                    playerSprite._backList[0].sprite = null;
                }
                break;
            case ItemType.Cloth:
                Sprite[] cloth_sprites = Resources.LoadAll<Sprite>(item.icon_path);
                if (flag)
                {
                    if (cloth_sprites.Length == 3)
                    {
                        playerSprite._clothList[0].sprite = cloth_sprites[0];
                        playerSprite._clothList[1].sprite = cloth_sprites[1];
                        playerSprite._clothList[2].sprite = cloth_sprites[2];
                    }
                    else
                    {
                        playerSprite._clothList[0].sprite = cloth_sprites[0];
                        playerSprite._clothList[1].sprite = null;
                        playerSprite._clothList[2].sprite = null;
                    }
                }
                else
                {
                    playerSprite._clothList[0].sprite = null;
                    playerSprite._clothList[1].sprite = null;
                    playerSprite._clothList[2].sprite = null;
                }
                break;
            case ItemType.Hair:
                if (flag)
                {
                    playerSprite._hairList[0].sprite = item.icon;
                }
                else
                {
                    playerSprite._hairList[0].sprite = null;
                }
                break;
            case ItemType.FaceHair:
                if (flag)
                {
                    playerSprite._hairList[3].sprite = item.icon;
                }
                else
                {
                    playerSprite._hairList[3].sprite = null;
                }
                break;
            case ItemType.Foot:
                Sprite[] foot_sprites = Resources.LoadAll<Sprite>(item.icon_path);
                if (flag)
                {
                    playerSprite._pantList[0].sprite = foot_sprites[0];
                    playerSprite._pantList[1].sprite = foot_sprites[1];
                }
                else
                {
                    playerSprite._pantList[0].sprite = null;
                    playerSprite._pantList[1].sprite = null;
                }
                break;
            case ItemType.Helmet:
                if (flag)
                {
                    playerSprite._hairList[1].sprite = item.icon;
                }
                else
                {
                    playerSprite._hairList[1].sprite = null;
                }
                break;
            case ItemType.Weapon:
                if (flag)
                {
                    playerSprite._weaponList[0].sprite = item.icon;
                }
                else
                {
                    playerSprite._weaponList[0].sprite = null;
                }
                break;
            case ItemType.SubWeapon:
                if (flag)
                {
                    playerSprite._weaponList[3].sprite = item.icon;
                }
                else
                {
                    playerSprite._weaponList[3].sprite = null;
                }
                break;
        }
    }
}
