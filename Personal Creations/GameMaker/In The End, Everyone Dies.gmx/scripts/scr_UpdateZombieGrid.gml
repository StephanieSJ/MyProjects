///scr_UpdateZombieGrid
mp_grid_destroy(global.ZombieGrid);
global.ZombieGrid = mp_grid_create(128, 128, 102, 52, 16, 16);
mp_grid_add_instances(global.ZombieGrid, obj_WoodWall, false);
mp_grid_add_instances(global.ZombieGrid, obj_BrickWall, false);
mp_grid_add_instances(global.ZombieGrid, obj_Window, false);
mp_grid_add_instances(global.ZombieGrid, obj_PerimeterWall, false);
mp_grid_add_instances(global.ZombieGrid, obj_PerimeterFence, false);
mp_grid_add_instances(global.ZombieGrid, obj_WoodDoor, false);
mp_grid_add_instances(global.ZombieGrid, obj_IronDoor, false);
mp_grid_add_instances(global.ZombieGrid, obj_FenceGate, false);
mp_grid_add_instances(global.ZombieGrid, obj_IronGate, false);
global.UpdateZombieGrid = false;
