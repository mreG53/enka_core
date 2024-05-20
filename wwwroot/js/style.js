function toggleSidebar() {
    document.querySelector('.sidebar').classList.toggle('active');
}

document.addEventListener('click', function (event) {
    var sidebar = document.querySelector('.sidebar');
    var sidebarToggle = document.querySelector('.sidebar-toggle');
    var isClickInside = sidebar.contains(event.target) || sidebarToggle.contains(event.target);

    if (!isClickInside && sidebar.classList.contains('active')) {
        sidebar.classList.remove('active');
    }
});
