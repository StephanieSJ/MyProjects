///scr_Gardening
if !(Distracted)
{
    if (place_meeting(x, y, obj_Garden))
    {
        Counter++;
        if (Counter == 750)
        {
            Counter = 0;
            obj_Resources.Food += 5;
        }
    }
}

if (global.UpdateSurvivorGrid)
{
    PathCreated = false;
    StopMoving = true;
    if (path_exists(Path))
    {
        path_end();
        path_delete(Path);
    }
    alarm[1] = 2;
}

if (PathCreated)
{
    if (path_position == 1)
    {
        PathCreated = false;
        StopMoving = true;
        alarm[1] = (irandom(4) + 1) * 60;
        path_delete(Path);
    }
}
else if !(StopMoving)
{
    Path = path_add();
    while !(PathCreated)
    {
        var RandomInstance = instance_find(obj_Garden, irandom(instance_number(obj_Garden) - 1));
        StartX = RandomInstance.x;
        StartY = RandomInstance.y;
        var ValidPath = mp_grid_path(global.SurvivorGrid, Path, x, y, StartX, StartY, true);
        if (ValidPath)
        {
            PathCreated = true;
            path_start(Path, Speed, path_action_stop, true);
        }
    }
}