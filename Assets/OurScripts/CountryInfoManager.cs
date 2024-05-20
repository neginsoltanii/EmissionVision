using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountryInfoManager : MonoBehaviour
{
    public GameObject wall;
    public GameObject generalInfoText;
    public GameObject countryInfoPanel1;
    public GameObject countryInfoPanel2;
    public Button closeButton1;
    public Button closeButton2;

    // Track whether the panels are active
    private bool isPanel1Active = false;
    private bool isPanel2Active = false;

    private Vector3 fullScale;
    private Vector3 halfScale;

    private RectTransform wallRectTransform;
    private RectTransform panel1RectTransform;
    private RectTransform panel2RectTransform;


    void Start()
    {
        // Hide panels initially
        countryInfoPanel1.SetActive(false);
        countryInfoPanel2.SetActive(false);

        closeButton1.onClick.AddListener(ClosePanel1);
        closeButton2.onClick.AddListener(ClosePanel2);

        // Initialize scales
        fullScale = new Vector3(0.9f, 0.7f, 1f); // Full size scale
        halfScale = new Vector3(0.45f, 0.7f, 1f); // Half size scale

        // Get RectTransforms
        wallRectTransform = wall.GetComponent<RectTransform>();
        panel1RectTransform = countryInfoPanel1.GetComponent<RectTransform>();
        panel2RectTransform = countryInfoPanel2.GetComponent<RectTransform>();

        // Set initial scale for the panels
        panel1RectTransform.localScale = fullScale;
        panel2RectTransform.localScale = halfScale;
    }

    void Update()
    {
        // Check for space key press to toggle panels (focus of the input system)
        if (Input.GetKeyDown(KeyCode.Space))
        {

            HandleSpaceKey();
        }
    }

    void HandleSpaceKey()
    {
        if (isPanel1Active && isPanel2Active)
        {
            // Do nothing if both panels are active
            return;
        }
        else if (isPanel1Active && !isPanel2Active)
        {
            ShowPanel2();
        }
        else if (!isPanel1Active && isPanel2Active)
        {
            ShowPanel1Switched();
        }
        else if (!isPanel1Active && !isPanel2Active)
        {
            ShowPanel1();
        }

        // Ensure focus is set back to the game window
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    }

    // Show the first panel and hide the general info text
    void ShowPanel1()
    {
        generalInfoText.SetActive(false);
        countryInfoPanel1.SetActive(true);
        panel1RectTransform.localScale = fullScale;
        panel1RectTransform.anchoredPosition = Vector2.zero;
        isPanel1Active = true;
    }

    // Show the first panel but switch places with the second panel
    void ShowPanel1Switched()
    {
        // Swap panel references
        var temp = countryInfoPanel1;
        countryInfoPanel1 = countryInfoPanel2;
        countryInfoPanel2 = temp;

        var tempRect = panel1RectTransform;
        panel1RectTransform = panel2RectTransform;
        panel2RectTransform = tempRect;

        isPanel1Active = true;
        isPanel2Active = false;

        // Resize and reposition the panels
        ShowPanel2();
    }

    // Show the second panel, resize and reposition the first panel
    void ShowPanel2()
    {
        // Resize the first panel to half its size from the right
        panel1RectTransform.localScale = halfScale;

        // Adjust position of the first panel to keep its left edge fixed
        float panel1NewCenterX = -wallRectTransform.rect.width * 0.225f;
        panel1RectTransform.anchoredPosition = new Vector2(panel1NewCenterX, 0);

        // Activate and position the second panel to the right of the first panel
        countryInfoPanel2.SetActive(true);
        float panel2NewCenterX = panel1NewCenterX + wallRectTransform.rect.width * 0.45f;
        panel2RectTransform.anchoredPosition = new Vector2(panel2NewCenterX, 0);

        panel2RectTransform.localScale = halfScale;
        isPanel2Active = true;
    }

    // Close the first panel
    void ClosePanel1()
    {
        if (!isPanel1Active) return; // Ensure only active panel can be closed

        countryInfoPanel1.SetActive(false);
        isPanel1Active = false;

        if (isPanel2Active)
        {
            // If the second panel is active, resize it to full size and reposition
            panel2RectTransform.localScale = fullScale;
            panel2RectTransform.anchoredPosition = Vector2.zero;
        }
        else
        {
            generalInfoText.SetActive(true); // Show the general info text again
        }
    }

    // Close the second panel 
    void ClosePanel2()
    {
        if (!isPanel2Active) return; // Ensure only active panel can be closed

        countryInfoPanel2.SetActive(false);
        isPanel2Active = false;

        if (isPanel1Active)
        {
            // If the first panel is active, resize it to full size and reposition
            panel1RectTransform.localScale = fullScale;
            panel1RectTransform.anchoredPosition = Vector2.zero;
        }
        else
        {
            generalInfoText.SetActive(true); // Show the general info text again
        }
    }
}