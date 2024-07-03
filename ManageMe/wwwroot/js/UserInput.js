function UserInput(scope, message, popUpClass, channelId) {
    let card = document.createElement("div");
    let buttonsContainer = document.createElement("div");

    card.className = popUpClass;
    let popUpTitle = document.createElement("h3");
    popUpTitle.innerHTML = scope;
    popUpTitle.style.fontWeight = "bold";
    let popUpText = document.createElement("p");
    popUpText.innerHTML = message;
    popUpText.style.fontWeight = "bold";

    let inputArea = document.createElement("input");
    inputArea.type = "text";
    inputArea.placeholder = "Enter the join code here";

    let incorrectJoinCodeMessage = document.createElement("p");
    incorrectJoinCodeMessage.innerHTML = "Incorrect join code!";
    incorrectJoinCodeMessage.style.color = "red";
    incorrectJoinCodeMessage.style.display = "none";

    let submitButton = document.createElement("a");
    $(submitButton).addClass(["btn", "btn-success"]);
    submitButton.innerHTML = "Submit";
    $(submitButton).on("click", function () {
        let userInput = card.getElementsByTagName("input")[0].value;
        fetch(`/Channels/AddNewMember?channelId=${channelId}&userJoinCode=${userInput}`, {
            method: "get"
        })
            .then(response => response.text())
            .then(resp => {
                if (resp === 'false') {
                    incorrectJoinCodeMessage.style.display = "";
                }
                else {
                    card.remove();
                    FadeBackground(false);

                    ReplaceJoinButtonWithLeave(channelId);

                    new ActionNotification("notificationsContainer", "You have succsessfully joined this group!", "Join", 4000);
                }
            });
      
        
    });

    let cancelButton = document.createElement("a");
    cancelButton.innerHTML = "Cancel"
    $(cancelButton).addClass(["btn", "btn-light"]);
    cancelButton.addEventListener("click", function () {
        card.remove();
        FadeBackground(false);
    });

    $(cancelButton).css("width", "10rem");
    $(submitButton).css("width", "10rem");
    buttonsContainer.style.display = "inline-block";
    buttonsContainer.appendChild(submitButton);
    buttonsContainer.appendChild(cancelButton);

    card.appendChild(popUpTitle);
    card.appendChild(popUpText);
    card.appendChild(inputArea);
    card.appendChild(incorrectJoinCodeMessage);
    card.appendChild(buttonsContainer);
    document.body.appendChild(card);
}