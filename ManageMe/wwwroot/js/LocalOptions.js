function LocalOptions(yCoordinate, xCoordinate, options, parent) {
    var optionsDiv =
        $("<div>").addClass("optionsDiv")
        .css({
            left: xCoordinate + "px",
            top: yCoordinate + "px",
        });

    optionsDiv.on("mouseover", function (event) {
        event.stopPropagation();
    });

    optionsDiv.mouseleave(function () {
        optionsDiv.remove();
    });

    var numberOfOptions = options.length;
    for (var i = 0; i < numberOfOptions; i++) {
        var currentOption = $(options[i]).addClass("local-option");

        currentOption.on("click", function () {
            optionsDiv.remove();
        });

        currentOption.on("mouseenter", function (event) {
            $(this).css({
                backgroundColor: "var(--secondary)",
                cursor: "pointer"
            });
        });
        currentOption.on("mouseleave", function () {
            $(this).css({
                backgroundColor: "var(--background)",
                cursor: "default"
            });
        });

        optionsDiv.append(currentOption);
    }

    $("body").append(optionsDiv);
}
