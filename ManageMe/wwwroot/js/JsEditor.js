var joinButtons = document.getElementsByClassName("joinButton")
for (int i = 0; i < joinButtons.length; i++) {
    joinButtons[i].addEventListener(onclick = HandleJoin(joinButtons[i].id, joinButtons[i].accessType, joinButtons[i].userId))
}