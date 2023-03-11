using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum PoolObject
    {
        Reticle,
        PredictReticle,
        OrderImage,
        StartSceneAI
    }

    public enum Scenes
    {
        None,
        Start,
        Day,
        Street,
        Shop,
        PizzaMake,
        Cost,
        Failure,
        Clear
    }

    public enum Shop
    {
        None,
        Hotdog,
        bowling,
        POLICE,
        GUITAR,
        COLA,
        CHINESE,
        HAMBURGER,
        SOJU,
        FRAPPE,
        CAFE,
        CANDY,
        POPCORN,
        PIZZA,
        COUNT
    }

    public enum Sound
    {
        Background,
        SFX,
    }

    private static GameObject player;

    public static GameObject Player
    {
        get
        {
            if (player == null)
                player = GameObject.FindGameObjectWithTag("Player");
            return player;
        }
    }

    public enum PizzaTopping
    {
        Cheese,
        Tomato,
        Pepperoni,
        Olive,
        Potato,
        Shrimp,
        Pineapple,
        SweetPotato,
        Count,
    };
}
