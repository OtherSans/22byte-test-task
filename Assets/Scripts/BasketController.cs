using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BasketController : MonoBehaviour
{
    private CrystalTriggerComponent[] crystals = new CrystalTriggerComponent[] { };
    private TextMeshProUGUI[] textOnImage = new TextMeshProUGUI[] { };
    private Image[] images = new Image[] { };
    private int randomIndex;
    private void Awake()
    {
        crystals = FindObjectsOfType<CrystalTriggerComponent>();
        images = GetComponentsInChildren<Image>();
        textOnImage = GetComponentsInChildren<TextMeshProUGUI>();
        randomIndex = Random.Range(0, images.Length);
    }
    private void Start()
    {
        ChooseBasket(randomIndex);
        ClickedButtons(randomIndex);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            ChooseBasket(0);
            ClickedButtons(0);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            ChooseBasket(1);
            ClickedButtons(1);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            ChooseBasket(2);
            ClickedButtons(2);
        }
    }
    public void ChooseBasket(int imageIndex)
    {
        var layerName = LayerMask.NameToLayer(textOnImage[imageIndex].text);
        foreach (var crystal in crystals)
        {
            crystal.crystalLayer = layerName;
        }
    }
    public void ClickedButtons(int imageIndex)
    {
        images[imageIndex].color = Color.gray;
        images[imageIndex].transform.localScale = new Vector3(0.9f, 0.9f, 1);
        for (int i = imageIndex + 1; i < images.Length; i++)
        {
            images[i].color = Color.white;
            images[i].transform.localScale = Vector3.one;
        }
        for(int i = imageIndex - 1; i >= 0 ; i--)
        {
            images[i].color = Color.white;
            images[i].transform.localScale = Vector3.one;
        }
    }
}
