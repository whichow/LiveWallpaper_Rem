package com.whichow.remlive;

import android.app.Service;
import android.app.WallpaperManager;
import android.content.ComponentName;
import android.content.Intent;
import android.service.wallpaper.WallpaperService;

public class LiveWallpaperManager {
    private static WallpaperService.Engine mWallpaperEngine;

    public static void previewWallpaper() {
        final Intent intent = new Intent(WallpaperManager.ACTION_CHANGE_LIVE_WALLPAPER);
        intent.setFlags(Intent.FLAG_ACTIVITY_NEW_TASK );
        intent.putExtra(WallpaperManager.EXTRA_LIVE_WALLPAPER_COMPONENT, new ComponentName(LiveWallpaperApp.getContext(), LiveWallpaperService.class));
        LiveWallpaperApp.getContext().startActivity(intent);
    }

    public static void setEngine(WallpaperService.Engine engine) {
        mWallpaperEngine = engine;
    }

    public static boolean isPreview() {
        if(mWallpaperEngine != null) {
            return mWallpaperEngine.isPreview();
        }
        return false;
    }

    public static boolean isVisiable() {
        if(mWallpaperEngine != null) {
            return mWallpaperEngine.isVisible();
        }
        return false;
    }
}
