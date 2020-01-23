///scr_CheckRooms
var Grid = mp_grid_create(0, 0, 120, 68, 16, 16);
mp_grid_add_instances(Grid, obj_WoodWall, false);
mp_grid_add_instances(Grid, obj_BrickWall, false);
mp_grid_add_instances(Grid, obj_Window, false);

for (var i = 0; i < instance_number(obj_Floor); i++)
{
    var FloorInstance = instance_find(obj_Floor, i);
    with (FloorInstance)
    {
        if !(collision_rectangle(x - 7, y - 7, x + 7, y + 7, obj_WoodWall, false, false)) and !(collision_rectangle(x - 7, y - 7, x + 7, y + 7, obj_BrickWall, false, false)) and !(collision_rectangle(x - 7, y - 7, x + 7, y + 7, obj_Window, false, false))
        {
            var Path = path_add();
            if !(mp_grid_path(Grid, Path, 0, 0, x, y, false))
            {
                mp_grid_destroy(Grid);
                return false;
            }
        }
    }
}

mp_grid_destroy(Grid);
return true;
