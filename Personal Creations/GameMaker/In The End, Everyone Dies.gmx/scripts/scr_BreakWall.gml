///scr_BreakWall
var ObjectToDamage = scr_GetBlocker();
if (ObjectToDamage == noone)
{
    State = scr_Walk;
    exit;
}

var InstanceToDamage = instance_nearest(x, y, ObjectToDamage);
var Direction = point_direction(x, y, InstanceToDamage.x, InstanceToDamage.y);
if (Direction >= 45) and (Direction <= 135)
{
    sprite_index = spr_ZBreakWallUp;
}
else if (Direction > 135) and (Direction < 225)
{
    sprite_index = spr_ZBreakWallLeft;
}
else if (Direction >= 225) and (Direction <= 315)
{
    sprite_index = spr_ZBreakWallDown;
}
else
{
    sprite_index = spr_ZBreakWallRight;
}

Counter++;
if (Counter == 60)
{
    var Damage = true;
    Counter = 0;
}
else
{
    var Damage = false;
}

if (Damage)
{
    with (InstanceToDamage)
    {
        Health -= 10;
    }
}
