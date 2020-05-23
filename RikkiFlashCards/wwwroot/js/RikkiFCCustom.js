var isTagOpened = false;

function addCodeTag(elementId) {
    var curElementVal = document.getElementById(elementId).value;
    var appendForwardNL = (curElementVal.length > 0) ? ("\r\n") : ("");
    document.getElementById(elementId).value += appendForwardNL + "<code>\r\n\r\n</code>\r\n";
}

function addCustomLinkTag(elementId) {
    var curElementVal = document.getElementById(elementId).value;
    var appendForwardNL = (curElementVal.length > 0) ? ("\r\n") : ("");
    document.getElementById(elementId).value += " [link][/link]";
}

function stripBRtoRN() {
    var codeBlocks = document.getElementsByName("codeBlock");
    codeBlocks.forEach(x => {
        x.innerHTML = x.innerHTML.replace(/<br>/g, "\r\n");
        x.innerHTML = escapeHtml(x.innerHTML);
    });

}

function escapeHtml(text) {
    alert('text input ' + text)
    var map = {
        '&': '&amp;',
        '<': '&lt;',
        '>': '&gt;',
        '"': '&quot;',
        "'": '&#039;'
    };

    //var replacedText = text.replace(/[&<>"']/g, function (m) { alert('found: ' + m); alert('replaced with: ' + map[m]); return map[m]; });
    var replacedText = text.replace(/[&<>"']/g, function (m) { return map[m]; });
    alert('text output ' + replacedText);
    return replacedText;
}