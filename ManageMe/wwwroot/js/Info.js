function Info(containerId, message, status, lifespan) {
    let container = document.getElementById(containerId);
    let card = document.createElement("div");
    card.className = "notificationPopUp";
    let notificationTitle = document.createElement("h3");
    notificationTitle.innerHTML = status;
    notificationTitle.style.fontWeight = "bold";
    let notificationDetails = document.createElement("p");
    notificationDetails.innerHTML = message;
    card.appendChild(notificationTitle);
    card.appendChild(notificationDetails);
    container.appendChild(card);
    var notificationLifespan = setTimeout(() => { card.remove(); }, lifespan);
}