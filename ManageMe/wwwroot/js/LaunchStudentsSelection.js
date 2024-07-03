
function createMultiSelectStudents(data, modalId, groupId) {
    let modal = document.getElementById(modalId);
    //modal.removeEventListener("wheel", PreventScroll);
    modal.innerHTML = "";
    let selectedStudentsCount = 0;

    let title = document.createElement("h1");
    title.innerHTML = "Choose students to add to the group";
    modal.appendChild(title);

    let submitButton = document.createElement("button");
    submitButton.classList.add("btn");
    submitButton.classList.add("btn-success");
    submitButton.innerHTML = "Add Students";
    submitButton.onclick = () => getMultiSelectResultStudents(modal, groupId);
    modal.appendChild(submitButton);

    let cancelButton = document.createElement("button");
    cancelButton.innerHTML = "Cancel"
    $(cancelButton).addClass(["btn", "btn-light"]);
    cancelButton.addEventListener("click", function () {
        modal.style.display = "none";
        FadeBackground(false);
    });
    modal.appendChild(cancelButton);

    let numberOfOptions = data.length;
    for (let i = 0; i < numberOfOptions; i++) {
        let currentOption = data[i];

        let currentOptionDiv = document.createElement("div");
        currentOptionDiv.userId = currentOption.id;
        currentOptionDiv.classList.add("card-body");
        currentOptionDiv.classList.add("option");
        currentOptionDiv.classList.add("unselected");

        let currentOptionName = document.createElement("p");
        currentOptionName.innerHTML = currentOption.name;
        currentOptionDiv.appendChild(currentOptionName);

        let currentOptionEmail = document.createElement("p");
        currentOptionEmail.innerHTML = currentOption.email;
        currentOptionDiv.appendChild(currentOptionEmail);

        currentOptionDiv.IsSelected = 0;
        currentOptionDiv.onclick = function () {
            if (currentOptionDiv.IsSelected) {
                currentOptionDiv.IsSelected = 0;
                currentOptionDiv.classList.remove("selected");
                currentOptionDiv.classList.add("unselected");
                selectedStudentsCount--;
            }
            else {
                currentOptionDiv.IsSelected = 1;
                currentOptionDiv.classList.remove("unselected");
                currentOptionDiv.classList.add("selected");
                selectedStudentsCount++;
            }

            if (selectedStudentsCount == 0) {
                submitButton.innerHTML = "Add Students";
            }
            else {
                if (selectedStudentsCount == 1) {
                    submitButton.innerHTML = "Add 1 student";
                }
                else {
                    submitButton.innerHTML = `Add ${selectedStudentsCount} students`;
                }
            }
        }
        modal.appendChild(currentOptionDiv)
    }
}

function AddStudentsToGroup(groupId, studentIds) {
    var numberOfOptions = studentIds.length;
    var numberOfFailedItems = 0;

    var ajaxRequests = [];

    for (let i = 0; i < numberOfOptions; i++) {
        let studentId = studentIds[i];

        var request = $.ajax({
            type: "POST",
            url: "/Groups/AddStudentToGroup",
            data: {
                groupId: groupId,
                studentId: studentId
            },
            success: (result) => {
                if (result.success !== true) {
                    numberOfFailedItems++;
                }
            },
            error: () => {
                numberOfFailedItems++;
            }
        });
        ajaxRequests.push(request);
    }

    $.when.apply($, ajaxRequests).done(function () {
        $("#AddStudentsModal").css("display", "none");
        FadeBackground(false);
        localStorage.setItem("reloadPage", true);
        localStorage.setItem("failedAddsCount", numberOfFailedItems)
        localStorage.setItem("succsessAddsCount", numberOfOptions - numberOfFailedItems)

        location.reload();
    });
}


function getMultiSelectResultStudents(modal, groupId) {
    let selectedOptions = modal.getElementsByClassName("selected");
    let selectedOptionsIds = [];
    let numberOfSelectedOptions = selectedOptions.length;
    for (let i = 0; i < numberOfSelectedOptions; i++) {
        selectedOptionsIds.push(selectedOptions[i].userId);
    }

    AddStudentsToGroup(groupId, selectedOptionsIds);
}

function MultipleCheckStudents(groupId) {
    $("#AddStudentsModal").css("display", "flex");
    $.ajax({
        type: "GET",
        url: `/Users/GetAllAvailableStudents`,
        success: (result) => {
            createMultiSelectStudents(result, "AddStudentsModal", groupId);
        },
        error: () => {
            new ActionNotification("notificationsContainer", "Something went wrong!", "Error", 4000);
        }
    });

}

function LaunchStudentsSelection(groupId) {
    FadeBackground(true);
    new MultipleCheckStudents(groupId);
}
