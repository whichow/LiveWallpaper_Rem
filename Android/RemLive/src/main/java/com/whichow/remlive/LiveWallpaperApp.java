package com.whichow.remlive;

import android.app.Application;
import android.content.Context;

public class LiveWallpaperApp extends Application {
    private static LiveWallpaperApp instance;

    public static LiveWallpaperApp getInstance() {
        return instance;
    }

    public static Context getContext(){
        return instance;
    }

    @Override
    public void onCreate() {
        instance = this;
        super.onCreate();
    }
}
