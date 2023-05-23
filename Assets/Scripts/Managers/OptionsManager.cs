using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class OptionsManager : MonoBehaviour
{
    #region Variables
    public static int optionSelector { get; set; }
    public static int style { get; set; }
    public static int textBorderStyle { get; set; }
    public static int fontStyle { get; set; }
    public static int difficulty { get; set; }
    private static char empty = '\0';
    public static char rgb { get; set; }
    private Vector3[,] originalTextPos = new Vector3[16, 5]; //used to save original position of text elements so when font style is changed, an appropriate text offset can be used; 1st element is optionSelector 2nd element is font style
    private int cursorOffset = 5;

    public static Color TextBorderColor { get; set; }
    public static Color TextBoxColor { get; set; }
    public static Color GradientColor { get; set; }
    public static Color TextColor { get; set; }
    public static Color TextShadowColor { get; set; }
    public static Font TextFont { get; set; }
    public static bool GridOn { get; set; }
    public static Color GridColor { get; set; }

    public Image screen;
    public GameObject mouseTrigger;
    public Image textCursor;
    public Text[] text;
    public Text[] rText;
    public Text[] gText;
    public Text[] bText;
    public Text[] borderNumText;
    public Text[] fontNumText;
    public Text[] difficultyNumText;

    public Image[] textBorders;
    public Image[] textBoxes;
    public Image[] gradients;

    public Slider textBorderColorSliderR;
    public Slider textBorderColorSliderG;
    public Slider textBorderColorSliderB;
    public Slider textBoxColorSliderR;
    public Slider textBoxColorSliderG;
    public Slider textBoxColorSliderB;
    public Slider textBoxTransparencySlider;
    public Slider gradientTransparencySlider;
    public Slider gradientSliderR;
    public Slider gradientSliderG;
    public Slider gradientSliderB;
    public Slider textColorSliderR;
    public Slider textColorSliderG;
    public Slider textColorSliderB;
    public Slider textShadowSliderR;
    public Slider textShadowSliderG;
    public Slider textShadowSliderB;
    public Slider textShadowTransparencySlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;

    private static GameObject saveSettingsPrompt;
    private static GameObject defaultSettingsPrompt;
    private static GameObject cancelChangesPrompt;
    private static GameObject prompt;
    private static GameObject emptyGameObject;

    public static OptionsManager Instance { get; private set; }
    #endregion

    #region Properties
    public int CursorOffset
    {
        get
        {
            return cursorOffset;
        }
        set
        {
            cursorOffset = value;
        }
    }
    #endregion

    #region MonoBehavior
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") != 0 && prompt == null && style == 0 && rgb == empty)
        {
            Option.Discard();
            if (optionSelector == 12 && Input.GetAxisRaw("Vertical") < 0)
            {
                optionSelector = 14;
            }
            else if (optionSelector < 13)
            {
                optionSelector = optionSelector - (int)Input.GetAxisRaw("Vertical");
                optionSelector = Mathf.Clamp(optionSelector, 0, 12);
            }
            else if (optionSelector > 12 && Input.GetAxisRaw("Vertical") > 0)
            {
                optionSelector = 12;
            }
            Option.Highlight();
        }
        else if (rgb != empty)
        {
            switch (rgb)
            {
                case 'r':
                    if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0)
                    {
                        rgb = 'g';
                    }
                    break;
                case 'g':
                    if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0)
                    {
                        rgb = 'r';
                    }
                    if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0)
                    {
                        rgb = 'b';
                    }
                    break;
                case 'b':
                    if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0)
                    {
                        rgb = 'g';
                    }
                    break;
            }
            Style.ToggleRGB();
        }
        if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") != 0 && prompt == null)
        {
            //if(prompt == null)
            {

                //put all the statements below within this?
            }
            //else
            {
                //
            }
            if (style != 0)
            {
                switch (optionSelector)
                {
                    case 0:
                        textBorderStyle = textBorderStyle + (int)Input.GetAxisRaw("Horizontal");
                        textBorderStyle = Mathf.Clamp(textBorderStyle, 1, 5);
                        break;
                    case 6:
                        fontStyle = fontStyle + (int)Input.GetAxisRaw("Horizontal");
                        fontStyle = Mathf.Clamp(fontStyle, 1, 5);
                        break;
                    case 12:
                        difficulty = difficulty + (int)Input.GetAxisRaw("Horizontal");
                        difficulty = Mathf.Clamp(difficulty, 1, 5);
                        break;
                }
                style = style + (int)Input.GetAxisRaw("Horizontal");
                style = Mathf.Clamp(style, 1, 5);
                Style.ToggleRGB(); //MAYBE DONT NEED. TEST. 
                Style.ToggleStyle();
            }
            else if (rgb == empty && style == 0)
            {
                Option.Discard();
                if (optionSelector < 13 && optionSelector > 5 && Input.GetAxisRaw("Horizontal") < 0)
                {
                    optionSelector = optionSelector + (int)Input.GetAxisRaw("Horizontal") * 6;
                    optionSelector = Mathf.Clamp(optionSelector, 0, 5);
                }
                else if (optionSelector < 6 && Input.GetAxisRaw("Horizontal") > 0)
                {
                    optionSelector = optionSelector + (int)Input.GetAxisRaw("Horizontal") * 6;
                    optionSelector = Mathf.Clamp(optionSelector, 6, 12);
                }
                else if (optionSelector < 16 && optionSelector > 12 && Input.GetAxisRaw("Horizontal") < 0)
                {
                    optionSelector = optionSelector + (int)Input.GetAxisRaw("Horizontal");
                    optionSelector = Mathf.Clamp(optionSelector, 13, 15);
                }
                else if (optionSelector > 12 && Input.GetAxisRaw("Horizontal") > 0)
                {
                    optionSelector = optionSelector + (int)Input.GetAxisRaw("Horizontal");
                    optionSelector = Mathf.Clamp(optionSelector, 13, 15);
                }
                Option.Highlight();
            }
        }

        if (Input.GetButtonDown("Confirm") && Input.GetMouseButtonDown(0) != true)
        {
            if (prompt == null)
            {
                Option.Select();
            }
            else if (prompt != null)
            {
                if (prompt == defaultSettingsPrompt)
                {
                    SetAllDefaultSettings();
                }
                if (prompt == saveSettingsPrompt)
                {
                    SaveChangesToSettings();
                }
                if (prompt == cancelChangesPrompt)
                {
                    CancelChangesToSettings();
                }
            }
        }
        if (Input.GetButtonDown("Cancel"))
        {
            if (prompt != null)
            {
                Destroy(prompt.gameObject);
            }
            else if (style == 0 && rgb == empty && prompt == null)
            {
                CancelChangesToSettings(); // ?
                StartCoroutine(FadeOut());
            }
        }
        if (Input.GetMouseButton(0) == true)
        {
            OptionCursor.Instance.enabled = false;
        }
        else
        {
            OptionCursor.Instance.enabled = true;
        }
    }
    #endregion

    #region Methods
    public void OptionToggle(int index)
    {
        if (style == 0 && rgb == empty && prompt == null) //?
        {
            Option.Discard();
            optionSelector = index;
            Option.Highlight();
        }
        else
        {
            Option.Select();
        }
    }
    public void SelectOption(int index)
    {
        if (prompt == null) //?
        {
            optionSelector = index;
            Option.Select();
        }
    }

    public void StyleToggle(int styleIndex)
    {
        switch (optionSelector)
        {
            case 0:
                textBorderStyle = styleIndex;
                break;
            case 6:
                fontStyle = styleIndex;
                break;
            case 12:
                difficulty = styleIndex;
                break;
        }
        if (style == 0)
        {
            Option.Select();
        }
        else
        {
            style = styleIndex;
            Style.ToggleStyle();
        }
    }
    public void RGBToggle(char rgbIndex)
    {
        rgb = rgbIndex;
        if (rgb == empty)
        {
            Option.Select();
        }
        else
        {
            rgb = rgbIndex;
            Style.ToggleRGB();
        }
    }
    //MAKE ALL METHODS BELOW THIS POINT STATIC?
    private void Initialize()
    {
        StartCoroutine(FadeIn());
        Sprite[] borders = Resources.LoadAll<Sprite>("Art/Interface/TextBorders");
        Sprite[] boxes = Resources.LoadAll<Sprite>("Art/Interface/TextBoxes");
        Sprite[] boxGradients = Resources.LoadAll<Sprite>("Art/Interface/Gradients");
        Sprite selectedBorder;
        Sprite selectedBox;
        Sprite selectedGradient;

        switch (GameManager.P1.TextBorder.name)
        {
            case "TextBorders_0":
                selectedBorder = borders[0];
                selectedBox = boxes[0];
                selectedGradient = boxGradients[0];
                foreach (Image border in textBorders)
                {
                    border.sprite = borders[0];
                }
                foreach (Image box in textBoxes)
                {
                    box.sprite = boxes[0];
                    box.rectTransform.anchoredPosition = new Vector2(1f, box.rectTransform.anchoredPosition.y);
                }
                foreach (Image gradient in gradients)
                {
                    gradient.sprite = boxGradients[0];
                    gradient.rectTransform.anchoredPosition = new Vector2(-1f, -1f);
                }
                textBoxes[2].rectTransform.anchoredPosition = new Vector2(1f, textBoxes[2].rectTransform.anchoredPosition.y);
                textBoxes[3].rectTransform.anchoredPosition = new Vector2(1f, textBoxes[3].rectTransform.anchoredPosition.y);
                textBoxes[4].rectTransform.anchoredPosition = new Vector2(1f, textBoxes[4].rectTransform.anchoredPosition.y);
                break;
            case "TextBorders_1":
                selectedBorder = borders[1];
                selectedBox = boxes[1];
                selectedGradient = boxGradients[1];
                foreach (Image border in textBorders)
                {
                    border.sprite = borders[1];
                }
                foreach (Image box in textBoxes)
                {
                    box.sprite = boxes[1];
                    box.rectTransform.anchoredPosition = new Vector2(0f, box.rectTransform.anchoredPosition.y);
                }
                foreach (Image gradient in gradients)
                {
                    gradient.sprite = boxGradients[1];
                    gradient.rectTransform.anchoredPosition = new Vector2(0f, 0f);
                }
                textBoxes[2].rectTransform.anchoredPosition = new Vector2(0f, textBoxes[2].rectTransform.anchoredPosition.y);
                textBoxes[3].rectTransform.anchoredPosition = new Vector2(0f, textBoxes[3].rectTransform.anchoredPosition.y);
                textBoxes[4].rectTransform.anchoredPosition = new Vector2(0f, textBoxes[4].rectTransform.anchoredPosition.y);
                break;
            case "TextBorders_2":
                selectedBorder = borders[2];
                selectedBox = boxes[2];
                selectedGradient = boxGradients[2];
                foreach (Image border in textBorders)
                {
                    border.sprite = borders[2];
                }
                foreach (Image box in textBoxes)
                {
                    box.sprite = boxes[2];
                    box.rectTransform.anchoredPosition = new Vector2(1f, box.rectTransform.anchoredPosition.y);
                }
                foreach (Image gradient in gradients)
                {
                    gradient.sprite = boxGradients[2];
                    gradient.rectTransform.anchoredPosition = new Vector2(-1f, 0f);
                }
                textBoxes[2].rectTransform.anchoredPosition = new Vector2(1f, textBoxes[2].rectTransform.anchoredPosition.y);
                textBoxes[3].rectTransform.anchoredPosition = new Vector2(1f, textBoxes[3].rectTransform.anchoredPosition.y);
                textBoxes[4].rectTransform.anchoredPosition = new Vector2(1f, textBoxes[4].rectTransform.anchoredPosition.y);
                break;
            case "TextBorders_3":
                selectedBorder = borders[3];
                selectedBox = boxes[3];
                selectedGradient = boxGradients[3];
                foreach (Image border in textBorders)
                {
                    border.sprite = borders[3];
                }
                foreach (Image box in textBoxes)
                {
                    box.sprite = boxes[3];
                    box.rectTransform.anchoredPosition = new Vector2(1f, box.rectTransform.anchoredPosition.y);
                }
                foreach (Image gradient in gradients)
                {
                    gradient.sprite = boxGradients[3];
                    gradient.rectTransform.anchoredPosition = new Vector2(-1f, 0f);
                }
                textBoxes[2].rectTransform.anchoredPosition = new Vector2(1f, textBoxes[2].rectTransform.anchoredPosition.y);
                textBoxes[3].rectTransform.anchoredPosition = new Vector2(1f, textBoxes[3].rectTransform.anchoredPosition.y);
                textBoxes[4].rectTransform.anchoredPosition = new Vector2(1f, textBoxes[4].rectTransform.anchoredPosition.y);
                break;
            case "TextBorders_4":
                selectedBorder = borders[4];
                selectedBox = boxes[2];
                selectedGradient = boxGradients[2];
                foreach (Image border in textBorders)
                {
                    border.sprite = borders[4];
                }
                foreach (Image box in textBoxes)
                {
                    box.sprite = boxes[1];
                    box.rectTransform.anchoredPosition = new Vector2(0f, box.rectTransform.anchoredPosition.y);
                }
                foreach (Image gradient in gradients)
                {
                    gradient.sprite = boxGradients[1];
                    gradient.rectTransform.anchoredPosition = new Vector2(0f, 0f);
                }
                textBoxes[2].rectTransform.anchoredPosition = new Vector2(0f, textBoxes[2].rectTransform.anchoredPosition.y);
                textBoxes[3].rectTransform.anchoredPosition = new Vector2(0f, textBoxes[3].rectTransform.anchoredPosition.y);
                textBoxes[4].rectTransform.anchoredPosition = new Vector2(0f, textBoxes[4].rectTransform.anchoredPosition.y);
                break;
        }


        for (int i = 0; i < 16; i++)
        {
            originalTextPos[i, 0] = text[i].rectTransform.anchoredPosition;
            originalTextPos[i, 1] = new Vector3(text[i].rectTransform.anchoredPosition.x - 2f, text[i].rectTransform.anchoredPosition.y - 12f, 0f);
            originalTextPos[i, 2] = new Vector3(text[i].rectTransform.anchoredPosition.x - 2f, text[i].rectTransform.anchoredPosition.y - 10f, 0f);
            originalTextPos[i, 3] = new Vector3(text[i].rectTransform.anchoredPosition.x - 2f, text[i].rectTransform.anchoredPosition.y - 18f, 0f);
            originalTextPos[i, 4] = new Vector3(text[i].rectTransform.anchoredPosition.x - 2f, text[i].rectTransform.anchoredPosition.y - 10f, 0f);
            switch (GameManager.P1.TextFont.name)
            {
                case "Masaaki-Regular":
                    cursorOffset = 5;
                    text[i].fontSize = 20;
                    text[i].rectTransform.anchoredPosition = originalTextPos[i, 0];
                    break;
                case "PressStart2P":
                    cursorOffset = -5;
                    text[i].fontSize = 11;
                    text[i].rectTransform.anchoredPosition = originalTextPos[i, 1];
                    break;
                case "BMarmy":
                    cursorOffset = -5;
                    text[i].fontSize = 14;
                    text[i].rectTransform.anchoredPosition = originalTextPos[i, 2];
                    break;
                case "celtic-bit":
                    cursorOffset = -10;
                    text[i].fontSize = 10;
                    text[i].rectTransform.anchoredPosition = originalTextPos[i, 3];
                    break;
                case "alagard":
                    cursorOffset = -5;
                    text[i].fontSize = 16;
                    text[i].rectTransform.anchoredPosition = originalTextPos[i, 4];
                    break;
            }
        }
        rgb = empty;
        emptyGameObject = new GameObject();
        CancelChangesToSettings(); //Sets the Option Scene settings to Game Manager settings...
        optionSelector = 0;
        Option.Highlight();

        textBorderColorSliderR.value = GameManager.P1.TextBorderColor.r;
        textBorderColorSliderG.value = GameManager.P1.TextBorderColor.g;
        textBorderColorSliderB.value = GameManager.P1.TextBorderColor.b;
        textBoxColorSliderR.value = GameManager.P1.TextBoxColor.r;
        textBoxColorSliderG.value = GameManager.P1.TextBoxColor.g;
        textBoxColorSliderB.value = GameManager.P1.TextBoxColor.b;
        textBoxTransparencySlider.value = GameManager.P1.TextBoxTransparency;
        gradientTransparencySlider.value = GameManager.P1.GradientTransparency;
        gradientSliderR.value = GameManager.P1.GradientColor.r;
        gradientSliderG.value = GameManager.P1.GradientColor.g;
        gradientSliderB.value = GameManager.P1.GradientColor.b;
        textColorSliderR.value = GameManager.P1.TextColor.r;
        textColorSliderG.value = GameManager.P1.TextColor.g;
        textColorSliderB.value = GameManager.P1.TextColor.b;
        textShadowSliderR.value = GameManager.P1.TextShadowColor.r;
        textShadowSliderG.value = GameManager.P1.TextShadowColor.g;
        textShadowSliderB.value = GameManager.P1.TextShadowColor.b;
        textShadowTransparencySlider.value = GameManager.P1.TextShadowTransparency;
        musicVolumeSlider.value = GameManager.P1.MusicVolume;
        sfxVolumeSlider.value = GameManager.P1.SfxVolume;
    }

    public void SaveChangesToSettings()
    {
        GameManager.P1.TextBox = textBoxes[0].sprite;
        GameManager.P1.TextBorder = textBorders[0].sprite;
        GameManager.P1.TextBoxGradient = gradients[0].sprite;

        GameManager.P1.TextBorderColor = TextBorderColor;
        GameManager.P1.TextBoxColor = TextBoxColor;
        GameManager.P1.TextBoxTransparency = textBoxTransparencySlider.value;
        GameManager.P1.GradientTransparency = gradientTransparencySlider.value;
        GameManager.P1.GradientColor = new Color(gradientSliderR.value, gradientSliderG.value, gradientSliderB.value);
        GameManager.P1.TextFont = TextFont;
        GameManager.P1.TextColor = TextColor;
        GameManager.P1.TextShadowColor = TextShadowColor;
        GameManager.P1.TextShadowTransparency = textShadowTransparencySlider.value;
        GameManager.P1.MusicVolume = musicVolumeSlider.value;
        GameManager.P1.SfxVolume = sfxVolumeSlider.value;
    }

    public void CancelChangesToSettings()
    {

        foreach (Image border in textBorders)
        {
            border.sprite = GameManager.P1.TextBorder;
            border.color = GameManager.P1.TextBorderColor;
        }
        foreach (Image box in textBoxes)
        {
            box.sprite = GameManager.P1.TextBox;
            box.color = new Color(GameManager.P1.TextBoxColor.r, GameManager.P1.TextBoxColor.g, GameManager.P1.TextBoxColor.b, GameManager.P1.TextBoxTransparency);
        }

        if (GameManager.P1.GradientEnabled != false)
        {
            foreach (Image gradient in gradients)
            {
                gradient.color = new Color(GameManager.P1.GradientColor.r, GameManager.P1.GradientColor.g, GameManager.P1.GradientColor.b, GameManager.P1.GradientTransparency);
            }
        }
        else
        {
            foreach (Image gradient in gradients)
            {
                gradient.color = new Color(0f, 0f, 0f, 0f);
                gradient.enabled = false;
            }
        }

        foreach (Text texts in text)
        {
            texts.font = GameManager.P1.TextFont;
            texts.color = new Color(GameManager.P1.TextColor.r / 2, GameManager.P1.TextColor.g / 2, GameManager.P1.TextColor.b / 2);
            texts.GetComponent<Shadow>().effectColor = new Color(GameManager.P1.TextShadowColor.r, GameManager.P1.TextShadowColor.g, GameManager.P1.TextShadowColor.b, GameManager.P1.TextShadowTransparency);
        }
        foreach (Text texts in rText)
        {
            texts.color = new Color(GameManager.P1.TextColor.r / 2, GameManager.P1.TextColor.g / 2, GameManager.P1.TextColor.b / 2);
            texts.GetComponent<Shadow>().effectColor = new Color(GameManager.P1.TextShadowColor.r, GameManager.P1.TextShadowColor.g, GameManager.P1.TextShadowColor.b, GameManager.P1.TextShadowTransparency);
        }
        foreach (Text texts in gText)
        {
            texts.color = new Color(GameManager.P1.TextColor.r / 2, GameManager.P1.TextColor.g / 2, GameManager.P1.TextColor.b / 2);
            texts.GetComponent<Shadow>().effectColor = new Color(GameManager.P1.TextShadowColor.r, GameManager.P1.TextShadowColor.g, GameManager.P1.TextShadowColor.b, GameManager.P1.TextShadowTransparency);
        }
        foreach (Text texts in bText)
        {
            texts.color = new Color(GameManager.P1.TextColor.r / 2, GameManager.P1.TextColor.g / 2, GameManager.P1.TextColor.b / 2);
            texts.GetComponent<Shadow>().effectColor = new Color(GameManager.P1.TextShadowColor.r, GameManager.P1.TextShadowColor.g, GameManager.P1.TextShadowColor.b, GameManager.P1.TextShadowTransparency);
        }
        foreach (Text texts in borderNumText)
        {
            //textBorderStyle = textBoxes[0].sprite.
            texts.color = new Color(GameManager.P1.TextColor.r / 2, GameManager.P1.TextColor.g / 2, GameManager.P1.TextColor.b / 2);
            texts.GetComponent<Shadow>().effectColor = new Color(GameManager.P1.TextShadowColor.r, GameManager.P1.TextShadowColor.g, GameManager.P1.TextShadowColor.b, GameManager.P1.TextShadowTransparency);
            //if (texts != borderNumText[textBorderStyle - 1])
            {
                //Debug.Log(textBorderStyle - 1);
                //texts.color = new Color(GameManager.P1.TextColor.r / 2, GameManager.P1.TextColor.g / 2, GameManager.P1.TextColor.b / 2);
                //texts.GetComponent<Shadow>().effectColor = new Color(GameManager.P1.TextShadowColor.r, GameManager.P1.TextShadowColor.g, GameManager.P1.TextShadowColor.b, GameManager.P1.TextShadowTransparency);
            }
        }
        foreach (Text texts in fontNumText)
        {
            texts.color = new Color(GameManager.P1.TextColor.r / 2, GameManager.P1.TextColor.g / 2, GameManager.P1.TextColor.b / 2);
            texts.GetComponent<Shadow>().effectColor = new Color(GameManager.P1.TextShadowColor.r, GameManager.P1.TextShadowColor.g, GameManager.P1.TextShadowColor.b, GameManager.P1.TextShadowTransparency);
        }
        foreach (Text texts in difficultyNumText)
        {
            texts.color = new Color(GameManager.P1.TextColor.r / 2, GameManager.P1.TextColor.g / 2, GameManager.P1.TextColor.b / 2);
            texts.GetComponent<Shadow>().effectColor = new Color(GameManager.P1.TextShadowColor.r, GameManager.P1.TextShadowColor.g, GameManager.P1.TextShadowColor.b, GameManager.P1.TextShadowTransparency);
        }

        TextBorderColor = GameManager.P1.TextBorderColor;
        TextBoxColor = GameManager.P1.TextBoxColor;
        GradientColor = GameManager.P1.GradientColor;
        TextColor = GameManager.P1.TextColor;
        TextShadowColor = GameManager.P1.TextShadowColor;
        TextFont = GameManager.P1.TextFont;
        GridOn = GameManager.P1.GridOn;
        GridColor = GameManager.P1.GridColor;
    }

    public void SetAllDefaultSettings()
    {
        GameManager.RestoreAllSettingsToDefault();
        CancelChangesToSettings();
    }

    public void ChangeTextBorderStyle()
    {
        if (optionSelector == 0)
        {
            Sprite[] borders = Resources.LoadAll<Sprite>("Art/Interface/TextBorders");
            Sprite[] boxes = Resources.LoadAll<Sprite>("Art/Interface/TextBoxes");
            Sprite[] boxGradients = Resources.LoadAll<Sprite>("Art/Interface/Gradients");
            EventSystem.current.SetSelectedGameObject(emptyGameObject);

            switch (textBorderStyle)
            {
                case 1:
                    foreach (Image border in textBorders)
                    {
                        border.sprite = borders[0];
                    }
                    foreach (Image box in textBoxes)
                    {
                        box.sprite = boxes[0];
                        box.rectTransform.anchoredPosition = new Vector2(1f, box.rectTransform.anchoredPosition.y);
                    }
                    foreach (Image gradient in OptionsManager.Instance.gradients)
                    {
                        gradient.sprite = boxGradients[0];
                        gradient.rectTransform.anchoredPosition = new Vector2(-1f, -1f);
                    }
                    textBoxes[2].rectTransform.anchoredPosition = new Vector2(1f, textBoxes[2].rectTransform.anchoredPosition.y);
                    textBoxes[3].rectTransform.anchoredPosition = new Vector2(1f, textBoxes[3].rectTransform.anchoredPosition.y);
                    textBoxes[4].rectTransform.anchoredPosition = new Vector2(1f, textBoxes[4].rectTransform.anchoredPosition.y);
                    break;

                case 2:
                    foreach (Image border in textBorders)
                    {
                        border.sprite = borders[1];
                    }
                    foreach (Image box in textBoxes)
                    {
                        box.sprite = boxes[1];
                        box.rectTransform.anchoredPosition = new Vector2(0f, box.rectTransform.anchoredPosition.y);
                    }
                    foreach (Image gradient in gradients)
                    {
                        gradient.sprite = boxGradients[1];
                        gradient.rectTransform.anchoredPosition = new Vector2(0f, 0f);
                    }
                    textBoxes[2].rectTransform.anchoredPosition = new Vector2(0f, textBoxes[2].rectTransform.anchoredPosition.y);
                    textBoxes[3].rectTransform.anchoredPosition = new Vector2(0f, textBoxes[3].rectTransform.anchoredPosition.y);
                    textBoxes[4].rectTransform.anchoredPosition = new Vector2(0f, textBoxes[4].rectTransform.anchoredPosition.y);
                    break;

                case 3:
                    foreach (Image border in textBorders)
                    {
                        border.sprite = borders[2];
                    }
                    foreach (Image box in textBoxes)
                    {
                        box.sprite = boxes[2];
                        box.rectTransform.anchoredPosition = new Vector2(1f, box.rectTransform.anchoredPosition.y);
                    }
                    foreach (Image gradient in gradients)
                    {
                        gradient.sprite = boxGradients[2];
                        gradient.rectTransform.anchoredPosition = new Vector2(-1f, 0f);
                    }
                    textBoxes[2].rectTransform.anchoredPosition = new Vector2(1f, textBoxes[2].rectTransform.anchoredPosition.y);
                    textBoxes[3].rectTransform.anchoredPosition = new Vector2(1f, textBoxes[3].rectTransform.anchoredPosition.y);
                    textBoxes[4].rectTransform.anchoredPosition = new Vector2(1f, textBoxes[4].rectTransform.anchoredPosition.y);
                    break;

                case 4:
                    foreach (Image border in textBorders)
                    {
                        border.sprite = borders[3];
                    }
                    foreach (Image box in textBoxes)
                    {

                        box.sprite = boxes[3];
                        box.rectTransform.anchoredPosition = new Vector2(1f, box.rectTransform.anchoredPosition.y);
                    }
                    foreach (Image gradient in gradients)
                    {
                        gradient.sprite = boxGradients[3];
                        gradient.rectTransform.anchoredPosition = new Vector2(-1f, 0f);
                    }
                    textBoxes[2].rectTransform.anchoredPosition = new Vector2(1f, textBoxes[2].rectTransform.anchoredPosition.y);
                    textBoxes[3].rectTransform.anchoredPosition = new Vector2(1f, textBoxes[3].rectTransform.anchoredPosition.y);
                    textBoxes[4].rectTransform.anchoredPosition = new Vector2(1f, textBoxes[4].rectTransform.anchoredPosition.y);
                    break;
                case 5:
                    foreach (Image border in textBorders)
                    {
                        border.sprite = borders[4];
                    }
                    foreach (Image box in textBoxes)
                    {
                        box.sprite = boxes[1];
                        box.rectTransform.anchoredPosition = new Vector2(0f, box.rectTransform.anchoredPosition.y);
                    }
                    foreach (Image gradient in gradients)
                    {
                        gradient.sprite = boxGradients[1];
                        gradient.rectTransform.anchoredPosition = new Vector2(0f, 0f);
                    }
                    textBoxes[2].rectTransform.anchoredPosition = new Vector2(0f, textBoxes[2].rectTransform.anchoredPosition.y);
                    textBoxes[3].rectTransform.anchoredPosition = new Vector2(0f, textBoxes[3].rectTransform.anchoredPosition.y);
                    textBoxes[4].rectTransform.anchoredPosition = new Vector2(0f, textBoxes[4].rectTransform.anchoredPosition.y);
                    break;
            }

            foreach (Text text in borderNumText)
            {
                if (text != borderNumText[textBorderStyle - 1])
                {
                    text.color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                }
                else
                {
                    text.color = TextColor;
                }
            }
            textCursor.rectTransform.position = new Vector2(borderNumText[textBorderStyle - 1].rectTransform.position.x - 150f, borderNumText[textBorderStyle - 1].rectTransform.position.y - 5f);
        }
    }

    public void ChangeTextBorderColor()
    {
        if (optionSelector == 1)
        {
            TextBorderColor = new Color(textBorderColorSliderR.value, textBorderColorSliderG.value, textBorderColorSliderB.value);

            foreach (Image border in textBorders)
            {
                border.color = TextBorderColor;
            }
        }
    }

    public void ChangeTextBoxColor()
    {
        if (optionSelector == 2)
        {
            TextBoxColor = new Color(textBoxColorSliderR.value, textBoxColorSliderG.value, textBoxColorSliderB.value, textBoxes[0].color.a);
            foreach (Image box in textBoxes)
            {
                box.color = TextBoxColor;
            }
        }
    }

    public void ChangeTextBoxTransparancy()
    {
        if (optionSelector == 3)
        {
            TextBoxColor = new Color(textBoxes[0].color.r, textBoxes[0].color.g, textBoxes[0].color.b, textBoxTransparencySlider.value);
            foreach (Image box in textBoxes)
            {
                box.color = TextBoxColor;
            }
        }
    }

    public void ChangeGradientTransparency()
    {
        GradientColor = new Color(gradients[0].color.r, gradients[0].color.g, gradients[0].color.b, gradientTransparencySlider.value);
        if (GradientColor.a != 0)
        {
            GameManager.P1.GradientEnabled = true;
            foreach (Image gradient in gradients)
            {
                gradient.enabled = true;
            }
        }
        else
        {
            GameManager.P1.GradientEnabled = false;
            foreach (Image gradient in gradients)
            {
                gradient.enabled = false;
            }
        }

        foreach (Image gradient in gradients)
        {
            gradient.color = GradientColor;
        }
    }

    public void ChangeGradientColor()
    {
        GradientColor = new Color(gradientSliderR.value, gradientSliderG.value, gradientSliderB.value, gradients[0].color.a);

        foreach (Image gradient in gradients)
        {
            gradient.color = GradientColor;
        }
    }

    public void ChangeFontStyle()
    {
        int fontSize = 20;
        switch (fontStyle)
        {
            case 1:
                fontSize = 20;
                cursorOffset = 5;
                TextFont = Resources.Load("Fonts/Masaaki-Regular", typeof(Font)) as Font;
                for (int i = 0; i < 16; i++)
                {
                    text[i].rectTransform.anchoredPosition = originalTextPos[i, 0];
                }
                break;
            case 2:
                fontSize = 11;
                cursorOffset = -5;
                TextFont = Resources.Load("Fonts/PressStart2P", typeof(Font)) as Font;
                for (int i = 0; i < 16; i++)
                {
                    text[i].rectTransform.anchoredPosition = originalTextPos[i, 1];
                }
                break;
            case 3:
                fontSize = 14;
                cursorOffset = -5;
                TextFont = Resources.Load("Fonts/BMarmy", typeof(Font)) as Font;
                for (int i = 0; i < 16; i++)
                {
                    text[i].rectTransform.anchoredPosition = originalTextPos[i, 2];
                }
                break;
            case 4:
                fontSize = 10;
                cursorOffset = -10;
                TextFont = Resources.Load("Fonts/celtic-bit", typeof(Font)) as Font;
                for (int i = 0; i < 16; i++)
                {
                    text[i].rectTransform.anchoredPosition = originalTextPos[i, 3];
                }
                break;
            case 5:
                fontSize = 16;
                cursorOffset = -5;
                TextFont = Resources.Load("Fonts/alagard", typeof(Font)) as Font;
                for (int i = 0; i < 16; i++)
                {
                    text[i].rectTransform.anchoredPosition = originalTextPos[i, 4];
                }
                break;
        }
        foreach (Text texts in text)
        {
            texts.font = TextFont;
            texts.fontSize = fontSize;
        }
    }

    public void ChangeTextColor()
    {
        foreach (Text texts in text)
        {
            if (texts == text[optionSelector])
            {
                texts.color = new Color(textColorSliderR.value, textColorSliderG.value, textColorSliderB.value);
            }
            else
            {
                texts.color = new Color(textColorSliderR.value / 2, textColorSliderG.value / 2, textColorSliderB.value / 2);
            }
        }
        foreach (Text texts in rText)
        {
            if (texts == rText[3] && rgb == 'r')
            {
                texts.color = new Color(textColorSliderR.value, textColorSliderG.value, textColorSliderB.value);
            }
            else
            {
                texts.color = new Color(textColorSliderR.value / 2, textColorSliderG.value / 2, textColorSliderB.value / 2);
            }
        }
        foreach (Text texts in gText)
        {
            if (texts == gText[3] && rgb == 'g')
            {
                texts.color = new Color(textColorSliderR.value, textColorSliderG.value, textColorSliderB.value);
            }
            else
            {
                texts.color = new Color(textColorSliderR.value / 2, textColorSliderG.value / 2, textColorSliderB.value / 2);
            }
        }
        foreach (Text texts in bText)
        {
            if (texts == bText[3] && rgb == 'b')
            {
                texts.color = new Color(textColorSliderR.value, textColorSliderG.value, textColorSliderB.value);
            }
            else
            {
                texts.color = new Color(textColorSliderR.value / 2, textColorSliderG.value / 2, textColorSliderB.value / 2);
            }
        }
        foreach (Text texts in borderNumText)
        {
            //if (texts != borderNumText[textBorderStyle - 1])
            texts.color = new Color(textColorSliderR.value / 2, textColorSliderG.value / 2, textColorSliderB.value / 2);
        }
        foreach (Text texts in fontNumText)
        {
            texts.color = new Color(textColorSliderR.value / 2, textColorSliderG.value / 2, textColorSliderB.value / 2);
        }
        foreach (Text texts in difficultyNumText)
        {
            texts.color = new Color(textColorSliderR.value / 2, textColorSliderG.value / 2, textColorSliderB.value / 2);
        }
        TextColor = new Color(textColorSliderR.value, textColorSliderG.value, textColorSliderB.value);
    }

    public void ChangeTextShadowColor()
    {
        Shadow textshadow = text[0].GetComponent<Shadow>();
        foreach (Text texts in text)
        {
            textshadow = texts.GetComponent<Shadow>();
            if (texts == text[optionSelector])
            {
                textshadow.effectColor = new Color(textShadowSliderR.value, textShadowSliderG.value, textShadowSliderB.value, textShadowTransparencySlider.value);
            }
            else
            {
                textshadow.effectColor = new Color(textShadowSliderR.value, textShadowSliderG.value, textShadowSliderB.value, textShadowTransparencySlider.value);
            }
        }
        foreach (Text texts in rText)
        {
            textshadow = texts.GetComponent<Shadow>();
            if (texts == rText[4] && rgb == 'r')
            {
                texts.color = new Color(textColorSliderR.value, textColorSliderG.value, textColorSliderB.value);
                textshadow.effectColor = new Color(textShadowSliderR.value, textShadowSliderG.value, textShadowSliderB.value, textShadowTransparencySlider.value);
            }
            else
            {
                texts.color = new Color(textColorSliderR.value / 2, textColorSliderG.value / 2, textColorSliderB.value / 2);
                textshadow.effectColor = new Color(textShadowSliderR.value, textShadowSliderG.value, textShadowSliderB.value, textShadowTransparencySlider.value);
            }
        }
        foreach (Text texts in gText)
        {
            textshadow = texts.GetComponent<Shadow>();
            if (texts == gText[4] && rgb == 'g')
            {
                texts.color = new Color(textColorSliderR.value, textColorSliderG.value, textColorSliderB.value);
                textshadow.effectColor = new Color(textShadowSliderR.value, textShadowSliderG.value, textShadowSliderB.value, textShadowTransparencySlider.value);
            }
            else
            {
                texts.color = new Color(textColorSliderR.value / 2, textColorSliderG.value / 2, textColorSliderB.value / 2);
                textshadow.effectColor = new Color(textShadowSliderR.value, textShadowSliderG.value, textShadowSliderB.value, textShadowTransparencySlider.value);
            }
        }
        foreach (Text texts in bText)
        {
            textshadow = texts.GetComponent<Shadow>();
            if (texts == bText[4] && rgb == 'b')
            {
                texts.color = new Color(textColorSliderR.value, textColorSliderG.value, textColorSliderB.value);
                textshadow.effectColor = new Color(textShadowSliderR.value, textShadowSliderG.value, textShadowSliderB.value, textShadowTransparencySlider.value);
            }
            else
            {
                texts.color = new Color(textColorSliderR.value / 2, textColorSliderG.value / 2, textColorSliderB.value / 2);
                textshadow.effectColor = new Color(textShadowSliderR.value, textShadowSliderG.value, textShadowSliderB.value, textShadowTransparencySlider.value);
            }
        }
        foreach (Text texts in borderNumText)
        {
            textshadow = texts.GetComponent<Shadow>();
            textshadow.effectColor = new Color(textShadowSliderR.value, textShadowSliderG.value, textShadowSliderB.value, textShadowTransparencySlider.value);
        }
        foreach (Text texts in fontNumText)
        {
            textshadow = texts.GetComponent<Shadow>();
            textshadow.effectColor = new Color(textShadowSliderR.value, textShadowSliderG.value, textShadowSliderB.value, textShadowTransparencySlider.value);
        }
        foreach (Text texts in difficultyNumText)
        {
            textshadow = texts.GetComponent<Shadow>();
            textshadow.effectColor = new Color(textShadowSliderR.value, textShadowSliderG.value, textShadowSliderB.value, textShadowTransparencySlider.value);
        }
        TextShadowColor = textshadow.effectColor;
    }

    public void ChangeTextShadowTransparency()
    {
        Shadow textShadow = text[0].GetComponent<Shadow>();
        foreach (Text texts in text)
        {
            textShadow = texts.GetComponent<Shadow>();
            textShadow.effectColor = new Color(TextShadowColor.r, TextShadowColor.g, TextShadowColor.b, textShadowTransparencySlider.value);
        }
        foreach (Text texts in rText)
        {
            textShadow = texts.GetComponent<Shadow>();
            textShadow.effectColor = new Color(TextShadowColor.r, TextShadowColor.g, TextShadowColor.b, textShadowTransparencySlider.value);
        }
        foreach (Text texts in gText)
        {
            textShadow = texts.GetComponent<Shadow>();
            textShadow.effectColor = new Color(TextShadowColor.r, TextShadowColor.g, TextShadowColor.b, textShadowTransparencySlider.value);
        }
        foreach (Text texts in bText)
        {
            textShadow = texts.GetComponent<Shadow>();
            textShadow.effectColor = new Color(TextShadowColor.r, TextShadowColor.g, TextShadowColor.b, textShadowTransparencySlider.value);
        }
        foreach (Text texts in borderNumText)
        {
            textShadow = texts.GetComponent<Shadow>();
            textShadow.effectColor = new Color(textShadowSliderR.value, textShadowSliderG.value, textShadowSliderB.value, textShadowTransparencySlider.value);
        }
        foreach (Text texts in fontNumText)
        {
            textShadow = texts.GetComponent<Shadow>();
            textShadow.effectColor = new Color(textShadowSliderR.value, textShadowSliderG.value, textShadowSliderB.value, textShadowTransparencySlider.value);
        }
        foreach (Text texts in difficultyNumText)
        {
            textShadow = texts.GetComponent<Shadow>();
            textShadow.effectColor = new Color(textShadowSliderR.value, textShadowSliderG.value, textShadowSliderB.value, textShadowTransparencySlider.value);
        }
    }
    #endregion

    #region Coroutines
    public IEnumerator FadeIn()
    {
        while (screen.color.a > 0f)
        {
            screen.color = new Color(0f, 0f, 0f, screen.color.a - 0.025f);
            yield return null;
        }
        screen.enabled = false;
        yield return null;
    }
    public IEnumerator FadeOut()
    {
        screen.enabled = true;

        while (screen.color.a < 1f)
        {
            screen.color = new Color(0f, 0f, 0f, screen.color.a + 0.025f);
            yield return null;
        }
        Application.LoadLevel("StartScreen");
        yield return null;
    }
    #endregion


    #region Strategy Design Pattern
    public interface IOption
    {
        //function declarations within interface...
        void Select();
        void Highlight();
        void Discard();
    }
    private class Option
    {
        private static Dictionary<int, IOption> options = new Dictionary<int, IOption>();

        static Option()
        {
            options.Add(0, new TextBorderOption()); //This interface method is called when optionSelector = 0...
            options.Add(1, new TextBorderColorOption());
            options.Add(2, new TextBoxColorOption());
            options.Add(3, new TextBoxTransparencyOption());
            options.Add(4, new GradientTransparencyOption());
            options.Add(5, new GradientColorOption());
            options.Add(6, new TextFontOption());
            options.Add(7, new TextColorOption());
            options.Add(8, new TextShadowColorOption());
            options.Add(9, new TextShadowTransparencyOption());
            options.Add(10, new MusicVolumeOption());
            options.Add(11, new SfxVolumeOption());
            options.Add(12, new DifficultyOption());
            options.Add(13, new DefaultSettingsOption());
            options.Add(14, new SaveSettingsOption());
            options.Add(15, new CancelSettingsOption());
        }

        public static void Select()
        {
            options[optionSelector].Select();
        }
        public static void Highlight()
        {
            options[optionSelector].Highlight();
        }
        public static void Discard()
        {
            options[optionSelector].Discard();
        }
    }
    private class TextBorderOption : IOption
    {
        public void Select()
        {
            if (style == 0)
            {
                if (textBorderStyle != 0)
                {
                    style = textBorderStyle;
                }
                else
                {
                    style = 1;
                    textBorderStyle = 1;
                }
                rgb = empty;
                Style.ToggleStyle();
            }
            else
            {
                style = 0;
                rgb = empty;
                OptionsManager.Instance.borderNumText[textBorderStyle - 1].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[0].rectTransform.position.x - 150, OptionsManager.Instance.text[0].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
            }
        }
        public void Highlight()
        {
            OptionsManager.Instance.text[0].color = TextColor;
            OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[0].rectTransform.position.x - 150, OptionsManager.Instance.text[0].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
        }
        public void Discard()
        {
            OptionsManager.Instance.text[0].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
        }
    }
    private class TextBorderColorOption : IOption
    {
        public void Select()
        {
            if (rgb == empty)
            {
                rgb = 'r';
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.rText[0].rectTransform.position.x - 150, OptionsManager.Instance.rText[0].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
                Style.ToggleRGB();
            }
            else
            {
                OptionsManager.Instance.textBorderColorSliderR.interactable = false;
                OptionsManager.Instance.textBorderColorSliderG.interactable = false;
                OptionsManager.Instance.textBorderColorSliderB.interactable = false;
                OptionsManager.Instance.rText[0].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                OptionsManager.Instance.gText[0].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                OptionsManager.Instance.bText[0].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                rgb = empty;
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[1].rectTransform.position.x - 150, OptionsManager.Instance.text[1].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
            }
        }
        public void Highlight()
        {
            OptionsManager.Instance.text[1].color = TextColor;
            OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[1].rectTransform.position.x - 150, OptionsManager.Instance.text[1].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
        }
        public void Discard()
        {
            OptionsManager.Instance.text[1].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
        }
    }
    private class TextBoxColorOption : IOption
    {
        public void Select()
        {
            if (rgb == empty)
            {
                rgb = 'r';
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.rText[1].rectTransform.position.x - 150, OptionsManager.Instance.rText[0].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
                Style.ToggleRGB();
            }
            else
            {
                OptionsManager.Instance.textBoxColorSliderR.interactable = false;
                OptionsManager.Instance.textBoxColorSliderG.interactable = false;
                OptionsManager.Instance.textBoxColorSliderB.interactable = false;
                OptionsManager.Instance.rText[1].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                OptionsManager.Instance.gText[1].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                OptionsManager.Instance.bText[1].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                rgb = empty;
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[2].rectTransform.position.x - 150, OptionsManager.Instance.text[2].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
            }
        }
        public void Highlight()
        {
            OptionsManager.Instance.text[2].color = TextColor;
            OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[2].rectTransform.position.x - 150, OptionsManager.Instance.text[2].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
        }
        public void Discard()
        {
            OptionsManager.Instance.text[2].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
        }
    }
    private class TextBoxTransparencyOption : IOption
    {
        public void Select()
        {
            if (style == 0)
            {
                rgb = empty;
                style = 1;
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[3].rectTransform.position.x + 140, OptionsManager.Instance.text[3].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
                EventSystem.current.SetSelectedGameObject(OptionsManager.Instance.textBoxTransparencySlider.gameObject);
                Style.ToggleStyle();
            }
            else
            {
                style = 0;
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[3].rectTransform.position.x - 150, OptionsManager.Instance.text[3].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
                OptionsManager.Instance.textBoxTransparencySlider.interactable = false;
                EventSystem.current.SetSelectedGameObject(emptyGameObject);
            }
        }
        public void Highlight()
        {
            OptionsManager.Instance.text[3].color = TextColor;
            OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[3].rectTransform.position.x - 150, OptionsManager.Instance.text[3].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
        }
        public void Discard()
        {
            OptionsManager.Instance.text[3].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
        }
    }
    private class GradientTransparencyOption : IOption
    {
        public void Select()
        {
            if (style == 0)
            {
                rgb = empty;
                style = 1;
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[4].rectTransform.position.x + 140, OptionsManager.Instance.text[4].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
                EventSystem.current.SetSelectedGameObject(OptionsManager.Instance.gradientTransparencySlider.gameObject);
                Style.ToggleStyle();
            }
            else
            {
                style = 0;
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[4].rectTransform.position.x - 150, OptionsManager.Instance.text[4].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
                EventSystem.current.SetSelectedGameObject(emptyGameObject);
                OptionsManager.Instance.gradientTransparencySlider.interactable = false;
            }
        }
        public void Highlight()
        {
            OptionsManager.Instance.text[4].color = TextColor;
            OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[4].rectTransform.position.x - 150, OptionsManager.Instance.text[4].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
        }
        public void Discard()
        {
            OptionsManager.Instance.text[4].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
        }
    }
    private class GradientColorOption : IOption
    {
        public void Select()
        {
            if (rgb == empty)
            {
                rgb = 'r';
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.rText[2].rectTransform.position.x - 150, OptionsManager.Instance.rText[2].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
                Style.ToggleRGB();
            }
            else
            {
                OptionsManager.Instance.gradientSliderR.interactable = false;
                OptionsManager.Instance.gradientSliderG.interactable = false;
                OptionsManager.Instance.gradientSliderB.interactable = false;
                OptionsManager.Instance.rText[2].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                OptionsManager.Instance.gText[2].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                OptionsManager.Instance.bText[2].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                rgb = empty;
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[5].rectTransform.position.x - 150, OptionsManager.Instance.text[5].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
            }
        }
        public void Highlight()
        {
            OptionsManager.Instance.text[5].color = TextColor;
            OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[5].rectTransform.position.x - 150, OptionsManager.Instance.text[5].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
        }
        public void Discard()
        {
            OptionsManager.Instance.text[5].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
        }
    }
    private class TextFontOption : IOption
    {
        public void Select()
        {
            if (style == 0)
            {
                if (fontStyle != 0)
                {
                    style = fontStyle; //?
                }
                else
                {
                    style = 1;
                    fontStyle = 1;
                }
                rgb = empty;
                Style.ToggleStyle();
            }
            else
            {
                style = 0;
                rgb = empty;
                OptionsManager.Instance.fontNumText[fontStyle - 1].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[6].rectTransform.position.x - 150, OptionsManager.Instance.text[6].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
            }
        }
        public void Highlight()
        {
            OptionsManager.Instance.text[6].color = TextColor;
            OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[6].rectTransform.position.x - 150, OptionsManager.Instance.text[6].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
        }
        public void Discard()
        {
            OptionsManager.Instance.text[6].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
        }
    }
    private class TextColorOption : IOption
    {
        public void Select()
        {
            if (rgb == empty)
            {
                rgb = 'r';
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.rText[3].rectTransform.position.x - 150, OptionsManager.Instance.rText[3].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
                Style.ToggleRGB();
            }
            else
            {
                OptionsManager.Instance.textColorSliderR.interactable = false;
                OptionsManager.Instance.textColorSliderG.interactable = false;
                OptionsManager.Instance.textColorSliderB.interactable = false;
                OptionsManager.Instance.rText[3].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                OptionsManager.Instance.gText[3].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                OptionsManager.Instance.bText[3].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                rgb = empty;
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[7].rectTransform.position.x - 150, OptionsManager.Instance.text[7].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
            }
        }
        public void Highlight()
        {
            OptionsManager.Instance.text[7].color = TextColor;
            OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[7].rectTransform.position.x - 150, OptionsManager.Instance.text[7].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
        }
        public void Discard()
        {
            OptionsManager.Instance.text[7].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
        }
    }
    private class TextShadowColorOption : IOption
    {
        public void Select()
        {
            if (rgb == empty)
            {
                rgb = 'r';
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.rText[4].rectTransform.position.x - 150, OptionsManager.Instance.rText[4].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
                Style.ToggleRGB();
            }
            else
            {
                OptionsManager.Instance.textShadowSliderR.interactable = false;
                OptionsManager.Instance.textShadowSliderG.interactable = false;
                OptionsManager.Instance.textShadowSliderB.interactable = false;
                OptionsManager.Instance.rText[4].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                OptionsManager.Instance.gText[4].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                OptionsManager.Instance.bText[4].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                rgb = empty;
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[8].rectTransform.position.x - 150, OptionsManager.Instance.text[8].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
            }
        }
        public void Highlight()
        {
            OptionsManager.Instance.text[8].color = TextColor;
            OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[8].rectTransform.position.x - 150, OptionsManager.Instance.text[8].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
        }
        public void Discard()
        {
            OptionsManager.Instance.text[8].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
        }
    }
    private class TextShadowTransparencyOption : IOption
    {
        public void Select()
        {
            if (style == 0)
            {
                rgb = empty;
                style = 1;
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[9].rectTransform.position.x + 140, OptionsManager.Instance.text[9].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
                EventSystem.current.SetSelectedGameObject(OptionsManager.Instance.textShadowTransparencySlider.gameObject);
                Style.ToggleStyle();
            }
            else
            {
                style = 0;
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[9].rectTransform.position.x - 150, OptionsManager.Instance.text[9].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
                EventSystem.current.SetSelectedGameObject(emptyGameObject);
                OptionsManager.Instance.textShadowTransparencySlider.interactable = false;
            }
        }
        public void Highlight()
        {
            OptionsManager.Instance.text[9].color = TextColor;
            OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[9].rectTransform.position.x - 150, OptionsManager.Instance.text[9].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
        }
        public void Discard()
        {
            OptionsManager.Instance.text[9].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
        }
    }
    private class MusicVolumeOption : IOption
    {
        public void Select()
        {
            if (style == 0)
            {
                rgb = empty;
                style = 1;
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[10].rectTransform.position.x + 140, OptionsManager.Instance.text[10].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
                EventSystem.current.SetSelectedGameObject(OptionsManager.Instance.musicVolumeSlider.gameObject);
                Style.ToggleStyle();
            }
            else
            {
                style = 0;
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[10].rectTransform.position.x - 150, OptionsManager.Instance.text[10].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
                EventSystem.current.SetSelectedGameObject(emptyGameObject);
                OptionsManager.Instance.musicVolumeSlider.interactable = false;
            }
        }
        public void Highlight()
        {
            OptionsManager.Instance.text[10].color = TextColor;
            OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[10].rectTransform.position.x - 150, OptionsManager.Instance.text[10].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
        }
        public void Discard()
        {
            OptionsManager.Instance.text[10].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
        }
    }
    private class SfxVolumeOption : IOption
    {
        public void Select()
        {
            if (style == 0)
            {
                rgb = empty;
                style = 1;
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[11].rectTransform.position.x + 140, OptionsManager.Instance.text[11].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
                EventSystem.current.SetSelectedGameObject(OptionsManager.Instance.sfxVolumeSlider.gameObject);
                Style.ToggleStyle();
            }
            else
            {
                style = 0;
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[11].rectTransform.position.x - 150, OptionsManager.Instance.text[11].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
                EventSystem.current.SetSelectedGameObject(emptyGameObject);
                OptionsManager.Instance.sfxVolumeSlider.interactable = false;
            }
        }
        public void Highlight()
        {
            OptionsManager.Instance.text[11].color = TextColor;
            OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[11].rectTransform.position.x - 150, OptionsManager.Instance.text[11].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
        }
        public void Discard()
        {
            OptionsManager.Instance.text[11].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
        }
    }
    private class DifficultyOption : IOption
    {
        public void Select()
        {
            if (style == 0)
            {
                if (difficulty != 0)
                {
                    style = difficulty; //?
                }
                else
                {
                    style = 1;
                    difficulty = 1;
                }
                rgb = empty;
                Style.ToggleStyle();
            }
            else
            {
                style = 0;
                rgb = empty;
                OptionsManager.Instance.difficultyNumText[difficulty - 1].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2, 1f);
                OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[12].rectTransform.position.x - 150, OptionsManager.Instance.text[12].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
            }
        }
        public void Highlight()
        {
            OptionsManager.Instance.text[12].color = TextColor;
            OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[12].rectTransform.position.x - 150, OptionsManager.Instance.text[12].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
        }
        public void Discard()
        {
            OptionsManager.Instance.text[12].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
        }
    }
    private class DefaultSettingsOption : IOption
    {
        public void Select()
        {
            if (prompt == null)
            {
                defaultSettingsPrompt = Instantiate(Resources.Load("Prefabs/Interface/DefaultSettingsPrompt", typeof(GameObject)) as GameObject);
                prompt = defaultSettingsPrompt;
            }
        }
        public void Highlight()
        {
            OptionsManager.Instance.text[13].color = TextColor;
            OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[13].rectTransform.position.x - 150, OptionsManager.Instance.text[13].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
        }
        public void Discard()
        {
            OptionsManager.Instance.text[13].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
        }
    }
    private class SaveSettingsOption : IOption
    {
        public void Select()
        {
            if (prompt == null)
            {
                saveSettingsPrompt = Instantiate(Resources.Load("Prefabs/Interface/SaveSettingsPrompt", typeof(GameObject)) as GameObject);
                prompt = saveSettingsPrompt;
            }
        }
        public void Highlight()
        {
            OptionsManager.Instance.text[14].color = TextColor;
            OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[14].rectTransform.position.x - 150, OptionsManager.Instance.text[14].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
        }
        public void Discard()
        {
            OptionsManager.Instance.text[14].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
        }
    }
    private class CancelSettingsOption : IOption
    {
        public void Select()
        {
            if (prompt == null)
            {
                cancelChangesPrompt = Instantiate(Resources.Load("Prefabs/Interface/CancelSettingsPrompt", typeof(GameObject)) as GameObject);
                prompt = cancelChangesPrompt;
            }
        }
        public void Highlight()
        {
            OptionsManager.Instance.text[15].color = TextColor;
            OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[15].rectTransform.position.x - 150, OptionsManager.Instance.text[15].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
        }
        public void Discard()
        {
            OptionsManager.Instance.text[15].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
        }
    }

    public interface IStyle
    {
        void ToggleStyle();
        void ToggleRGB();
    }
    private class Style
    {
        private static Dictionary<int, IStyle> styles = new Dictionary<int, IStyle>();

        static Style()
        {
            styles.Add(0, new TextBorderStyle());
            styles.Add(1, new TextBorderColorStyle());
            styles.Add(2, new TextBoxColorStyle());
            styles.Add(3, new TextBoxTransparencyStyle());
            styles.Add(4, new GradientTransparencyStyle());
            styles.Add(5, new GradientColorStyle());
            styles.Add(6, new TextFontStyle());
            styles.Add(7, new TextColorStyle());
            styles.Add(8, new TextShadowColorStyle());
            styles.Add(9, new TextShadowTransparencyStyle());
            styles.Add(10, new MusicVolumeStyle());
            styles.Add(11, new SfxVolumeStyle());
            styles.Add(12, new DifficultySetting());
        }
        public static void ToggleStyle()
        {
            styles[optionSelector].ToggleStyle();
        }
        public static void ToggleRGB()
        {
            styles[optionSelector].ToggleRGB();
        }
    }

    private class TextBorderStyle : IStyle
    {
        public void ToggleStyle()
        {
            OptionsManager.Instance.ChangeTextBorderStyle();
            OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.borderNumText[textBorderStyle - 1].rectTransform.position.x - 150f, OptionsManager.Instance.borderNumText[textBorderStyle - 1].rectTransform.position.y - 5f);
        }
        public void ToggleRGB()
        {
            rgb = empty;
        }
    }
    private class TextBorderColorStyle : IStyle
    {
        public void ToggleStyle()
        {
            style = 0;
        }
        public void ToggleRGB()
        {
            switch (rgb)
            {
                case 'r':
                    OptionsManager.Instance.textBorderColorSliderR.interactable = true;
                    OptionsManager.Instance.textBorderColorSliderG.interactable = false;
                    OptionsManager.Instance.textBorderColorSliderB.interactable = false;
                    EventSystem.current.SetSelectedGameObject(OptionsManager.Instance.textBorderColorSliderR.gameObject);
                    OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.rText[0].rectTransform.position.x - 150f, OptionsManager.Instance.rText[0].rectTransform.position.y - 5f);
                    OptionsManager.Instance.rText[0].color = TextColor;
                    OptionsManager.Instance.gText[0].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    OptionsManager.Instance.bText[0].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    break;
                case 'g':
                    OptionsManager.Instance.textBorderColorSliderR.interactable = false;
                    OptionsManager.Instance.textBorderColorSliderG.interactable = true;
                    OptionsManager.Instance.textBorderColorSliderB.interactable = false;
                    EventSystem.current.SetSelectedGameObject(OptionsManager.Instance.textBorderColorSliderG.gameObject);
                    OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.gText[0].rectTransform.position.x - 150f, OptionsManager.Instance.gText[0].rectTransform.position.y - 5f);
                    OptionsManager.Instance.rText[0].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    OptionsManager.Instance.gText[0].color = TextColor;
                    OptionsManager.Instance.bText[0].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    break;
                case 'b':
                    OptionsManager.Instance.textBorderColorSliderR.interactable = false;
                    OptionsManager.Instance.textBorderColorSliderG.interactable = false;
                    OptionsManager.Instance.textBorderColorSliderB.interactable = true;
                    EventSystem.current.SetSelectedGameObject(OptionsManager.Instance.textBorderColorSliderB.gameObject);
                    OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.bText[0].rectTransform.position.x - 150f, OptionsManager.Instance.bText[0].rectTransform.position.y - 5f);
                    OptionsManager.Instance.rText[0].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    OptionsManager.Instance.gText[0].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    OptionsManager.Instance.bText[0].color = TextColor;
                    break;
            }
        }
    }
    private class TextBoxColorStyle : IStyle
    {
        public void ToggleStyle()
        {
            style = 0;
        }
        public void ToggleRGB()
        {
            switch (rgb)
            {
                case 'r':
                    OptionsManager.Instance.textBoxColorSliderR.interactable = true;
                    OptionsManager.Instance.textBoxColorSliderG.interactable = false;
                    OptionsManager.Instance.textBoxColorSliderB.interactable = false;
                    EventSystem.current.SetSelectedGameObject(OptionsManager.Instance.textBoxColorSliderR.gameObject);
                    OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.rText[1].rectTransform.position.x - 150f, OptionsManager.Instance.rText[1].rectTransform.position.y - 5f);
                    OptionsManager.Instance.rText[1].color = TextColor;
                    OptionsManager.Instance.gText[1].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    OptionsManager.Instance.bText[1].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    break;
                case 'g':
                    OptionsManager.Instance.textBoxColorSliderR.interactable = false;
                    OptionsManager.Instance.textBoxColorSliderG.interactable = true;
                    OptionsManager.Instance.textBoxColorSliderB.interactable = false;
                    EventSystem.current.SetSelectedGameObject(OptionsManager.Instance.textBoxColorSliderG.gameObject);
                    OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.gText[1].rectTransform.position.x - 150f, OptionsManager.Instance.gText[1].rectTransform.position.y - 5f);
                    OptionsManager.Instance.rText[1].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    OptionsManager.Instance.gText[1].color = TextColor;
                    OptionsManager.Instance.bText[1].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    break;
                case 'b':
                    OptionsManager.Instance.textBoxColorSliderR.interactable = false;
                    OptionsManager.Instance.textBoxColorSliderG.interactable = false;
                    OptionsManager.Instance.textBoxColorSliderB.interactable = true;
                    EventSystem.current.SetSelectedGameObject(OptionsManager.Instance.textBoxColorSliderB.gameObject);
                    OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.bText[1].rectTransform.position.x - 150f, OptionsManager.Instance.bText[1].rectTransform.position.y - 5f);
                    OptionsManager.Instance.rText[1].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    OptionsManager.Instance.gText[1].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    OptionsManager.Instance.bText[1].color = TextColor;
                    break;
            }
        }
    }
    private class TextBoxTransparencyStyle : IStyle
    {
        public void ToggleStyle()
        {
            OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[3].rectTransform.position.x + 140, OptionsManager.Instance.text[3].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
            EventSystem.current.SetSelectedGameObject(OptionsManager.Instance.textBoxTransparencySlider.gameObject);
            OptionsManager.Instance.textBoxTransparencySlider.interactable = true;
        }
        public void ToggleRGB()
        {
            rgb = empty;
        }
    }
    private class GradientTransparencyStyle : IStyle
    {
        public void ToggleStyle()
        {
            OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.text[4].rectTransform.position.x + 140f, OptionsManager.Instance.text[4].rectTransform.position.y - OptionsManager.Instance.cursorOffset);
            EventSystem.current.SetSelectedGameObject(OptionsManager.Instance.gradientTransparencySlider.gameObject);
            OptionsManager.Instance.gradientTransparencySlider.interactable = true;
        }
        public void ToggleRGB()
        {
            rgb = empty;
        }
    }
    private class GradientColorStyle : IStyle
    {
        public void ToggleStyle()
        {
            style = 0;
        }
        public void ToggleRGB()
        {
            switch (rgb)
            {
                case 'r':
                    OptionsManager.Instance.gradientSliderR.interactable = true;
                    OptionsManager.Instance.gradientSliderG.interactable = false;
                    OptionsManager.Instance.gradientSliderB.interactable = false;
                    EventSystem.current.SetSelectedGameObject(OptionsManager.Instance.gradientSliderR.gameObject);
                    OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.rText[2].rectTransform.position.x - 150f, OptionsManager.Instance.rText[2].rectTransform.position.y - 5f);
                    OptionsManager.Instance.rText[2].color = TextColor;
                    OptionsManager.Instance.gText[2].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    OptionsManager.Instance.bText[2].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    break;
                case 'g':
                    OptionsManager.Instance.gradientSliderR.interactable = false;
                    OptionsManager.Instance.gradientSliderG.interactable = true;
                    OptionsManager.Instance.gradientSliderB.interactable = false;
                    EventSystem.current.SetSelectedGameObject(OptionsManager.Instance.gradientSliderG.gameObject);
                    OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.gText[2].rectTransform.position.x - 150f, OptionsManager.Instance.gText[2].rectTransform.position.y - 5f);
                    OptionsManager.Instance.rText[2].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    OptionsManager.Instance.gText[2].color = TextColor;
                    OptionsManager.Instance.bText[2].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    break;
                case 'b':
                    OptionsManager.Instance.gradientSliderB.interactable = true;
                    EventSystem.current.SetSelectedGameObject(OptionsManager.Instance.gradientSliderB.gameObject);
                    OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.bText[2].rectTransform.position.x - 150f, OptionsManager.Instance.bText[2].rectTransform.position.y - 5f);
                    OptionsManager.Instance.rText[2].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    OptionsManager.Instance.gText[2].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    OptionsManager.Instance.bText[2].color = TextColor;
                    break;
            }
        }
    }
    private class TextFontStyle : IStyle
    {
        public void ToggleStyle()
        {
            EventSystem.current.SetSelectedGameObject(emptyGameObject);
            OptionsManager.Instance.ChangeFontStyle();

            foreach (Text text in OptionsManager.Instance.fontNumText)
            {
                if (text != OptionsManager.Instance.fontNumText[fontStyle - 1])
                {
                    text.color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2, 1f);
                }
                else
                {
                    text.color = TextColor;
                }
            }
            OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.fontNumText[fontStyle - 1].rectTransform.position.x - 150f, OptionsManager.Instance.borderNumText[fontStyle - 1].rectTransform.position.y - 5f);
        }
        public void ToggleRGB()
        {
            rgb = empty;
        }
    }
    private class TextColorStyle : IStyle
    {
        public void ToggleStyle()
        {
            style = 0;
        }
        public void ToggleRGB()
        {
            switch (rgb)
            {
                case 'r':
                    OptionsManager.Instance.textColorSliderR.interactable = true;
                    OptionsManager.Instance.textColorSliderG.interactable = false;
                    OptionsManager.Instance.textColorSliderB.interactable = false;
                    EventSystem.current.SetSelectedGameObject(OptionsManager.Instance.textColorSliderR.gameObject);
                    OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.rText[3].rectTransform.position.x - 150f, OptionsManager.Instance.rText[3].rectTransform.position.y - 5f);
                    OptionsManager.Instance.rText[3].color = TextColor;
                    OptionsManager.Instance.gText[3].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    OptionsManager.Instance.bText[3].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    break;
                case 'g':
                    OptionsManager.Instance.textColorSliderR.interactable = false;
                    OptionsManager.Instance.textColorSliderG.interactable = true;
                    OptionsManager.Instance.textColorSliderB.interactable = false;
                    EventSystem.current.SetSelectedGameObject(OptionsManager.Instance.textColorSliderG.gameObject);
                    OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.gText[3].rectTransform.position.x - 150f, OptionsManager.Instance.gText[3].rectTransform.position.y - 5f);
                    OptionsManager.Instance.rText[3].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    OptionsManager.Instance.gText[3].color = TextColor;
                    OptionsManager.Instance.bText[3].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    break;
                case 'b':
                    OptionsManager.Instance.textColorSliderR.interactable = false;
                    OptionsManager.Instance.textColorSliderG.interactable = false;
                    OptionsManager.Instance.textColorSliderB.interactable = true;
                    EventSystem.current.SetSelectedGameObject(OptionsManager.Instance.textColorSliderB.gameObject);
                    OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.bText[3].rectTransform.position.x - 150f, OptionsManager.Instance.bText[3].rectTransform.position.y - 5f);
                    OptionsManager.Instance.rText[3].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    OptionsManager.Instance.gText[3].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    OptionsManager.Instance.bText[3].color = TextColor;
                    break;
            }
        }
    }
    private class TextShadowColorStyle : IStyle
    {
        public void ToggleStyle()
        {
            style = 0;
        }
        public void ToggleRGB()
        {
            switch (rgb)
            {
                case 'r':
                    OptionsManager.Instance.textShadowSliderR.interactable = true;
                    OptionsManager.Instance.textShadowSliderG.interactable = false;
                    OptionsManager.Instance.textShadowSliderB.interactable = false;
                    EventSystem.current.SetSelectedGameObject(OptionsManager.Instance.textShadowSliderR.gameObject);
                    OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.rText[4].rectTransform.position.x - 150f, OptionsManager.Instance.rText[4].rectTransform.position.y - 5f);
                    OptionsManager.Instance.rText[4].color = TextColor;
                    OptionsManager.Instance.gText[4].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    OptionsManager.Instance.bText[4].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    break;
                case 'g':
                    OptionsManager.Instance.textShadowSliderR.interactable = false;
                    OptionsManager.Instance.textShadowSliderG.interactable = true;
                    OptionsManager.Instance.textShadowSliderB.interactable = false;
                    EventSystem.current.SetSelectedGameObject(OptionsManager.Instance.textShadowSliderG.gameObject);
                    OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.gText[4].rectTransform.position.x - 150f, OptionsManager.Instance.gText[4].rectTransform.position.y - 5f);
                    OptionsManager.Instance.rText[4].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    OptionsManager.Instance.gText[4].color = TextColor;
                    OptionsManager.Instance.bText[4].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    break;
                case 'b':
                    OptionsManager.Instance.textShadowSliderR.interactable = false;
                    OptionsManager.Instance.textShadowSliderG.interactable = false;
                    OptionsManager.Instance.textShadowSliderB.interactable = true;
                    EventSystem.current.SetSelectedGameObject(OptionsManager.Instance.textShadowSliderB.gameObject);
                    OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.bText[4].rectTransform.position.x - 150f, OptionsManager.Instance.bText[4].rectTransform.position.y - 5f);
                    OptionsManager.Instance.rText[4].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    OptionsManager.Instance.gText[4].color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2);
                    OptionsManager.Instance.bText[4].color = TextColor;
                    break;
            }
        }
    }
    private class TextShadowTransparencyStyle : IStyle
    {
        public void ToggleStyle()
        {
            OptionsManager.Instance.textShadowTransparencySlider.interactable = true;
        }
        public void ToggleRGB()
        {
            rgb = empty;
        }
    }
    private class MusicVolumeStyle : IStyle
    {
        public void ToggleStyle()
        {
            OptionsManager.Instance.musicVolumeSlider.interactable = true;
        }
        public void ToggleRGB()
        {
            rgb = empty;
        }
    }
    private class SfxVolumeStyle : IStyle
    {
        public void ToggleStyle()
        {
            OptionsManager.Instance.sfxVolumeSlider.interactable = true;
        }
        public void ToggleRGB()
        {
            rgb = empty;
        }
    }
    private class DifficultySetting : IStyle
    {
        public void ToggleStyle()
        {
            EventSystem.current.SetSelectedGameObject(emptyGameObject);

            foreach (Text text in OptionsManager.Instance.difficultyNumText)
            {
                if (text != OptionsManager.Instance.difficultyNumText[difficulty - 1])
                {
                    text.color = new Color(TextColor.r / 2, TextColor.g / 2, TextColor.b / 2, 1f);
                }
                else
                {
                    text.color = TextColor;
                }
            }
            OptionsManager.Instance.textCursor.rectTransform.position = new Vector2(OptionsManager.Instance.difficultyNumText[difficulty - 1].rectTransform.position.x - 150f, OptionsManager.Instance.difficultyNumText[difficulty - 1].rectTransform.position.y - 5f);
        }
        public void ToggleRGB()
        {
            rgb = empty;
        }
    }
    #endregion 
}