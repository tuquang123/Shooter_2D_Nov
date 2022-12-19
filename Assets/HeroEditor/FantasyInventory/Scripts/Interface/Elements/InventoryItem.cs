using System;
using System.Collections;
using System.Linq;
using Assets.HeroEditor.Common.CommonScripts;
using Assets.HeroEditor.FantasyInventory.Scripts.Data;
using Assets.HeroEditor.FantasyInventory.Scripts.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Assets.HeroEditor.FantasyInventory.Scripts.Interface.Elements
{
    /// <summary>
    /// Represents inventory item.
    /// </summary>
    public class InventoryItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        //public GameObject textNoEnoughGold;
        
        public Text textPrice;
        public GameObject panel;
        public bool active;
        public Image Icon;
        public Image Background;
        public Text Count;
        public GameObject Modificator;
        public Item Item;
        public Toggle Toggle;
        public ItemContainer Container;

        private Action _scheduled;
        private float _clickTime;

        public void OnEnable()
        {
            if (_scheduled != null)
            {
                StartCoroutine(ExecuteScheduled());
            }
        }

        public int price = 0 ;
        public void Buy()
        {
            if (GameManager.Instance.gold >= price)
            {
                panel.SetActive(false);
                GameManager.Instance.gold -= price;
                active = true;
                GameManager.Instance.dame += 5;
                Toggle.interactable = true;
            }
            else
            {
                //no enough
            }
        }
        public void Start()
        {
            Toggle.interactable = false;
            textPrice.text = price.ToString();
            if (Icon != null)
            {
                var collection = IconCollection.Active ?? IconCollection.Instances.First().Value;

                Icon.sprite = collection.FindIcon(Item.Params.Id);
            }

            if (Toggle)
            {
                Toggle.group = GetComponentInParent<ToggleGroup>();
            }

            if (Modificator)
            {
                var mod = Item.Modifier != null && Item.Modifier.Id != ItemModifier.None;

                Modificator.SetActive(mod);

                if (mod)
                {
                    Modificator.GetComponentInChildren<Text>().text = Item.Modifier.Id.ToString().ToUpper()[0].ToString();
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {

            if (active)
            {
                StartCoroutine(OnPointerClickDelayed(eventData));
                Invoke("ActiveFalse",0.25f);
            }
            
        }

        void ActiveFalse()
        {
            gameObject.SetActive(false);
        }

        private IEnumerator OnPointerClickDelayed(PointerEventData eventData) // TODO: A workaround. We should wait for initializing other components.
        {
            yield return null;

            OnPointerClick(eventData.button);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Container.OnMouseEnter?.Invoke(Item);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Container.OnMouseExit?.Invoke(Item);
        }

        public void OnPointerClick(PointerEventData.InputButton button)
        {
            if (button == PointerEventData.InputButton.Left)
            {
                Container.OnLeftClick?.Invoke(Item);

                var delta = Mathf.Abs(Time.time - _clickTime);

                if (delta < 0.5f) // If double click.
                {
                    _clickTime = 0;
                    Container.OnDoubleClick?.Invoke(Item);
                }
                else
                {
                    _clickTime = Time.time;
                }
            }
            else if (button == PointerEventData.InputButton.Right)
            {
                Container.OnRightClick?.Invoke(Item);
            }
        }

        public void Select(bool selected)
        {
            if (Toggle == null) return;

            if (gameObject.activeInHierarchy || !selected)
            {
                Toggle.isOn = selected;
            }
            else
            {
                _scheduled = () => Toggle.isOn = true;
            }

            if (selected)
            {
                Container.OnLeftClick?.Invoke(Item);
            }
        }

        private IEnumerator ExecuteScheduled()
        {
            yield return null;

            _scheduled();
            _scheduled = null;
        }
    }
}