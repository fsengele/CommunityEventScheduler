// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// =========================================
// COMMUNITY EVENT SCHEDULER - HOME PAGE
// Rotating community messages
// =========================================

document.addEventListener("DOMContentLoaded", function () {

    const messageElement = document.getElementById("communityMessage");

    // Run only when the message exists on the page.
    if (!messageElement) {
        return;
    }

    const messages = [
        "Celebrating Our Culture. Connecting Our People.",
        "One Community. One Calendar. Stronger Connections.",
        "Discover Events. Plan Together. Celebrate Together.",
        "Honoring Our Heritage. Building Our Future."
    ];

    // Respect reduced-motion settings.
    if (window.matchMedia("(prefers-reduced-motion: reduce)").matches) {
        return;
    }

    let currentMessage = 0;

    // Change the message every 5 seconds.
    setInterval(function () {

        // Fade out
        messageElement.classList.add("message-hidden");

        setTimeout(function () {

            // Move to the next message.
            currentMessage = (currentMessage + 1) % messages.length;

            // Update the text.
            messageElement.textContent = messages[currentMessage];

            // Fade in
            messageElement.classList.remove("message-hidden");

        }, 500);

    }, 5000);

});

