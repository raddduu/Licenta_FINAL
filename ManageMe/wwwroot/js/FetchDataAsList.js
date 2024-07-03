function FetchDataAsList(selectId, httpGetAction) {
    var selectRegion = document.getElementById(selectId);

    fetch(`/${httpGetAction}`, {
        method: "get"
    }
    )
        .then(response => response.text())
        .then(resp => {
            selectRegion.innerHTML = resp;
        });
}