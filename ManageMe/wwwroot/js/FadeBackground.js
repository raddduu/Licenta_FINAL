function PreventScroll(e) {
    e.preventDefault();
}


function FadeBackground(fade) {
    let overlay = $("#fadeOverlay");
    let body = document.body;
    if (fade) {
        overlay.removeClass("hidden");
        //body.addEventListener("wheel", PreventScroll, { passive: false });
    }

    else {
        overlay.addClass("hidden");
        //body.removeEventListener('wheel', PreventScroll);
    }
}