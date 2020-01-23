///scr_FindClosestZombie(lineofsight)
var LineOfSight = argument0;

if (LineOfSight)
{
    var MinZombie = noone;
    var MinDistance = 9999;
    for (var i = 0; i < instance_number(obj_Enemy); i++)
    {
        var ZombieInstance = instance_find(obj_Enemy, i);
        if (scr_FindCollisionLine(x, y, ZombieInstance.x, ZombieInstance.y))
        {
            var TempDistance = point_distance(x, y, ZombieInstance.x, ZombieInstance.y);
            if (TempDistance < MinDistance)
            {
                MinDistance = TempDistance;
                MinZombie = ZombieInstance;
            }
        }
    }
}
else
{
    var MinZombie = noone;
    var MinDistance = 9999;
    for (var i = 0; i < instance_number(obj_Enemy); i++)
    {
        var ZombieInstance = instance_find(obj_Enemy, i);
        var TempDistance = point_distance(x, y, ZombieInstance.x, ZombieInstance.y);
        if (TempDistance < MinDistance)
        {
            MinDistance = TempDistance;
            MinZombie = ZombieInstance;
        }
    }
}

return MinZombie;
