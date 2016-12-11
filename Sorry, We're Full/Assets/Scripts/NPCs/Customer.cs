using UnityEngine;
// Project: Sorry, We're Full
// Author : bradur

// Date   : 11.12.2016 09:19 UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(RandomMovement))]
public class Customer : MonoBehaviour
{

    private bool intervalFinished = true;
    private float interval = 3f;
    private float timer;


    private KeyCode keyStartFight;


    [SerializeField]
    private Color color;
    public Color Color { get { return color; } }
    private string colorText;
    public string ColorText { get { return colorText; } }

    [SerializeField]
    private string customerName;
    public string CustomerName { get { return customerName; } }

    private Business business;
    private PlayerController pc;

    [SerializeField]
    private RandomMovement randomMovement;

    [SerializeField]
    private Enemy enemy;

    [SerializeField]
    private string greeting;
    [SerializeField]
    private string greetingTaunt;

    private bool isFighting = false;
    public bool IsFighting { set { isFighting = value; } }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (!isFighting)
        {
            if (collision2D.gameObject.tag == "Player")
            {
                string message = greeting;
                if((int)pc.EquippedWeapon < (int)enemy.EnemyWeapon.Weapon)
                {
                    message = greetingTaunt;
                }
                UIManager.main.ShowMessage(string.Format("<color=#{0}><b>{1}</b></color>: {2}", ColorText, customerName, message));
                UIManager.main.ShowMessage(string.Format("Press {0} to fight <color=#{1}><b>{2}</b></color>!", keyStartFight, ColorText, customerName));
                
            }
        }
    }

    public void Init(Transform target, Transform parent)
    {
        transform.position = target.position;
        transform.SetParent(parent, false);
        keyStartFight = KeyManager.main.GetKey(Action.StartFight);
        business = WorldManager.main.Player.GetComponent<Business>();
        pc = WorldManager.main.Player.GetComponent<PlayerController>();
        colorText = UIManager.main.GetColorAsString(color);
        timer = interval;
    }

    void Start()
    {
        Init(transform, transform.parent);
    }

    public void TeleportTo(Transform target, float direction)
    {
        transform.position = target.position;
        float radian = direction * Mathf.Deg2Rad;
        //transform.eulerAngles = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        randomMovement.enabled = false;
        isFighting = true;
        enemy.enabled = true;
        enemy.Init(pc.transform);
    }


    void Update()
    {
        if (!isFighting)
        {
            if (!intervalFinished)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    timer = interval;
                    intervalFinished = true;
                }
            }
            if (Input.GetKeyUp(keyStartFight))
            {
                if (Vector2.Distance(transform.position, WorldManager.main.Player.position) <= 2f)
                {
                    InventoryItemType equippedWeapon = pc.EquippedWeapon;
                    if (equippedWeapon == InventoryItemType.Bow)
                    {
                        UIManager.main.ShowMessage(string.Format("<color=#{0}><b>{1}</b></color>: You don't even have a weapon!", ColorText, customerName));
                        UIManager.main.ShowMessage(string.Format("<color=#{0}><b>{1}</b></color>: I'm not going to fight anyone who doesn't even have a proper melee weapon!", ColorText, customerName));
                    }
                    else
                    {
                        WorldManager.main.StartFight(this);
                    }
                }
            }
        }
    }
}
