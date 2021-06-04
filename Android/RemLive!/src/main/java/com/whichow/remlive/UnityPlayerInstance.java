package com.whichow.remlive;

import android.app.Application;
import android.util.Log;

import com.unity3d.player.UnityPlayer;

public class UnityPlayerInstance {
    private static UnityPlayer mUnityPlayer;
    private static final String TAG = "UnityPlayerInstance";

    public static synchronized UnityPlayer getUnityPlayer() {
        if(mUnityPlayer == null) {
            mUnityPlayer = new UnityPlayer(LiveWallpaperApp.getContext());
            Log.d(TAG, "getUnityPlayer: Create new instance");
        }
        return mUnityPlayer;
    }
}
