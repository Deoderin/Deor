using UnityEngine;
public class Dice : MonoBehaviour
{
    Rigidbody rb;

    public bool hasLanded;
    bool thrown;

    public GameObject DiceRoll;
    public int i = 0;
    bool grab;
    public int diceValue;
    public DiceSide[] diceSides;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }
    void Update()
    {
        if (hasLanded == false)
        {
            diceValue = 0;
        }
        if (grab == true)
        {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x + 20 , Input.mousePosition.y, 5); // переменной записываються координаты мыши по иксу и игрику
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition); // переменной - объекту присваиваеться переменная с координатами мыши
        transform.position = objPosition; // и собственно объекту записываються координаты
        }
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    grab = true;
                    DiceRoll.transform.rotation = DiceRoll.transform.rotation * Quaternion.Euler(1, 1, 0);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    RollDice();
                }
            }
            grab = false;
        }
        if (rb.IsSleeping() && !hasLanded && thrown)
        {
            hasLanded = true;
            SideValueCheck();
        }
        else if (rb.IsSleeping() && hasLanded && diceValue == 0)
        {
            Debug.LogError("else");
            RollAgain();
        }
    }
    void RollDice()
    {
        if (!thrown && !hasLanded)
        {
            Torsion();
        }
        else if (thrown && hasLanded)
        {
            Reset();
        }
    }
    void Torsion()
    {
        thrown = true;
        rb.useGravity = true;
        rb.AddTorque(Random.Range(100, 5000), Random.Range(100, 5000), Random.Range(100, 5000));
    }

    private void Reset()
    {
        //transform.position = initPosition;
        thrown = false;
        hasLanded = false;
        rb.useGravity = false;
        rb.isKinematic = false;
    }
    void RollAgain()
    {
        Torsion();
    }

    void SideValueCheck()
    {
        diceValue = 0;
        foreach (DiceSide side in diceSides)
        {
            if (side.onGround())
            {
                diceValue = side.sideValue;
                i++;
                Reset();
            }
        }
    }
}