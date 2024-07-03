function ChangeRole(userId, roleName) {
    fetch(`Users/RoleChange?userId=${userId}&roleName=${roleName}`)
        .then(response => response.text())
        .then(resp => {
            if (resp == 'true') {
                new ActionNotification("notificationsContainer", `User's role has been changed to ${roleName}`, "Success", 4000);
            }

        });
}