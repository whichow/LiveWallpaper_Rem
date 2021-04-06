package com.whichow.remlive;

import android.preference.PreferenceManager;

import com.lostpolygon.unity.livewallpaper.UnityWallpaperService;

/**
 * Live Wallpaper Service. Override your stuff here.
 */
public class LiveWallpaperService extends UnityWallpaperService {
    @Override
    public void onCreate() {
        // Load default preferences values
        PreferenceManager.setDefaultValues(getApplicationContext(), R.xml.preferences, false);

        super.onCreate();
    }
}
