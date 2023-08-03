window.mylabAdmin = {
    modal: {
        initialize: (elementId, backdrop, dotNetHelper) => {
            let modalEl = document.getElementById(elementId);

            modalEl.addEventListener('show.bs.modal', function () {
                dotNetHelper.invokeMethodAsync('OnShowModal');
            });
            modalEl.addEventListener('shown.bs.modal', function () {
                dotNetHelper.invokeMethodAsync('OnShownModal');
            });
            modalEl.addEventListener('hide.bs.modal', function () {
                dotNetHelper.invokeMethodAsync('OnHideModal');
            });
            modalEl.addEventListener('hidden.bs.modal', function () {
                dotNetHelper.invokeMethodAsync('OnHiddenModal');
            });
            modalEl.addEventListener('hidePrevented.bs.modal', function () {
                dotNetHelper.invokeMethodAsync('OnHidePreventedModal');
            });

            let options = { backdrop: backdrop, keyboard: true };
            bootstrap?.Modal?.getOrCreateInstance(modalEl, options);
        },
        show: (elementId) => {
            bootstrap?.Modal?.getOrCreateInstance(document.getElementById(elementId))?.show();
        },
        hide: (elementId) => {
            bootstrap?.Modal?.getOrCreateInstance(document.getElementById(elementId))?.hide();
        },
        dispose: (elementId) => {
            bootstrap?.Modal?.getOrCreateInstance(document.getElementById(elementId))?.dispose();
        }
    }
}