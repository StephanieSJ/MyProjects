///scr_LoudNoise(x, y)
var XPos = argument0;
var YPos = argument1;

with (obj_Enemy)
{
    if (State == scr_BreakWall)
    {
        var LineOfSight = scr_FindCollisionLine(x, y, XPos, YPos);
        if (LineOfSight)
        {
            State = scr_Walk;
            Noise = true;
            alarm[2] = 30;
        }
    }
}
