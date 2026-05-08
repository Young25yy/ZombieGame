using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;
    private int atk;
    private int money;
    private float rotateSpeed = 145;
    private bool isCrouch = false;
    private float layerWeight;
    public Transform shootPoint;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
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
    }
    public void KnifeEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward * 0.5f + transform.up * 1,
                                                     0.5f, 1 << LayerMask.NameToLayer("Monster"));
        for (int i = 0; i < colliders.Length; i++)
        {

        }
    }
    public void ShootEvent()
    {
        RaycastHit[] hits = Physics.RaycastAll(shootPoint.position, shootPoint.forward, 1000, 1 << LayerMask.NameToLayer("Monster"));
        for(int i = 0;i < hits.Length;i++)
        {

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
}
