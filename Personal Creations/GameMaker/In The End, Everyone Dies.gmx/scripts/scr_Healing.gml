///scr_Healing
if !(Distracted)
{
    if (place_meeting(x, y, HealingStation))
    {
        if (obj_Resources.Medicine <= 0)
        {
            HealingStation.Used = false;
            HealingStation = noone;
            scr_GoBackToAssignedState();
            exit;
        }
        Counter++;
        if (Counter == 375)
        {
            Counter = 0;
            if (HealingStation.Assigned != 'NOONE')
            {
                Health += 25;
                if (HealingStation.Person.Profession == 'DOCTOR')
                {
                    Health += 25;
                }
            }
            Health += 25;
            obj_Resources.Medicine--;
            if (Health >= 100)
            {
                Health = 100;
                HealingStation.Used = false;
                HealingStation = noone;
                scr_GoBackToAssignedState();
            }
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
        if (HealingStation == noone)
        {
            HealingStation = scr_FindHealingStation();
        }
        if (HealingStation == noone)
        {
            scr_GoBackToAssignedState();
        }
        else
        {
            with (HealingStation)
            {
                Used = true;
            }
            StartX = HealingStation.x;
            StartY = HealingStation.y;
        }
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
