using UnityEngine;

public class Dice1 : MonoBehaviour
{
    Rigidbody rb;

    bool hasLanded;
    bool thrown;

    Vector3 initPosition;

    public GameObject gen;
    private int diceValue1;
    public int _diceValue1;
    public DiceSide[] diceSides;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initPosition = transform.position;
     //   rb.useGravity = false;
        AutoDicePush();
    }

    public void AutoDicePush()
    {
        if(GM.State == GM.GameState.Roll)
        {
            _diceValue1 = 0;
            RollDice();
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AutoDicePush();
        }
        if (rb.IsSleeping() && !hasLanded && thrown)
        {
            hasLanded = true;
           // rb.useGravity = false;
         //   rb.isKinematic = true;

            SideValueCheck();
        }
        else if (rb.IsSleeping() && hasLanded && diceValue1 == 0)
        {
            RollAgain();
        }
        {
            Generator _gen = gen.GetComponent<Generator>();
            if (_gen.moveDice == true)
            {
                _diceValue1 = 0;
                _gen.moveDice = false;
            }
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
            Torsion();
        }
    }
    void Torsion()
    {
        thrown = true;
       // rb.useGravity = true;
        rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
    }

    private void Reset()
    {
        transform.position = initPosition;
        thrown = false;
        hasLanded = false;
       // rb.useGravity = false;
        //rb.isKinematic = false;
    }
    void RollAgain()
    {
        Reset();
        Torsion();
    }

    void SideValueCheck()
    {
        diceValue1 = 0;
        foreach (DiceSide side in diceSides)
        {
            if (side.onGround())
            {
                diceValue1 = side.sideValue;
                _diceValue1 = diceValue1;
               // GM.State = GM.GameState.Turn;
              //  Debug.LogError(side.sideValue);
            }
        }
    }
}