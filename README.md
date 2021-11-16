# AngryBirds


An angry bird like game - you need to push the enemies out of the screen!
For best experience, please play in full screen

The game features 3 levels, we needed to add several things to the project

1. Delete objects when they exit the scene

this is done by the script `DestroyInvisible.cs`

```csharp
    void OnBecameInvisible()
    {
        if(this.gameObject.tag != "Player")
        {
            LevelManagerScript.objects_left -= 1;
        }
        Destroy(gameObject);
    }
```

The level manager counts the numer of destructions in every level, and once the counter hits 0, it will move to the next scene.

Each level is slightly more difficult, with barriers added in the 3rd level and two different slingshots!

https://ariel-gamers.itch.io/angry-birds

