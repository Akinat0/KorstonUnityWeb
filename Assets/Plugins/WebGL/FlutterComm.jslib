mergeInto(LibraryManager.library, {
  SendToFlutter: function (message) {
    // Call Flutter's JavaScript channel
    if (window.Flutter) {
        window.Flutter.postMessage(UTF8ToString(message));
    }
  }
});