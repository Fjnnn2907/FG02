using System;
using UnityEngine;

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
    [SerializeField] private int bouceAmout;
    [SerializeField] private float bouceGravity;

    [Header("Perice")]
    [SerializeField] private int periceAmout;
    [SerializeField] private float periceGravity;

    [Header("Skill")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 swordForce;
    [SerializeField] private float swordGravity;

    private Vector2 finalDir;

    [Header("Dots")]
    [SerializeField] private int munberDots;
    [SerializeField] private float spaceBeetwenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private GameObject dotsParent;

    private GameObject[] dots;

    protected override void Start()
    {
        base.Start();

        GenerateDots();

        SetGravity();
    }

    private void SetGravity()
    {
        if(swordTpye == SwordTpye.Bounce)
            swordGravity = bouceGravity;
        else if(swordTpye == SwordTpye.Pierce)
            swordGravity = periceGravity;

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
        
        if(swordTpye == SwordTpye.Bounce)
            newSowrdScript.SetupBouce(true, bouceAmout);
        else if(swordTpye == SwordTpye.Pierce)
            newSowrdScript.SetUpPierce(periceAmout);

        newSowrdScript.SetUpSword(finalDir, swordGravity, character);
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
}

