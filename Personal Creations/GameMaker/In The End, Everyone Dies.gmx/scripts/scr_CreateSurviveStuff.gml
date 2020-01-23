///scr_CreateSurviveStuff
instance_create(0, 0, obj_EnemyController);
instance_create(0, 0, obj_SurvivalButtons);
instance_create(0, 0, obj_JobManager);
instance_create(0, 0, obj_Weapons);
instance_create(0, 0, obj_LightController);
global.SurvivorSelected = noone;
global.ElectronicSelected = noone;
global.DragHover = false;
global.BackHover = false;
global.SelectionHover = false;
global.TimeHover = false;
global.ScrollBarHover = false;
global.WeaponHover = false;
global.HealHover = false;

Professions[11] = 0;

Professions[0] = 'CHEF';
Professions[1] = 'MILITARY';
Professions[2] = 'DOCTOR';
Professions[3] = 'GARDENER';
Professions[4] = 'JANITOR';
Professions[5] = 'SNIPER';
Professions[6] = 'MAID';
Professions[7] = 'MARTIAL ARTIST';
Professions[8] = 'HUNTER';
Professions[9] = 'ENGINEER';
Professions[10] = 'THIEF';
Professions[11] = 'SURVIVALIST';
Professions = scr_RandomizeArray(Professions);

global.UpdateSurvivorGrid = false;
global.SurvivorGrid = mp_grid_create(128, 128, 102, 52, 16, 16);
mp_grid_add_instances(global.SurvivorGrid, obj_WoodWall, false);
mp_grid_add_instances(global.SurvivorGrid, obj_BrickWall, false);
mp_grid_add_instances(global.SurvivorGrid, obj_Window, false);
mp_grid_add_instances(global.SurvivorGrid, obj_PerimeterWall, false);
mp_grid_add_instances(global.SurvivorGrid, obj_PerimeterFence, false);

global.UpdateZombieGrid = false;
global.ZombieGrid = mp_grid_create(128, 128, 102, 52, 16, 16);
mp_grid_add_instances(global.ZombieGrid, obj_WoodWall, false);
mp_grid_add_instances(global.ZombieGrid, obj_BrickWall, false);
mp_grid_add_instances(global.ZombieGrid, obj_Window, false);
mp_grid_add_instances(global.ZombieGrid, obj_PerimeterWall, false);
mp_grid_add_instances(global.ZombieGrid, obj_PerimeterFence, false);
mp_grid_add_instances(global.ZombieGrid, obj_WoodDoor, false);
mp_grid_add_instances(global.ZombieGrid, obj_IronDoor, false);
mp_grid_add_instances(global.ZombieGrid, obj_IronGate, false);
mp_grid_add_instances(global.ZombieGrid, obj_FenceGate, false);

var Depth = -11;
global.SurvivorInstances[global.Survivors - 1] = 0;
for (var i = 0; i < global.Survivors; i++)
{
    var ValidSpawn = false;
    var XSpawn = room_width / 2;
    var YSpawn = room_height / 2;
    while !(ValidSpawn)
    {
        if (instance_exists(obj_Floor))
        {
            SpawnInstance = instance_find(obj_Floor, irandom(instance_number(obj_Floor) - 1));
            if !(collision_rectangle(SpawnInstance.x - 7, SpawnInstance.y - 7, SpawnInstance.x + 7, SpawnInstance.y + 7, obj_ParentWall, false, true))
            {
                XSpawn = SpawnInstance.x;
                YSpawn = SpawnInstance.y;
                ValidSpawn = true;
            }
        }
        else
        {
            ValidSpawn = true;
        }
    }
    var Survivor = instance_create(XSpawn, YSpawn, obj_ParentSurvivor);
    with (Survivor)
    {
        Profession = other.Professions[i];
        depth = Depth;
        Name = Profession;
    }
    global.SurvivorInstances[i] = Survivor;
}

with (obj_Resources)
{
    BuildingMaterial = round(0.3 * Wood) + round(0.3 * Stone) + round(0.3 * Shops);
    Ammo = Guns;
    alarm[0] = 18000;
    alarm[1] = 750;
}
