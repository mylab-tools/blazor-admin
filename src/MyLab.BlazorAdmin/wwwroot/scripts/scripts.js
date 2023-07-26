window.getElementHtmlText = (elementId) => {
    await setTimeout(() => { }, 1);
    var element = document.getElementById(elementId);
    return element.innerHTML;
};