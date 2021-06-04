package com.whichow.remlive;

import android.service.wallpaper.WallpaperService;
import android.util.Log;
import android.view.SurfaceHolder;
import android.view.MotionEvent;

import com.unity3d.player.UnityPlayer;

public class LiveWallpaperService extends WallpaperService {

    private UnityPlayer mUnityPlayer;
    private static boolean mIsPreview = false;

    public static boolean isPreview() {
        return mIsPreview;
    }

    @Override
    public void onCreate () {
        super.onCreate();
        mIsPreview = true;
        mUnityPlayer = UnityPlayerInstance.getUnityPlayer();
    }

//    @Override
//    public void onDestroy () {
//        mUnityPlayer.quit();
//        super.onDestroy();
//    }

    @Override
    public Engine onCreateEngine() {
        return new WallpaperEngine();
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
            mUnityPlayer.displayChanged(0, holder.getSurface());
        }

        @Override
        public void onSurfaceChanged(SurfaceHolder holder, int format, int width, int height) {
            super.onSurfaceChanged(holder, format, width, height);
            mHolder = holder;
            mUnityPlayer.displayChanged(0, holder.getSurface());
        }

        @Override
        public void onVisibilityChanged(boolean visible) {
            Log.d(TAG, "onVisibilityChanged: " + visible);
            if(visible) {
                if (mHolder != null) {
                    mUnityPlayer.displayChanged(0, mHolder.getSurface());
                }
                mUnityPlayer.windowFocusChanged(true);
                mUnityPlayer.resume();
            } else {
//                mUnityPlayer.displayChanged(0, null);
//                mUnityPlayer.windowFocusChanged(false);
//                mUnityPlayer.pause();
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
            Log.d(TAG, "onDestroy: ");
        }

        @Override
        public void onTouchEvent(MotionEvent event) {
            super.onTouchEvent(event);
            Log.d(TAG, "onTouchEvent: ");
            mUnityPlayer.injectEvent(event);
        }
    }
}
