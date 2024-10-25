using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class Trashcan : MonoBehaviour
{
    [SerializeField]
    private Sprite TrashcanFullSprite;

    private Image trashCanIconImage;

    private void Awake()
    {
        trashCanIconImage = GetComponent<Image>();
    }

    public void SetTrashFull()
    {
        // set icon image
        trashCanIconImage.sprite = TrashcanFullSprite;
    }
}
