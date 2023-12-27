using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TheoryButtonController : MonoBehaviour
{
    
    [SerializeField] private float speed;
    [SerializeField] private float distancePos;
    [SerializeField] private int maxPages;
    private float initialPos;

    [SerializeField] private GameObject homeBtn;
    [SerializeField] private GameObject backBtn;
    [SerializeField] private GameObject nextBtn;
    [SerializeField] private GameObject pages;

    private int pageNum = 1;
    private bool pageMoving = false;
    private int increasingPage;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = pages.GetComponent<RectTransform>().anchoredPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        MovingPage();

        if (pageNum == maxPages) nextBtn.SetActive(false);
        else if (pageNum == 1) backBtn.SetActive(false);
        else { nextBtn.SetActive(true); backBtn.SetActive(true); }
    }

    private void MovingPage()
    {
        if (pageMoving == true)
        {
            if (increasingPage == 1)
            {
                RectTransform pagePos = pages.GetComponent<RectTransform>();
                pagePos.anchoredPosition = new Vector2(pagePos.anchoredPosition.x - (speed * Time.deltaTime),
                                                                        pagePos.anchoredPosition.y);

                if (pagePos.anchoredPosition.x <= initialPos - ((pageNum - 1) * distancePos))
                {
                    pagePos.anchoredPosition = new Vector2(initialPos - ((pageNum - 1) * distancePos),
                                                            pagePos.anchoredPosition.y);
                    pageMoving = false;
                }
            }

            else
            {
                RectTransform pagePos = pages.GetComponent<RectTransform>();
                pagePos.anchoredPosition = new Vector2(pagePos.anchoredPosition.x + (speed * Time.deltaTime),
                                                                        pagePos.anchoredPosition.y);

                if (pagePos.anchoredPosition.x >= initialPos - ((pageNum - 1) * distancePos))
                {
                    pagePos.anchoredPosition = new Vector2(initialPos - ((pageNum - 1) * distancePos),
                                                            pagePos.anchoredPosition.y);
                    pageMoving = false;
                }
            }

        }
    }

    public void NextBtn()
    {
        pageNum += 1;
        pageMoving = true;
        increasingPage = 1;
    }

    public void BackBtn()
    {
        pageNum -= 1;
        pageMoving = true;
        increasingPage = -1;
    }

    public void HomeBtn(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
