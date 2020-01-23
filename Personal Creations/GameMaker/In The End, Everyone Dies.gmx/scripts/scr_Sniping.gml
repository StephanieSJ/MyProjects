///scr_Sniping
if (obj_Resources.Ammo == 0)
{
    with (AssignedObject)
    {
        Person = noone;
        Assigned = 'NOONE';
    }
    Assignment = 'Nothing';
    AssignedObject = noone;
    State = scr_SIdle;
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

if !(Distracted)
{
    if (place_meeting(x, y, obj_SniperNest))
    {
        if !(Shooting) and !(Reloading)
        {
            Counter++;
            if (Counter == 15)
            {
                Counter = 0;
                SniperInstance = scr_FindClosestZombie(false);
                if (SniperInstance != noone)
                {
                    if (point_distance(x, y, SniperInstance.x, SniperInstance.y) <= 1000)
                    {
                        Shooting = true;
                    }
                }
            }
        }
        else if !(Reloading)
        {
            if (instance_exists(SniperInstance))
            {
                obj_Resources.Ammo--;
                scr_Shoot(0, 10, point_direction(x, y, SniperInstance.x, SniperInstance.y), 1000, 100, 'Sniper');
                Reloading = true;
                alarm[5] = 120;
            }
            else
            {
                Shooting = false;
                Counter = 0;
            }
        }
    }
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
        StartX = AssignedObject.x;
        StartY = AssignedObject.y;
        var RandomX = 0;
        var RandomY = 0;
        var ValidPath = mp_grid_path(global.SurvivorGrid, Path, x, y, StartX + RandomX, StartY + RandomY, true);
        if (ValidPath)
        {
            PathCreated = true;
            path_start(Path, Speed, path_action_stop, true);
        }
    }
}
