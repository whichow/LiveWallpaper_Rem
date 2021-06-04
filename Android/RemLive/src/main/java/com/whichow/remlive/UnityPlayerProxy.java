package com.whichow.remlive;

public class UnityPlayerProxy {
    public interface ActivityActiveListener {
        void onActivityActive(boolean active);
    }

    private static ActivityActiveListener activityActiveListener;
    private static boolean mActive;

    public static void setActivityActiveListener(ActivityActiveListener listener) {
        activityActiveListener = listener;
        notifyActivityActive(mActive);
    }

    public static void notifyActivityActive(boolean active) {
        mActive = active;
        if(activityActiveListener != null) {
            activityActiveListener.onActivityActive(active);
        }
    }
}
