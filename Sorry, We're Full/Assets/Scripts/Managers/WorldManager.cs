// Date   : 10.12.2016 09:53
// Project: Sorry, We're Full
// Author : bradur

using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{

    public static WorldManager main;

    [SerializeField]
    private Material outMaterial;
    [SerializeField]
    private Material houseMaterial;

    [SerializeField]
    private Transform projectileContainer;
    public Transform ProjectileContainer { get { return projectileContainer; } }

    [SerializeField]
    private InnKeeperTable innKeeper;

    [SerializeField]
    private Transform player;
    public Transform Player { get { return player; } }

    private bool customerHasBeenBeat = false;
    public bool CustomerHasBeenBeat { get { return customerHasBeenBeat; } }

    [SerializeField]
    private Business business;
    public Business Business { get { return business; } }

    [SerializeField]
    private Transform fightArenaPosition;

    [SerializeField]
    private Transform fightArenaPositionCustomer;

    [SerializeField]
    private GameObject arenaDoor;

    [SerializeField]
    [Range(0, 360)]
    private float playerFightDirection = 0f;

    [SerializeField]
    [Range(0, 360)]
    private float customerFightDirection = 180f;

    private bool heDed = false;

    RandomWrapper rng = new RandomWrapper();

    [SerializeField]
    private Transform customerPosition;

    [SerializeField]
    private Transform customerContainer;

    [SerializeField]
    private GameObject[] customers;

    [SerializeField]
    private GameObject[] animals;

    int level = 0;
    public int Level { get { return level; } }

    bool sleeping = false;

    [Range(0.5f, 5f)]
    [SerializeField]
    private float sleepTime = 2f;
    private float sleepTimer;

    Customer currentFightingCustomer = null;
    Enemy currentFightingEnemy = null;
    public Enemy CurrentFightingEnemy { get { return currentFightingEnemy; } set { currentFightingEnemy = value; } }

    [SerializeField]
    private Light globalLight;
    private float lightIntensityCache;

    [SerializeField]
    private Transform bed;

    public void CustomerWasBeaten(Customer customer)
    {
        UIManager.main.ClearQueue();
        UIManager.main.ShowMessage(string.Format(
            "You beat <color=#{0}><b>{1}</b></color>! You can rent the room for the night!",
            customer.ColorText,
            customer.CustomerName
        ));
        currentFightingEnemy = null;
        UIManager.main.HideEnemyHealth();
        customerHasBeenBeat = true;
        innKeeper.IToldYou = 0;
        arenaDoor.SetActive(false);
        level++;
    }

    void Start()
    {
        NewDay();
    }

    public void SleepTheNight()
    {
        UIManager.main.ClearQueue();
        player.GetComponent<PlayerController>().enabled = false;
        sleeping = true;
        sleepTimer = sleepTime;
        TeleportTo(player, bed);
    }

    public void NewDay()
    {
        player.GetComponent<PlayerController>().enabled = true;
        animals[level].SetActive(true);
        customers[level].SetActive(true);
        globalLight.intensity = lightIntensityCache;
        //SpawnNextCustomer(level);
    }

    /*void SpawnNextCustomer(int level)
    {
        Customer customer = Instantiate(customers[level]);
        customer.Init(customerPosition, customerContainer);
    }*/

    public void WakeUp()
    {
        innKeeper.IToldYou = 0;
        customerHasBeenBeat = false;
        UIManager.main.GainLevel(level);
        NewDay();
    }

    public void StartFight(Customer customer)
    {
        outMaterial.SetFloat("_Darken", 0);
        houseMaterial.SetFloat("_Transparency", 0);
        arenaDoor.SetActive(true);
        TeleportTo(player, fightArenaPosition);
        customer.TeleportTo(fightArenaPositionCustomer, customerFightDirection);
        currentFightingCustomer = customer;
        UIManager.main.ClearQueue();
    }

    public void EnemyWasHit(InventoryItemType weapon)
    {
        currentFightingEnemy.GetHit(weapon);
    }

    void TeleportTo(Transform playerTransform, Transform target)
    {
        player.position = target.position;
        float radian = playerFightDirection * Mathf.Deg2Rad;
        player.GetComponent<PlayerController>().ArrowDirection = player.transform.right;
        //player.eulerAngles = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public void PlayerWasHit(Customer customer, InventoryItemType weapon)
    {
        if (exitKey == KeyCode.None)
        {
            restartKey = KeyManager.main.GetKey(Action.RestartGame);
            exitKey = KeyManager.main.GetKey(Action.Exit);
        }
        int healthLoss = 0;
        if (weapon == InventoryItemType.Dagger)
        {
            healthLoss = rng.Range(1, 3);
        }
        else if (weapon == InventoryItemType.ShortSword)
        {
            healthLoss = rng.Range(2, 5);
        }
        else if (weapon == InventoryItemType.Scimitar)
        {
            healthLoss = rng.Range(5, 8);
        }
        UIManager.main.ShowMessage(string.Format(
            "You were hit by <b><color=#{0}>{1}</color></b>'s <color=#{2}><b>{3}</b></color>! You lost {4} health!",
            customer.ColorText,
            customer.CustomerName,
            UIManager.main.GetColorAsString(weapon),
            weapon,
            healthLoss
        ));
        int newHealth = UIManager.main.AddToPlayerHealth(-healthLoss);
        if (newHealth <= 0)
        {
            PlayerDied();
        }

        SoundManager.main.PlaySound(SoundType.PlayerWasHit);
    }

    KeyCode exitKey;
    KeyCode restartKey;

    void Update()
    {
        if (sleeping)
        {
            sleepTimer -= Time.deltaTime;
            globalLight.intensity = Mathf.Lerp(globalLight.intensity, 0f, 1.0f - (sleepTimer / sleepTime));
            if (sleepTimer <= 0.01f)
            {
                sleeping = false;
                sleepTimer = sleepTime;
                WakeUp();
            }
        }
        if (heDed)
        {
            if (Input.GetKeyUp(exitKey))
            {
                Application.Quit();
            }
            else if (Input.GetKeyUp(restartKey))
            {
                RestartGame();
            }
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restart!");
        SceneManager.LoadScene("main");
    }
    public void PlayerDied()
    {
        currentFightingEnemy.IsFighting = false;
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Business>().enabled = false;
        //currentFightingCustomer.GetComponent<RandomMovement>().enabled = true;
        currentFightingEnemy.GetComponent<RandomMovement>().enabled = true;
        heDed = true;
        UIManager.main.ClearQueue();
        UIManager.main.LoopQueue();
        UIManager.main.ShowMessage("<color=red><b>You died!</b></color>");
        UIManager.main.ShowMessage(string.Format(
            "Press {0} to start again.", restartKey
        ));
        UIManager.main.ShowMessage(string.Format(
            "Press {0} to exit the game.", exitKey
        ));
    }
    void Awake()
    {
        main = this;
        outMaterial.SetFloat("_Darken", 0);
        houseMaterial.SetFloat("_Transparency", 0);
        sleepTimer = sleepTime;
        lightIntensityCache = globalLight.intensity;
    }

}
