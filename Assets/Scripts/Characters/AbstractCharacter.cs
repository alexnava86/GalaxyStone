using UnityEngine;
using UnityEngine.UI;
using System; //Allows access to EventHandler class
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractCharacter : MonoBehaviour
{
    #region Variables
    public string[] defaultNames = new string[5];
    public List<Color32> mainColor = new List<Color32>();
    public List<Color32> subColor = new List<Color32>();
    public List<Color32> accentColor = new List<Color32>();
    public Texture2D ColorPalette { get; set; }
    public Color[] SpriteColors { get; set; }
    protected CSVReader data;

    public enum StatusEffect { Poisoned, Silenced, Sleeping, Charmed, Hypnotized, Blinded, Confused, Paralyzed };
    private enum MovementType { Forest, Desert, Mountain };// heat, may just use subclasses to define these?
    private enum AttackType { Bash, Slash }; // + bite?
    private enum Direction { North, East, South, West };
    private MovementType movement = MovementType.Forest;//maybe dont set initially, set in individual characters script
    private AttackType attackType = AttackType.Bash;
    private Direction direction = Direction.South;
    #endregion

    #region Properties
    public Player Operator { get; set; } //Player that controls this character, can change if character is charmed, etc...
    public Sprite CharImage { get; set; }
    public string CharName { get; set; }
    public int width { get; set; } // # of mapnodes wide ? may only be necessary on characters larger than 1x1...
    public int height { get; set; } //# of mapnodes tall ?
    public int Level { get; set; }
    public int Hp { get; set; }
    public int MaxHp { get; set; }
    public int Mp { get; set; }
    public int MaxMp { get; set; }
    public int PhysicalAttack { get; set; }
    public int PhysicalDefense { get; set; }
    public int MagicAttack { get; set; }
    public int MagicDefense { get; set; }
    public int Speed { get; set; } //Character's range...
    public int Agility { get; set; } //Character's turn order in battle, also helps evade attacks
    public int Luck { get; set; } //ability to avoid critical attacks (but not miss the attack), item discovery 
    public int Critical { get; set; }  //ability to inflict critical attacks
    public MapNode Node { get; set; }
    public Item[] Inventory { get; set; }
    public Skill[] Skills { get; set; }
    public Spell[] Spells { get; set; }
    public bool Hostile { get; set; }
    public bool Autonomous { get; set; }
    public List<MapNode> AttackRange { get; set; }
    public List<MapNode> MovementRange { get; set; }
    public List<StatusEffect> StatusEffects { get; set; } //enum?
    public virtual string Species // change this to string? maybe best
    {
        get
        {
            return this.GetType().ToString(); //Test this
        }
    }
    #endregion

    #region MonoBehaviour
    protected void Awake()
    {
        //DontDestroyOnLoad (this.gameObject);
    }
    protected void Start()
    {

    }
    #endregion

    #region Methods
    protected void SetLevelData(int Level)
    {
        data = this.GetComponent<CSVReader>();
        this.MaxHp = Convert.ToInt32(data.grid[Level, 1]);
        this.MaxMp = Convert.ToInt32(data.grid[Level, 2]);
        this.PhysicalAttack = Convert.ToInt32(data.grid[Level, 3]);
        this.PhysicalDefense = Convert.ToInt32(data.grid[Level, 4]);
        this.MagicAttack = Convert.ToInt32(data.grid[Level, 5]);
        this.MagicDefense = Convert.ToInt32(data.grid[Level, 6]);
        this.Speed = Convert.ToInt32(data.grid[Level, 7]);
        this.Agility = Convert.ToInt32(data.grid[Level, 8]);
        this.Luck = Convert.ToInt32(data.grid[Level, 9]);
        this.Critical = Convert.ToInt32(data.grid[Level, 10]);
    }
    //public abstract void DisplayCharacterInfo(); 
    //public abstract void HideCharacterInfo();
    //public abstract void SelectTargetAtXY();
    //public abstract void Die(); //virtual?

    public virtual void ExecuteActiveStatusEffects()
    {
        foreach (StatusEffect effect in this.StatusEffects)
        {
            Status.Effect(effect);
        }
    }
    public void SetAnim(string animName)
    {
        if (this.GetComponent<Animator>() != null)
        {
            Animator anim = this.GetComponent<Animator>();
            IEnumerable<string> state = from s in anim.parameters where s.name != animName select s.name;

            foreach (string s in state)
            {
                if (anim.GetBool(animName) != true)
                {

                    anim.SetBool(animName, true);
                }
                else
                {
                    anim.SetBool(s, false);
                }
            }

        }
    }
    public virtual void ConfirmLocation()
    {

    }
    #endregion

    #region Coroutines
    public virtual IEnumerator SetRange()
    {
        //Set Range of Movement
        //Set Range of Attack
        yield return null;
    }
    public virtual IEnumerator MoveToDestination()
    {
        yield return null;
    }
    #endregion

    #region Interfaces
    public interface IStatus
    {
        void Effect(StatusEffect effectName);//AbstractCharacter character)
        void Cure();
    }
    protected class Status
    {
        private static Dictionary<StatusEffect, IStatus> statusLibrary = new Dictionary<StatusEffect, IStatus>();
        //private static List<IStatus> statusIndex = new List<IStatus>();
        static Status()
        {
            statusLibrary.Add(StatusEffect.Poisoned, new Poisoned());
            statusLibrary.Add(StatusEffect.Silenced, new Silenced());
            statusLibrary.Add(StatusEffect.Sleeping, new Sleeping());
            statusLibrary.Add(StatusEffect.Charmed, new Charmed());
            statusLibrary.Add(StatusEffect.Hypnotized, new Hypnotized());
            statusLibrary.Add(StatusEffect.Blinded, new Blinded());
            statusLibrary.Add(StatusEffect.Confused, new Confused());
            statusLibrary.Add(StatusEffect.Paralyzed, new Paralyzed());
        }
        public static void Effect(StatusEffect effectName)
        {
            statusLibrary[effectName].Effect(effectName);
        }
        public static void Cure()
        {

        }
    }
    private class Poisoned : IStatus
    {
        public void Effect(StatusEffect effectName)
        {
            Debug.Log("Im friggin' poisoned! "); // wrks
                                                 //Determine the damage of poison effect here, display damage text
                                                 //Display any visual effects of poison to character
        }
        public void Cure()
        {

        }
    }
    private class Silenced : IStatus
    {
        public void Effect(StatusEffect effectName)
        {

        }
        public void Cure()
        {

        }
    }
    private class Sleeping : IStatus
    {
        public void Effect(StatusEffect effectName)
        {

        }
        public void Cure()
        {

        }
    }
    private class Charmed : IStatus
    {
        public void Effect(StatusEffect effectName)
        {

        }
        public void Cure()
        {

        }
    }
    private class Hypnotized : IStatus
    {
        public void Effect(StatusEffect effectName)
        {

        }
        public void Cure()
        {

        }
    }
    private class Blinded : IStatus
    {
        public void Effect(StatusEffect effectName)
        {

        }
        public void Cure()
        {

        }
    }
    private class Confused : IStatus
    {
        public void Effect(StatusEffect effectName)
        {

        }
        public void Cure()
        {

        }
    }
    private class Paralyzed : IStatus
    {
        public void Effect(StatusEffect effectName)
        {

        }
        public void Cure()
        {

        }
    }
    #endregion
}
