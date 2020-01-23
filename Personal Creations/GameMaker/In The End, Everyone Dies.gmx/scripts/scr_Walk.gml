///scr_Walk
if (global.UpdateZombieGrid)
{
    OnPath = false;
    StopMoving = true;
    alarm[0] = 3;
}

MinSurvivor = noone;

if (OnPath)
{
    UpdatePath++;
    if (UpdatePath == 10)
    {
        UpdatePath = 0;
        var MinDistance = 9999;
        var MinSurvivor = noone;
        for (var i = 0; i < instance_number(obj_ParentSurvivor); i++)
        {
            var SurvivorInstance = instance_find(obj_ParentSurvivor, i);
            if (SurvivorInstance.State != scr_Scavenge)
            {
                var InSight = scr_FindCollisionLine(x, y, SurvivorInstance.x, SurvivorInstance.y);
                if (InSight)
                {
                    var Distance = point_distance(x, y, SurvivorInstance.x, SurvivorInstance.y);
                    if (Distance < MinDistance)
                    {
                        MinDistance = Distance;
                        MinSurvivor = SurvivorInstance;
                    }
                }
            }
        }
        
        if (MinSurvivor != noone)
        {
            if (MinDistance <= 25)
            {
                with (MinSurvivor)
                {
                    Counter = 0;
                    State = scr_GettingEaten;
                }
                mp_linear_step(x, y, Speed, false);
                State = scr_EatBrains;
                exit;
            }
            if (path_exists(MyPath))
            {
                path_end();
                path_delete(MyPath);
            }
            MyPath = path_add();
            if (mp_grid_path(global.ZombieGrid, MyPath, x, y, MinSurvivor.x, MinSurvivor.y, true))
            {
                path_start(MyPath, Speed, path_action_stop, true);
            }
            else
            {
                path_delete(MyPath);
                OnPath = false;
            }
        }
        else
        {
            if (path_position == 1)
            {
                path_delete(MyPath);
                OnPath = false;
            }
        }
    }
}
else if !(StopMoving)
{
    var MinDistance = 9999;
    var MinInsightDistance = 9999;
    var MinSurvivor = noone;
    var MinInsightSurvivor = noone;
    for (var i = 0; i < instance_number(obj_ParentSurvivor); i++)
    {
        var SurvivorInstance = instance_find(obj_ParentSurvivor, i);
        if (SurvivorInstance.State != scr_Scavenge)
        {
            var InSight = scr_FindCollisionLine(x, y, SurvivorInstance.x, SurvivorInstance.y);
            if (InSight)
            {
                var Distance = point_distance(x, y, SurvivorInstance.x, SurvivorInstance.y);
                if (Distance < MinInsightDistance)
                {
                    MinInsightDistance = Distance;
                    MinInsightSurvivor = SurvivorInstance;
                }
            }
            else
            {
                var Distance = point_distance(x, y, SurvivorInstance.x, SurvivorInstance.y)
                if (Distance < MinDistance)
                {
                    MinDistance = Distance;
                    MinSurvivor = SurvivorInstance;
                }
            }
        }
    }
    
    if (MinInsightSurvivor != noone)
    {
        if (MinInsightDistance <= 25)
        {
            with (MinInsightSurvivor)
            {
                Counter = 0;
                State = scr_GettingEaten;
            }
            mp_linear_step(x, y, Speed, false);
            State = scr_EatBrains;
            exit;
        }
        OnPath = true;
        if (path_exists(MyPath))
        {
            path_end();
            path_delete(MyPath);
        }
        MyPath = path_add();
        if (mp_grid_path(global.ZombieGrid, MyPath, x, y, MinInsightSurvivor.x, MinInsightSurvivor.y, true))
        {
            path_start(MyPath, Speed, path_action_stop, true);
            UpdatePath = 0;
        }
    }
    else
    {
        if (MinSurvivor != noone)
        {
            mp_linear_step(MinSurvivor.x, MinSurvivor.y, Speed, false);
        }
    }
}

if (OnPath)
{
    var Direction = point_direction(x, y, path_get_x(MyPath, 1), path_get_y(MyPath, 1));
    if (Direction > 90) and (Direction < 270)
    {
        sprite_index = spr_ZWalkLeft;
    }
    else
    {
        sprite_index = spr_ZWalkRight;
    }
}
else if (MinSurvivor != noone)
{
    var Direction = point_direction(x, y, MinSurvivor.x, MinSurvivor.y);
    if (Direction > 90) and (Direction < 270)
    {
        sprite_index = spr_ZWalkLeft;
    }
    else
    {
        sprite_index = spr_ZWalkRight;
    }
}
