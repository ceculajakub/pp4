const logNames = async () => {
    const response = await fetch("/api/names");
    const names = await response;
    return names;
}

(() => {
    const namesElement = document.querySelector("#names");
    logNames()
        .then(names => {
            namesElement.innerHTML = names.join(" | ");
        });
})();