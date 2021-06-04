package com.whichow.remlive;

import android.app.Activity;
import android.app.Application;
import android.content.Context;
import android.content.res.Configuration;
import android.util.Log;
import android.view.InputEvent;
import android.view.Surface;
import android.view.ViewGroup;

import com.unity3d.player.UnityPlayer;

public class UnityPlayerInstance {
    private static UnityPlayerInstance mInstance;
    private UnityPlayer mUnityPlayer;
//    private boolean mIsInView = false;
    private boolean mIsActivityActive = false;
    private static final String TAG = "UnityPlayerInstance";

    public static synchronized UnityPlayerInstance getInstance() {
        if(mInstance == null) {
            mInstance = new UnityPlayerInstance();
            mInstance.mUnityPlayer = new UnityPlayer(LiveWallpaperApp.getContext());
            Log.d(TAG, "getUnityPlayer: Create new instance");
        }
        return mInstance;
    }

    public UnityPlayer getUnityPlayer() {
        return mUnityPlayer;
    }

    public void setView(Activity activity) {
        Log.d(TAG, "setView: ");
        activity.setContentView(mUnityPlayer);
//        mIsInView = true;
    }

    public void removeView() {
        Log.d(TAG, "removeView: ");
        ((ViewGroup)mUnityPlayer.getParent()).removeView(mUnityPlayer);
//        mIsInView = false;
    }

    public void setActivityActive(boolean active) {
        mIsActivityActive = active;
    }

//    public void startPlayer() {
//        mUnityPlayer.start();
//    }

    public void pausePlayer() {
        Log.d(TAG, "pausePlayer: ");
        if(!LiveWallpaperManager.isVisiable() && !mIsActivityActive) {
            Log.d(TAG, "pause: ");
            mUnityPlayer.pause();
            mUnityPlayer.windowFocusChanged(false);
        }
    }

    public void resumePlayer() {
        Log.d(TAG, "resumePlayer: ");
        Log.d(TAG, "resume: ");
        mUnityPlayer.resume();
        mUnityPlayer.windowFocusChanged(true);
    }

//    public void stopPlayer() {
//        mUnityPlayer.stop();
//    }

    public void destroyPlayer() {
        Log.d(TAG, "destroyPlayer: ");
        if(!LiveWallpaperManager.isPreview() && !LiveWallpaperManager.isVisiable() && !mIsActivityActive) {
            Log.d(TAG, "destroy: ");
            mUnityPlayer.destroy();
        }
    }

    public void displayChanged(int i, Surface surface) {
        Log.d(TAG, "displayChanged: ");
        mUnityPlayer.displayChanged(i, surface);
    }

    public void configurationChanged(Configuration configuration) {
        Log.d(TAG, "configurationChanged: ");
        mUnityPlayer.configurationChanged( configuration);
    }

//    public void windowFocusChanged(boolean focus) {
//        Log.d(TAG, "windowFocusChanged: " + focus);
//        mUnityPlayer.windowFocusChanged(focus);
//    }

    public boolean injectEvent(InputEvent inputEvent) {
        return mUnityPlayer.injectEvent(inputEvent);
    }
}
