using System.Collections.Generic;
using System;

public class Attack 
{
    public string Name;
    public int startup;
    public int active;
    public int recovery;
    public int hitStun;
    public int blockStun;
    public List<float> hitboxDimensions;

    public void TurnOnHitbox(Action<List<float>, int[]> Generate)
    {
        Generate(hitboxDimensions, new int[]{ active, hitStun, blockStun});
    }
}