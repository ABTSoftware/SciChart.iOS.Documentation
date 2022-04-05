function showCodeFor(evt, language) {
    let tabsContainier = $(evt.target).closest("div");
    let tabs = tabsContainier.children()
    let count = tabs.length;

    // Hide all code-snippets content
    tabsContainier.nextAll(".code-snippet").slice(0, count).hide();
    // And show the needed one for passed in language
    tabsContainier.nextAll("#" + language).first().show();

    // Deselect all
    tabs.removeClass("selected");
    // Except the target
    $(evt.target).addClass("selected");
}

// Wrap all the code from markwodn with <pre><code></code></pre>
$(".code-snippet").wrapInner(function () {
    return "<pre><code class=language-" + this.id + "></code></pre>";
});
// Select the first tab for code snippets
$(".code-snippet-tabs").each(function () {
    $(this).find(".code-snippet-tab").first().click();
});

$(document).ready(function () {
    const pagePath = window.location.href;
    const lastSlashPosition = pagePath.lastIndexOf('/');
    const pageUrl = pagePath.substring(lastSlashPosition + 1);

    const els = document.querySelectorAll(`a[href*="${pageUrl}"]`);
    const el = els[0];
    el.style.fontWeight = "700";

    if (el) el.parentElement.parentElement.parentElement.setAttribute("open", true);
});

anchors.add('.heading');