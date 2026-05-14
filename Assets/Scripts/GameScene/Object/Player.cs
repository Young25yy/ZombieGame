using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;
    private int atk;
    public int money;
    private float rotateSpeed = 120;
    private bool isCrouch = false;
    private float layerWeight;
    public Transform shootPoint;
    public Transform gunPoint;
    private LineRenderer aimLine;
    private bool cursorVisible;
    private Vector3 shootPointOrigin;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        InitAimLine();
        if (shootPoint != null) shootPointOrigin = shootPoint.localPosition;
    }

    private void InitAimLine()
    {
        GameObject lineObj = new GameObject("AimLine");
        lineObj.transform.position = Vector3.zero;
        aimLine = lineObj.AddComponent<LineRenderer>();
        aimLine.startWidth = 0.03f;
        aimLine.endWidth = 0.03f;
        aimLine.material = new Material(Shader.Find("Sprites/Default"));
        aimLine.startColor = Color.red;
        aimLine.endColor = Color.red;
    }
    public void InitPlayer(int atk, int money)
    {
        this.atk = atk;
        this.money = money;
        UpdateMoney();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorVisible = !cursorVisible;
            Cursor.visible = cursorVisible;
            Cursor.lockState = cursorVisible ? CursorLockMode.None : CursorLockMode.Locked;
        }
        if (cursorVisible) return;
        animator.SetFloat("HSpeed", Input.GetAxis("Horizontal"));
        animator.SetFloat("VSpeed", Input.GetAxis("Vertical"));
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime * Input.GetAxis("Mouse X"));
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetTrigger("Roll");
        }
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Fire");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouch = !isCrouch;
            if (shootPoint != null)
                shootPoint.localPosition = isCrouch ? shootPointOrigin + Vector3.down * 0.3f : shootPointOrigin;
        }
        if (isCrouch && animator.GetLayerWeight(1) != 1)
        {
            layerWeight = animator.GetLayerWeight(1);
            layerWeight += Time.deltaTime * 3;
            if (layerWeight > 1) layerWeight = 1;
            animator.SetLayerWeight(1, layerWeight);
        }
        else if (!isCrouch && animator.GetLayerWeight(1) != 0)
        {
            layerWeight = animator.GetLayerWeight(1);
            layerWeight -= Time.deltaTime * 3;
            if (layerWeight < 0) layerWeight = 0;
            animator.SetLayerWeight(1, layerWeight);
        }
        UpdateAimLine();
    }

    private void UpdateAimLine()
    {
        if (aimLine == null || shootPoint == null) return;
        aimLine.SetPosition(0, shootPoint.position);
        aimLine.SetPosition(1, shootPoint.position + transform.forward * 50);
    }
    public void KnifeEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward * 0.5f + transform.up * 1,
                                                     0.5f, 1 << LayerMask.NameToLayer("Zombie"));
        GameDataMgr.Instance.PlaySound("Music/Knife");
        for (int i = 0; i < colliders.Length; i++)
        {
            Zombie zombie = colliders[i].GetComponent<Zombie>();
            if (zombie.gameObject.layer == LayerMask.NameToLayer("Zombie") && !zombie.isDead)
            {
                zombie.Wound(atk);
            }
        }
    }
    public void ShootEvent()
    {
        RaycastHit[] hits = Physics.RaycastAll(shootPoint.position, shootPoint.forward, 1000, 1 << LayerMask.NameToLayer("Zombie"));
        GameDataMgr.Instance.PlaySound("Music/Gun");
        GameObject effObj2 = Instantiate(Resources.Load<GameObject>("Eff/2"), gunPoint.position, gunPoint.rotation);
        effObj2.transform.SetParent(gunPoint);
        Destroy(effObj2, 0.2f);
        for(int i = 0;i < hits.Length;i++)
        {
            Zombie zombie = hits[i].collider.GetComponent<Zombie>();
            if (zombie.gameObject.layer == LayerMask.NameToLayer("Zombie") && !zombie.isDead)
            {
                GameObject effObj = Instantiate(Resources.Load<GameObject>(GameDataMgr.Instance.nowRole.hitEff), hits[i].point, Quaternion.LookRotation(hits[i].normal));
                Destroy(effObj, 0.5f);
                zombie.Wound(atk);
                break;
            }
        }
    }
    public void UpdateMoney()
    {
        UIManager.Instance.GetPanel<GamePanel>().UpdateMoney(money);
    }
    public void AddMoney(int money)
    {
        this.money += money;
        UpdateMoney();
    }

    public void SetCursorVisible(bool visible)
    {
        cursorVisible = visible;
        Cursor.visible = visible;
        Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Locked;
    }

}
