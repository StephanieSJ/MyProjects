///scr_LoadOptions
if (file_exists('Save.sav'))
{
    ini_open('Save.sav');
    global.MusicVolume = ini_read_real('Options', 'MusicVolume', 1);
    global.SoundVolume = ini_read_real('Options', 'SoundVolume', 1);
    global.Fullscreen = ini_read_real('Options', 'Fullscreen', false);
    //global.Music = ini_read_real('Options', 'Music', 0);
    global.Music = 0;
    ini_close();
}
else
{
    global.MusicVolume = 1;
    global.SoundVolume = 1;
    global.Fullscreen = false;
    global.Music = 0;
}

audio_play_sound(snd_FeelingGood, 100, true);
audio_sound_gain(snd_FeelingGood, global.MusicVolume, 0);
instance_create(0, 0, obj_Music);
window_set_fullscreen(global.Fullscreen);
