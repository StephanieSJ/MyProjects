///scr_Fighting
if !(Reloading)
{
    switch (Weapon)
    {
        case 'Cricket Bat':
        {
            var RangeType = 'melee';
            var AttackRange = 25;
            var Damage = 50;
            var ReloadTime = 30;
            break;
        }
        case 'Baseball Bat':
        {
            var RangeType = 'melee';
            var AttackRange = 25;
            var Damage = 50;
            var ReloadTime = 30;
            break;
        }
        case 'Hockey Stick':
        {
            var RangeType = 'melee';
            var AttackRange = 25;
            var Damage = 50;
            var ReloadTime = 30;
            break;
        }
        case 'Tennis Racket':
        {
            var RangeType = 'melee';
            var AttackRange = 25;
            var Damage = 50;
            var ReloadTime = 30;
            break;
        }
        case 'Golf Club':
        {
            var RangeType = 'melee';
            var AttackRange = 25;
            var Damage = 50;
            var ReloadTime = 30;
            break;
        }
    
        case 'Assault Rifle':
        {
            var RangeType = 'ranged';
            if (obj_Resources.Ammo <= 0)
            {
                var AttackRange = 25;
            }
            else
            {
                var AttackRange = 1000;
            }
            var Damage = 20;
            var ReloadTime = 15;
            var Spread = 5;
            var BSpeed = 5;
            break;
        }
        case 'Shotgun':
        {
            var RangeType = 'ranged';
            if (obj_Resources.Ammo <= 0)
            {
                var AttackRange = 25;
            }
            else
            {
                var AttackRange = 150;
            }
            var Damage = 50;
            var ReloadTime = 30;
            var Spread = 20;
            var BSpeed = 5;
            break;
        }
        case 'Handgun':
        {
            var RangeType = 'ranged';
            if (obj_Resources.Ammo <= 0)
            {
                var AttackRange = 25;
            }
            else
            {
                var AttackRange = 500;
            }
            var Damage = 100;
            var ReloadTime = 20;
            var Spread = 10;
            var BSpeed = 3;
            break;
        }
        case 'Sub-machine Gun':
        {
            var RangeType = 'ranged';
            if (obj_Resources.Ammo <= 0)
            {
                var AttackRange = 25;
            }
            else
            {
                var AttackRange = 500;
            }
            var Damage = 100;
            var ReloadTime = 10;
            var Spread = 10;
            var BSpeed = 5;
            break;
        }
        case 'Light-machine Gun':
        {
            var RangeType = 'ranged';
            if (obj_Resources.Ammo <= 0)
            {
                var AttackRange = 25;
            }
            else
            {
                var AttackRange = 500;
            }
            var Damage = 100;
            var ReloadTime = 5;
            var Spread = 15;
            var BSpeed = 5;
            break;
        }
    
        default:
        {
            var RangeType = 'melee';
            var AttackRange = 15;
            var Damage = 40;
            var ReloadTime = 40;
            break;
        }
    }

    var Zombie = scr_FindClosestZombie(true);
    if (obj_Resources.Ammo <= 0)
    {
        RangeType = 'melee';
        AttackRange = 15;
        Damage = 40;
        ReloadTime = 40;
    }

    if (Zombie == noone)
    {
        scr_GoBackToAssignedState();
    }
    else
    {
        var ZombieDistance = point_distance(x, y, Zombie.x, Zombie.y);
        if (ZombieDistance > AttackRange)
        {
            if !(PathCreated)
            {
                Path = path_add();
                var ValidPath = mp_grid_path(global.SurvivorGrid, Path, x, y, Zombie.x, Zombie.y, true);
                if (ValidPath)
                {
                    PathCreated = true;
                    path_start(Path, Speed, path_action_stop, true);
                }
            }
        }
        else
        {
            if (PathCreated)
            {
                path_end();
                if (path_exists(Path))
                {
                    path_delete(Path);
                }
                PathCreated = false;
            }
            
            if (RangeType == 'ranged') and (AttackRange > 15)
            {
                if (Weapon != 'Shotgun')
                {
                    var Direction = point_direction(x, y, Zombie.x, Zombie.y);
                    obj_Resources.Ammo--;
                    scr_Shoot(Spread, BSpeed, Direction, AttackRange, Damage, 'Normal');
                }
                else
                {
                    obj_Resources.Ammo--;
                    for (var i = 0; i < 5; i++)
                    {
                        var Direction = point_direction(x, y, Zombie.x, Zombie.y);
                        scr_Shoot(Spread, BSpeed, Direction, AttackRange, Damage, 'Normal');
                    }
                }
            }
            else
            {
                with (Zombie)
                {
                    Health -= Damage;
                }
            }
            Reloading = true;
            alarm[5] = ReloadTime;
        }
    }
}
