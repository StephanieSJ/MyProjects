///scr_UpdateSurvivorGrid
mp_grid_destroy(global.SurvivorGrid);
global.SurvivorGrid = mp_grid_create(128, 128, 102, 52, 16, 16);
mp_grid_add_instances(global.SurvivorGrid, obj_WoodWall, false);
mp_grid_add_instances(global.SurvivorGrid, obj_BrickWall, false);
mp_grid_add_instances(global.SurvivorGrid, obj_Window, false);
mp_grid_add_instances(global.SurvivorGrid, obj_PerimeterWall, false);
mp_grid_add_instances(global.SurvivorGrid, obj_PerimeterFence, false);
global.UpdateSurvivorGrid = false;
