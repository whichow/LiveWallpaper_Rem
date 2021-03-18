package com.wh.rem;

import android.view.SurfaceView;

import com.lostpolygon.unity.livewallpaper.activities.LiveWallpaperCompatibleUnityPlayerActivity;

public class UnityPlayerActivity extends LiveWallpaperCompatibleUnityPlayerActivity {
    /**
     * Override this method to implement custom layout.
     * @return the {@code SurfaceView} that will contain the Unity player.
     */
    protected SurfaceView onCreateLayout() {
        return super.onCreateLayout();
    }

    /**
     * Determines the event at which the Unity player must be resumed.
     */
    protected UnityPlayerResumeEventType getUnityPlayerResumeEvent() {
        return UnityPlayerResumeEventType.OnActivityStart;
    }

    /**
     * Determines the event at which the Unity player must be paused.
     */
    protected UnityPlayerPauseEventType getUnityPlayerPauseEvent() {
        return UnityPlayerPauseEventType.OnActivityStop;
    }
}