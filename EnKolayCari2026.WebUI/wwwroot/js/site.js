// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

(function () {
    var toggle = document.getElementById('sidebar-toggle');
    var drawer = document.getElementById('mobile-sidebar');
    var backdrop = document.getElementById('mobile-sidebar-backdrop');

    if (!toggle || !drawer || !backdrop) {
        return;
    }

    function openDrawer() {
        drawer.classList.remove('hidden');
        drawer.setAttribute('aria-hidden', 'false');
        toggle.setAttribute('aria-expanded', 'true');
    }

    function closeDrawer() {
        drawer.classList.add('hidden');
        drawer.setAttribute('aria-hidden', 'true');
        toggle.setAttribute('aria-expanded', 'false');
    }

    toggle.addEventListener('click', function () {
        if (drawer.classList.contains('hidden')) {
            openDrawer();
        } else {
            closeDrawer();
        }
    });

    backdrop.addEventListener('click', closeDrawer);

    document.addEventListener('keydown', function (event) {
        if (event.key === 'Escape') {
            closeDrawer();
        }
    });
})();
