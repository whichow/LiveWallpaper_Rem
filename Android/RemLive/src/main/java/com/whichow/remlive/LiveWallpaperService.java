package com.whichow.remlive;

import android.service.wallpaper.WallpaperService;
import android.util.Log;
import android.view.SurfaceHolder;
import android.view.MotionEvent;

import com.unity3d.player.UnityPlayer;

public class LiveWallpaperService extends WallpaperService {

    private UnityPlayerInstance unityPlayerInstance;
    private Engine mEngine;

    @Override
    public void onCreate () {
        super.onCreate();
        unityPlayerInstance = UnityPlayerInstance.getInstance();
    }

    @Override
    public void onDestroy () {
//        mUnityPlayer.quit();
        super.onDestroy();
    }

    @Override
    public Engine onCreateEngine() {
        mEngine = new WallpaperEngine();
        LiveWallpaperManager.setEngine(mEngine);
        return mEngine;
    }

    class WallpaperEngine extends Engine {
        private static final String TAG = "WallpaperEngine";
        private SurfaceHolder mHolder;

        @Override
        public void onCreate(SurfaceHolder surfaceHolder) {
            super.onCreate(surfaceHolder);
            Log.d(TAG, "onCreate: ");
        }

        @Override
        public void onSurfaceCreated(SurfaceHolder holder) {
            super.onSurfaceCreated(holder);
            Log.d(TAG, "onSurfaceCreated: ");
            mHolder = holder;
            unityPlayerInstance.displayChanged(0, holder.getSurface());
        }

        @Override
        public void onSurfaceChanged(SurfaceHolder holder, int format, int width, int height) {
            super.onSurfaceChanged(holder, format, width, height);
            mHolder = holder;
            unityPlayerInstance.displayChanged(0, holder.getSurface());
        }

        @Override
        public void onVisibilityChanged(boolean visible) {
            Log.d(TAG, "onVisibilityChanged: " + visible);
            if(visible) {
                if (mHolder != null) {
                    unityPlayerInstance.displayChanged(0, mHolder.getSurface());
                }
//                unityPlayerInstance.windowFocusChanged(true);
                unityPlayerInstance.resumePlayer();
            } else {
//                unityPlayerInstance.displayChanged(0, null);
//                unityPlayerInstance.windowFocusChanged(false);
                unityPlayerInstance.pausePlayer();
            }
        }

        @Override
        public void onSurfaceDestroyed(SurfaceHolder holder) {
            super.onSurfaceDestroyed(holder);
            Log.d(TAG, "onSurfaceDestroyed: ");
        }

        @Override
        public void onDestroy() {
            super.onDestroy();
            unityPlayerInstance.destroyPlayer();
            Log.d(TAG, "onDestroy: ");
        }

        @Override
        public void onTouchEvent(MotionEvent event) {
            super.onTouchEvent(event);
            Log.d(TAG, "onTouchEvent: ");
            unityPlayerInstance.injectEvent(event);
        }
    }
}
