function pageNext() {
    for (var i = 1; i < 4; i++) {
        var btn = $(".pagination > .page-item > .page-link > .btn" + i);
        btn.text(Number(btn.text()) + 1);
    }
    $("[aria-label=Previous]").show();
}

function pagePrev() {
    var prevBtn = $("[aria-label=Previous]");
    var firstBtn = $(".pagination > .page-item > .page-link > .btn1");

    if (Number(firstBtn.text()) === 2) {
        prevBtn.hide();
    }

    for (var i = 1; i < 4; i++) {
        var btn = $(".pagination > .page-item > .page-link > .btn" + i);
        btn.text(Number(btn.text()) - 1);
    }
}

function getPage(page) {
    var btnContent = $(".pagination > .page-item > .page-link > ." + page).text();

    $.ajax({
        type: "GET",
        url: "/Rotations/GetRotationsPage" + "?page=" + btnContent,
        success: function (response) {
            populateRotations(response);
        }
    });
}

var numberOfCardsInRow = 3;

function populateRotations(rotations) {
    clearGrid();
    var cardIndex = 0;
    if (rotations.length % numberOfCardsInRow === 0) {
        for (var i = 0; i < rotations.length / numberOfCardsInRow; i++) {
            addRow(i);
            for (var j = 0; j < numberOfCardsInRow; j++) {
                addCard(i, rotations[cardIndex]);
                cardIndex++;
            }
        }
    }
    else {
        var rowsFullOfCards = calculateCards(rotations);
        for (var i = 0; i < calculateRows(rotations); i++) {
            addRow(i);
            if (rowsFullOfCards !== 0) {
                for (var j = 0; j < numberOfCardsInRow; j++) {
                    addCard(i, rotations[cardIndex]);
                    cardIndex++;
                }
                rowsFullOfCards--;
            }
            else {
                var cardsLeft = rotations.length - calculateCards(rotations) * numberOfCardsInRow;
                for (var j = 0; j < cardsLeft; j++) {
                    addCard(i, rotations[cardIndex]);
                    cardIndex++;
                }
            }
        }
    }
}

function calculateRows(rotations) {
    var numberOfRows = rotations.length / numberOfCardsInRow;

    if (!(Number.isInteger(numberOfRows))) {
        numberOfRows = Number.parseInt(numberOfRows) + 1;
    }

    return numberOfRows;
}

function calculateCards(rotations) {
    var numberOfFullRows = Number.parseInt(rotations.length / numberOfCardsInRow);
    return numberOfFullRows;
}

function addCard(i, model) {
    var buttonId = 0;
    var cardDiv =
        '<div class="card">\
                <div class="card-body">\
                    <h5 class="card-title">' + "IGN " + model.Creator + '</h5>\
                    <p class="card-text">' + model.League + '</p>\
                    <p class="card-text">' + model.Type + '</p>\
                    <p class="card-text">' + "Spots left: " + model.Spots + '</p>\
                    <p class="card-text"><small class="text-muted">Created at ' + model.CreationTime + '</small></p>\
                    <button class="btn btn-default btn-primary" id="join-button-' + buttonId + '" onclick="joinRotation('+ buttonId +')"> Join </button>\
                    <p hidden id="rotation-id-' + buttonId + '">' + model.RotationId + "</p>\
                </div>\
            </div>";

    buttonId++;
    $("#rotation-row-" + i).append(cardDiv);
}

function addRow(i) {
    var rowDiv = '<div id="rotation-row-' + i + '" class="card-group mb-1">' +
        '</div>';

    $("#Rotations").append(rowDiv);
}

function clearGrid() {
    $("#Rotations").empty();
}

function joinRotation(id) {
    var rotationId = $("#rotation-id-" + id).text();
    var userId = "8DE0F706-10CB-4E49-9888-580EBD37DF7F";
    $.ajax({
        type: "POST",
        url: "/Rotations/JoinRotation?rotationId=" + rotationId + "&userId=" + userId,
        success: function (response) {
            console.log("Successfully joined rotation with id: " + rotationId);
        },
        error: function (response) {
            console.log(response);
        }
    });
}