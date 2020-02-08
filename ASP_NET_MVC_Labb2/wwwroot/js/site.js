// Funktion för att lägga till aktiv CSS-klass på det aktiva menyalternativet
window.addEventListener('load', function () {
    
    document.querySelectorAll('.nav-item').forEach(function (element) {
        if (element.firstElementChild.href === window.location.href) {
            element.classList.add('active');
        }
    });

    if (shouldLoadModal) {
        $('#saveUsernameModal').modal();
    }
});