using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleManagementScript : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bingChilling;
    [SerializeField] private GameObject battleArea;
    [SerializeField] private GameObject fightArea;
    [SerializeField] private GameObject actMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject itemMenu;
    [SerializeField] private GameObject spareMenu;
    [SerializeField] private GameObject testBullet;
    [SerializeField] private BattleButton[] buttons;

    private Context currentContext = Context.MainMenu;

    private Vector2 battleAreaScale;
    private Vector2 battleAreaInitialScale;
    private Vector2 playerInitialPosition;

    private int menuIndex = 0;
    private int menuIndexMax = 3; // might want to change this for special cases like asgore
    [HideInInspector] public bool AttackFinished = false;
    private bool ready = true;
    Vector2Int itemIndex = new Vector2Int(0, 0);

    // Use this for initialization
    void Start()
    {
        battleAreaScale = battleArea.transform.localScale;
        battleAreaInitialScale = battleArea.transform.localScale;
        playerInitialPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("j"))
        {
            player.GetComponent<Player>().takeDmg(7);
        }
        if (Input.GetKeyDown("x") && !actMenu.GetComponent<Acts>().actText && currentContext != Context.EnemyTurn && currentContext != Context.PlayerTurn)
        {//unselect a button
            currentContext = Context.MainMenu;
        }
        // Move around the UI
        switch (currentContext)
        {
            case (Context.MainMenu)://default (if MainMenu, choose fight act item mercy)
                if (!bingChilling.GetComponent<enemy>().alive)
                {
                    mainMenu.transform.GetChild(0).GetComponent<TextMeshPro>().text = "YOU WON :)";
                    currentContext = Context.MainMenu;
                }
                else if (Input.GetKeyDown("z") && ready)
                {//select a button
                    currentContext = buttons[menuIndex].context;
                }
                if (Input.GetKeyDown("left"))
                {
                    menuIndex--;
                }
                else if (Input.GetKeyDown("right"))
                {
                    menuIndex++;
                }

                if (menuIndex < 0)
                {
                    menuIndex = menuIndexMax;
                }

                if (menuIndex > menuIndexMax)
                {
                    menuIndex = 0;
                }
                battleAreaScale = battleAreaInitialScale;
                actMenu.SetActive(false);
                mainMenu.SetActive(true);
                itemMenu.SetActive(false);
                spareMenu.SetActive(false);
                UpdateMenu();
                break;
            case (Context.PlayerTurn):
                if (ready)
                {
                    PlayerFight();
                }
                mainMenu.SetActive(false);
                break;
            case (Context.EnemyTurn):
                battleAreaScale = new Vector2(0.5f, battleAreaInitialScale.y);
                mainMenu.SetActive(false);
                break;
            case (Context.ActMenu):
                if (ready)
                {
                    actMenu.SetActive(true);
                    mainMenu.SetActive(false);
                    UpdateActMenu();
                }
                break;
            case (Context.ItemMenu):
                if (ready)
                {
                    itemMenu.SetActive(true);
                    mainMenu.SetActive(false);
                    UpdateItemMenu();
                }
                break;
            case (Context.SpareMenu):
                if (ready)
                {
                    spareMenu.SetActive(true);
                    mainMenu.SetActive(false);
                    UpdateMercyMenu();
                }
                break;
        }
    }

    private void FixedUpdate()
    {
        ResizeBattleArea(battleAreaScale);
    }

    private void PlayerFight()
    {
        fightArea.SetActive(true);
        if (AttackFinished)
        {
            AttackFinished = false;
            if (bingChilling.GetComponent<enemy>().alive)
            {
                StartCoroutine(EnemyFight());
            }
            else
            {
                bingChilling.transform.GetChild(0).gameObject.SetActive(false);
                fightArea.SetActive(false);
                player.GetComponent<Player>().StopMoving();
                currentContext = Context.MainMenu;
            }
        }
    }

    public IEnumerator EnemyFight()
    {
        allEnemyAttacks allenemyattacks = bingChilling.transform.GetChild(1).GetComponent<allEnemyAttacks>();
        enemy phases = bingChilling.GetComponent<enemy>();
        fightArea.SetActive(false);
        itemMenu.SetActive(false);
        spareMenu.SetActive(false);
        actMenu.SetActive(false);
        currentContext = Context.EnemyTurn;
        bingChilling.transform.GetChild(0).gameObject.SetActive(false);//stop talking
        if (player.GetComponent<Player>().canMove == false)//make player move
        {
            player.transform.position = playerInitialPosition;
            player.GetComponent<Player>().canMove = true;
        }
        yield return new WaitForSeconds(0.5f);//wait a time before Jhon Cena attacks
        if (phases.phase0)//phase 1
        {
            int randomAttack = Random.Range(0, 3);
            StartCoroutine(allenemyattacks.activateAttack(randomAttack));
            yield return new WaitForSeconds(8f);//reset to menu after seconds
            allenemyattacks.endAttack(randomAttack);
        }
        else if (phases.phase1)//phase 2
        {
            int randomAttack0 = UnityEngine.Random.Range(3,6);
            int randomAttack1;
            do
            {
                randomAttack1 = UnityEngine.Random.Range(3,6);
            } while (randomAttack1 == randomAttack0);
            StartCoroutine(allenemyattacks.activateAttack(randomAttack0));
            StartCoroutine(allenemyattacks.activateAttack(randomAttack1));
            yield return new WaitForSeconds(8f);//reset to menu after seconds
            allenemyattacks.endAttack(randomAttack0);
            allenemyattacks.endAttack(randomAttack1);
        }
        else if (phases.phase2)//phase 3
        {
            
        }
        player.GetComponent<Player>().StopMoving();
        currentContext = Context.MainMenu;
        bingChilling.transform.GetChild(0).gameObject.SetActive(true);//let Jhon Cena speak
    }

    void UpdateMenu()
    {//update base on context
        for (int i = 0; i <= menuIndexMax; i++)
        {
            GameObject currentButton = buttons[i].instance;

            if (menuIndex == i)
            {
                currentButton.GetComponent<SpriteRenderer>().sprite = buttons[i].spriteActive;
                player.transform.position = new Vector2(
                    currentButton.transform.position.x - 0.38f,
                    currentButton.transform.position.y
                );
            }
            else
            {
                currentButton.GetComponent<SpriteRenderer>().sprite = buttons[i].spriteInactive;
            }
        }
    }
    private void UpdateMercyMenu()
    {
        if (Input.GetKeyDown("left"))
        {
            itemIndex.x--;
        }
        else if (Input.GetKeyDown("right"))
        {
            itemIndex.x++;
        }
        else if (Input.GetKeyDown("down"))
        {
            itemIndex.y++;
        }
        else if (Input.GetKeyDown("up"))
        {
            itemIndex.y--;
        }
        if (itemIndex.x < 0)//left
        {
            itemIndex.x = 0;
        }
        if (itemIndex.x > 1)//right
        {
            itemIndex.x = 1;
        }
        if (itemIndex.y < 0)//up
        {
            itemIndex.y = 0;
        }
        if (itemIndex.y > 0)//down
        {
            itemIndex.y = 0;
        }
        Vector3 pos;
        pos = spareMenu.GetComponent<Acts>().actsMenu[itemIndex.x + itemIndex.y].gameObject.transform.position;
        player.transform.position = new Vector2(pos.x - 0.12f, pos.y - 0.12f);
        if (Input.GetKeyDown("z") && spareMenu.GetComponent<Acts>().actText)
        {
            spareMenu.GetComponent<Acts>().actText = false;
            StartCoroutine(EnemyFight());
            player.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown("z"))
        {
            spareMenu.GetComponent<Acts>().showActText(itemIndex.x + itemIndex.y * 2);
            player.gameObject.SetActive(false);
        }
    }
    private void UpdateActMenu()
    {
        if (Input.GetKeyDown("left"))
        {
            itemIndex.x--;
        }
        else if (Input.GetKeyDown("right"))
        {
            itemIndex.x++;
        }
        else if (Input.GetKeyDown("down"))
        {
            itemIndex.y++;
        }
        else if (Input.GetKeyDown("up"))
        {
            itemIndex.y--;
        }
        if (itemIndex.x < 0)//left
        {
            itemIndex.x = 0;
        }
        if (itemIndex.x > 1)//right
        {
            itemIndex.x = 1;
        }
        if (itemIndex.y < 0)//up
        {
            itemIndex.y = 0;
        }
        if (itemIndex.y > 1)//down
        {
            itemIndex.y = 1;
        }
        Vector3 pos;
        pos = actMenu.GetComponent<Acts>().actsMenu[itemIndex.x + itemIndex.y * 2].gameObject.transform.position;
        player.transform.position = new Vector2(pos.x - 0.12f, pos.y - 0.12f);
        if (Input.GetKeyDown("z") && actMenu.GetComponent<Acts>().actText)//after text, enemy ettack
        {
            actMenu.GetComponent<Acts>().actText = false;
            StartCoroutine(EnemyFight());
            player.gameObject.SetActive(true);
        }
        else if (Input.GetKeyDown("z"))//show etxt of act
        {
            actMenu.GetComponent<Acts>().showActText(itemIndex.x + itemIndex.y * 2);
            player.gameObject.SetActive(false);
        }
    }
    private void UpdateItemMenu()
    {
        if (Input.GetKeyDown("left"))
        {
            itemIndex.x--;
        }
        else if (Input.GetKeyDown("right"))
        {
            itemIndex.x++;
        }
        else if (Input.GetKeyDown("down"))
        {
            itemIndex.y++;
        }
        else if (Input.GetKeyDown("up"))
        {
            itemIndex.y--;
        }
        if (itemIndex.x < 0)//left
        {
            itemMenu.GetComponent<Items>().changePage();
            itemIndex.x = 0;
        }
        if (itemIndex.x > 1)//right
        {
            itemMenu.GetComponent<Items>().changePage();
            itemIndex.x = 1;
        }
        if (itemIndex.y < 0)//up
        {
            itemIndex.y = 0;
        }
        if (itemIndex.y > 1)//down
        {
            itemIndex.y = 1;
        }
        Vector3 pos;
        pos = itemMenu.GetComponent<Items>().itemsMenu[itemIndex.x + itemIndex.y * 2].gameObject.transform.position;
        player.transform.position = new Vector2(pos.x - 0.12f, pos.y - 0.12f);
        if (Input.GetKeyDown("z"))
        {
            theItem itm = itemMenu.GetComponent<Items>().allItems[itemIndex.x + itemIndex.y * 2 + 4 * itemMenu.GetComponent<Items>().page];
            if (itm.name != " ")//delete item
            {
                player.GetComponent<Player>().heal(itm.HP);
                itemMenu.GetComponent<Items>().allItems[itemIndex.x + itemIndex.y * 2 + 4 * itemMenu.GetComponent<Items>().page].name = " ";
                StartCoroutine(EnemyFight());
            }
        }
    }

    void ResizeBattleArea(Vector2 size)
    {
        battleArea.transform.localScale = Vector2.Lerp(battleArea.transform.localScale, size, 5f * Time.deltaTime);
        if (battleAreaInitialScale.x.ToString().Substring(0, 4) == battleArea.transform.localScale.x.ToString().Substring(0, 4))
        {//can attack or use items only if the box is ready
            ready = true;
        }
        else
        {
            ready = false;
        }
    }
}
