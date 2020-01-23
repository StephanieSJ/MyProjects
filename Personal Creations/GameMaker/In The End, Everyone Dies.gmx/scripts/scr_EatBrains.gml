///scr_EatBrains
if (path_exists(MyPath))
{
    path_end();
    path_delete(MyPath);
}
OnPath = false;

var ClosestSurvivor = instance_nearest(x, y, obj_ParentSurvivor);
if (point_distance(x, y, ClosestSurvivor.x, ClosestSurvivor.y) > 20)
{
    State = scr_Walk;
}
else
{
    var Direction = point_direction(x, y, ClosestSurvivor.x, ClosestSurvivor.y);
    if (Stunned)
    {
        if (Direction >= 45) and (Direction <= 135)
        {
            sprite_index = spr_ZStunUp;
        }
        else if (Direction > 135) and (Direction < 225)
        {
            sprite_index = spr_ZStunLeft;
        }
        else if (Direction >= 225) and (Direction <= 315)
        {
            sprite_index = spr_ZStunDown;
        }
        else
        {
            sprite_index = spr_ZStunRight;
        }
    }
    else
    {
        if (Direction >= 45) and (Direction <= 135)
        {
            sprite_index = spr_ZEatBrainsUp;
        }
        else if (Direction > 135) and (Direction < 225)
        {
            sprite_index = spr_ZEatBrainsLeft;
        }
        else if (Direction >= 225) and (Direction <= 315)
        {
            sprite_index = spr_ZEatBrainsDown;
        }
        else
        {
            sprite_index = spr_ZEatBrainsRight;
        }
    }
}
