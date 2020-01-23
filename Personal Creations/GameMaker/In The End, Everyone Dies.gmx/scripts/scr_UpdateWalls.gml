///scr_UpdateWalls(foundations, count)
var FoundationsToCheck = argument0;
var Num = argument1;

for (var i = 0; i < Num; i++)
{
    var Instance = FoundationsToCheck[0];
    with (Instance)
    {
        if !(collision_rectangle(x - 16, y + 1, x - 2, y + 15, obj_ParentFoundation, false, false))
        {
            instance_create(x, y, obj_WoodWall);
        }
        else if !(collision_rectangle(x + 1, y - 16, x + 15, y - 2, obj_ParentFoundation, false, false))
        {
            instance_create(x, y, obj_WoodWall);
        }
        else if !(collision_rectangle(x + 17, y + 1, x + 30, y + 15, obj_ParentFoundation, false, false))
        {
            instance_create(x, y, obj_WoodWall);
        }
        else if !(collision_rectangle(x + 1, y + 17, x + 15, y + 30, obj_ParentFoundation, false, false))
        {
            instance_create(x, y, obj_WoodWall);
        }
        else
        {
            if (collision_rectangle(x + 1, y + 1, x + 15, y + 15, obj_ParentWall, false, false))
            {
                with (instance_nearest(x, y, obj_WoodWall))
                {
                    instance_destroy();
                }
            }
        }
    }
}
