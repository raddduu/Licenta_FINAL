function PopUp(status, message, callback, verb, popUpClass) {
    let card = document.createElement("div");
    card.className = popUpClass;
    let popUpTitle = document.createElement("h3");
    popUpTitle.innerHTML = status;
    popUpTitle.style.fontWeight = "bold";
    popUpTitle.style.color = "red";
    let popUpText = document.createElement("p");
    popUpText.innerHTML = message;
    popUpText.style.fontWeight = "bold";

    let confirmButton = document.createElement("a");
    $(confirmButton).addClass(["btn", "btn-danger"]);
    $(confirmButton).css("width", "10rem");
    confirmButton.innerHTML = verb;
    $(confirmButton).on("click", callback);

    let cancelButton = document.createElement("a");
    cancelButton.innerHTML = "Cancel"
    $(cancelButton).addClass(["btn", "btn-light"]);
    $(cancelButton).css("width", "10rem");
    cancelButton.addEventListener("click", function () {
        card.remove();
        FadeBackground(false);
    });

    card.appendChild(popUpTitle);
    card.appendChild(popUpText);

    let buttonsContainer = document.createElement("div");
    buttonsContainer.style.display = "inline-block";
    buttonsContainer.appendChild(confirmButton);
    buttonsContainer.appendChild(cancelButton);
    card.appendChild(buttonsContainer);
    document.body.appendChild(card);
}