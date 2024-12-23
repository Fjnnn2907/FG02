using UnityEngine;
using UnityEngine.UI;

public enum SwordTpye
{
    Regular,
    Bounce,
    Pierce,
    Spin
}
public class SwordSkill : Skill
{
    public SwordTpye swordTpye = SwordTpye.Regular;
    [Header("Bounce")]
    [SerializeField] private int bouceAmout = 4;
    [SerializeField] private float bouceGravity = 1;
    [SerializeField] private float bounceSpeed = 10;
    [SerializeField] private UISkillTreeSlot bouceButton;

    [Header("Perice")]
    [SerializeField] private int periceAmout = 2;
    [SerializeField] private float periceGravity = 1;
    [SerializeField] private UISkillTreeSlot periceButton;

    [Header("Spin")]
    [SerializeField] private float hitCooldown = .35f;
    [SerializeField] private float maxTravelDistace = 7;
    [SerializeField] private float spinDuration = 0.75f;
    [SerializeField] private float spinGravity = 1;
    [SerializeField] private UISkillTreeSlot spineButton;

    [Header("Passive")]
    public bool timeStop;
    [SerializeField] private UISkillTreeSlot timeStopButton;
    public bool xuyenGiap;
    [SerializeField] private UISkillTreeSlot xuyenGiapButton;

    [Header("Skill")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 swordForce = new Vector2(35, 25);
    [SerializeField] private float swordGravity = 0.5f;
    [SerializeField] private float freeTime = 1;
    [SerializeField] private float returnSpeed = 20;
    public bool unlocedSowrd { get; private set; }
    [SerializeField] private UISkillTreeSlot swordButton;

    private Vector2 finalDir;

    [Header("Dots")]
    [SerializeField] private int munberDots = 20;
    [SerializeField] private float spaceBeetwenDots = .07f;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private GameObject dotsParent;

    private GameObject[] dots;

    protected override void Start()
    {
        base.Start();

        GenerateDots();

        SetGravity();


        swordButton.GetComponent<Button>().onClick.AddListener(() => UnlocedSword());
        timeStopButton.GetComponent<Button>().onClick.AddListener(() => UnlocedStopTime());
        xuyenGiapButton.GetComponent<Button>().onClick.AddListener(() => XuyenGiap());
        periceButton.GetComponent<Button>().onClick.AddListener(() => PericeSword());
        bouceButton.GetComponent<Button>().onClick.AddListener(() => BouceSword());
        spineButton.GetComponent<Button>().onClick.AddListener(() => SpinSword());

    }

    private void SetGravity()
    {
        if (swordTpye == SwordTpye.Bounce)
            swordGravity = bouceGravity;
        else if (swordTpye == SwordTpye.Pierce)
            swordGravity = periceGravity;
        else if (swordTpye == SwordTpye.Spin)
            swordGravity = spinGravity;

    }

    protected override void Update()
    {

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            finalDir = new Vector2(AnimDirection().normalized.x * swordForce.x,
                                   AnimDirection().normalized.y * swordForce.y);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBeetwenDots);
            }
        }
    }
    public void CreateSword()
    {
        GameObject newSowrd = Instantiate(swordPrefab, character.transform.position, Quaternion.identity);
        var newSowrdScript = newSowrd.GetComponent<SwordSkillController>();

        if (swordTpye == SwordTpye.Bounce)
            newSowrdScript.SetupBouce(true, bouceAmout, bounceSpeed);
        else if (swordTpye == SwordTpye.Pierce)
            newSowrdScript.SetUpPierce(periceAmout);
        else if (swordTpye == SwordTpye.Spin)
            newSowrdScript.SetupSpin(true,maxTravelDistace,spinDuration, hitCooldown);

        newSowrdScript.SetUpSword(finalDir, swordGravity, character, freeTime, returnSpeed);
        character.NewSword(newSowrd);
        DotsActive(false);
    }
    #region AnimSword
    public Vector2 AnimDirection()
    {
        var characterPos = character.transform.position;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var dir = mousePos - characterPos;

        return dir;
    }

    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }

    private void GenerateDots()
    {
        dots = new GameObject[munberDots];
        for (int i = 0; i < munberDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, character.transform.position, Quaternion.identity, dotsParent.transform);
            dots[i].SetActive(false);
        }
    }
    private Vector2 DotsPosition(float t)
    {
        Vector2 pos = (Vector2)character.transform.position + new Vector2(
            AnimDirection().normalized.x * swordForce.x,
            AnimDirection().normalized.y * swordForce.y)
            * t + .5f * (Physics2D.gravity * swordGravity) * (t * t);

        return pos;
    }
    #endregion

    #region Unloced SKill Tree

    private void UnlocedSword()
    {
        if (swordButton.unlock)
        {
            unlocedSowrd = true;
            swordTpye = SwordTpye.Regular;
        }

    }
    private void UnlocedStopTime()
    {
        if(timeStopButton.unlock)
            timeStop = true;
    }

    private void XuyenGiap()
    {
        if(xuyenGiapButton.unlock)
            xuyenGiap = true;
    }
    private void BouceSword()
    {
        if (bouceButton.unlock)
            swordTpye = SwordTpye.Bounce;
    }

    private void PericeSword()
    {
        if (periceButton.unlock)
            swordTpye = SwordTpye.Pierce;
    }
    private void SpinSword()
    {
        if (spineButton.unlock)
            swordTpye = SwordTpye.Spin;
    }

    #endregion
}

