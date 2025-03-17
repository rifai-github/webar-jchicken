mergeInto(LibraryManager.library, {
    enableWakeLock: function () {
        if ("wakeLock" in navigator) {
            navigator.wakeLock.request("screen").then((lock) => {
                window.wakeLock = lock;
                console.log("✅ Wake Lock enabled!");
            }).catch((err) => {
                console.error("❌ Wake Lock failed:", err);
            });
        } else {
            console.warn("⚠️ Wake Lock API not supported!");
        }
    },

    disableWakeLock: function () {
        if (window.wakeLock) {
            window.wakeLock.release().then(() => {
                window.wakeLock = null;
                console.log("🛑 Wake Lock disabled");
            });
        }
    }
});
