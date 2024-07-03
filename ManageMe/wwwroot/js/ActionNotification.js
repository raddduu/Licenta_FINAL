function ActionNotification(containerId, message, status, lifespan) {
    let container = document.getElementById(containerId);
    let card = document.createElement("div");
    card.classList.add("notificationPopUp");
    card.classList.add("move");
    if (status == "Error") {
        card.classList.add("fail");
    }
    else {
        card.classList.add("success");
    }
    let notificationTitle = document.createElement("h3");
    notificationTitle.innerHTML = status;
    notificationTitle.style.fontWeight = "bold";
    let notificationDetails = document.createElement("p");
    notificationDetails.innerHTML = message;
    card.appendChild(notificationTitle);
    card.appendChild(notificationDetails);
    container.appendChild(card);
    var fullDisplayLifespan = setTimeout(() => { card.classList.add('hide'); }, lifespan * 0.75);
    var notificationLifespan = setTimeout(() => { card.remove(); }, lifespan);
}