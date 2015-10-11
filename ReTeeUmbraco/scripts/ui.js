function changeLang() {
    switch($("html").prop("lang").toLowerCase()) {
        case "en-us":
            location.replace("http://" + window.location.host);
            break;
        case "fi-fi":
            location.replace("http://" + window.location.host + "/en");
            break;
        default:
            return false;
    }

    return false;
}