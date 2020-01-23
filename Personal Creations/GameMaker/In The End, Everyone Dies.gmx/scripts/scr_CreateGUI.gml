///scr_CreateGUI()
global.MenuCreated = '';
instance_create(0, 0, obj_CameraController);
instance_create(0, 0, obj_ObjectSelection);

global.SurviveMenu = false;
global.MenuTabs = 8;
global.BuildMenus[global.MenuTabs - 1] = 0;

global.BuildMenus[0] = obj_BuildDestroy;
global.BuildMenus[1] = obj_BuildNothing;
global.BuildMenus[2] = obj_BuildFoundations;
global.BuildMenus[3] = obj_BuildWalls;
global.BuildMenus[4] = obj_BuildElectronics;
global.BuildMenus[5] = obj_BuildDecorations;
global.BuildMenus[6] = obj_BuildSurvival;
global.BuildMenus[7] = obj_StartSurviving;


for (var i = 0; i < global.MenuTabs; i++)
{
    var GUIInstance = instance_create(0, 0, global.BuildMenus[i]);
    with (GUIInstance)
    {
        Width = 64;
        Height = 64;
        YPos = 1012;
        Main = true;
        XPos = (i * 64) + 82 + (i * 150);
    }
}
