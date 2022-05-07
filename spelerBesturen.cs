using UnityEngine;

public class spelerBesturen : MonoBehaviour
{
    [SerializeField] private float SpelerSnelheid; // zorgt ervoor dat SpelerSnelheid aangepast kan worden in Unity
    private Rigidbody2D body; // RigidBody (oftewel hitbox) van het spelerskarakter
    private Animator animeren; //zorgt voor het besturen v/d animaties i.c.m Unity
    private bool OpDeGrond; //self-explanatory om eerlijk te zijn
    private void Awake() // wordt aangeroepen als game gestart wordt
    {
        body = GetComponent<Rigidbody2D>();
        animeren = GetComponent<Animator>();
    }
    private void Update() //spelloop
    {
        //float met waarde van x-as
        float LinksofRechts = Input.GetAxis("Horizontal");
        //gebruikt horizontale input voor bewegen karakter
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * SpelerSnelheid, body.velocity.y);

        //switch orientatie van sprite als deze naar links / rechts beweegt
        if (LinksofRechts > 0.01f)
            transform.localScale = Vector3.one;
        else if (LinksofRechts < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1); // flipt karakter
        //springen met spatiebalk
        if (Input.GetKey(KeyCode.Space) && OpDeGrond) //input en condities voor uitvoeren Springen()
            Springen();

        animeren.SetBool("lopen", LinksofRechts != 0);
        animeren.SetBool("OpDeGrond", OpDeGrond);
    }
   //activeert parameters voor springen en animatie daarvan
    private void Springen() // zorgt voor springen en animatie hiervoor
    {
        body.velocity = new Vector2(body.velocity.x, SpelerSnelheid);
        animeren.SetTrigger("springen");
        OpDeGrond = false;
    }
    
    //zorgt ervoor dat springanimatie niet op de grond uitgevoerd wordt
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Grond") // wel of niet op een object met tag "Grond" staand
            OpDeGrond = true;
    }
}
