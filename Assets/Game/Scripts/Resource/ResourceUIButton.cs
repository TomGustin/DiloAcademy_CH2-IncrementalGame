using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResourceUIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    [SerializeField] private Sprite buttonLockIdle;
    [SerializeField] private Sprite buttonLockClick;
    [SerializeField] private Sprite buttonSufficient;
    [SerializeField] private Sprite buttonNormalIdle;
    [SerializeField] private Sprite buttonNormalClick;
    [SerializeField] private Image image;
    public TextMeshProUGUI resourceDescription;
    public TextMeshProUGUI resourceUpgradeCost;
    public TextMeshProUGUI resourceUnlockCost;
    private ButtonType buttonType;

    private bool canInteract;

    private UnityEvent OnClick = new UnityEvent();

    private void OnEnable()
    {
        ChangeSprite(false);
    }

    public void Init(UnityAction OnClick)
    {
        this.OnClick.AddListener(OnClick);
    }

    public void UpdateDetail(string description, string upgradeCost, string unlockCost)
    {
        resourceDescription.text = description;
        resourceUnlockCost.text = unlockCost;
        resourceUpgradeCost.text = upgradeCost;
    }

    public void SetBuyable(bool isBuyable)
    {
        canInteract = isBuyable;

        ChangeSprite(false);
    }

    public void ChangeType(ButtonType buttonType)
    {
        this.buttonType = buttonType;

        if (buttonType.Equals(ButtonType.Lock))
        {
            resourceUpgradeCost.gameObject.SetActive(false);
            resourceUnlockCost.gameObject.SetActive(true);
        }
        else
        {
            resourceUpgradeCost.gameObject.SetActive(true);
            resourceUnlockCost.gameObject.SetActive(false);
        }
    }

    private void ChangeSprite(bool click)
    {
        if (buttonType.Equals(ButtonType.Lock))
        {
            if (canInteract) image.sprite = click ? buttonLockClick : buttonLockIdle;
            else image.sprite = buttonSufficient;
        }
        else
        {
            if (canInteract) image.sprite = click ? buttonNormalClick : buttonNormalIdle;
            else image.sprite = buttonSufficient;
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        ChangeSprite(true);
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        ChangeSprite(false);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        OnClick.Invoke();
    }

    public enum ButtonType { Lock, Unlock }
}
